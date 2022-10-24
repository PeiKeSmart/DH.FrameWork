using XCode;

namespace DH.Core.Caching
{
    /// <summary>
    /// 表示与缓存实体相关的默认值
    /// </summary>
    public static partial class DHEntityCacheDefaults<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// 获取缓存键中使用的实体类型名称
        /// </summary>
        public static string EntityTypeName => typeof(TEntity).Name.ToLowerInvariant();

        /// <summary>
        /// 获取用于按标识符缓存实体的键
        /// </summary>
        /// <remarks>
        /// {0} : 实体id
        /// </remarks>
        public static CacheKey ByIdCacheKey => new($"DH.{EntityTypeName}.byid.{{0}}", ByIdPrefix, Prefix);

        /// <summary>
        /// 获取用于按标识符缓存实体的键
        /// </summary>
        /// <remarks>
        /// {0} : 实体 ids
        /// </remarks>
        public static CacheKey ByIdsCacheKey => new($"DH.{EntityTypeName}.byids.{{0}}", ByIdsPrefix, Prefix);

        /// <summary>
        /// 获取用于缓存所有实体的键
        /// </summary>
        public static CacheKey AllCacheKey => new($"DH.{EntityTypeName}.all.", AllPrefix, Prefix);

        /// <summary>
        /// 获取要清除缓存的键模式
        /// </summary>
        public static string Prefix => $"DH.{EntityTypeName}.";

        /// <summary>
        /// 获取要清除缓存的键模式
        /// </summary>
        public static string ByIdPrefix => $"DH.{EntityTypeName}.byid.";

        /// <summary>
        /// 获取要清除缓存的键模式
        /// </summary>
        public static string ByIdsPrefix => $"DH.{EntityTypeName}.byids.";

        /// <summary>
        /// 获取要清除缓存的键模式
        /// </summary>
        public static string AllPrefix => $"DH.{EntityTypeName}.all.";
    }
}
