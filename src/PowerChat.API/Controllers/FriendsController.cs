using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerChat.Application.Friends.Commands.AddFriend;
using PowerChat.Application.Friends.Queries.GetFriends;

namespace PowerChat.API.Controllers
{
    [Authorize]
    public class FriendsController : ControllerBase
    {
        public FriendsController(IMediator mediator) : base(mediator)
        { }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetFriendsQuery(), cancellationToken);

            return Json(result);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromBody] AddFriendCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            result.ThrowIfFailed();

            return Ok();
        }
    }
}
