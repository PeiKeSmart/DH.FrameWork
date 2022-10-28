using DH.Core.Infrastructure;
using DH.Entity;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DH.Web.Framework.Infrastructure
{
    /// <summary>
    /// 表示应用程序启动时配置路由的类
    /// </summary>
    public partial class DHUrlRewrite : IDHStartup
    {
        /// <summary>
        /// 添加并配置任何中间件
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        /// <param name="configuration">应用程序的配置</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

        }

        /// <summary>
        /// 配置添加的中间件的使用
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public void Configure(IApplicationBuilder application)
        {
            var options = new RewriteOptions();

            var RewriteList = RouteRewrite.GetAll();
            var language = Language.FindByStatus();

            foreach (var item in RewriteList)
            {
                var regexInfo = item.RegexInfo;
                options.AddRewrite("(?i)" + regexInfo.Replace("{language}/", ""), item.ReplacementInfo.Replace("{language}/", ""), skipRemainingRules: true);

                foreach (var item1 in language)
                {
                    options.AddRewrite("(?i)" + regexInfo.Replace("{language}", item1.UniqueSeoCode), item.ReplacementInfo.Replace("{language}", item1.UniqueSeoCode), skipRemainingRules: true);
                }
            }

            application.UseRewriter(options);
        }

        /// <summary>
        /// 获取此启动配置实现的顺序
        /// </summary>
        public int Order => 98; // URL重写中间件应在静态文件之前注册
    }
}
