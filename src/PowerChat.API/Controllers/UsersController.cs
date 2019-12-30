using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerChat.Application.Channels.Commands.CreateUserChannel;
using PowerChat.Application.Channels.Queries.GetUserChannel;
using PowerChat.Application.Users.Queries.GetUser;
using PowerChat.Application.Users.Queries.SearchUsers;

namespace PowerChat.API.Controllers
{
    [Authorize]
    public class UsersController : ControllerBase
    {
        public UsersController(IMediator mediator) : base(mediator)
        { }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        {
            var getUserQuery = new GetUserQuery(id);
            var result = await Mediator.Send(getUserQuery, cancellationToken);

            return Json(result);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery]SearchUsersQuery searchQuery, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(searchQuery, cancellationToken);

            return Json(result);
        }

        [HttpGet]
        [Route("{id}/channel")]
        public async Task<IActionResult> GetChannel(long id, CancellationToken cancellationToken)
        {
            var query = new GetUserChannelQuery { UserId = id };
            var result = await Mediator.Send(query, cancellationToken);

            return Json(result);
        }
    }
}
