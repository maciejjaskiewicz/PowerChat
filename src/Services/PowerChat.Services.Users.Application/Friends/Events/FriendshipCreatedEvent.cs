using PowerChat.Services.Common.Application.Events;

namespace PowerChat.Services.Users.Application.Friends.Events
{
    public class FriendshipCreatedEvent : IEvent
    {
        public long FriendshipId { get; set; }
        public long RequestedById { get; set; }
        public long RequestedToId { get; set; }
    }
}
