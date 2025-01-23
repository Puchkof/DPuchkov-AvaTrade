using AvaTrade.News.Domain.Entities;

namespace AvaTrade.News.Infrastructure.ExternalServices;

public class MockPolygonNewsClient : IPolygonNewsClient
{
    private readonly Random _random = new();
    private readonly string[] _instruments = { "AAPL", "MSFT", "GOOGL", "AMZN", "META" };

    public async Task<IEnumerable<NewsItem>> FetchLatestNewsAsync()
    {
        // Simulate API delay
        await Task.Delay(100);

        var news = new List<NewsItem>();
        for (int i = 0; i < 10; i++)
        {
            var instrument = _instruments[_random.Next(_instruments.Length)];
            news.Add(new NewsItem(
                $"News Title for {instrument} #{i}",
                $"This is a mock news content for {instrument}",
                instrument,
                DateTime.UtcNow.AddMinutes(-_random.Next(60)),
                "Polygon.io"
            ));
        }

        return news;
    }
} 