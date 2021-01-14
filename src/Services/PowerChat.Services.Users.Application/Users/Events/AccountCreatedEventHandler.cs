using System;
using System.Threading;
using PowerChat.Common.Interfaces;
using PowerChat.Services.Common.Application.Contract.Identity;
using PowerChat.Services.Users.Application.Services;
using PowerChat.Services.Users.Core.Entities;
using PowerChat.Services.Users.Core.Enums;
using PowerChat.Services.Users.Core.ValueObjects;

namespace PowerChat.Services.Users.Application.Users.Events
{
    public class AccountCreatedEventHandler : Common.Application.Events.EventHandler<AccountCreatedEvent>
    {
        private readonly IDateTime _dateTime;
        private readonly IPowerChatServiceDbContext _dbContext;

        public AccountCreatedEventHandler(IDateTime dateTime, 
            IPowerChatServiceDbContext dbContext)
        {
            _dateTime = dateTime;
            _dbContext = dbContext;
        }

        protected override void Handle(AccountCreatedEvent notification)
        {
            var user = new User
            {
                IdentityId = notification.IdentityId,
                Email = notification.Email,
                Name = PersonName.Create(notification.FirstName, notification.LastName),
                Gender = !string.IsNullOrEmpty(notification.Gender) ? (Gender?)Enum.Parse<Gender>(notification.Gender) : null,
                LastActive = _dateTime.UtcNow
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}
