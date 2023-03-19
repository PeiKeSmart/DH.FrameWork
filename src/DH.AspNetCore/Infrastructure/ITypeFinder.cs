using System.Reflection;

namespace DH.Core.Infrastructure;

/// <summary>
/// 实现此接口的类为DH引擎中的各种服务提供有关类型的信息。
/// </summary>
public interface ITypeFinder
{
    /// <summary>
    /// 查找类型的类
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="onlyConcreteClasses">一个值，指示是否只查找具体类</param>
    /// <returns>Result</returns>
    IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

    /// <summary>
    /// 查找类型的类
    /// </summary>
    /// <param name="assignTypeFrom">分配类型的来源</param>
    /// <param name="onlyConcreteClasses">指示是否仅查找具体类的值</param>
    /// <returns>Result</returns>
    /// <returns></returns>
    IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

    /// <summary>
    /// 查找类型的类别
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="assemblies">装配体</param>
    /// <param name="onlyConcreteClasses">指示是否仅查找具体类的值</param>
    /// <returns>Result</returns>
    IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

    /// <summary>
    /// 查找类型的类别
    /// </summary>
    /// <param name="assignTypeFrom">从分配类型</param>
    /// <param name="assemblies">装配体</param>
    /// <param name="onlyConcreteClasses">指示是否仅查找具体类的值</param>
    /// <returns>Result</returns>
    IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

    /// <summary>
    /// 获取与当前实现相关的程序集。
    /// </summary>
    /// <returns>程序集列表</returns>
    IList<Assembly> GetAssemblies();
}
