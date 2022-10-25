using DH.Core.Caching;
using DH.Entity;

namespace DH.Services.Configuration
{
    /// <summary>
    /// 表示与设置相关的默认值
    /// </summary>
    public static partial class DHSettingsDefaults
    {
        #region Caching defaults

        /// <summary>
        /// 获取用于缓存的键
        /// </summary>
        public static CacheKey SettingsAllAsDictionaryCacheKey => new("dh.setting.all.dictionary.", DHEntityCacheDefaults<Setting>.Prefix);

        #endregion
    }
}
