using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PowerChat.Services.Chat.Application.Channels.Errors;
using PowerChat.Services.Chat.Application.Services;

namespace PowerChat.Services.Chat.Application.Channels.Queries.GetChannel
{
    public class GetChannelQueryValidator : AbstractValidator<GetChannelQuery>
    {
        private readonly IPowerChatServiceDbContext _dbContext;

        public GetChannelQueryValidator(IPowerChatServiceDbContext dbContext)
        {
            _dbContext = dbContext;

            Validate();
        }

        private void Validate()
        {
            RuleFor(x => x.Id)
                .MustAsync(async (id, cancellationToken) =>
                    await _dbContext.Channels.AnyAsync(c => c.Id == id, cancellationToken))
                .WithErrorCode(ChannelErrorCodes.ChannelNotFound)
                .WithMessage(q => $"Channel with id '{q.Id}' was not found");
        }
    }
}
