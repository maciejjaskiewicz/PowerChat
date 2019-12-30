using MediatR;
using PowerChat.Application.Common.Results;

namespace PowerChat.Application.Channels.Commands.CreateUserChannel
{
    public class CreateUserChannelCommand : IRequest<ApplicationResult<long>>
    {
        public long UserId { get; set; }
    }
}
