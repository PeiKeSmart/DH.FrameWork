using JetBrains.Annotations;

using NewLife;

using System.Diagnostics;

namespace DH;

[DebuggerStepThrough]
public static class Check
{
    [ContractAnnotation("value:null => halt")]
    public static T NotNull<T>(
        T value,
        [InvokerParameterName][NotNull] string parameterName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName);
        }

        return value;
    }

    [ContractAnnotation("value:null => halt")]
    public static T NotNull<T>(
        T value,
        [InvokerParameterName][NotNull] string parameterName,
        string message)
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName, message);
        }

        return value;
    }

    [ContractAnnotation("value:null => halt")]
    public static string NotNull(
        string value,
        [InvokerParameterName][NotNull] string parameterName,
        int maxLength = int.MaxValue,
        int minLength = 0)
    {
        if (value == null)
        {
            throw new ArgumentException($"{parameterName}不能为空!", parameterName);
        }

        if (value.Length > maxLength)
        {
            throw new ArgumentException($"{parameterName}长度必须等于或小于{maxLength}!", parameterName);
        }

        if (minLength > 0 && value.Length < minLength)
        {
            throw new ArgumentException($"{parameterName}长度必须等于或大于{minLength}!", parameterName);
        }

        return value;
    }

    [ContractAnnotation("value:null => halt")]
    public static string NotNullOrWhiteSpace(
        string value,
        [InvokerParameterName][NotNull] string parameterName,
        int maxLength = int.MaxValue,
        int minLength = 0)
    {
        if (value.IsNullOrWhiteSpace())
        {
            throw new ArgumentException($"{parameterName}不能为空或空白!", parameterName);
        }

        if (value.Length > maxLength)
        {
            throw new ArgumentException($"{parameterName}长度必须等于或小于{maxLength}!", parameterName);
        }

        if (minLength > 0 && value.Length < minLength)
        {
            throw new ArgumentException($"{parameterName}长度必须等于或大于{minLength}!", parameterName);
        }

        return value;
    }

    [ContractAnnotation("value:null => halt")]
    public static string NotNullOrEmpty(
        string value,
        [InvokerParameterName][NotNull] string parameterName,
        int maxLength = int.MaxValue,
        int minLength = 0)
    {
        if (value.IsNullOrEmpty())
        {
            throw new ArgumentException($"{parameterName}不能为null或空!", parameterName);
        }

        if (value.Length > maxLength)
        {
            throw new ArgumentException($"{parameterName}长度必须等于或小于{maxLength}!", parameterName);
        }

        if (minLength > 0 && value.Length < minLength)
        {
            throw new ArgumentException($"{parameterName}长度必须等于或大于{minLength}!", parameterName);
        }

        return value;
    }

    [ContractAnnotation("value:null => halt")]
    public static ICollection<T> NotNullOrEmpty<T>(ICollection<T> value, [InvokerParameterName][NotNull] string parameterName)
    {
        if (value.IsNullOrEmpty())
        {
            throw new ArgumentException(parameterName + "不能为null或空!", parameterName);
        }

        return value;
    }

    [ContractAnnotation("type:null => halt")]
    public static Type AssignableTo<TBaseType>(
        Type type,
        [InvokerParameterName][NotNull] string parameterName)
    {
        NotNull(type, parameterName);

        if (!type.IsAssignableTo<TBaseType>())
        {
            throw new ArgumentException($"{parameterName} (type of {type.AssemblyQualifiedName})应分配给{typeof(TBaseType).GetFullNameWithAssemblyName()}!");
        }

        return type;
    }

    public static string Length(
        [CanBeNull] string value,
        [InvokerParameterName][NotNull] string parameterName,
        int maxLength,
        int minLength = 0)
    {
        if (minLength > 0)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(parameterName + "不能为null或空!", parameterName);
            }

            if (value.Length < minLength)
            {
                throw new ArgumentException($"{parameterName}长度必须等于或大于{minLength}!", parameterName);
            }
        }

        if (value != null && value.Length > maxLength)
        {
            throw new ArgumentException($"{parameterName}长度必须等于或小于{maxLength}!", parameterName);
        }

        return value;
    }

    public static Int16 Positive(
        Int16 value,
        [InvokerParameterName][NotNull] string parameterName)
    {
        if (value == 0)
        {
            throw new ArgumentException($"{parameterName}等于零");
        }
        else if (value < 0)
        {
            throw new ArgumentException($"{parameterName}小于零");
        }
        return value;
    }

    public static Int32 Positive(
        Int32 value,
        [InvokerParameterName][NotNull] string parameterName)
    {
        if (value == 0)
        {
            throw new ArgumentException($"{parameterName}等于零");
        }
        else if (value < 0)
        {
            throw new ArgumentException($"{parameterName}小于零");
        }
        return value;
    }

    public static Int64 Positive(
        Int64 value,
        [InvokerParameterName][NotNull] string parameterName)
    {
        if (value == 0)
        {
            throw new ArgumentException($"{parameterName}等于零");
        }
        else if (value < 0)
        {
            throw new ArgumentException($"{parameterName}小于零");
        }
        return value;
    }

    public static float Positive(
        float value,
        [InvokerParameterName][NotNull] string parameterName)
    {
        if (value == 0)
        {
            throw new ArgumentException($"{parameterName}等于零");
        }
        else if (value < 0)
        {
            throw new ArgumentException($"{parameterName}小于零");
        }
        return value;
    }

    public static double Positive(
        double value,
        [InvokerParameterName][NotNull] string parameterName)
    {
        if (value == 0)
        {
            throw new ArgumentException($"{parameterName}等于零");
        }
        else if (value < 0)
        {
            throw new ArgumentException($"{parameterName}小于零");
        }
        return value;
    }

    public static decimal Positive(
        decimal value,
        [InvokerParameterName][NotNull] string parameterName)
    {
        if (value == 0)
        {
            throw new ArgumentException($"{parameterName}等于零");
        }
        else if (value < 0)
        {
            throw new ArgumentException($"{parameterName}小于零");
        }
        return value;
    }

    public static Int16 Range(
        Int16 value,
        [InvokerParameterName][NotNull] string parameterName,
        Int16 minimumValue,
        Int16 maximumValue = Int16.MaxValue)
    {
        if (value < minimumValue || value > maximumValue)
        {
            throw new ArgumentException($"{parameterName}超出范围最小值：{minimumValue}-最大值：{maximumValue}");
        }

        return value;
    }
    public static Int32 Range(
        Int32 value,
        [InvokerParameterName][NotNull] string parameterName,
        Int32 minimumValue,
        Int32 maximumValue = Int32.MaxValue)
    {
        if (value < minimumValue || value > maximumValue)
        {
            throw new ArgumentException($"{parameterName}超出范围最小值：{minimumValue}-最大值：{maximumValue}");
        }

        return value;
    }

    public static Int64 Range(
        Int64 value,
        [InvokerParameterName][NotNull] string parameterName,
        Int64 minimumValue,
        Int64 maximumValue = Int64.MaxValue)
    {
        if (value < minimumValue || value > maximumValue)
        {
            throw new ArgumentException($"{parameterName}超出范围最小值：{minimumValue}-最大值：{maximumValue}");
        }

        return value;
    }


    public static float Range(
        float value,
        [InvokerParameterName][NotNull] string parameterName,
        float minimumValue,
        float maximumValue = float.MaxValue)
    {
        if (value < minimumValue || value > maximumValue)
        {
            throw new ArgumentException($"{parameterName}超出范围最小值：{minimumValue}-最大值：{maximumValue}");
        }
        return value;
    }


    public static double Range(
        double value,
        [InvokerParameterName][NotNull] string parameterName,
        double minimumValue,
        double maximumValue = double.MaxValue)
    {
        if (value < minimumValue || value > maximumValue)
        {
            throw new ArgumentException($"{parameterName}超出范围最小值：{minimumValue}-最大值：{maximumValue}");
        }

        return value;
    }


    public static decimal Range(
        decimal value,
        [InvokerParameterName][NotNull] string parameterName,
        decimal minimumValue,
        decimal maximumValue = decimal.MaxValue)
    {
        if (value < minimumValue || value > maximumValue)
        {
            throw new ArgumentException($"{parameterName}超出范围最小值：{minimumValue}-最大值：{maximumValue}");
        }

        return value;
    }

    public static T NotDefaultOrNull<T>(
        T? value,
        [InvokerParameterName][NotNull] string parameterName)
        where T : struct
    {
        if (value == null)
        {
            throw new ArgumentException($"{parameterName}空值!", parameterName);
        }

        if (value.Value.Equals(default(T)))
        {
            throw new ArgumentException($"{parameterName}具有默认值!", parameterName);
        }

        return value.Value;
    }
}
