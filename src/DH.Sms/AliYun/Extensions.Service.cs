using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace DH.Sms.AliYun
{
    /// <summary>
    /// 阿里短信扩展
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 注册短信操作
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="setupAction">配置操作</param>
        public static void AddAliSms(this IServiceCollection services, Action<SmsOptions> setupAction)
        {
            services.Configure(setupAction);
            services.TryAddScoped<ISmsService, SmsService>();
        }
    }
}
