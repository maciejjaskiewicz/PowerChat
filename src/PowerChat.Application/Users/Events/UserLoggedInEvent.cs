using MediatR;

namespace PowerChat.Application.Users.Events
{
    public class UserLoggedInEvent : INotification
    {
        public long UserId { get; set; }
    }
}
