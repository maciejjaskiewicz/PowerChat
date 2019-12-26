using FluentValidation;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Users.Errors;

namespace PowerChat.Application.Users.Queries.GetUser
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        private readonly IUserService _userService;
        public GetUserQueryValidator(IUserService userService)
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
