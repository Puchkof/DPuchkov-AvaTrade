using AvaTrade.News.Application.DTOs;
using AvaTrade.News.Domain.Entities;
using AvaTrade.News.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AvaTrade.News.Application.Queries;

public record GetNewsByInstrumentQuery(string InstrumentName, int Limit) : IRequest<IEnumerable<NewsItemDto>>;

public class GetNewsByInstrumentQueryHandler : IRequestHandler<GetNewsByInstrumentQuery, IEnumerable<NewsItemDto>>
{
    private readonly INewsRepository _newsRepository;
    private readonly ILogger<GetNewsByInstrumentQueryHandler> _logger;

    public GetNewsByInstrumentQueryHandler(INewsRepository newsRepository, ILogger<GetNewsByInstrumentQueryHandler> logger)
    {
        _newsRepository = newsRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<NewsItemDto>> Handle(GetNewsByInstrumentQuery request, CancellationToken cancellationToken)
    {
        var news = await _newsRepository.GetByInstrumentAsync(request.InstrumentName, request.Limit);
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