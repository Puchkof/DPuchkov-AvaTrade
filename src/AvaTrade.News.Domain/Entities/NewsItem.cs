namespace AvaTrade.News.Domain.Entities;

public class NewsItem
{
    public Guid Id { get; set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public string InstrumentName { get; private set; }
    public DateTime PublishedAt { get; private set; }
    public string Source { get; private set; }
    public Dictionary<string, object> AdditionalData { get; private set; }

    public NewsItem(string title, string content, string instrumentName, DateTime publishedAt, string source)
    {
        Id = Guid.NewGuid();
        Title = title;
        Content = content;
        InstrumentName = instrumentName;
        PublishedAt = publishedAt;
        Source = source;
        AdditionalData = new Dictionary<string, object>();
    }

    public void AddAdditionalData(string key, object value)
    {
        AdditionalData[key] = value;
    }
} 