using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Common.Results;
using PowerChat.Domain.Enums;
using PowerChat.Domain.ValueObjects;

namespace PowerChat.Application.Users.Commands.UpdateAccountProfile
{
    public class UpdateAccountProfileCommandHandler : IRequestHandler<UpdateAccountProfileCommand, ApplicationResult>
    {
        private readonly IPowerChatDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public UpdateAccountProfileCommandHandler(IPowerChatDbContext dbContext, 
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<ApplicationResult> Handle(UpdateAccountProfileCommand request, CancellationToken cancellationToken)
        {
            var currentUserResult = _currentUserService.GetResultUserId();

            if (currentUserResult.Succeeded == false)
            {
                return ApplicationResult.Fail(currentUserResult.Error);
            }

            var user = await _dbContext.Users.FindAsync(currentUserResult.Value);

            user.Name = PersonName.Create(request.FirstName, request.LastName);
            user.Gender = !string.IsNullOrEmpty(request.Gender) ? (Gender?) Enum.Parse<Gender>(request.Gender) : null;
            user.About = request.About;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return ApplicationResult.Ok();
        }
    }
}
