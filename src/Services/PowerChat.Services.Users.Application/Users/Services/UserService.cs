using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PowerChat.Services.Users.Application.Services;
using PowerChat.Services.Users.Core.Entities;

namespace PowerChat.Services.Users.Application.Users.Services
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string email, CancellationToken cancellationToken);
        Task<bool> UserExistsAsync(long id, CancellationToken cancellationToken);
        Task<bool> UserExistsAsync(string email, CancellationToken cancellationToken);
    }

    public class UserService : IUserService
    {
        private readonly IPowerChatServiceDbContext _dbContext;

        public UserService(IPowerChatServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<bool> UserExistsAsync(long id, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> UserExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await GetUserAsync(email, cancellationToken) != null;
        }
    }
}