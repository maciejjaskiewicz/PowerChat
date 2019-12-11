using MediatR;

namespace PowerChat.Application.Users.Events
{
    public class UserCreatedEvent : INotification
    {
        public long UserId { get; set; }
    }
}
