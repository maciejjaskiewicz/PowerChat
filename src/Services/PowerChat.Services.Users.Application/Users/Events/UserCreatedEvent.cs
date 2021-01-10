using PowerChat.Services.Common.Application.Events;

namespace PowerChat.Services.Users.Application.Users.Events
{
    public class UserCreatedEvent : IEvent
    {
        public long UserId { get; set; }
    }
}
