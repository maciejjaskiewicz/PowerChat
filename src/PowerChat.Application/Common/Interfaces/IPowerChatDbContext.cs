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
        DbSet<Interlocutor> Interlocutors { get; set; }
        DbSet<Message> Messages { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
