using DH.Core;
using DH.Core.Domain.Localization;
using DH.Core.Infrastructure;
using DH.Entity;
using DH.Services.Helpers;
using DH.Services.Localization;
using DH.Services.ScheduleTasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

using NewLife;

using Pek.Cookies;
using Pek.Http;

using XCode.Membership;

namespace DH.Services.Services;

/// <summary>
/// 表示web应用程序的工作上下文
/// </summary>
public partial class WebWorkContext : IWorkContext {
    #region 字段

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStoreContext _storeContext;
    private readonly IUserAgentHelper _userAgentHelper;
    private readonly ICookie _cookie;

    private Language _cachedLanguage;

    #endregion

    #region 初始化

    public WebWorkContext(IHttpContextAccessor httpContextAccessor,
        IUserAgentHelper userAgentHelper,
        ICookie cookie)
    {
        _httpContextAccessor = httpContextAccessor;
        _storeContext = EngineContext.Current.Resolve<IStoreContext>();
        _userAgentHelper = userAgentHelper;
        _cookie = cookie;
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
        if (requestCultureFeature.Provider is not DHSeoUrlCultureProvider && !LocalizationSettings.Current.AutomaticallyDetectLanguage)
            return null;

        // 获取请求区域性
        if (requestCultureFeature.RequestCulture is null)
            return null;

        // 尝试通过文化名称获取语言
        var requestLanguage = Language.GetAllLanguages().FirstOrDefault(language =>
            language.LanguageCulture.Equals(requestCultureFeature.RequestCulture.Culture.Name, StringComparison.InvariantCultureIgnoreCase));

        // 检查语言可用性
        if (requestLanguage == null || !requestLanguage.Status)
            return null;

        return requestLanguage;
    }

    #endregion

    #region 属性

    /// <summary>
    /// 获取当前用户
    /// </summary>
    /// <returns>表示异步操作的任务</returns>
    public virtual UserDetail CurrentCustomer
    {
        get
        {
            var model = ManageProvider.User;

            if (model != null)
            {
                return UserDetail.FindById(model.ID);
            }

            // 检查请求是否由后台（计划）任务发出
            if (_httpContextAccessor.HttpContext?.Request
                ?.Path.Equals(new PathString($"/{DHTaskDefaults.ScheduleTaskPath}"), StringComparison.InvariantCultureIgnoreCase)
                ?? true)
            {
                // 在这种情况下，返回后台任务的内置客户记录
            }

            if (model == null)
            {
                // 检查请求是否由搜索引擎发出，在这种情况下，返回搜索引擎的内置客户记录
                if (_userAgentHelper.IsSearchEngine())
                {

                }
            }

            return new UserDetail() { Id = -1 };
        }
    }

    /// <summary>
    /// 获取当前用户工作语言
    /// </summary>
    /// <returns>表示异步操作的任务</returns>
    public virtual Language WorkingLanguage
    {
        get
        {
            // 是否存在缓存值
            if (_cachedLanguage != null)
                return _cachedLanguage;

            var customer = CurrentCustomer;
            var store = _storeContext.CurrentStore;

            Language detectedLanguage = null;

            // 尝试从请求的页面URL获取语言
            if (LocalizationSettings.Current.SeoFriendlyUrlsForLanguagesEnabled)
            {
                var (isLocalized, language) = _httpContextAccessor.HttpContext.Request.Path.Value.IsLocalizedUrlAsync(_httpContextAccessor.HttpContext.Request.PathBase, false);
                if (isLocalized && language != null)
                {
                    detectedLanguage = language;
                }
            }

            // 从Cookie、Query等中获取
            if (detectedLanguage == null)
            {
                var lang = String.Empty;

                if (_httpContextAccessor.HttpContext!.Request.Query.ContainsKey(DHSetting.Current.LangName))
                    lang = _httpContextAccessor.HttpContext.Request.Query[DHSetting.Current.LangName].ToString();
                else if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(DHSetting.Current.LangName))
                    lang = _httpContextAccessor.HttpContext.Request.Cookies[DHSetting.Current.LangName];

                if (!lang.IsNullOrWhiteSpace())
                {
                    detectedLanguage = Language.FindAllWithCache().Where(e => e.Status).FirstOrDefault(language => language.UniqueSeoCode.Equals(lang, StringComparison.InvariantCultureIgnoreCase));
                }
            }

            // 是否应该从请求中检测语言
            if (detectedLanguage == null)
            {
                detectedLanguage = GetLanguageFromRequest();
            }

            // 获取当前保存的语言标识符
            var currentLanguageId = customer?.LanguageId;
            if (currentLanguageId == 0)
            {
                currentLanguageId = Language.FindByDefault()?.Id;
            }

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

                // 找不到，然后尝试获取当前站点的默认语言（如果指定）
                detectedLanguage ??= allStoreLanguages.FirstOrDefault(language => language.Id == store.DefaultLanguageId);

                // 如果当前站点没有语言，请尝试获取默认语言，而不考虑站点
                detectedLanguage ??= Language.FindByDefault();

                // 如果未找到默认语言，则尝试获取第一种语言
                detectedLanguage ??= allStoreLanguages.FirstOrDefault();

                SetLanguageCookie(detectedLanguage);
            }

            // 缓存找到的语言
            _cachedLanguage = detectedLanguage;

            // 保存到cookie
            _cookie.SetValue(DHSetting.Current.LangName, detectedLanguage?.UniqueSeoCode);

            return _cachedLanguage;
        }
        set
        {
            Language detectedLanguage = value;

            if (value == null || value.Id == 0)
            {
                detectedLanguage = Language.FindByDefault();
            }

            _cachedLanguage = detectedLanguage;

            _cookie.SetValue(DHSetting.Current.LangName, detectedLanguage?.UniqueSeoCode);

            SetLanguageCookie(detectedLanguage);
        }
    }

    /// <summary>
    /// 设置当前用户工作语言
    /// </summary>
    /// <param name="language">语言</param>
    /// <returns>表示异步操作的任务</returns>
    public virtual void SetWorkingLanguage(Language language)
    {
        // 保存传递的语言标识符
        var customer = CurrentCustomer;
        if (customer != null)
        {
            customer.LanguageId = language?.Id ?? 0;
            customer.Update();
        }

        // 设置cookie
        SetLanguageCookie(language);

        // 然后重置缓存的值
        _cachedLanguage = null;
    }

    /// <summary>
    /// 指示我们是否在管理区域中
    /// </summary>
    public virtual Boolean IsAdmin { get; set; }

    #endregion
}