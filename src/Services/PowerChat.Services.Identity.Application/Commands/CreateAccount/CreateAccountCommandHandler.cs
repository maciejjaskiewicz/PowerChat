using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PowerChat.Common.Results;
using PowerChat.Services.Common.Application.Contract.Identity;
using PowerChat.Services.Common.Application.Results;
using PowerChat.Services.Common.Application.Services;

namespace PowerChat.Services.Identity.Application.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ApplicationResult>
    {
        private readonly IMessageBroker _messageBroker;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateAccountCommandHandler(IMessageBroker messageBroker,
            UserManager<IdentityUser> userManager)
        {
            _messageBroker = messageBroker;
            _userManager = userManager;
        }

        public async Task<ApplicationResult> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var identityResult = await _userManager.CreateAsync(user, request.Password);
            var result = ToApplicationResult(identityResult, user);

            if (result.Succeeded)
            {
                var accountCreatedEvent = new AccountCreatedEvent
                {
                    IdentityId = user.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Gender = request.Gender
                };

                // TODO: refactor to direct users service call
                await _messageBroker.PublishAsync(accountCreatedEvent);
            }

            return result;
        }

        private static ApplicationResult<string> ToApplicationResult(IdentityResult identityResult, IdentityUser user)
        {
            if (identityResult.Succeeded)
                return ApplicationResult<string>.Ok(user.Id);

            return ApplicationResult<string>.Fail(identityResult.Errors.Select(e 
                => PowerChatError.Create(e.Code, e.Description)).First());
        }
    }
}
