using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PowerChat.Services.Chat.Core.Entities;

namespace PowerChat.Services.Chat.Application.Services
{
    public interface IPowerChatServiceDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Channel> Channels { get; set; }
        DbSet<Interlocutor> Interlocutors { get; set; }
        DbSet<Message> Messages { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
