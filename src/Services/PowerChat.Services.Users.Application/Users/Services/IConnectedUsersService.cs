using System.Collections.Generic;
using System.Threading.Tasks;
using PowerChat.Services.Users.Application.Users.Models;

namespace PowerChat.Services.Users.Application.Users.Services
{
    public interface IConnectedUsersService
    {
        IEnumerable<ConnectedUser> ConnectedUsers { get; }
        Task SendAsync(long userId, string method, object payload);
    }
}
