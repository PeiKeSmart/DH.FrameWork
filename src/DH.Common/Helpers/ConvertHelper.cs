using System.Globalization;

using Pek.Helpers;
using Pek.Timing;

namespace DH.Helpers;

public class ConvertHelper {
    public static T ToType<T>(String value)
    {
        Object obj = default(T);
        T result;
        if (String.IsNullOrEmpty(value))
        {
            result = (T)((Object)obj);
        }
        else
        {
            obj = ToType(value, typeof(T));
            result = (T)((Object)obj);
        }
        return result;
    }

    public static T ToType<T>(String value, T defaultValue)
    {
        T result;
        if (String.IsNullOrEmpty(value))
        {
            result = defaultValue;
        }
        else
        {
            try
            {
                result = ToType<T>(value);
            }
            catch (Exception)
            {
                result = defaultValue;
            }
        }
        return result;
    }

    private static Object ToType(String value, Type conversionType)
    {
        Object result;
        if (conversionType == typeof(String))
        {
            result = value;
        }
        else if (conversionType == typeof(int))
        {
            result = ((value == null) ? 0 : int.Parse(value, NumberStyles.Any));
        }
        else if (conversionType == typeof(bool))
        {
            result = value.ToDGBool();
        }
        else if (conversionType == typeof(float))
        {
            result = ((value == null) ? 0f : float.Parse(value, NumberStyles.Any));
        }
        else if (conversionType == typeof(double))
        {
            result = ((value == null) ? 0.0 : double.Parse(value, NumberStyles.Any));
        }
        else if (conversionType == typeof(decimal))
        {
            result = ((value == null) ? 0m : decimal.Parse(value, NumberStyles.Any));
        }
        else if (conversionType == typeof(DateTime))
        {
            result = ((value == null) ? DateTimeUtil.MinValue : DateTime.Parse(value, CultureInfo.CurrentCulture, DateTimeStyles.None));
        }
        else if (conversionType == typeof(char))
        {
            result = Convert.ToChar(value);
        }
        else if (conversionType == typeof(sbyte))
        {
            result = sbyte.Parse(value, NumberStyles.Any);
        }
        else if (conversionType == typeof(byte))
        {
            result = byte.Parse(value, NumberStyles.Any);
        }
        else if (conversionType == typeof(short))
        {
            result = (int)((value == null) ? 0 : short.Parse(value));
        }
        else if (conversionType == typeof(ushort))
        {
            result = (int)((value == null) ? 0 : ushort.Parse(value, NumberStyles.Any));
        }
        else if (conversionType == typeof(uint))
        {
            result = ((value == null) ? 0U : uint.Parse(value, NumberStyles.Any));
        }
        else if (conversionType == typeof(long))
        {
            result = ((value == null) ? 0L : long.Parse(value, NumberStyles.Any));
        }
        else if (conversionType == typeof(ulong))
        {
            result = ((value == null) ? 0UL : ulong.Parse(value, NumberStyles.Any));
        }
        else if (conversionType == typeof(Guid))
        {
            result = ((value == null) ? Guid.Empty : new Guid(value));
        }
        else
        {
            result = null;
        }
        return result;
    }

}
