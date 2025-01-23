namespace AvaTrade.News.Domain.Events;

public record NewsCreatedEvent
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Content { get; init; }
    public string InstrumentName { get; init; }
    public DateTime PublishedAt { get; init; }
    public string Source { get; init; }
    public Dictionary<string, object> AdditionalData { get; init; }
} 