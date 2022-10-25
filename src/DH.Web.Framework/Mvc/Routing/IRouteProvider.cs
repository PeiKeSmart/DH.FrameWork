using Microsoft.AspNetCore.Routing;

namespace DH.Web.Framework.Mvc.Routing
{
    /// <summary>
    /// 路由提供商
    /// </summary>
    public partial interface IRouteProvider
    {
        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="endpointRouteBuilder">路由生成器</param>
        void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder);

        /// <summary>
        /// 获取路由提供程序的优先级
        /// </summary>
        int Priority { get; }
    }
}
