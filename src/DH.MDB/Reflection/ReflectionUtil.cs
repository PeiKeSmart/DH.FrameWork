using System.Reflection;

namespace DH.Reflection;

/// <summary>
/// 封装了反射的常用操作方法
/// </summary>
public class ReflectionUtil
{
    /// <summary>
    /// 通过反射创建对象(Activator.CreateInstance)
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Object GetInstance(Type t)
    {
        return Activator.CreateInstance(t);
    }

    /// <summary>
    /// 通过反射创建对象(Activator.CreateInstance)，并提供构造函数
    /// </summary>
    /// <param name="t"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static Object GetInstance(Type t, params object[] args)
    {
        return Activator.CreateInstance(t, args);
    }

    /// <summary>
    /// 创建对象(通过加载指定程序集中的类型)
    /// </summary>
    /// <param name="asmName">不需要后缀名</param>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public static Object GetInstance(String asmName, String typeName)
    {
        // Load不需要ext，LoadFrom需要
        Assembly asm = Assembly.Load(asmName);
        return asm.CreateInstance(typeName);
    }

    /// <summary>
    /// 初始化匿名类型
    /// </summary>
    /// <param name="t">匿名类型的type</param>
    /// <param name="values">参数的值</param>
    /// <returns></returns>
    public static Object GetAnonymousInstance(Type t, Object[] values)
    {
        ConstructorInfo constructor = t.GetConstructors()[0];
        return constructor.Invoke(values);
    }

    public static Object GetInstanceFromProgId(String progId)
    {
        return rft.GetInstance(Type.GetTypeFromProgID(progId));
    }

    //---------------------------------------------------------------------------------------------------------------

    public static Object GetPropertyValue(Object currentObject, String propertyName)
    {

        if (currentObject == null) return null;

        if (strUtil.IsNullOrEmpty(propertyName)) return null;

        PropertyInfo p = currentObject.GetType().GetProperty(propertyName);
        if (p == null) return null;

        return p.GetValue(currentObject, null);
    }

    public static void SetPropertyValue(Object currentObject, String propertyName, Object propertyValue)
    {

        if (currentObject == null)
        {
            throw new NullReferenceException(String.Format("propertyName={0}, propertyValue={1}", propertyName, propertyValue));
        }

        PropertyInfo p = currentObject.GetType().GetProperty(propertyName);
        if (p == null)
        {
            throw new NullReferenceException("property not exist=" + propertyName + ", type=" + currentObject.GetType().FullName);
        }

        try
        {
            p.SetValue(currentObject, propertyValue, null);
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message + " (property=" + propertyName + ", type=" + currentObject.GetType().FullName + ")");
        }
    }

    public static Attribute GetAttribute(MemberInfo memberInfo, Type attributeType)
    {

        object[] customAttributes = memberInfo.GetCustomAttributes(attributeType, false);
        if (customAttributes.Length == 0)
        {
            return null;
        }
        return customAttributes[0] as Attribute;
    }

    public static object[] GetAttributes(MemberInfo memberInfo)
    {
        return memberInfo.GetCustomAttributes(false);
    }

    public static object[] GetAttributes(MemberInfo memberInfo, Type attributeType)
    {
        return memberInfo.GetCustomAttributes(attributeType, false);
    }

    public static Boolean IsBaseType(Type type)
    {
        return type == typeof(int) ||
            type == typeof(String) ||
            type == typeof(DateTime) ||
            type == typeof(bool) ||
            type == typeof(long) ||
            type == typeof(double) ||
            type == typeof(decimal);
    }

    /// <summary>
    /// 判断 t 是否实现了某种接口
    /// </summary>
    /// <param name="t">需要判断的类型</param>
    /// <param name="interfaceType">是否实现的接口</param>
    /// <returns></returns>
    public static Boolean IsInterface(Type t, Type interfaceType)
    {
        if (t.IsInterface) return false;
        return interfaceType.IsAssignableFrom(t);
    }
}
