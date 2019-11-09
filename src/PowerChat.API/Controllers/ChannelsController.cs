using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PowerChat.Application.Channels.Commands.CreateChannel;
using PowerChat.Application.Channels.Queries.GetChannelsList;

namespace PowerChat.API.Controllers
{
    public class ChannelsController : Controller
    {
        private readonly IMediator _mediator;

        public ChannelsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = await _mediator.Send(new GetChannelsListQuery());

            return Ok(Json(model));
        }

        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var createChannelCommand = new CreateChannelCommand
            {
                Name = Guid.NewGuid().ToString()
            };

            var id = await _mediator.Send(createChannelCommand, cancellationToken);

            return Ok(Json(id));
        }
    }
}
