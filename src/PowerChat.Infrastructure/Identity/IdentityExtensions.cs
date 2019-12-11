using System.Linq;
using Microsoft.AspNetCore.Identity;
using PowerChat.Application.Common.Results;
using PowerChat.Common.Results;
using PowerChat.Domain.Entities;

namespace PowerChat.Infrastructure.Identity
{
    public static class IdentityExtensions
    {
        public static ApplicationResult<long> ToApplicationResult(this IdentityResult identityResult, User user)
        {
            if (identityResult.Succeeded)
            {
                return ApplicationResult<long>.Ok(user.Id);
            }

            return ApplicationResult<long>.Fail(identityResult.Errors.Select(e => PowerChatError.Create(e.Code, e.Description)).First());
        }
    }
}
