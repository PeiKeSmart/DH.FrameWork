using DH.Core;
using DH.Core.Domain.Localization;
using DH.Core.Http;
using DH.Entity;
using DH.Web.Framework.Globalization;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

using XCode.Membership;

namespace DH.Web.Framework
{
    /// <summary>
    /// 表示web应用程序的工作上下文
    /// </summary>
    public partial class WebWorkContext : IWorkContext
    {
        #region 字段

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LocalizationSettings _localizationSettings;
        private readonly IStoreContext _storeContext;

        private Language _cachedLanguage;

        #endregion

        #region 初始化

        public WebWorkContext(IHttpContextAccessor httpContextAccessor,
            IStoreContext storeContext,
            LocalizationSettings localizationSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _localizationSettings = localizationSettings;
            _storeContext = storeContext;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 设置语言文化cookie
        /// </summary>
        /// <param name="language">Language</param>
        protected virtual void SetLanguageCookie(Language language)
        {
            if (_httpContextAccessor.HttpContext?.Response?.HasStarted ?? true)
                return;

            // 删除当前cookie值
            var cookieName = $"{DHCookieDefaults.Prefix}{DHCookieDefaults.CultureCookie}";
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieName);

            if (string.IsNullOrEmpty(language?.LanguageCulture))
                return;

            // 设置新的cookie值
            var value = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(language.LanguageCulture));
            var options = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, value, options);
        }

        /// <summary>
        /// 从请求中获取语言
        /// </summary>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含找到的语言
        /// </returns>
        protected virtual Language GetLanguageFromRequest()
        {
            var requestCultureFeature = _httpContextAccessor.HttpContext?.Features.Get<IRequestCultureFeature>();
            if (requestCultureFeature is null)
                return null;

            // 我们是否应该通过客户设置检测当前语言
            if (requestCultureFeature.Provider is not DHSeoUrlCultureProvider && !_localizationSettings.AutomaticallyDetectLanguage)
                return null;

            // 获取请求区域性
            if (requestCultureFeature.RequestCulture is null)
                return null;

            // 尝试通过文化名称获取语言
            var requestLanguage = Language.GetAllLanguages().FirstOrDefault(language =>
                language.LanguageCulture.Equals(requestCultureFeature.RequestCulture.Culture.Name, StringComparison.InvariantCultureIgnoreCase));

            // 检查语言可用性
            if (requestLanguage == null || !requestLanguage.Published)
                return null;

            return requestLanguage;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        public virtual UserDetail GetCurrentCustomer()
        {
            var model = ManageProvider.User;

            if (model != null)
            {
                return UserDetail.FindById(model.ID);
            }

            return null;
        }

        /// <summary>
        /// 获取当前用户工作语言
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        public virtual async Task<Language> GetWorkingLanguageAsync()
        {
            // 是否存在缓存值
            if (_cachedLanguage != null)
                return _cachedLanguage;

            var customer = GetCurrentCustomer();
            var store = await _storeContext.GetCurrentStoreAsync();

            // 是否应该从请求中检测语言
            var detectedLanguage = GetLanguageFromRequest();

            // 获取当前保存的语言标识符
            var currentLanguageId = customer.LanguageId;

            // 如果检测到语言，我们需要保存它
            if (detectedLanguage != null)
            {
                // 如果检测到的语言标识符与当前语言标识符不同，则保存该标识符
                if (detectedLanguage.Id != currentLanguageId)
                    SetWorkingLanguage(detectedLanguage);
            }
            else
            {
                var allStoreLanguages = Language.GetAllLanguages();

                // 检查客户语言可用性
                detectedLanguage = allStoreLanguages.FirstOrDefault(language => language.Id == currentLanguageId);

                // 找不到，然后尝试获取当前存储的默认语言（如果指定）
                detectedLanguage ??= allStoreLanguages.FirstOrDefault(language => language.Id == store.DefaultLanguageId);

                // 如果未找到当前存储的默认语言，则尝试获取第一种语言
                detectedLanguage ??= allStoreLanguages.FirstOrDefault();

                // 如果当前站点没有语言，请尝试获取第一种语言，而不考虑站点
                detectedLanguage ??= Language.GetAllLanguages().FirstOrDefault();

                SetLanguageCookie(detectedLanguage);
            }

            // 缓存找到的语言
            _cachedLanguage = detectedLanguage;

            return _cachedLanguage;
        }

        /// <summary>
        /// 设置当前用户工作语言
        /// </summary>
        /// <param name="language">语言</param>
        /// <returns>表示异步操作的任务</returns>
        public virtual void SetWorkingLanguage(Language language)
        {
            // 保存传递的语言标识符
            var customer = GetCurrentCustomer();
            customer.LanguageId = language?.Id ?? 0;
            customer.Update();

            // 设置cookie
            SetLanguageCookie(language);

            // 然后重置缓存的值
            _cachedLanguage = null;
        }

        #endregion
    }
}
