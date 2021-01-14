using PowerChat.Services.Common.Application.Events;

namespace PowerChat.Services.Chat.Application.Channels.Events
{
    public class ChannelCreatedEvent : IEvent
    {
        public long ChannelId { get; set; }
    }
}
