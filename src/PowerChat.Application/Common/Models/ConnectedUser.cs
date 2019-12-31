namespace PowerChat.Application.Common.Models
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

        public override int GetHashCode()
        {
            return 16769023 * UserId.GetHashCode();
        }
    }
}
