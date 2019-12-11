using FluentValidation;

namespace PowerChat.Application.Users.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
