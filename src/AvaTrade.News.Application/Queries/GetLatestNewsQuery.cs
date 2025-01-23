using AvaTrade.News.Application.DTOs;
using AvaTrade.News.Domain.Entities;
using AvaTrade.News.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AvaTrade.News.Application.Queries;

public record GetLatestNewsQuery(int Limit) : IRequest<IEnumerable<NewsItemDto>>;

public class GetLatestNewsQueryHandler : IRequestHandler<GetLatestNewsQuery, IEnumerable<NewsItemDto>>
{
    private readonly INewsRepository _newsRepository;
    private readonly ILogger<GetLatestNewsQueryHandler> _logger;

    public GetLatestNewsQueryHandler(INewsRepository newsRepository, ILogger<GetLatestNewsQueryHandler> logger)
    {
        _newsRepository = newsRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<
        NewsItemDto>> Handle(GetLatestNewsQuery request, CancellationToken cancellationToken)
    {
        var news = await _newsRepository.GetLatestAsync(request.Limit);
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