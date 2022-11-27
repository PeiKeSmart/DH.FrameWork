using DH.Core.Configuration;
using DH.Web.Framework.Mvc.Routing;

namespace VueDemo1.Infrastructure
{
    /// <summary>
    /// 表示提供通用路由的提供程序
    /// </summary>
    public partial class GenericUrlRouteProvider : BaseRouteProvider, IRouteProvider
    {

        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="endpointRouteBuilder">Route builder</param>
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var lang = GetLanguageRoutePattern();

            // 默认路由
            // 这些路由不是通用的，它们只是默认映射与其他模式不匹配的请求，
            // 但是我们在这里定义它们，因为这个路由提供者的优先级最低，允许在它们之前添加其他路由
            if (!string.IsNullOrEmpty(lang))
            {
                endpointRouteBuilder.MapControllerRoute(name: "DefaultWithLanguageCode",
                    pattern: $"{lang}/{{controller=Home}}/{{action=Index}}/{{id?}}");
            }

            endpointRouteBuilder.MapControllerRoute(name: "Default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            if (!DHSetting.Current.IsInstalled)
                return;

            // 通用路由（实际路由稍后在SlugRouteTransformer中处理）
            var genericCatalogPattern = $"{lang}/{{{DHRoutingDefaults.RouteValue.CatalogSeName}}}/{{{DHRoutingDefaults.RouteValue.SeName}}}";

            var genericPattern = $"{lang}/{{{DHRoutingDefaults.RouteValue.SeName}}}";
            endpointRouteBuilder.MapDynamicControllerRoute<SlugRouteTransformer>(genericPattern);

            // 未找到段路的路由
            if (!string.IsNullOrEmpty(lang))
            {
                endpointRouteBuilder.MapControllerRoute(name: DHRoutingDefaults.RouteName.Generic.GenericUrlWithLanguageCode,
                    pattern: genericPattern,
                    defaults: new { controller = "Common", action = "GenericUrl" });

                endpointRouteBuilder.MapControllerRoute(name: DHRoutingDefaults.RouteName.Generic.GenericCatalogUrlWithLanguageCode,
                    pattern: genericCatalogPattern,
                    defaults: new { controller = "Common", action = "GenericUrl" });
            }

            endpointRouteBuilder.MapControllerRoute(name: DHRoutingDefaults.RouteName.Generic.GenericUrl,
                pattern: $"{{{DHRoutingDefaults.RouteValue.SeName}}}",
                defaults: new { controller = "Common", action = "GenericUrl" });
        }

        /// <summary>
        /// 获取路由提供程序的优先级
        /// </summary>
        /// <remarks>
        /// 这应该是最后一条路线。我们没有将其设置为-int。MaxValue，以便可以覆盖（如果需要）
        /// </remarks>
        public int Priority => -1000000;
    }
}
