using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.JsonWebTokens;

namespace PowerChat.Services.Users.API.Services
{
    public class IdentityBasedUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);
        }
    }
}
