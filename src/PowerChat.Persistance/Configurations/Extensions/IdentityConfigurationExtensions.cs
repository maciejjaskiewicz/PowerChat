using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PowerChat.Domain.Entities;

namespace PowerChat.Persistence.Configurations.Extensions
{
    public static class IdentityConfigurationExtensions
    {
        public static string IdentitySchema = "idp";

        public static ModelBuilder ApplyIdentityEntitiesConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<long>>(e => e.ToTable("UserClaim", IdentitySchema));
            modelBuilder.Entity<IdentityUserLogin<long>>(e => e.ToTable("UserLogin", IdentitySchema));
            modelBuilder.Entity<IdentityUserToken<long>>(e => e.ToTable("UserToken", IdentitySchema));
            modelBuilder.Entity<IdentityRoleClaim<long>>(e => e.ToTable("RoleClaim", IdentitySchema));
            modelBuilder.Entity<IdentityUserRole<long>>(e => e.ToTable("UserRole", IdentitySchema));
            modelBuilder.Entity<IdentityRole<long>>(e => e.ToTable("Role", IdentitySchema));
            modelBuilder.Entity<User>(e => e.ToTable("User", IdentitySchema));

            return modelBuilder;
        }
    }
}
