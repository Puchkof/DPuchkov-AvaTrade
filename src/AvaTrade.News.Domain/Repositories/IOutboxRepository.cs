using AvaTrade.News.Domain.Entities;

namespace AvaTrade.News.Domain.Repositories;

public interface IOutboxRepository
{
    Task AddAsync(OutboxMessage message);
    Task<IEnumerable<OutboxMessage>> GetUnprocessedMessagesAsync(int batchSize = 100);
    Task MarkAsProcessedAsync(Guid messageId);
    Task MarkAsFailedAsync(Guid messageId, string error);
} 