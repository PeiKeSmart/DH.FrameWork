﻿using DH.PaySharp;
using DH.PaySharp.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加PaySharp
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        public static void AddPaySharp(this IServiceCollection services, Action<IGateways> setupAction)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IGateways>(a =>
            {
                var gateways = new Gateways();
                setupAction(gateways);

                return gateways;
            });
        }

        /// <summary>
        /// 使用PaySharp
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UsePaySharp(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            HttpUtil.Configure(httpContextAccessor);

            return app;
        }
    }
}
