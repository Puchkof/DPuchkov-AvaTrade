using AvaTrade.News.Domain.Entities;

namespace AvaTrade.News.Domain.Repositories;

public interface INewsRepository
{
    Task<NewsItem> GetByIdAsync(Guid id);
    Task<IEnumerable<NewsItem>> GetLatestAsync(int limit);
    Task<IEnumerable<NewsItem>> GetByInstrumentAsync(string instrumentName, int limit);
    Task AddAsync(NewsItem newsItem);
    Task<IEnumerable<NewsItem>> GetLatestForTopInstrumentsAsync(int instrumentCount, int newsPerInstrument);
} 