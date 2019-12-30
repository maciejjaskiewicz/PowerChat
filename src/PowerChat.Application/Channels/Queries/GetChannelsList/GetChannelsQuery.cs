using System.Collections.Generic;
using MediatR;
using PowerChat.Application.Channels.Queries.GetChannelsList.Models;

namespace PowerChat.Application.Channels.Queries.GetChannelsList
{
    public class GetChannelsQuery : IRequest<IList<ChannelPreviewModel>>
    {
    }
}
