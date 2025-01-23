using AvaTrade.News.Domain.Entities;
using AvaTrade.News.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AvaTrade.News.Infrastructure.Persistence;

public class NewsDbContext : DbContext
{
    public DbSet<NewsItem> News { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NewsConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
    }
} 