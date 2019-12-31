using System.Collections.Generic;
using System.Threading.Tasks;
using PowerChat.Application.Common.Models;
using PowerChat.Common.Interfaces;

namespace PowerChat.Application.Common.Interfaces
{
    public interface IConnectedUsersService : IApiService
    {
        IEnumerable<ConnectedUser> ConnectedUsers { get; }
        Task SendAsync(long userId, string method, object payload);
    }
}
