using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace PowerChat.Services.Identity.Application.Commands.CreateAccount
{
    public class CreateUserCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CreateUserCommandValidator(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;

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
                .WithMessage(x => $"'{x.Email}' is not a valid email address.")
                .NotEmpty()
                .MustAsync(async (email, cancellationToken) => await UserExistsAsync(email) == false)
                .WithErrorCode("EmailAlreadyExists")
                .WithMessage(x => $"User with email '{x.Email}' already exists.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .CustomAsync(async (password, context, cancellationToken) =>
                {
                    var result = await ValidatePasswordAsync(password);

                    if (result.Succeeded == false)
                    {
                        foreach (var error in result.ValidationFailures)
                        {
                            var validationFailure = new ValidationFailure("Password", error)
                            {
                                ErrorCode = "InvalidPassword"
                            };

                            context.AddFailure(validationFailure);
                        }
                    }
                });
        }

        public async Task<Common.Application.Results.ValidationResult> ValidatePasswordAsync(string password)
        {
            var passwordErrors = new List<string>();

            foreach (var validator in _userManager.PasswordValidators)
            {
                var result = await validator.ValidateAsync(_userManager, null, password);

                if (result.Succeeded == false)
                {
                    foreach (var error in result.Errors)
                    {
                        passwordErrors.Add(error.Description);
                    }
                }
            }

            return passwordErrors.Any() 
                ? Common.Application.Results.ValidationResult.Fail(passwordErrors) 
                : Common.Application.Results.ValidationResult.Ok();
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}