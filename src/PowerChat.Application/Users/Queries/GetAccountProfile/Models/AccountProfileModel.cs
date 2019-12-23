using System;
using PowerChat.Domain.Enums;

namespace PowerChat.Application.Users.Queries.GetAccountProfile.Models
{
    public class AccountProfileModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string About { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
