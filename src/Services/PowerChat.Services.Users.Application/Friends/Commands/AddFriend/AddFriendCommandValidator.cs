using FluentValidation;
using PowerChat.Services.Common.Application.Services;
using PowerChat.Services.Users.Application.Users.Errors;
using PowerChat.Services.Users.Application.Users.Services;

namespace PowerChat.Services.Users.Application.Friends.Commands.AddFriend
{
    public class AddFriendCommandValidator : AbstractValidator<AddFriendCommand>
    {
        private readonly UserService _userService;
        private readonly ICurrentUserService _currentUserService;

        public AddFriendCommandValidator(UserService userService, 
            ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;

            Validate();
        }

        private void Validate()
        {
            RuleFor(x => x.UserId)
                .MustAsync(async (id, cancellationToken) => await _userService.UserExistsAsync(id, cancellationToken))
                .WithErrorCode(UserErrorCodes.UserNotFound)
                .WithMessage(x => $"User with id '{x.UserId}' was not found.")
                .Must(id => !_currentUserService.UserId.HasValue || _currentUserService.UserId.Value != id)
                .WithErrorCode("InvalidFriendship")
                .WithMessage("You cannot create a friendship with yourself.");
        }
    }
}
