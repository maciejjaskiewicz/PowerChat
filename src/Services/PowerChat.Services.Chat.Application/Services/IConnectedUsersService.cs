using System.Threading;
using System.Threading.Tasks;

namespace PowerChat.Services.Chat.Application.Services
{
    public interface IConnectedUsersService
    {
        bool IsConnected(string userIdentityId);
        Task<bool> IsConnectedAsync(string userIdentityId, CancellationToken cancellationToken);
    }
}
