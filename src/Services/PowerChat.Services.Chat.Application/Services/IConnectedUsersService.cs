using System.Collections.Generic;
using PowerChat.Services.Chat.Application.Services.Models;

namespace PowerChat.Services.Chat.Application.Services
{
    public interface IConnectedUsersService
    {
        IEnumerable<ConnectedUser> ConnectedUsers { get; }
    }
}
