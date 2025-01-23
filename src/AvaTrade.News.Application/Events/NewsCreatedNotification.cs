using AvaTrade.News.Domain.Events;
using MediatR;

namespace AvaTrade.News.Application.Events;

public class NewsCreatedNotification : INotification
{
    public NewsCreatedEvent Event { get; }

    public NewsCreatedNotification(NewsCreatedEvent @event)
    {
        Event = @event;
    }
} 