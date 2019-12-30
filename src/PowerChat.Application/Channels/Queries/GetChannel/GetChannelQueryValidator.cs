using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PowerChat.Application.Channels.Errors;
using PowerChat.Application.Common.Interfaces;

namespace PowerChat.Application.Channels.Queries.GetChannel
{
    public class GetChannelQueryValidator : AbstractValidator<GetChannelQuery>
    {
        private readonly IPowerChatDbContext _dbContext;

        public GetChannelQueryValidator(IPowerChatDbContext dbContext)
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
