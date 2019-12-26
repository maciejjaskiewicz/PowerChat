using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Get(long id)
        {
            var getUserQuery = new GetUserQuery(id);
            var result = await Mediator.Send(getUserQuery);

            return Json(result);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery]SearchUsersQuery searchQuery, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(searchQuery, cancellationToken);

            return Json(result);
        }
    }
}
