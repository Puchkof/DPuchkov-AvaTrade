using AvaTrade.News.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace AvaTrade.News.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly NewsDbContext _context;
    
    public INewsRepository NewsRepository { get; }
    public IOutboxRepository OutboxRepository { get; }

    public UnitOfWork(
        NewsDbContext context,
        INewsRepository newsRepository,
        IOutboxRepository outboxRepository)
    {
        _context = context;
        NewsRepository = newsRepository;
        OutboxRepository = outboxRepository;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
} 