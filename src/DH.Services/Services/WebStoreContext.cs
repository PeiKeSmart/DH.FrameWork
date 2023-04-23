using DH.Core;
using DH.Entity;

using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace DH.Services.Services;

/// <summary>
/// web站点应用程序的上下文
/// </summary>
public partial class WebStoreContext : IStoreContext {
    #region Fields

    private readonly IHttpContextAccessor _httpContextAccessor;

    private SiteInfo _cachedStore;

    #endregion

    #region Ctor

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="httpContextAccessor">HTTP context accessor</param>
    public WebStoreContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    #endregion

    #region Properties

    /// <summary>
    /// 获取当前站点
    /// </summary>
    /// <returns>当前站点</returns>
    public virtual SiteInfo CurrentStore
    {
        get
        {
            if (_cachedStore != null)
                return _cachedStore;

            // 尝试通过HOST标头确定当前站点
            string host = _httpContextAccessor.HttpContext?.Request.Headers[HeaderNames.Host];

            var allStores = SiteInfo.GetAllStores();
            var store = allStores.FirstOrDefault(s => SiteInfo.ContainsHostValue(s, host));

            if (store == null)
                // 加载第一个找到的站点
                store = allStores.FirstOrDefault();

            _cachedStore = store ?? throw new Exception("无法加载任何站点");

            return _cachedStore;
        }
    }

    #endregion
}