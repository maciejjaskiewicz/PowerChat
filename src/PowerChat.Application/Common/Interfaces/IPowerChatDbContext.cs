using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PowerChat.Domain.Entities;

namespace PowerChat.Application.Common.Interfaces
{
    public interface IPowerChatDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Channel> Channels { get; set; }
        DbSet<Friendship> Friendships { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
