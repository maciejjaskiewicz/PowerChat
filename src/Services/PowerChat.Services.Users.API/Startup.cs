using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PowerChat.Services.Common.Application.Contract.Identity;
using PowerChat.Services.Common.Infrastructure.Framework.Middleware;
using PowerChat.Services.Common.Infrastructure.ServiceBus;
using PowerChat.Services.Users.API.Framework;
using PowerChat.Services.Users.API.Hubs;
using PowerChat.Services.Users.Infrastructure.Persistence;
using Serilog;

namespace PowerChat.Services.Users.API
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
            services.AddCors();

            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:44353";
                    options.ApiName = "PowerChatAPI";
                    options.ApiSecret = "PowerChatAPI";
                });

            services.AddSignalR();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();

            services.AddServiceComponents(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PowerChatUsersDbContext dbContext)
        {
            app.SubscribeToEvent<AccountCreatedEvent>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
            );

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PowerChat API");
            });

            app.UseRouting();
            app.UseExceptionsHandler();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/ws");
                endpoints.MapDefaultControllerRoute();
            });

            dbContext.Database.Migrate();
        }
    }
}
