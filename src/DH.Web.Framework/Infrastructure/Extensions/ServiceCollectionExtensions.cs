using DH.Core;
using DH.Core.Infrastructure;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using System.Net;

namespace DH.Web.Framework.Infrastructure.Extensions
{
    /// <summary>
    /// 表示IServiceCollection的扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 向应用程序添加服务并配置服务提供商
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        /// <param name="builder">web应用程序和服务的生成器</param>
        public static void ConfigureApplicationServices(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            //让操作系统决定使用什么TLS协议版本
            //参见https://docs.microsoft.com/dotnet/framework/network-programming/tls
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;

            //create default file provider
            CommonHelper.DefaultFileProvider = new DHFileProvider(builder.Environment);


        }
    }
}
