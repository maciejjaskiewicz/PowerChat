﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PowerChat.API.Hubs;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Common.Models;
using PowerChat.Common.Exceptions;

namespace PowerChat.API.Services
{
    public interface IInternalConnectedUsersService : IConnectedUsersService
    {
        void AddUser(string userId, string connectionId);
        void RemoveUser(string userIdString);
    }

    public class ConnectedUsersService : IInternalConnectedUsersService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        private readonly ISet<ConnectedUser> _connectedUsers;
        public IEnumerable<ConnectedUser> ConnectedUsers => _connectedUsers;

        public ConnectedUsersService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;

            _connectedUsers = new HashSet<ConnectedUser>();
        }

        public async Task SendAsync(long userId, string method, object payload)
        {
            if (_connectedUsers.Any(x => x.UserId == userId))
            {
                await _hubContext.Clients.User(userId.ToString())
                    .SendAsync(method, payload);
            }
        }

        public void AddUser(string userIdString, string connectionId)
        {
            if (string.IsNullOrEmpty(userIdString) == false)
            {
                if (long.TryParse(userIdString, out var userId))
                {
                    _connectedUsers.Add(new ConnectedUser(userId, connectionId));

                    return;
                }
            }

            throw new PowerChatException("UserIdParseFailed",
                $"Failed to parse userId: {userIdString}");
        }

        public void RemoveUser(string userIdString)
        {
            ConnectedUser userToRemove = null;

            if (string.IsNullOrEmpty(userIdString) == false)
            {
                if (long.TryParse(userIdString, out var userId))
                {
                    userToRemove = _connectedUsers.SingleOrDefault(x => x.UserId == userId);
                }
            }

            if (userToRemove != null)
            {
                _connectedUsers.Remove(userToRemove);
            }
        }
    }
}
