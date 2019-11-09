using FluentValidation;

namespace PowerChat.Application.Channels.Commands.CreateChannel
{
    public class CreateChannelCommandValidator : AbstractValidator<CreateChannelCommand>
    {
        public CreateChannelCommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
