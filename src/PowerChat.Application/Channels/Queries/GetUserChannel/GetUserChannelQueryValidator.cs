using FluentValidation;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Users.Errors;

namespace PowerChat.Application.Channels.Queries.GetUserChannel
{
    public class GetUserChannelQueryValidator : AbstractValidator<GetUserChannelQuery>
    {
        private readonly IUserService _userService;
        public GetUserChannelQueryValidator(IUserService userService)
        {
            _userService = userService;

            Validate();
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
