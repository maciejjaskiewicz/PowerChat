using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PowerChat.Services.Users.Core.Entities;

namespace PowerChat.Services.Users.Application.Services
{
    public interface IPowerChatServiceDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Friendship> Friendships { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
