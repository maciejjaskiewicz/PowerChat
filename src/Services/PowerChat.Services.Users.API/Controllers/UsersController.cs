using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerChat.Services.Users.Application.Users.Queries.GetUser;
using PowerChat.Services.Users.Application.Users.Queries.SearchUsers;

namespace PowerChat.Services.Users.API.Controllers
{
    [Authorize]
    public class UsersController : ControllerBase
    {
        public UsersController(IMediator mediator) 
            : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        {
            var getUserQuery = new GetUserQuery(id);
            var result = await Mediator.Send(getUserQuery, cancellationToken);

            return Json(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchUsersQuery searchQuery, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(searchQuery, cancellationToken);

            return Json(result);
        }
    }
}
