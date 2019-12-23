using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerChat.Application.Users.Commands.CreateUser;
using PowerChat.Application.Users.Commands.Login;
using PowerChat.Application.Users.Commands.UpdateAccountProfile;
using PowerChat.Application.Users.Queries.GetAccountProfile;

namespace PowerChat.API.Controllers
{
    public class AccountController : ControllerBase
    {
        public AccountController(IMediator mediator) : base(mediator)
        { }

        [HttpGet]
        [Authorize]
        [Route("")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAccountProfileQuery(), cancellationToken);

            return Json(result);
        }

        [HttpPut]
        [Authorize]
        [Route("")]
        public async Task<IActionResult> Update([FromBody] UpdateAccountProfileCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            result.ThrowIfFailed();

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginCommand command, CancellationToken cancellationToken)
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
