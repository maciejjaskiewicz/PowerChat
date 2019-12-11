using FluentValidation;
using PowerChat.Application.Common.Interfaces;

namespace PowerChat.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserService _userService;

        public CreateUserCommandValidator(IUserService userService)
        {
            _userService = userService;

            Validate();
        }

        private void Validate()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty()
                .MustAsync(async (email, cancellationToken) => await _userService.UserExistsAsync(email) == false)
                .WithMessage(x => $"User with email '{x.Email}' already exists.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .CustomAsync(async (password, context, cancellationToken) =>
                {
                    var result = await _userService.ValidatePasswordAsync(password);

                    if (result.Succeeded == false)
                    {
                        foreach (var error in result.ValidationFailures)
                        {
                            context.AddFailure(error);
                        }
                    }
                });
        }
    }
}
