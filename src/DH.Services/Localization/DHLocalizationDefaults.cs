using DH.Core.Caching;
using DH.Entity;

namespace DH.Services.Localization
{
    /// <summary>
    /// 表示与本地化服务相关的默认值
    /// </summary>
    public static partial class DHLocalizationDefaults
    {
        #region Locales

        /// <summary>
        /// 获取管理区域的区域设置资源的前缀
        /// </summary>
        public static string AdminLocaleStringResourcesPrefix => "Admin.";

        /// <summary>
        /// 获取枚举的区域设置资源的前缀
        /// </summary>
        public static string EnumLocaleStringResourcesPrefix => "Enums.";

        /// <summary>
        /// 获取权限的区域设置资源前缀
        /// </summary>
        public static string PermissionLocaleStringResourcesPrefix => "Permission.";

        /// <summary>
        /// 获取插件友好名称的区域设置资源前缀
        /// </summary>
        public static string PluginNameLocaleStringResourcesPrefix => "Plugins.FriendlyName.";

        #endregion

        #region Caching defaults

        #region Languages

        /// <summary>
        /// 获取用于缓存的密钥
        /// </summary>
        /// <remarks>
        /// {0} : 存储ID
        /// {1} : 是否显示隐藏记录？
        /// </remarks>
        public static CacheKey LanguagesAllCacheKey => new("DH.language.all.{0}-{1}", LanguagesByStorePrefix, DHEntityCacheDefaults<Language>.AllPrefix);

        /// <summary>
        /// 获取要清除缓存的键模式
        /// </summary>
        /// <remarks>
        /// {0} : 站点ID
        /// </remarks>
        public static string LanguagesByStorePrefix => "DH.language.all.{0}";

        #endregion

        #region Locales

        /// <summary>
        /// 获取用于缓存的键
        /// </summary>
        /// <remarks>
        /// {0} : 语言ID
        /// </remarks>
        public static CacheKey LocaleStringResourcesAllPublicCacheKey => new("DH.localestringresource.bylanguage.public.{0}", DHEntityCacheDefaults<LocaleStringResource>.Prefix);

        /// <summary>
        /// 获取用于缓存的键
        /// </summary>
        /// <remarks>
        /// {0} : 语言ID
        /// </remarks>
        public static CacheKey LocaleStringResourcesAllAdminCacheKey => new("DH.localestringresource.bylanguage.admin.{0}", DHEntityCacheDefaults<LocaleStringResource>.Prefix);

        /// <summary>
        /// 获取用于缓存的键
        /// </summary>
        /// <remarks>
        /// {0} : 语言ID
        /// </remarks>
        public static CacheKey LocaleStringResourcesAllCacheKey => new("DH.localestringresource.bylanguage.{0}", DHEntityCacheDefaults<LocaleStringResource>.Prefix);

        /// <summary>
        /// 获取用于缓存的键
        /// </summary>
        /// <remarks>
        /// {0} : 语言ID
        /// {1} : 资源键
        /// </remarks>
        public static CacheKey LocaleStringResourcesByNameCacheKey => new("DH.localestringresource.byname.{0}-{1}", LocaleStringResourcesByNamePrefix, DHEntityCacheDefaults<LocaleStringResource>.Prefix);

        /// <summary>
        /// 获取要清除缓存的键模式
        /// </summary>
        /// <remarks>
        /// {0} : 语言ID
        /// </remarks>
        public static string LocaleStringResourcesByNamePrefix => "DH.localestringresource.byname.{0}";

        #endregion

        #region Localized properties

        /// <summary>
        /// 获取用于缓存的键
        /// </summary>
        /// <remarks>
        /// {0} : 语言ID
        /// {1} : 实体ID
        /// {2} : 区域设置键组
        /// {3} : 区域设置键
        /// </remarks>
        public static CacheKey LocalizedPropertyCacheKey => new("DH.localizedproperty.value.{0}-{1}-{2}-{3}");

        #endregion

        #endregion
    }
}
