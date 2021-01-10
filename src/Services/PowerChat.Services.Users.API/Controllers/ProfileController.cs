using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerChat.Services.Users.Application.Users.Commands.UpdateProfile;
using PowerChat.Services.Users.Application.Users.Queries.GetProfile;

namespace PowerChat.Services.Users.API.Controllers
{
    [Authorize]
    public class ProfileController : ControllerBase
    {
        public ProfileController(IMediator mediator) 
            : base(mediator)
        {
        }

        [HttpGet("")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetProfileQuery(), cancellationToken);

            return Json(result);
        }

        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] UpdateProfileCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            result.ThrowIfFailed();

            return Ok();
        }
    }
}
