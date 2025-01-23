using AvaTrade.News.Domain.Entities;
using AvaTrade.News.Domain.Events;
using AvaTrade.News.Domain.Repositories;
using AvaTrade.News.Infrastructure.ExternalServices;

namespace AvaTrade.News.Processor.Services;
using System.Text.Json;

public class NewsProcessorService : BackgroundService
{
    private readonly IPolygonNewsClient _newsClient;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<NewsProcessorService> _logger;

    public NewsProcessorService(
        IPolygonNewsClient newsClient,
        IUnitOfWork unitOfWork,
        ILogger<NewsProcessorService> logger)
    {
        _newsClient = newsClient;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Fetching latest news from Polygon.io");
                var news = await _newsClient.FetchLatestNewsAsync();

                foreach (var newsItem in news)
                {
                    using var transaction = await _unitOfWork.BeginTransactionAsync();
                    try
                    {
                        await _unitOfWork.NewsRepository.AddAsync(newsItem);
                        
                        var newsEvent = new NewsCreatedEvent
                        {
                            Id = newsItem.Id,
                            Title = newsItem.Title,
                            Content = newsItem.Content,
                            InstrumentName = newsItem.InstrumentName,
                            PublishedAt = newsItem.PublishedAt,
                            Source = newsItem.Source,
                            AdditionalData = newsItem.AdditionalData
                        };

                        var outboxMessage = new OutboxMessage(
                            "NewsCreated",
                            JsonSerializer.Serialize(newsEvent)
                        );

                        await _unitOfWork.OutboxRepository.AddAsync(outboxMessage);
                        
                        await _unitOfWork.CommitAsync();
                        await transaction.CommitAsync();
                        _logger.LogInformation("Added news item to outbox: {Title}", newsItem.Title);
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        _logger.LogError(ex, "Error processing news item: {Title}", newsItem.Title);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing news");
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
} 