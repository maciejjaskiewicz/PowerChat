using FluentValidation;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Users.Errors;

namespace PowerChat.Application.Channels.Commands.CreateUserChannel
{
    public class CreateUserChannelCommandValidator : AbstractValidator<CreateUserChannelCommand>
    {
        private readonly IUserService _userService;
        public CreateUserChannelCommandValidator(IUserService userService)
        {
            _userService = userService;
        }

        private void Validate()
        {
            RuleFor(x => x.UserId)
                .MustAsync(async (id, cancellationToken) => await _userService.UserExistsAsync(id, cancellationToken))
                .WithErrorCode(UserErrorCodes.UserNotFound)
                .WithMessage(x => $"User with id '{x.UserId}' was not found.");
        }
    }
}
