using System.Reflection;

namespace DH.DotNetDetour.Extensions;

public static class TypeExtensions {
    public static T GetCustomAttribute<T>(this MemberInfo @this)
    {
        var list = @this.GetCustomAttributes(typeof(T), true)?.ToList();
        return (T)list.FirstOrDefault();
    }

    public static T GetCustomAttribute<T>(this ParameterInfo @this)
    {
        var list = @this.GetCustomAttributes(typeof(T), true)?.ToList();
        return (T)list.FirstOrDefault();
    }
}