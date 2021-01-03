using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PowerChat.Services.Identity.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ControllerBase : Controller
    {
        protected readonly IMediator Mediator;

        protected ControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
