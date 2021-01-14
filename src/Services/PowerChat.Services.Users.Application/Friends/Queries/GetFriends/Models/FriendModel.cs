namespace PowerChat.Services.Users.Application.Friends.Queries.GetFriends.Models
{
    public class FriendModel
    {
        public long Id { get; set; }
        public string IdentityId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public bool Approved { get; set; }
        public bool IsOnline { get; set; }
    }
}
