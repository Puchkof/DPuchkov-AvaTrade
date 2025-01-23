using AvaTrade.News.Application.Events;
using MediatR;

namespace AvaTrade.News.API.EventHandlers;

public class NewsCreatedEventHandler : INotificationHandler<NewsCreatedNotification>
{
    private readonly ILogger<NewsCreatedEventHandler> _logger;
    // Add your read database repository here
    
    public NewsCreatedEventHandler(ILogger<NewsCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(NewsCreatedNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling news created event: {Id}", notification.Event.Id);
            
            // Here you would update your read database
            // await _readRepository.AddAsync(new ReadNewsItem(notification.Event));
            
            _logger.LogInformation("Successfully processed news event: {Id}", notification.Event.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling news created event: {Id}", notification.Event.Id);
            throw;
        }
    }
} 