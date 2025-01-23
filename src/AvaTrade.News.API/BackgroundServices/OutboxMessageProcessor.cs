using System.Text.Json;
using AvaTrade.News.Application.Events;
using AvaTrade.News.Domain.Events;
using AvaTrade.News.Domain.Repositories;
using MediatR;

namespace AvaTrade.News.API.BackgroundServices;

public class OutboxMessageProcessor : BackgroundService
{
    private readonly IOutboxRepository _outboxRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<OutboxMessageProcessor> _logger;

    public OutboxMessageProcessor(
        IOutboxRepository outboxRepository,
        IMediator mediator,
        ILogger<OutboxMessageProcessor> logger)
    {
        _outboxRepository = outboxRepository;
        _mediator = mediator;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var messages = await _outboxRepository.GetUnprocessedMessagesAsync();

                foreach (var message in messages)
                {
                    try
                    {
                        if (message.Type == "NewsCreated")
                        {
                            var newsEvent = JsonSerializer.Deserialize<NewsCreatedEvent>(message.Content);
                            await _mediator.Publish(new NewsCreatedNotification(newsEvent), stoppingToken);
                            await _outboxRepository.MarkAsProcessedAsync(message.Id);
                            _logger.LogInformation("Processed news event: {Id}", newsEvent.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        await _outboxRepository.MarkAsFailedAsync(message.Id, ex.Message);
                        _logger.LogError(ex, "Error processing outbox message {Id}", message.Id);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in outbox processor");
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
} 