using Autofac;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DH.Core.Infrastructure
{
    /// <summary>
    /// 实现此接口的类可以作为组成DH引擎的各种服务的门户。编辑功能、模块和实现通过此界面访问大多数Nop功能。
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 添加和配置服务
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        /// <param name="configuration">应用程序的配置</param>
        /// <param name="webHostEnvironment">环境</param>
        void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment);

        /// <summary>
        /// 配置HTTP请求管道
        /// </summary>
        /// <param name="application">用于配置应用程序请求管道的生成器</param>
        void ConfigureRequestPipeline(IApplicationBuilder application);

        /// <summary>
        /// 解决依赖关系
        /// </summary>
        /// <param name="scope">范围</param>
        /// <typeparam name="T">解析服务的类型</typeparam>
        /// <returns>已解决的服务</returns>
        T Resolve<T>(IServiceScope scope = null) where T : class;

        /// <summary>
        /// 解决依赖关系
        /// </summary>
        /// <param name="type">已解析服务的类型</param>
        /// <param name="scope">范围</param>
        /// <returns>已解决的服务</returns>
        object Resolve(Type type, IServiceScope scope = null);

        /// <summary>
        /// 解析依赖项
        /// </summary>
        /// <typeparam name="T">已解析服务的类型</typeparam>
        /// <returns>已解析服务的集合</returns>
        IEnumerable<T> ResolveAll<T>();

        /// <summary>
        /// 解析未注册的服务
        /// </summary>
        /// <param name="type">服务类型</param>
        /// <returns>已解决的服务</returns>
        object ResolveUnregistered(Type type);

        /// <summary>
        /// 解决依赖关系。旧项目使用，新项目不再使用
        /// </summary>
        /// <param name="containerBuilder">容器制造商</param>
        void RegisterDependencies(ContainerBuilder containerBuilder);
    }
}
