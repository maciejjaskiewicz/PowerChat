using MediatR;
using PowerChat.Services.Common.Application.Results;

namespace PowerChat.Services.Users.Application.Friends.Commands.AddFriend
{
    public class AddFriendCommand : IRequest<ApplicationResult>
    {
        public long UserId { get; set; }
    }
}
