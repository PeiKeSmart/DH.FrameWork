using NewLife;

namespace DH.AspNetCore.MVC;

internal class ViewHelper {
    /// <summary>格式化数据用于显示</summary>
    /// <param name="itemType"></param>
    /// <param name="value"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public static String FormatValue(String itemType, Object value, String description = null)
    {
        if (value == null) return null;
        if (itemType.IsNullOrEmpty()) return value + "";

        // TimeSpan格式化字符串
        if (itemType.StartsWithIgnoreCase("TimeSpan"))
        {
            var ts = TimeSpan.FromSeconds(value.ToInt());
            if (!description.IsNullOrEmpty())
            {
                if (description.Contains("毫秒") || description.Contains("ms"))
                {
                    ts = TimeSpan.FromMilliseconds(value.ToInt());
                }
                else if (description.Contains("秒"))
                {
                    //ts = TimeSpan.FromSeconds(value.ToInt());
                }
                else if (description.Contains("分"))
                {
                    ts = TimeSpan.FromMinutes(value.ToInt());
                }
                else if (description.Contains("小时"))
                {
                    ts = TimeSpan.FromHours(value.ToInt());
                }
            }

            if (itemType.StartsWithIgnoreCase("TimeSpan:"))
            {
                var format = itemType.Substring("TimeSpan:".Length);
                if (!format.IsNullOrEmpty()) return ts.ToString(format);
            }

            return ts.ToString();
        }

        // Time格式化字符串
        if (itemType.StartsWithIgnoreCase("Time"))
        {
            var time = value.ToLong().ToDateTime().ToLocalTime();
            if (time.Year <= 1) return "";

            if (itemType.StartsWithIgnoreCase("Time:"))
            {
                var format = itemType.Substring("Time:".Length);
                if (!format.IsNullOrEmpty()) return time.ToString(format);
            }

            return time.ToFullString("");
        }

        if (itemType.EqualIgnoreCase("GMK"))
            return value.ToLong().ToGMK().ToString();

        if (itemType.EqualIgnoreCase("file", "image")) return $"<a href=\"/Cube/Download?id={value}\">{value}</a>";

        return value + "";
    }
}
