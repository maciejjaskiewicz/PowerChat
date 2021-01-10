using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PowerChat.Services.Common.Infrastructure.DistributedCache
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRedis(this IServiceCollection services, IConfigurationSection redisSection)
        {
            var options = new RedisConfiguration();
            redisSection.Bind(options);

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = options.ConnectionString;
                option.InstanceName = options.Instance;
            });
        }
    }
}
