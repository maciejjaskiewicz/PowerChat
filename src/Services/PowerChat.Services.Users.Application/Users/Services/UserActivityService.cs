using System.Threading;
using System.Threading.Tasks;
using PowerChat.Common.Interfaces;
using PowerChat.Services.Users.Application.Services;

namespace PowerChat.Services.Users.Application.Users.Services
{
    public interface IUserActivityService
    {
        Task UpdateActivity(long userId, CancellationToken cancellationToken = default);
    }

    public class UserActivityService : IUserActivityService
    {
        private readonly IPowerChatServiceDbContext _dbContext;
        private readonly IDateTime _dateTime;

        public UserActivityService(IPowerChatServiceDbContext dbContext, 
            IDateTime dateTime)
        {
            _dbContext = dbContext;
            _dateTime = dateTime;
        }

        public async Task UpdateActivity(long userId, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.FindAsync(new object[] {userId}, cancellationToken);
            user.LastActive = _dateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
