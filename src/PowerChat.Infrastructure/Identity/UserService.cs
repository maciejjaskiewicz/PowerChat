using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Application.Common.Results;
using PowerChat.Domain.Entities;

namespace PowerChat.Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IPowerChatDbContext _dbContext;

        public UserService(UserManager<User> userManager, 
            IPowerChatDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> UserExistsAsync(long id, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        public async Task<ValidationResult> ValidatePasswordAsync(string password)
        {
            var passwordErrors = new List<string>();

            foreach (var validator in _userManager.PasswordValidators)
            {
                var result = await validator.ValidateAsync(_userManager, null, password);

                if (result.Succeeded == false)
                {
                    foreach (var error in result.Errors)
                    {
                        passwordErrors.Add(error.Description);
                    }
                }
            }

            return passwordErrors.Any() ? ValidationResult.Fail(passwordErrors) : ValidationResult.Ok();
        }

        public async Task<ApplicationResult<long>> CreateUserAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.ToApplicationResult(user);
        }
    }   
}
