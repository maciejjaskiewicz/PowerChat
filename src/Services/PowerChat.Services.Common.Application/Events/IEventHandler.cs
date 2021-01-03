using MediatR;

namespace PowerChat.Services.Common.Application.Events
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IEvent
    { }

    public abstract class EventHandler<TEvent> : NotificationHandler<TEvent>, IEventHandler<TEvent>
        where TEvent : IEvent
    { }
}