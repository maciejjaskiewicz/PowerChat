namespace PowerChat.Services.Users.Application.Users.Models
{
    public class ConnectedUser
    {
        public long UserId { get; }
        public string ConnectionId { get; }

        public ConnectedUser(long userId, string connectionId)
        {
            UserId = userId;
            ConnectionId = connectionId;
        }
    }
}
