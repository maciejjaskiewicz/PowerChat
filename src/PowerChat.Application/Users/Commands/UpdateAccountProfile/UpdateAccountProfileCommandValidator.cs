using System;
using FluentValidation;
using PowerChat.Domain.Enums;

namespace PowerChat.Application.Users.Commands.UpdateAccountProfile
{
    public class UpdateAccountProfileCommandValidator : AbstractValidator<UpdateAccountProfileCommand>
    {
        public UpdateAccountProfileCommandValidator()
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
