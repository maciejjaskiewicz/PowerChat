using MediatR;
using PowerChat.Services.Chat.Application.Channels.Queries.GetChannelsList.Models;

namespace PowerChat.Services.Chat.Application.Channels.Queries.GetUserChannel
{
    public class GetUserChannelQuery : IRequest<ChannelPreviewModel>
    {
        //TODO: should based on IdentityId
        public long UserId { get; set; }
    }
}
