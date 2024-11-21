using System.Reflection;

using Autofac;

using AutoMapper;

using DH.Core.Infrastructure.Mapper;
using DH.Infrastructure.DependencyManagement;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NewLife.Log;
using NewLife.Model;

using Pek.Exceptions;
using Pek.Infrastructure;

using IServiceScope = Microsoft.Extensions.DependencyInjection.IServiceScope;

namespace DH.Core.Infrastructure;

/// <summary>
/// 表示DH引擎
/// </summary>
public partial class DHEngine : IEngine
{
    #region 字段

    private ITypeFinder _typeFinder;

    #endregion

    #region 公共方法

    /// <summary>
    /// 获取IService提供商
    /// </summary>
    /// <returns>IService提供商</returns>
    protected IServiceProvider GetServiceProvider(IServiceScope scope = null)
    {
        if (scope == null)
        {
            var accessor = ServiceProviderServiceExtensions.GetService<IHttpContextAccessor>(ServiceProvider);
            var context = accessor?.HttpContext;
            return context?.RequestServices ?? ServiceProvider;
        }
        return scope.ServiceProvider;
    }

    /// <summary>
    /// 运行启动任务
    /// </summary>
    protected virtual void RunStartupTasks()
    {
        // 查找其他程序集提供的启动任务
        var typeFinder = ObjectContainer.Provider.GetPekService<ITypeFinder>();
        var startupTasks = typeFinder.FindClassesOfType<IStartupTask>();

        // 创建和排序启动任务的实例
        // 我们甚至为未安装的插件启动此界面。
        // 否则，DbContext初始值设定项将无法运行，插件安装将无法工作
        var instances = startupTasks
            .Select(startupTask => (IStartupTask)Activator.CreateInstance(startupTask))
            .OrderBy(startupTask => startupTask.Order);

        // 执行任务
        foreach (var task in instances)
            task.ExecuteAsync().Wait();
    }

    /// <summary>
    /// 注册依赖
    /// </summary>
    /// <param name="containerBuilder">容器制造商</param>
    public virtual void RegisterDependencies(ContainerBuilder containerBuilder)
    {
        //注册引擎
        containerBuilder.RegisterInstance(this).As<IEngine>().SingleInstance();

        //寄存器类型查找器
        containerBuilder.RegisterInstance(_typeFinder).As<ITypeFinder>().SingleInstance();

        //寄存器类型查找器
        var dependencyRegistrars = _typeFinder.FindClassesOfType<IDependencyRegistrar>();

        //创建和排序依赖项注册器的实例
        var instances = dependencyRegistrars
            .Select(dependencyRegistrar => (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar))
            .OrderBy(dependencyRegistrar => dependencyRegistrar.Order);

        //注册所有提供的依赖项
        foreach (var dependencyRegistrar in instances)
            dependencyRegistrar.Register(containerBuilder, _typeFinder);
    }

    /// <summary>
    /// 注册和配置AutoMapper
    /// </summary>
    protected virtual void AddAutoMapper()
    {
        // 查找其他程序集提供的映射器配置
        var typeFinder = ObjectContainer.Provider.GetPekService<ITypeFinder>();
        var mapperConfigurations = typeFinder.FindClassesOfType<IOrderedMapperProfile>();

        // 创建和排序映射器配置的实例
        var instances = mapperConfigurations
            .Select(mapperConfiguration => (IOrderedMapperProfile)Activator.CreateInstance(mapperConfiguration))
            .OrderBy(mapperConfiguration => mapperConfiguration.Order);

        // 创建AutoMapper配置
        var config = new MapperConfiguration(cfg =>
        {
            foreach (var instance in instances)
            {
                cfg.AddProfile(instance.GetType());
            }
        });

        // 注册
        AutoMapperConfiguration.Init(config);
    }

    private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
        // 检查是否已加载程序集
        var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
        if (assembly != null)
            return assembly;

