using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PowerChat.Services.Common.Application.Results;
using PowerChat.Services.Common.Application.Services;
using PowerChat.Services.Users.Application.Services;
using PowerChat.Services.Users.Core.Enums;
using PowerChat.Services.Users.Core.ValueObjects;

namespace PowerChat.Services.Users.Application.Users.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ApplicationResult>
    {
        private readonly IPowerChatServiceDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public UpdateProfileCommandHandler(IPowerChatServiceDbContext dbContext, 
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<ApplicationResult> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var currentUserResult = _currentUserService.GetResultUserId();

            if (currentUserResult.Succeeded == false)
            {
                return ApplicationResult.Fail(currentUserResult.Error);
            }

            var user = await _dbContext.Users.SingleAsync(x => x.Id == currentUserResult.Value, cancellationToken);

            user.Name = PersonName.Create(request.FirstName, request.LastName);
            user.Gender = !string.IsNullOrEmpty(request.Gender) ? (Gender?) Enum.Parse<Gender>(request.Gender) : null;
            user.About = request.About;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return ApplicationResult.Ok();
        }
    }
}
