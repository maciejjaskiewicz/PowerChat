using MediatR;

namespace PowerChat.Application.Channels.Events
{
    public class ChannelCreatedEvent : INotification
    {
        public long UserId { get; set; }
    }
}
