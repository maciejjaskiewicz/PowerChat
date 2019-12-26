using FluentValidation;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Users.Errors;

namespace PowerChat.Application.Friends.Commands.AddFriend
{
    public class AddFriendCommandValidator : AbstractValidator<AddFriendCommand>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;

        public AddFriendCommandValidator(IUserService userService, 
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
