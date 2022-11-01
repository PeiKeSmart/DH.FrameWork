using DH;
using JetBrains.Annotations;

namespace System;

public static class DHTypeExtensions
{
    public static string GetFullNameWithAssemblyName(this Type type)
    {
        return type.FullName + ", " + type.Assembly.GetName().Name;
    }

    /// <summary>
    /// 确定是否可以将此类型的实例分配给
    /// <typeparamref name="TTarget"></typeparamref>的实例。
    ///
    /// 内部使用<see cref="Type.IsAssignableFrom"/>.
    /// </summary>
    /// <typeparam name="TTarget">目标类型</typeparam> (相反).
    public static bool IsAssignableTo<TTarget>([NotNull] this Type type)
    {
        Check.NotNull(type, nameof(type));

        return type.IsAssignableTo(typeof(TTarget));
    }

    /// <summary>
    /// 确定是否可以将此类型的实例分配给
    /// <paramref name="targetType"></paramref>的实例。
    ///
    /// 内部使用<see cref="Type.IsAssignableFrom"/> (相反).
    /// </summary>
    /// <param name="type">此类型</param>
    /// <param name="targetType">目标类型</param>
    public static bool IsAssignableTo([NotNull] this Type type, [NotNull] Type targetType)
    {
        Check.NotNull(type, nameof(type));
        Check.NotNull(targetType, nameof(targetType));

        return targetType.IsAssignableFrom(type);
    }

    /// <summary>
    /// 获取此类型的所有基类。
    /// </summary>
    /// <param name="type">获取其基类的类型。</param>
    /// <param name="includeObject">True，以在返回的数组中包含标准<see cref="object"/>类型。</param>
    public static Type[] GetBaseClasses([NotNull] this Type type, bool includeObject = true)
    {
        Check.NotNull(type, nameof(type));

        var types = new List<Type>();
        AddTypeAndBaseTypesRecursively(types, type.BaseType, includeObject);
        return types.ToArray();
    }

    /// <summary>
    /// 获取此类型的所有基类。
    /// </summary>
    /// <param name="type">获取其基类的类型。</param>
    /// <param name="stoppingType">一种停止转到更深层基类的类型。此类型将包含在返回的数组中</param>
    /// <param name="includeObject">True，以在返回的数组中包含标准<see cref="object"/>类型。</param>
    public static Type[] GetBaseClasses([NotNull] this Type type, Type stoppingType, bool includeObject = true)
    {
        Check.NotNull(type, nameof(type));

        var types = new List<Type>();
        AddTypeAndBaseTypesRecursively(types, type.BaseType, includeObject, stoppingType);
        return types.ToArray();
    }

    private static void AddTypeAndBaseTypesRecursively(
        [NotNull] List<Type> types,
        [CanBeNull] Type type,
        bool includeObject,
        [CanBeNull] Type stoppingType = null)
    {
        if (type == null || type == stoppingType)
        {
            return;
        }

        if (!includeObject && type == typeof(object))
        {
            return;
        }

        AddTypeAndBaseTypesRecursively(types, type.BaseType, includeObject, stoppingType);
        types.Add(type);
    }

}
