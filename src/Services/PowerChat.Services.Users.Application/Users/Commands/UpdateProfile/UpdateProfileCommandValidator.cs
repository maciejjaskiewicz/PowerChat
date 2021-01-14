using System;
using FluentValidation;
using PowerChat.Services.Users.Core.Enums;

namespace PowerChat.Services.Users.Application.Users.Commands.UpdateProfile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .MaximumLength(100)
                .NotEmpty();

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
