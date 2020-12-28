using System;
using FluentValidation;
using FluentValidation.Results;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Domain.Enums;

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
            RuleFor(x => x.IdentityId)
                .NotEmpty()
                .NotNull();

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
                .MustAsync(async (email, cancellationToken) => await _userService.UserExistsAsync(email) == false)
                .WithErrorCode("EmailAlreadyExists")
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
                            var validationFailure = new ValidationFailure("Password", error)
                            {
                                ErrorCode = "InvalidPassword"
                            };

                            context.AddFailure(validationFailure);
                        }
                    }
                });

            RuleFor(x => x.Gender)
                .Custom((gender, context) =>
                {
                    if (string.IsNullOrEmpty(gender)) return;

                    if (!Enum.IsDefined(typeof(Gender), gender))
                    {
                        context.AddFailure($"'{gender}' is invalid gender type.");
                    }

                });
        }
    }
}
