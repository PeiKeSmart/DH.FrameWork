using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace DH.IdGenerators.MongoId;

/// <summary>
/// 参考MongoDB的ObjectId生成，参考https://mp.weixin.qq.com/s/ooquxazWRcFOqPj9XjF93Q
/// 生产ObjectId
/// Oid 的数据结构主要由四个部分组成，分别是：Unix时间戳、机器名称、进程编号、自增编号。Oid 实际上是总长度为12个字节24的字符串，易记口诀为：4323，时间4字节，机器名3字节，进程编号2字节，自增编号3字节。
/// </summary>
public class ObjectIdFactory
{
    private Int32 increment;
    private readonly Byte[] pidHex;
    private readonly Byte[] machineHash;
    private readonly UTF8Encoding utf8 = new UTF8Encoding(false);
    private readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    private Int32 GetIncrement() => System.Threading.Interlocked.Increment(ref increment);
    private Int32 GetTimestamp() => Convert.ToInt32(Math.Floor((DateTime.UtcNow - unixEpoch).TotalSeconds));

    public ObjectIdFactory()
    {
        MD5 md5 = MD5.Create();
        machineHash = md5.ComputeHash(utf8.GetBytes(Dns.GetHostName()));
        pidHex = BitConverter.GetBytes(Process.GetCurrentProcess().Id);
        Array.Reverse(pidHex);  // 反转
    }

    /// <summary>
    /// 产品一个新的24位唯一编号
    /// </summary>
    /// <returns></returns>
    public MObjectId NewId()
    {
        var copyIdx = 0;
        var hex = new Byte[12];
        var time = BitConverter.GetBytes(GetTimestamp());
        Array.Reverse(time);
        Array.Copy(time, 0, hex, copyIdx, 4);
        copyIdx += 4;

        Array.Copy(machineHash, 0, hex, copyIdx, 3);
        copyIdx += 3;

        Array.Copy(pidHex, 2, hex, copyIdx, 2);
        copyIdx += 2;

        var inc = BitConverter.GetBytes(GetIncrement());
        Array.Reverse(inc);
        Array.Copy(inc, 1, hex, copyIdx, 3);

        return new MObjectId(hex);
    }
}