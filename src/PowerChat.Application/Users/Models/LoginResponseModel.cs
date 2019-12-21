using PowerChat.Application.Users.Dto;

namespace PowerChat.Application.Users.Models
{
    public class LoginResponseModel
    {
        public long UserId { get; set; }
        public string Token { get; set; }
        public long Expires { get; set; }
    }
}
