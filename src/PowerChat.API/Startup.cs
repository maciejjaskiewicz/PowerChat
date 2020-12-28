using Autofac;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PowerChat.API.Framework.Middleware;
using PowerChat.API.Hubs;
using PowerChat.API.IoC;
using PowerChat.Persistence;
using Serilog;

namespace PowerChat.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:44325";
                    options.ApiName = "PowerChatAPI";
                    options.ApiSecret = "PowerChatAPI";
                });

            services.AddSignalR();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();

            AspNetRegistration.Register(services, Configuration);
        }

        // Autofac configuration
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new RootModule(Configuration));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

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
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllers();
            });

            var dbContext = app.ApplicationServices.GetService<PowerChatDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
