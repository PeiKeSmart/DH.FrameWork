using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DG.Sms.FengHuo
{
    /// <summary>
    /// 短信扩展
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 注册短信操作
        /// </summary>
        /// <param name="services">服务集合</param>
        public static void AddFengHuoSms(this IServiceCollection services)
        {
            services.TryAddScoped<ISmsService, SmsService>();
        }
    }
}
