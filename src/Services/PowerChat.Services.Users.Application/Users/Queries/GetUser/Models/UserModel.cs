using System;

namespace PowerChat.Services.Users.Application.Users.Queries.GetUser.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string About { get; set; }
        public bool IsFriend { get; set; }
        public int Friends { get; set; }
        public DateTime? LastActive { get; set; }
        public bool IsOnline { get; set; }
    }
}
