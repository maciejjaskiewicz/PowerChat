using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PowerChat.Persistence;

namespace PowerChat.IdentityServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("IdentityDatabase");
            var migrationsAssembly = typeof(PowerChatIdentityDbContext).GetTypeInfo()
                .Assembly.GetName().Name;

            services.AddDbContext<PowerChatIdentityDbContext>(config =>
            {
                config.UseSqlServer(connectionString);
            });

            var identityServerBuilder = services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString, 
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                });

            if (CurrentEnvironment.IsDevelopment())
                identityServerBuilder.AddTestUsers(IdentityConfiguration.GetTestUsers());

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseIdentityInitializer();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseIdentityServer();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
