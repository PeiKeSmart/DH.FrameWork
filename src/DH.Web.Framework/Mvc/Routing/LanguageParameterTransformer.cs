using DH.Core;
using DH.Core.Infrastructure;
using DH.Entity;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DH.Web.Framework.Mvc.Routing
{
    /// <summary>
    /// 表示语言参数转换器类
    /// </summary>
    public class LanguageParameterTransformer : IOutboundParameterTransformer
    {
        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor

        public LanguageParameterTransformer(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 将指定的路由值转换为包含在URI中的字符串
        /// </summary>
        /// <param name="value">要转换的路由值</param>
        /// <returns>转换后的值</returns>
        public string TransformOutbound(object value)
        {
            // 首先尝试从路由值中获取语言代码
            var routeValues = _httpContextAccessor.HttpContext.Request.RouteValues;
            if (routeValues.TryGetValue(DHRoutingDefaults.RouteValue.Language, out var routeValue))
            {
                // 确保此语言可用
                var code = routeValue?.ToString();
                var storeContext = EngineContext.Current.Resolve<IStoreContext>();
                var store = storeContext.GetCurrentStore();
                var languages = Language.GetAllLanguages();
                var language = languages
                    .FirstOrDefault(lang => lang.Status && lang.UniqueSeoCode.Equals(code, StringComparison.InvariantCultureIgnoreCase));
                if (language is not null)
                    return language.UniqueSeoCode.ToLowerInvariant();
            }

            // 如果路由值中没有代码，请检查该值是否已传递
            if (value is null)
                return string.Empty;

            // 或使用当前语言代码
            // 我们不使用传递的值，因为它总是与当前值相同，或者是默认值（en）
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var currentLanguage = workContext.GetWorkingLanguage();
            return currentLanguage.UniqueSeoCode.ToLowerInvariant();
        }

        #endregion
    }
}
