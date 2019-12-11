using MediatR;

namespace PowerChat.Application.Channels.Events
{
    public class ChannelCreatedEvent : INotification
    {
        public long ChannelId { get; set; }
    }
}
