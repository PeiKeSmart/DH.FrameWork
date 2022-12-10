using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DH.LazyCaptcha
{
    public static class RedisCacheCaptchaExtensions
    {
        /// <summary>
        /// 即将弃用，调整为直接使用 AddCache()，并且配置 AddStackExchangeRedisCache，参考 https://docs.microsoft.com/zh-cn/aspnet/core/performance/caching/distributed?view=aspnetcore-6.0#distributed-redis-cache
        /// </summary> 
        [Obsolete("调整为直接使用 AddCache()，使用 Redis 缓存时配置 AddStackExchangeRedisCache，参考 https://docs.microsoft.com/zh-cn/aspnet/core/performance/caching/distributed?view=aspnetcore-6.0#distributed-redis-cache", false)]
        public static IServiceCollection AddRedisCacheCaptcha(this IServiceCollection services, IConfiguration configuration, Action<CaptchaOptions> optionsAction = default!, string connName = null!)
        {
            var connStr = configuration.GetConnectionString(connName ?? "RedisCache");
            if (string.IsNullOrEmpty(connStr))
            {
                throw new Exception("Please set connection string for key `RedisCache` in setting json file");
            }
            services.AddCaptcha(configuration, optionsAction);
            return services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = connStr;
                options.InstanceName = "captcha:";
            });
        }
    }
}
