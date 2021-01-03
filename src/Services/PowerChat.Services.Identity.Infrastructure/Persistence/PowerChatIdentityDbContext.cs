using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PowerChat.Services.Identity.Infrastructure.Persistence
{
    public class PowerChatIdentityDbContext : IdentityDbContext
    {
        public PowerChatIdentityDbContext(DbContextOptions<PowerChatIdentityDbContext> options)
            : base(options)
        { }
    }
}