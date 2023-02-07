﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;

namespace DG.Payment.QPay
{
    public static class ServiceCollectionExtensions
    {
        public static void AddQPay(
            this IServiceCollection services)
        {
            services.AddQPay(null);
        }

        public static void AddQPay(
            this IServiceCollection services,
            Action<QPayOptions> setupAction)
        {
            services.AddHttpClient(nameof(QPayClient));

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, QPayHandlerBuilderFilter>());

            services.AddSingleton<QPayCertificateManager>();
            services.AddSingleton<IQPayClient, QPayClient>();

            services.AddSingleton<IQPayNotifyClient, QPayNotifyClient>();

            if (setupAction != null)
            {
                services.Configure(setupAction);
            }
        }
    }
}