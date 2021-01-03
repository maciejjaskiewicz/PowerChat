using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PowerChat.Services.Identity.Application.Commands.CreateAccount;

namespace PowerChat.Services.Identity.API.Controllers
{
    public class AccountController : ControllerBase
    {
        public AccountController(IMediator mediator) 
            : base(mediator)
        { }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateAccountCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            result.ThrowIfFailed();

            return Ok();
        }
    }
}