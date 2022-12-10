using DH.RateLimter;
using DH.RateLimter.Store;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRateLimter(this IServiceCollection services, Action<RateLimterOptions> options)
        {
            services.Configure(options);
            var opts = new RateLimterOptions();
            options(opts);
            services.AddSingleton(opts);

            services.TryAddSingleton<IRateLimitStore<RateLimitCounter>, CacheRateLimitStore<RateLimitCounter>>();
            services.TryAddSingleton<RateLimitProcessor>();

            return services;
        }
    }
}
