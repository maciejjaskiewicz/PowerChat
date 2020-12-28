using System.Linq;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PowerChat.IdentityServer
{
    public static class IdentityInitializer
    {
        public static IApplicationBuilder UseIdentityInitializer(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>()
                .Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            context.Database.Migrate();

            InitializeApiResources(context);
            InitializeApiScopes(context);
            InitializeIdentityResources(context);
            InitializeClients(context);

            return app;
        }

        private static void InitializeApiResources(ConfigurationDbContext context)
        {
            if (context.ApiResources.Any()) 
                return;

            foreach (var resource in IdentityConfiguration.GetApis())
                context.ApiResources.Add(resource.ToEntity());

            context.SaveChanges();
        }

        private static void InitializeApiScopes(ConfigurationDbContext context)
        {
            if (context.ApiScopes.Any())
                return;

            foreach (var resource in IdentityConfiguration.GetApiScopes())
                context.ApiScopes.Add(resource.ToEntity());

            context.SaveChanges();
        }

        private static void InitializeIdentityResources(ConfigurationDbContext context)
        {
            if (context.IdentityResources.Any())
                return;

            foreach (var resource in IdentityConfiguration.GetIdentityResources())
                context.IdentityResources.Add(resource.ToEntity());

            context.SaveChanges();
        }

        private static void InitializeClients(ConfigurationDbContext context)
        {
            if (context.Clients.Any())
                return;

            foreach (var client in IdentityConfiguration.GetClients())
                context.Clients.Add(client.ToEntity());

            context.SaveChanges();
        }
    }
}
