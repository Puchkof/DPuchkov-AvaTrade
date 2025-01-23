namespace AvaTrade.News.Application.DTOs;

public class NewsItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string InstrumentName { get; set; }
    public DateTime PublishedAt { get; set; }
    public string Source { get; set; }
    public Dictionary<string, object> AdditionalData { get; set; }
} 