namespace DH.Timing;

/// <summary>
/// Unix时间操作
/// </summary>
public static class UnixTime
{
    /// <summary>
    /// Unix纪元时间
    /// </summary>
    public static DateTime EpochTime = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// 转换为Unix时间戳
    /// </summary>
    /// <param name="isContainMillisecond">是否包含毫秒</param>
    /// <returns></returns>
    public static long ToTimestamp(bool isContainMillisecond = true)
    {
        return ToTimestamp(DateTime.Now, isContainMillisecond);
    }

    /// <summary>
    /// 转换为Unix时间戳
    /// </summary>
    /// <param name="dateTime">时间</param>
    /// <param name="isContainMillisecond">是否包含毫秒</param>
    /// <returns></returns>
    public static long ToTimestamp(DateTime dateTime, bool isContainMillisecond = true)
    {
        return dateTime.Kind == DateTimeKind.Utc
            ? System.Convert.ToInt64((dateTime - EpochTime).TotalMilliseconds / (isContainMillisecond ? 1 : 1000))
            : System.Convert.ToInt64((TimeZoneInfo.ConvertTimeToUtc(dateTime) - EpochTime).TotalMilliseconds /
                              (isContainMillisecond ? 1 : 1000));
    }

    /// <summary>
    /// 转换为DateTime对象
    /// </summary>
    /// <param name="timestamp">时间戳。</param>
    /// <param name="isContainMillisecond">是否包含毫秒</param>
    /// <returns></returns>
    public static DateTime ToDateTime(long timestamp, bool isContainMillisecond = true)
    {
        if (isContainMillisecond)
            return EpochTime.AddMilliseconds(timestamp).ToLocalTime();

        return EpochTime.AddSeconds(timestamp).ToLocalTime();
    }

    /// <summary>
    /// 转换为Utc DateTime时区为0的时间
    /// </summary>
    /// <param name="timestamp">时间戳。毫秒</param>
    /// <returns></returns>
    public static DateTimeOffset ToUtcDateTime(Int64 timestamp)
    {
        DateTimeOffset utcTime = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
        DateTimeOffset zeroOffsetTime = utcTime.ToOffset(TimeSpan.Zero);

        return zeroOffsetTime;
    }

    /// <summary>
    /// 当前时间转换为Utc DateTime时区为0的时间
    /// </summary>
    /// <returns></returns>
    public static DateTimeOffset ToUtcZeroDateTime()
    {
        // 获取当前时间
        DateTimeOffset localTime = DateTimeOffset.Now;

        // 获取时区为0的时间
        DateTimeOffset utcTime = TimeZoneInfo.ConvertTime(localTime, TimeZoneInfo.Utc);

        return utcTime;
    }

    /// <summary>
    /// 指定时间转换为Utc DateTime时区为0的时间
    /// </summary>
    /// <param name="dateTime">带时区的UTC时间</param>
    /// <returns></returns>
    public static DateTimeOffset ToUtcZeroDateTime(DateTimeOffset dateTime)
    {
        // 获取时区为0的时间
        DateTimeOffset utcTime = TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Utc);

        return utcTime;
    }

    /// <summary>
    /// 指定Utc DateTime时区为0的时间转化为本地时间
    /// </summary>
    /// <param name="dateTime">带时区的UTC时间</param>
    /// <returns></returns>
    public static DateTimeOffset ToUtcZeroDateTime(DateTime dateTime)
    {
        DateTimeOffset inputTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);  // 时区为0的时间

        DateTimeOffset utcTime = inputTime.ToUniversalTime().ToLocalTime();

        return utcTime;
    }
}