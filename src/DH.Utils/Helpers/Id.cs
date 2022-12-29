using DH.IdGenerators;
using DH.IdGenerators.Abstractions;
using DH.IdGenerators.Core;
using DH.IdGenerators.MongoId;

using NewLife.Data;

namespace DH.Helpers;

/// <summary>
/// Id 生成器
/// </summary>
public static class Id
{
    /// <summary>
    /// Id
    /// </summary>
    private static string _id;

    #region 生成器
    /// <summary>
    /// Guid 生成器
    /// </summary>
    public static IGuidGenerator GuidGenerator { get; set; } = SequentialGuidGenerator.Current;

    /// <summary>
    /// Long 生成器
    /// </summary>
    public static ILongGenerator LongGenerator { get; set; } = SnowflakeIdGenerator.Current;

    /// <summary>
    /// String 生成器
    /// </summary>
    public static IStringGenerator StringGenerator { get; set; } = TimestampIdGenerator.Current;
    #endregion

    /// <summary>
    /// 设置Id
    /// </summary>
    /// <param name="id">Id</param>
    public static void SetId(string id) => _id = id;

    /// <summary>
    /// 重置Id
    /// </summary>
    public static void Reset() => _id = null;

    public static readonly Snowflake snowflake = new Snowflake();

    /// <summary>
    /// 创建标识
    /// </summary>
    /// <example>5e9bb8bbdc47398e0c739267</example>
    public static string ObjectId()
    {
        return string.IsNullOrWhiteSpace(_id) ? DH.IdGenerators.Ids.ObjectId.GenerateNewStringId() : _id;
    }

    /// <summary>
    /// 用Guid创建标识,去掉分隔符
    /// </summary>
    /// <example>779608b6fc404fc2aebe0af7f0820f1c</example>
    public static string Guid() => string.IsNullOrWhiteSpace(_id) ? System.Guid.NewGuid().ToString("N") : _id;

    /// <summary>
    /// 获取Guid
    /// </summary>
    /// <example>d927cde3-d960-48d1-9b24-36ad047f5f0a</example>
    public static Guid GetGuid() => string.IsNullOrWhiteSpace(_id) ? System.Guid.NewGuid() : _id.ToGuid();

    /// <summary>
    /// 创建有序 Guid ID
    /// </summary>
    /// <example>3be19121-54fe-5e97-b283-39f4a258df37</example>
    public static Guid SequentialGuid() => GuidGenerator.Create();

    /// <summary>
    /// 创建 Long ID
    /// </summary>
    /// <example>1251699579028639744</example>
    public static long GetLong() => LongGenerator.Create();

    /// <summary>
    /// 创建 String ID
    /// </summary>
    /// <example>15872632243810001</example>
    public static string GetString() => StringGenerator.Create();

    /// <summary>
    /// 获取13位Id字符串
    /// </summary>
    /// <example>0HLV413UK2B7R</example>
    public static string GetIdString() => IDGenerator.Instance.Generate;

    /// <summary>
    /// 获取新的13位Id字符串
    /// </summary>
    /// <example>0HLV413GIHKK5</example>
    /// <returns></returns>
    public static String GetNextId() => CorrelationIdGenerator.GetNextId();

    /// <summary>
    /// 生成sessionid
    /// </summary>
    /// <example>62acfda11f5a4b3c</example>
    public static string GenerateSid()
    {
        long i = 1;
        byte[] byteArray = System.Guid.NewGuid().ToByteArray();
        foreach (byte b in byteArray)
        {
            i *= b + 1;
        }
        return string.Format("{0:x}", i - DateTime.Now.Ticks);
    }

    /// <summary>
    /// 32位压缩短Id
    /// </summary>
    /// <example>5e9bc0e6dc4739d158107f63</example>
    /// <returns></returns>
    public static String GetStringI32()
    {
        return CompresTo.IntToi32(GetIdString().ToLong());
    }

    /// <summary>
    /// 64位压缩短Id
    /// </summary>
    /// <example>5e9bc101dc47398e80a4f34b</example>
    /// <returns></returns>
    public static String GetStringI64()
    {
        return CompresTo.IntToi64(GetIdString().ToLong());
    }

    /// <summary>
    /// 32位压缩长Id
    /// </summary>
    /// <example>12nnnbu7z44zz</example>
    /// <returns></returns>
    public static String GetLongI32()
    {
        return CompresTo.IntToi32(GetLong().ToLong());
    }

    /// <summary>
    /// 32位压缩长Id
    /// </summary>
    /// <example>15u-3Ytzxzz</example>
    /// <returns></returns>
    public static String GetLongI64()
    {
        return CompresTo.IntToi64(GetLong().ToLong());
    }

    /// <summary>
    /// 自定义35进制Id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public static String Get35Base(Int32 Id)
    {
        return ShortUniqueCode.CreateCode(Id);
    }

    /// <summary>
    /// 获取可以解包的ID
    /// </summary>
    /// <returns></returns>
    public static String GetMObjectId()
    {
        return MObjectId.NewId();
    }

    /// <summary>
    /// 获取NewLife的改进雪花算法
    /// </summary>
    /// <returns></returns>
    public static Int64 GetSId()
    {
        return snowflake.NewId();
    }
}