        // 从TypeFinder获取程序集
        var typeFinder = ObjectContainer.Provider.GetPekService<ITypeFinder>();
        assembly = typeFinder?.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
        return assembly;
    }

    #endregion

    #region 方法

    /// <summary>
    /// 添加和配置服务
    /// </summary>
    /// <param name="services">服务描述符集合</param>
    /// <param name="configuration">应用程序的配置</param>
    /// <param name="webHostEnvironment">环境</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        // 注册引擎
        services.AddSingleton<IEngine>(this);

        // 创建和排序启动配置的实例
        XTrace.WriteLine($"添加和配置服务顺序：ConfigureServices");

        // 配置服务
        foreach (var instance in DHConast.DHStartups.OrderBy(e => e.StartupOrder))
        {
            XTrace.WriteLine($"{instance.GetType().Name}:{instance.StartupOrder}");
            instance.ConfigureServices(services, configuration, webHostEnvironment);
        }

        services.AddSingleton(services);

        // 注册映射器配置
        AddAutoMapper();

        // 运行启动任务
        RunStartupTasks();

        // 在此处解析程序集。否则，插件在呈现视图时会引发异常
        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
    }

    /// <summary>
    /// 配置HTTP请求管道
    /// </summary>
    /// <param name="application">用于配置应用程序的请求管道的生成器</param>
    public void ConfigureRequestPipeline(IApplicationBuilder application)
    {
        ServiceProvider = application.ApplicationServices;

        // 查找其他程序集提供的启动配置
        XTrace.WriteLine($"配置HTTP请求管道：Configure");

        // 配置请求管道
        foreach (var instance in DHConast.DHStartups.OrderBy(e => e.StartupOrder))
        {
            XTrace.WriteLine($"{instance.GetType().Name}:{instance.StartupOrder}");
            instance.Configure(application);
        }
    }

    /// <summary>
    /// 解决依赖关系
    /// </summary>
    /// <param name="scope">范围</param>
    /// <typeparam name="T">已解决服务的类型</typeparam>
    /// <returns>已解决的服务</returns>
    public T Resolve<T>(IServiceScope scope = null) where T : class
    {
        return (T)Resolve(typeof(T), scope);
    }

    /// <summary>
    /// 解决依赖关系
    /// </summary>
    /// <param name="type">已解决服务的类型</param>
    /// <param name="scope">范围</param>
    /// <returns>已解决的服务</returns>
    public object Resolve(Type type, IServiceScope scope = null) => GetServiceProvider(scope)?.GetService(type);

    /// <summary>
    /// 解析依赖项
    /// </summary>
    /// <typeparam name="T">已解析服务的类型</typeparam>
    /// <returns>已解析服务的集合</returns>
    public virtual IEnumerable<T> ResolveAll<T>()
    {
        return (IEnumerable<T>)ServiceProviderServiceExtensions.GetServices(GetServiceProvider(), typeof(T));
    }

    /// <summary>
    /// 解析未注册的服务
    /// </summary>
    /// <param name="type">服务类型</param>
    /// <returns>已解决的服务</returns>
    public virtual object ResolveUnregistered(Type type)
    {
        Exception innerException = null;
        foreach (var constructor in type.GetConstructors())
        {
            try
            {
                // 尝试解析构造函数参数
                var parameters = constructor.GetParameters().Select(parameter =>
                {
                    var service = Resolve(parameter.ParameterType);
                    if (service == null)
                        throw new DHException("Unknown dependency");
                    return service;
                });

                // 一切都好，所以创建实例
                return Activator.CreateInstance(type, parameters.ToArray());
            }
            catch (Exception ex)
            {
                innerException = ex;
            }
        }

        throw new DHException("No constructor was found that had all the dependencies satisfied.", innerException);
    }

    #endregion

    #region Properties

    /// <summary>
    /// 服务提供商
    /// </summary>
    public virtual IServiceProvider ServiceProvider { get; protected set; }

    #endregion
}
