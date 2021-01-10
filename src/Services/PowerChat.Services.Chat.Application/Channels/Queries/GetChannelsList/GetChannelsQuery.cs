using System.Collections.Generic;
using MediatR;
using PowerChat.Services.Chat.Application.Channels.Queries.GetChannelsList.Models;

namespace PowerChat.Services.Chat.Application.Channels.Queries.GetChannelsList
{
    public class GetChannelsQuery : IRequest<IList<ChannelPreviewModel>>
    {
    }
}
