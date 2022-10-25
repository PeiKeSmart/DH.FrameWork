using DH.Core.Infrastructure;
using Microsoft.AspNetCore.Routing;

namespace DH.Web.Framework.Mvc.Routing
{
    /// <summary>
    /// 表示路由发布者的实现
    /// </summary>
    public class RoutePublisher : IRoutePublisher
    {
        #region Fields

        /// <summary>
        /// 类型查找器
        /// </summary>
        protected readonly ITypeFinder _typeFinder;

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="typeFinder">类型查找器</param>
        public RoutePublisher(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 注册路线
        /// </summary>
        /// <param name="endpointRouteBuilder">路线生成器</param>
        public virtual void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            // 查找其他程序集提供的路由提供程序
            var routeProviders = _typeFinder.FindClassesOfType<IRouteProvider>();

            // 创建和排序路由提供程序的实例
            var instances = routeProviders
                .Select(routeProvider => (IRouteProvider)Activator.CreateInstance(routeProvider))
                .OrderByDescending(routeProvider => routeProvider.Priority);

            // 注册所有提供的路线
            foreach (var routeProvider in instances)
                routeProvider.RegisterRoutes(endpointRouteBuilder);
        }

        #endregion
    }
}
