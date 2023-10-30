namespace EasyNotice.Helpers;

public class DateTimeHelper {
    public static long GetTimestamp => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}