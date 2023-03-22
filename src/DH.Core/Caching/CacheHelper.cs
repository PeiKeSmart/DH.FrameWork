using DH.Core;
using DH.Core.Caching;
using DH.Core.Configuration;
using DH.Core.Infrastructure;

using System.Globalization;
using System.Text;

using XCode;

namespace DH.Caching;

public class CacheHelper {
    /// <summary>
    /// 获取用于创建需要缓存的标识符的哈希值的算法
    /// </summary>
    private static string HashAlgorithm => "SHA1";

    /// <summary>
    /// 创建传递的标识符的哈希值
    /// </summary>
    /// <param name="ids">标识符集合</param>
    /// <returns>字符串哈希值</returns>
    protected static string CreateIdsHash(IEnumerable<int> ids)
    {
        var identifiers = ids.ToList();

        if (!identifiers.Any())
            return string.Empty;

        var identifiersString = string.Join(", ", identifiers.OrderBy(id => id));
        return HashHelper.CreateHash(Encoding.UTF8.GetBytes(identifiersString), HashAlgorithm);
    }

    /// <summary>
    /// 将对象转换为缓存参数
    /// </summary>
    /// <param name="parameter">要转换的对象</param>
    /// <returns>缓存参数</returns>
    protected static object CreateCacheKeyParameters(object parameter)
    {
        return parameter switch
        {
            null => "null",
            IEnumerable<int> ids => CreateIdsHash(ids),
            IEnumerable<EntityBase> entities => CreateIdsHash(entities.Select(entity => entity["Id"].ToInt())),
            EntityBase entity => entity["Id"],
            decimal param => param.ToString(CultureInfo.InvariantCulture),
            _ => parameter
        };
    }

    /// <summary>
    /// 使用短缓存时间创建缓存密钥的副本，并通过传递的参数填充
    /// </summary>
    /// <param name="cacheKey">初始缓存密钥</param>
    /// <param name="cacheKeyParameters">用于创建缓存密钥的参数</param>
    /// <returns>缓存密钥</returns>
    public static CacheKey PrepareKeyForShortTermCache(CacheKey cacheKey, params object[] cacheKeyParameters)
    {
        var key = cacheKey.Create(CreateCacheKeyParameters, cacheKeyParameters);

        var _appSettings = EngineContext.Current.Resolve<AppSettings>();

        key.CacheTime = _appSettings.Get<CacheConfig>().ShortTermCacheTime;

        return key;
    }

}
