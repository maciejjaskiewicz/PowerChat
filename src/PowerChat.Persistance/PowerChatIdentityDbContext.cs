using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PowerChat.Persistence
{
    public class PowerChatIdentityDbContext : IdentityDbContext
    {
        public PowerChatIdentityDbContext(DbContextOptions<PowerChatIdentityDbContext> options)
            : base(options)
        { }
    }
}
