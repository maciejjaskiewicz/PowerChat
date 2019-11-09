using Autofac;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PowerChat.API.Framework.Middleware;
using PowerChat.API.IoC;
using PowerChat.Application.Common.Interfaces;
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
            services.AddControllers(x =>
                {

                })
                .AddNewtonsoftJson()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<IPowerChatDbContext>();
                    fv.LocalizationEnabled = false;
                });
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

            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseExceptionsHandler();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{controller=Channels}/{action=GetAll}/{id?}");
            });
        }
    }
}
