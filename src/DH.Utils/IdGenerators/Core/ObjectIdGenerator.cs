using DH.IdGenerators.Abstractions;
using DH.IdGenerators.Ids;

namespace DH.IdGenerators.Core;

/// <summary>
/// ObjectId 生成器
/// </summary>
public class ObjectIdGenerator : IStringGenerator
{
    /// <summary>
    /// 获取<see cref="ObjectIdGenerator"/>类型的实例
    /// </summary>
    public static ObjectIdGenerator Current { get; } = new ObjectIdGenerator();

    /// <summary>
    /// 创建ID
    /// </summary>
    /// <returns></returns>
    public string Create()
    {
        return ObjectId.GenerateNewStringId();
    }
}