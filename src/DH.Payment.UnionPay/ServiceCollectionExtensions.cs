using DG.Payment.UnionPay;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddUnionPay(
            this IServiceCollection services)
        {
            services.AddUnionPay(null);
        }

        public static void AddUnionPay(
            this IServiceCollection services,
            Action<UnionPayOptions> setupAction)
        {
            services.AddHttpClient(nameof(UnionPayClient));

            services.AddSingleton<IUnionPayClient, UnionPayClient>();

            services.AddSingleton<IUnionPayNotifyClient, UnionPayNotifyClient>();

            if (setupAction != null)
            {
                services.Configure(setupAction);
            }
        }
    }
}