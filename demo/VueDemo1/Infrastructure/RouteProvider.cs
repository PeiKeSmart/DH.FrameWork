using DH.Services.Installation;
using DH.Web.Framework.Mvc.Routing;

namespace VueDemo1.Infrastructure
{
    /// <summary>
    /// 表示提供基本路由的提供程序
    /// </summary>
    public partial class RouteProvider : BaseRouteProvider, IRouteProvider
    {
        /// <summary>
        /// 获取路由提供程序的优先级
        /// </summary>
        public int Priority => 0;

        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="endpointRouteBuilder">路由生成器</param>
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            // 获取语言模式
            // 不需要在AJAX请求中使用语言模式，并且对于直接返回结果的动作 (例如：下载文件),
            // 仅将其用于用户可以访问的页面的URL
            var lang = GetLanguageRoutePattern();

            // 区域
            endpointRouteBuilder.MapControllerRoute(name: "areaRoute",
                pattern: $"{{area:exists}}/{{controller=Home}}/{{action=Index}}/{{id?}}");

            // 主页
            endpointRouteBuilder.MapControllerRoute(name: "Homepage",
                pattern: $"{lang}",
                defaults: new { controller = "Home", action = "Index" });




            //robots.txt (file result)
            endpointRouteBuilder.MapControllerRoute(name: "robots.txt",
                pattern: $"robots.txt",
                defaults: new { controller = "Common", action = "RobotsTextFile" });

            //sitemap
            endpointRouteBuilder.MapControllerRoute(name: "Sitemap",
                pattern: $"{lang}/sitemap",
                defaults: new { controller = "Common", action = "Sitemap" });

            //sitemap.xml (file result)
            endpointRouteBuilder.MapControllerRoute(name: "sitemap.xml",
                pattern: $"sitemap.xml",
                defaults: new { controller = "Common", action = "SitemapXml" });

            endpointRouteBuilder.MapControllerRoute(name: "sitemap-indexed.xml",
                pattern: $"sitemap-{{Id:min(0)}}.xml",
                defaults: new { controller = "Common", action = "SitemapXml" });

            //store closed
            endpointRouteBuilder.MapControllerRoute(name: "StoreClosed",
                pattern: $"{lang}/storeclosed",
                defaults: new { controller = "Common", action = "StoreClosed" });

            //install
            endpointRouteBuilder.MapControllerRoute(name: "Installation",
                pattern: $"{DHInstallationDefaults.InstallPath}",
                defaults: new { controller = "Install", action = "Index" });

            //error page
            endpointRouteBuilder.MapControllerRoute(name: "Error",
                pattern: $"error",
                defaults: new { controller = "Common", action = "Error" });

            //page not found
            endpointRouteBuilder.MapControllerRoute(name: "PageNotFound",
                pattern: $"{lang}/page-not-found",
                defaults: new { controller = "Common", action = "PageNotFound" });
        }
    }
}
