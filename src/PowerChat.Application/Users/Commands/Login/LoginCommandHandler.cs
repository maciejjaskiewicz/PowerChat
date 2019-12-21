using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Common.Results;
using PowerChat.Application.Users.Errors;
using PowerChat.Application.Users.Events;
using PowerChat.Application.Users.Models;
using PowerChat.Common.Results;

namespace PowerChat.Application.Users.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ApplicationResult<LoginResponseModel>>
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public LoginCommandHandler(IAuthService authService, 
            IUserService userService, 
            IMediator mediator)
        {
            _authService = authService;
            _userService = userService;
            _mediator = mediator;
        }

        public async Task<ApplicationResult<LoginResponseModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserAsync(request.Email);

            if (user == null)
            {
                return ApplicationResult<LoginResponseModel>.Fail(PowerChatError.Create(UserErrorCodes.UserNotFound,
                    $"User with email '{request.Email}' was not found."));
            }

            var result = await _authService.LoginAsync(request.Email, request.Password);

            if (result.Succeeded)
            {
                var userLoggedInEvent = new UserLoggedInEvent {UserId = user.Id};
                await _mediator.Publish(userLoggedInEvent, cancellationToken);

                var loginResponse = new LoginResponseModel
                {
                    UserId = user.Id,
                    Token = result.Value.Token,
                    Expires = result.Value.Expires
                };

                return ApplicationResult<LoginResponseModel>.Ok(loginResponse);
            }

            return ApplicationResult<LoginResponseModel>.Fail(result.Error);
        }
    }
}
