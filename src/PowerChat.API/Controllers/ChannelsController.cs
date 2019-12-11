using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerChat.Application.Channels.Commands.CreateChannel;
using PowerChat.Application.Channels.Queries.GetChannelsList;

namespace PowerChat.API.Controllers
{
    [Authorize]
    public class ChannelsController : ControllerBase
    {
        public ChannelsController(IMediator mediator) 
            : base(mediator)
        { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await Mediator.Send(new GetChannelsListQuery());

            return Ok(Json(model));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CancellationToken cancellationToken)
        {
            var createChannelCommand = new CreateChannelCommand
            {
                Name = Guid.NewGuid().ToString()
            };

            var id = await Mediator.Send(createChannelCommand, cancellationToken);

            return Ok(Json(id));
        }
    }
}
