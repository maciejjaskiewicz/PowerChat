using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerChat.Application.Channels.Commands.SendChannelMessage;
using PowerChat.Application.Channels.Queries.GetChannel;
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
        [Route("")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetChannelsQuery(), cancellationToken);

            return Json(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetChannelQuery {Id = id}, cancellationToken);

            return Json(result);
        }

        [HttpPost]
        [Route("{id}/messages")]
        public async Task<IActionResult> Send(long id, [FromBody]SendMessageRequestBody body, CancellationToken cancellationToken)
        {
            var sendMessageCommand = new SendChannelMessageCommand
            {
                ChannelId = id,
                Content = body.Content
            };
            var result = await Mediator.Send(sendMessageCommand, cancellationToken);
            var messageId = result.GetValueOrThrow();

            return Json(messageId);
        }

        public class SendMessageRequestBody
        {
            public string Content { get; set; }
        }
    }
}
