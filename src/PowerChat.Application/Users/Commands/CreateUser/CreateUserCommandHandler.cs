using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Common.Results;
using PowerChat.Application.Users.Events;
using PowerChat.Domain.Entities;
using PowerChat.Domain.ValueObjects;

namespace PowerChat.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApplicationResult>
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public CreateUserCommandHandler(IUserService userService, 
            IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        public async Task<ApplicationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = request.Email,
                UserName = request.Email,
                Name = PersonName.Create(request.FirstName, request.LastName)
            };

            var result = await _userService.CreateUserAsync(user, request.Password);

            if (result.Succeeded)
            {
                var userCreatedEvent = new UserCreatedEvent {UserId = result.Value};
                await _mediator.Publish(userCreatedEvent, cancellationToken);
            }

            return result;
        }
    }
}
