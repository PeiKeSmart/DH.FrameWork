using Autofac;

using Pek.Infrastructure;

namespace DH.Infrastructure.DependencyManagement;

/// <summary>
/// 依赖注册器界面
/// </summary>
public interface IDependencyRegistrar {
    /// <summary>
    /// 注册服务和接口
    /// </summary>
    /// <param name="builder">窗口制造商</param>
    /// <param name="typeFinder">类型查找器</param>
    void Register(ContainerBuilder builder, ITypeFinder typeFinder);

    /// <summary>
    /// 获取此依赖关系注册器实现的顺序
    /// </summary>
    int Order { get; }
}