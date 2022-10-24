using DH.Core;
using DH.Core.Domain.Localization;
using DH.Core.Infrastructure;
using DH.Entity;

using NewLife.Log;

using XCode.Membership;

namespace DH.Services.Localization
{
    /// <summary>
    /// 提供有关本地化的信息
    /// </summary>
    public partial class LocalizationService : ILocalizationService
    {
        #region Fields

        protected readonly LocalizationSettings _localizationSettings;
        protected readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public LocalizationService(LocalizationSettings localizationSettings,
            IWorkContext workContext)
        {
            _localizationSettings = localizationSettings;
            _workContext = workContext;
        }

        #endregion

        #region Utilities



        #endregion

        #region 方法

        /// <summary>
        /// 按语言标识符获取所有区域设置字符串资源
        /// </summary>
        /// <param name="languageId">语言标识符</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含区域设置字符串资源
        /// </returns>
        public virtual Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(int languageId)
        {
            var list = LocaleStringResource.GetAll();

            return ResourceValuesToDictionary(list);
        }

        protected virtual Dictionary<string, KeyValuePair<int, string>> ResourceValuesToDictionary(IEnumerable<LocaleStringResource> locales)
        {
            // 格式: <name, <id, value>>
            var dictionary = new Dictionary<string, KeyValuePair<int, string>>();
            foreach (var locale in locales)
            {
                var resourceName = locale.ResourceName.ToLowerInvariant();
                if (!dictionary.ContainsKey(resourceName))
                    dictionary.Add(resourceName, new KeyValuePair<int, string>(locale.Id, locale.ResourceValue));
            }

            return dictionary;
        }

        /// <summary>
        /// 基于指定的ResourceKey属性获取资源字符串。
        /// </summary>
        /// <param name="resourceKey">表示resourceKey的字符串。</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含表示请求的资源字符串的字符串。
        /// </returns>
        public virtual async Task<string> GetResourceAsync(string resourceKey)
        {
            var workingLanguage = await _workContext.GetWorkingLanguageAsync();

            if (workingLanguage != null)
                return await GetResourceAsync(resourceKey, workingLanguage.Id);

            return string.Empty;
        }

        /// <summary>
        /// 基于指定的ResourceKey属性获取资源字符串。
        /// </summary>
        /// <param name="resourceKey">表示resourceKey的字符串.</param>
        /// <param name="languageId">语言标识符</param>
        /// <param name="logIfNotFound">一个值，指示如果未找到语言环境字符串资源，是否记录错误</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="returnEmptyIfNotFound">一个值，指示如果找不到资源并且默认值设置为空字符串，是否会返回空字符串</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含表示请求的资源字符串的字符串.
        /// </returns>
        public virtual async Task<string> GetResourceAsync(string resourceKey, int languageId,
            bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false)
        {
            var result = string.Empty;
            if (resourceKey == null)
                resourceKey = string.Empty;
            resourceKey = resourceKey.Trim().ToLowerInvariant();
            if (_localizationSettings.LoadAllLocaleRecordsOnStartup)
            {
                // 加载所有记录（我们知道它们是缓存的）
                var resources = GetAllResourceValues(languageId);
                if (resources.ContainsKey(resourceKey))
                {
                    result = resources[resourceKey].Value;
                }
            }
            else
            {
                // 逐步加载
                var lsr = LocaleStringResource.FindByResourceNameAndLanguageId(resourceKey, languageId)?.ResourceValue;

                if (lsr != null)
                    result = lsr;
            }

            if (!string.IsNullOrEmpty(result))
                return result;

            if (logIfNotFound)
            {
                var webHelper = EngineContext.Current.Resolve<IWebHelper>();

                // 获取当前客户
                var currentCustomer = EngineContext.Current.Resolve<IWorkContext>().GetCurrentCustomer();

                var msg = $"Resource string ({resourceKey}) is not found. Language ID = {languageId}";
                XTrace.Log.Warn(msg);
                LogProvider.Provider?.WriteLog("系统", "警告", false, msg, currentCustomer.User.ID, currentCustomer.User.Name, webHelper.GetCurrentIpAddress());
            }

            if (!string.IsNullOrEmpty(defaultValue))
            {
                result = defaultValue;
            }
            else
            {
                if (!returnEmptyIfNotFound)
                    result = resourceKey;
            }

            return result;
        }

        #endregion
    }
}
