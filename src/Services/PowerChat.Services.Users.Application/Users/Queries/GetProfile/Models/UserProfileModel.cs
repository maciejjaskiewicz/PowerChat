using System;

namespace PowerChat.Services.Users.Application.Users.Queries.GetProfile.Models
{
    public class UserProfileModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string About { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
