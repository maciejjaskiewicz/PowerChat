using MediatR;

namespace PowerChat.Application.Channels.Commands
{
    public class CreateChannelCommand : IRequest<long>
    {
        public string Name { get; set; }
    }
}
