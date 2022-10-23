using Microsoft.AspNetCore.Routing;

namespace DH.Web.Framework.Mvc.Routing
{
    /// <summary>
    /// 表示路由发布者
    /// </summary>
    public partial interface IRoutePublisher
    {
        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="endpointRouteBuilder">路线生成器</param>
        void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder);
    }
}
