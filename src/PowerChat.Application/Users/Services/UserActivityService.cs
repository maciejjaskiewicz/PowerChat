using System.Threading;
using System.Threading.Tasks;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Common.Interfaces;

namespace PowerChat.Application.Users.Services
{
    public interface IUserActivityService : IApplicationService
    {
        Task UpdateActivity(long userId, CancellationToken cancellationToken = default);
    }

    public class UserActivityService : IUserActivityService
    {
        private readonly IPowerChatDbContext _dbContext;
        private readonly IDateTime _dateTime;

        public UserActivityService(IPowerChatDbContext dbContext, 
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
