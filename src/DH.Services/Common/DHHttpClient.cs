using System.Net;

using DH.Core;
using DH.Core.Domain.Common;
using DH.Core.Rss;
using DH.Entity;

using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

using Pek.Webs;

namespace DH.Services.Common;

/// <summary>
/// 表示请求官方网站的HTTP客户端
/// </summary>
public partial class DHHttpClient
{
    #region Fields

    private readonly AdminAreaSettings _adminAreaSettings;
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStoreContext _storeContext;
    private readonly IWebHelper _webHelper;
    private readonly IWorkContext _workContext;

    #endregion

    #region Ctor

    public DHHttpClient(AdminAreaSettings adminAreaSettings,
        HttpClient client,
        IHttpContextAccessor httpContextAccessor,
        IStoreContext storeContext,
        IWebHelper webHelper,
        IWorkContext workContext)
    {
        // 配置客户端
        client.BaseAddress = new Uri("https://www.deng-hao.com/");
        client.Timeout = TimeSpan.FromSeconds(5);
        client.DefaultRequestHeaders.Add(HeaderNames.UserAgent, $"denghao-{DHVersion.CURRENT_VERSION}");

        _adminAreaSettings = adminAreaSettings;
        _httpClient = client;
        _httpContextAccessor = httpContextAccessor;
        _storeContext = storeContext;
        _webHelper = webHelper;
        _workContext = workContext;
    }

    #endregion

    #region Methods

    /// <summary>
    /// 检查站点是否可用
    /// </summary>
    /// <returns>
    /// 表示异步操作的任务
    /// 任务结果包含异步任务，其结果确定请求已完成
    /// </returns>
    public virtual async Task PingAsync()
    {
        await _httpClient.GetStringAsync("/");
    }

    /// <summary>
    /// 检查当前存储中的版权删除密钥
    /// </summary>
    /// <returns>
    /// 表示异步操作的任务
    /// 任务结果包含异步任务，其结果包含警告文本
    /// </returns>
    public virtual async Task<string> GetCopyrightWarningAsync()
    {
        // 准备请求的URL
        var language = Language.GetTwoLetterIsoLanguageName(_workContext.WorkingLanguage);
        var store = _storeContext.CurrentStore;
        var url = string.Format(DHCommonDefaults.DHCopyrightWarningPath,
            store.Url,
            _webHelper.IsLocalRequest(_httpContextAccessor.HttpContext.Request),
            language).ToLowerInvariant();

        // 获取消息
        return await _httpClient.GetStringAsync(url);
    }

    /// <summary>
    /// 获取官方新闻RSS
    /// </summary>
    /// <returns>
    /// 表示异步操作的任务
    /// 任务结果包含异步任务，其结果包含新闻RSS提要
    /// </returns>
    public virtual async Task<RssFeed> GetNewsRssAsync()
    {
        // 准备请求的URL
        var language = Language.GetTwoLetterIsoLanguageName(_workContext.WorkingLanguage);
        var url = string.Format(DHCommonDefaults.DHNewsRssPath,
            DHVersion.CURRENT_VERSION,
            _webHelper.IsLocalRequest(_httpContextAccessor.HttpContext.Request),
            _adminAreaSettings.HideAdvertisementsOnAdminArea,
            _webHelper.GetStoreLocation(),
            language).ToLowerInvariant();

        // 获取新闻源
        await using var stream = await _httpClient.GetStreamAsync(url);
        return await RssFeed.LoadAsync(stream);
    }

    /// <summary>
    /// 关于成功安装的通知
    /// </summary>
    /// <param name="email">管理员电子邮件</param>
    /// <param name="languageCode">语言代码</param>
    /// <param name="culture">文化名称</param>
    /// <returns>
    /// 表示异步操作的任务
    /// 任务结果包含异步任务，其结果包含结果字符串
    /// </returns>
    public virtual async Task<string> InstallationCompletedAsync(string email, string languageCode, string culture)
    {
        // 准备请求的URL
        var url = string.Format(DHCommonDefaults.DHInstallationCompletedPath,
            DHVersion.CURRENT_VERSION,
            _webHelper.IsLocalRequest(_httpContextAccessor.HttpContext.Request),
            WebUtility.UrlEncode(email),
            _webHelper.GetStoreLocation(),
            languageCode,
            culture)
            .ToLowerInvariant();

        // 这个请求需要更多的时间
        _httpClient.Timeout = TimeSpan.FromSeconds(30);

        return await _httpClient.GetStringAsync(url);
    }

    /// <summary>
    /// 获取有关可用市场扩展类别的响应
    /// </summary>
    /// <returns>
    /// 表示异步操作的任务
    /// 任务结果包含异步任务，其结果包含结果字符串
    /// </returns>
    public virtual async Task<string> GetExtensionsCategoriesAsync()
    {
        // 准备请求的URL
        var language = Language.GetTwoLetterIsoLanguageName(_workContext.WorkingLanguage);
        var url = string.Format(DHCommonDefaults.DHExtensionsCategoriesPath, language).ToLowerInvariant();

        // 获取XML响应
        return await _httpClient.GetStringAsync(url);
    }

    /// <summary>
    /// 获取有关市场扩展可用版本的响应
    /// </summary>
    /// <returns>
    /// 表示异步操作的任务
    /// 任务结果包含异步任务，其结果包含结果字符串
    /// </returns>
    public virtual async Task<string> GetExtensionsVersionsAsync()
    {
        // 准备请求的URL
        var language = Language.GetTwoLetterIsoLanguageName(_workContext.WorkingLanguage);
        var url = string.Format(DHCommonDefaults.DHExtensionsVersionsPath, language).ToLowerInvariant();

        // 获取XML响应
        return await _httpClient.GetStringAsync(url);
    }

    /// <summary>
    /// 获取有关市场扩展的响应
    /// </summary>
    /// <param name="categoryId">类别标识符</param>
    /// <param name="versionId">版本标识符</param>
    /// <param name="price">价格；0-全部，10-免费，20-付费</param>
    /// <param name="searchTerm">搜索术语</param>
    /// <param name="pageIndex">页面索引</param>
    /// <param name="pageSize">页面大小</param>
    /// <returns>
    /// 表示异步操作的任务
    /// 任务结果包含异步任务，其结果包含结果字符串
    /// </returns>
    public virtual async Task<string> GetExtensionsAsync(int categoryId = 0,
        int versionId = 0, int price = 0, string searchTerm = null,
        int pageIndex = 0, int pageSize = int.MaxValue)
    {
        // 准备请求的URL
        var language = Language.GetTwoLetterIsoLanguageName(_workContext.WorkingLanguage);
        var url = string.Format(DHCommonDefaults.DHExtensionsPath,
            categoryId, versionId, price, WebUtility.UrlEncode(searchTerm), pageIndex, pageSize, language).ToLowerInvariant();

        // 获取XML响应
        return await _httpClient.GetStringAsync(url);
    }

    #endregion
}
