using Microsoft.EntityFrameworkCore.Storage;

namespace AvaTrade.News.Domain.Repositories;

public interface IUnitOfWork
{
    INewsRepository NewsRepository { get; }
    IOutboxRepository OutboxRepository { get; }
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitAsync();
} 