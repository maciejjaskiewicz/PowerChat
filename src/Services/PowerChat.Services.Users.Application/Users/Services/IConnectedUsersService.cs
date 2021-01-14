using System.Threading;
using System.Threading.Tasks;

namespace PowerChat.Services.Users.Application.Users.Services
{
    public interface IConnectedUsersService
    {
        bool IsConnected(string userIdentityId);
        Task<bool> IsConnectedAsync(string userIdentityId, CancellationToken cancellationToken);
        Task SendAsync(string userIdentityId, string method, object payload);
    }
}
