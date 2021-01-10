using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PowerChat.Services.Chat.Application.Channels.Errors;
using PowerChat.Services.Chat.Application.Services;

namespace PowerChat.Services.Chat.Application.Channels.Commands.SendChannelMessage
{
    public class SendChannelMessageCommandValidator : AbstractValidator<SendChannelMessageCommand>
    {
        private readonly IPowerChatServiceDbContext _dbContext;

        public SendChannelMessageCommandValidator(IPowerChatServiceDbContext dbContext)
        {
            _dbContext = dbContext;

            Validate();
        }

        private void Validate()
        {
            RuleFor(x => x.ChannelId)
                .MustAsync(async (id, cancellationToken) =>
                    await _dbContext.Channels.AnyAsync(c => c.Id == id, cancellationToken))
                .WithErrorCode(ChannelErrorCodes.ChannelNotFound)
                .WithMessage(q => $"Channel with id '{q.ChannelId}' was not found");

            RuleFor(x => x.Content)
                .NotEmpty();
        }
    }
}
