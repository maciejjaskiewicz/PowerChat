namespace PowerChat.Services.Users.Application.Users.Models
{
    public class ConnectedUser
    {
        public string UserIdentityId { get; }
        public string ConnectionId { get; }

        public ConnectedUser(string userIdentityId, string connectionId)
        {
            UserIdentityId = userIdentityId;
            ConnectionId = connectionId;
        }
    }
}
