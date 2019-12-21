using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PowerChat.Application.Users.Commands.CreateUser;
using PowerChat.Application.Users.Commands.Login;

namespace PowerChat.API.Controllers
{
    public class AccountController : ControllerBase
    {
        public AccountController(IMediator mediator) : base(mediator)
        { }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Post([FromBody]LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            var response = result.GetValueOrThrow();

            return Json(response);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody]CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            result.ThrowIfFailed();

            return Ok();
        }
    }
}
