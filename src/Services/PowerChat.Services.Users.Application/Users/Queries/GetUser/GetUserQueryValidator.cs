using FluentValidation;
using PowerChat.Services.Users.Application.Users.Errors;
using PowerChat.Services.Users.Application.Users.Services;

namespace PowerChat.Services.Users.Application.Users.Queries.GetUser
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        private readonly UserService _userService;
        public GetUserQueryValidator(UserService userService)
        {
            _userService = userService;

            Validate();
        }

        private void Validate()
        {
            RuleFor(x => x.Id)
                .MustAsync(async (id, cancellationToken) => await _userService.UserExistsAsync(id, cancellationToken))
                .WithErrorCode(UserErrorCodes.UserNotFound)
                .WithMessage(x => $"User with id '{x.Id}' was not found.");
        }
    }
}
