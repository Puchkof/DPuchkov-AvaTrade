using AvaTrade.News.Domain.Entities;

namespace AvaTrade.News.Infrastructure.ExternalServices;

public interface IPolygonNewsClient
{
    Task<IEnumerable<NewsItem>> FetchLatestNewsAsync();
} 