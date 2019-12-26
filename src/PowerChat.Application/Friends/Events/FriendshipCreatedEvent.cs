using MediatR;

namespace PowerChat.Application.Friends.Events
{
    public class FriendshipCreatedEvent : INotification
    {
        public long FriendshipId { get; set; }
        public long RequestedById { get; set; }
        public long RequestedToId { get; set; }
    }
}
