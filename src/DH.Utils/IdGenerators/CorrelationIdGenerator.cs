namespace DH.IdGenerators;

public static class CorrelationIdGenerator
{
    /// <summary>
    /// Base32编码-以ascii排序顺序进行基于文本的轻松排序
    /// </summary>
    private static readonly Char[] s_encode32Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUV".ToCharArray();

    /// <summary>
    /// 为此应用程序实例的_lastConnectionId种子
    /// 自0001年1月1日午夜12:00:00以来经过的100纳秒间隔数
    /// 通过重新启动来大致增加_lastId
    /// </summary>
    private static Int64 _lastId = DateTime.UtcNow.Ticks;

    public static String GetNextId() => GenerateId(Interlocked.Increment(ref _lastId));

    private static String GenerateId(Int64 id)
    {
        return String.Create(13, id, (buffer, value) =>
        {
            var encode32Chars = s_encode32Chars;

            buffer[12] = encode32Chars[value & 31];
            buffer[11] = encode32Chars[(value >> 5) & 31];
            buffer[10] = encode32Chars[(value >> 10) & 31];
            buffer[9] = encode32Chars[(value >> 15) & 31];
            buffer[8] = encode32Chars[(value >> 20) & 31];
            buffer[7] = encode32Chars[(value >> 25) & 31];
            buffer[6] = encode32Chars[(value >> 30) & 31];
            buffer[5] = encode32Chars[(value >> 35) & 31];
            buffer[4] = encode32Chars[(value >> 40) & 31];
            buffer[3] = encode32Chars[(value >> 45) & 31];
            buffer[2] = encode32Chars[(value >> 50) & 31];
            buffer[1] = encode32Chars[(value >> 55) & 31];
            buffer[0] = encode32Chars[(value >> 60) & 31];
        });
    }
}