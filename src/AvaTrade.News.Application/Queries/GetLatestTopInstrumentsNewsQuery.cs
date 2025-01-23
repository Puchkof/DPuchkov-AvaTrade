using AvaTrade.News.Application.DTOs;
using AvaTrade.News.Domain.Entities;
using AvaTrade.News.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AvaTrade.News.Application.Queries;

public record GetLatestTopInstrumentsNewsQuery(int Limit = 5) : IRequest<IEnumerable<NewsItemDto>>;

public class GetLatestTopInstrumentsNewsQueryHandler 
    : IRequestHandler<GetLatestTopInstrumentsNewsQuery, IEnumerable<NewsItemDto>>
{
    private readonly INewsRepository _newsRepository;
    private readonly ILogger<GetLatestTopInstrumentsNewsQueryHandler> _logger;

    public GetLatestTopInstrumentsNewsQueryHandler(
        INewsRepository newsRepository, 
        ILogger<GetLatestTopInstrumentsNewsQueryHandler> logger)
    {
        _newsRepository = newsRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<NewsItemDto>> Handle(
        GetLatestTopInstrumentsNewsQuery request, 
        CancellationToken cancellationToken)
    {
        var news = await _newsRepository.GetLatestForTopInstrumentsAsync(5, request.Limit);
        return news.Select(MapToDto);
    }

    private static NewsItemDto MapToDto(NewsItem news) =>
        new()
        {
            Id = news.Id,
            Title = news.Title,
            Content = news.Content,
            InstrumentName = news.InstrumentName,
            PublishedAt = news.PublishedAt,
            Source = news.Source,
            AdditionalData = news.AdditionalData
        };
} 