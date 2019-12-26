using MediatR;
using PowerChat.Application.Common.Results;

namespace PowerChat.Application.Friends.Commands.AddFriend
{
    public class AddFriendCommand : IRequest<ApplicationResult>
    {
        public long UserId { get; set; }
    }
}
