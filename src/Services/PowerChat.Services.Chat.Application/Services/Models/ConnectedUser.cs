namespace PowerChat.Services.Chat.Application.Services.Models
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
