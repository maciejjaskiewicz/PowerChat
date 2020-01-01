using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Common.Results;
using PowerChat.Application.Users.Events;
using PowerChat.Common.Interfaces;
using PowerChat.Domain.Entities;
using PowerChat.Domain.Enums;
using PowerChat.Domain.ValueObjects;

namespace PowerChat.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApplicationResult>
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        private readonly IDateTime _dateTime;

        public CreateUserCommandHandler(IUserService userService, 
            IMediator mediator, 
            IDateTime dateTime)
        {
            _userService = userService;
            _mediator = mediator;
            _dateTime = dateTime;
        }

        public async Task<ApplicationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = request.Email,
                UserName = request.Email,
                Name = PersonName.Create(request.FirstName, request.LastName),
                Gender = !string.IsNullOrEmpty(request.Gender) ? (Gender?)Enum.Parse<Gender>(request.Gender) : null,
                LastActive = _dateTime.UtcNow
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
