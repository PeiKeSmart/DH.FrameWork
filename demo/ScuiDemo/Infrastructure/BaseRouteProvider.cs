using DH;
using DH.Core.Domain.Localization;
using DH.Core.Infrastructure;
using DH.Web.Framework.Mvc.Routing;

namespace ScuiDemo.Infrastructure;

/// <summary>
/// 表示基本提供程序
/// </summary>
public partial class BaseRouteProvider
{
    /// <summary>
    /// 获取用于使用语言代码检测路线的模式
    /// </summary>
    /// <returns></returns>
    protected string GetLanguageRoutePattern()
    {
        if (DHSetting.Current.IsInstalled)
        {
            var localizationSettings = EngineContext.Current.Resolve<LocalizationSettings>();
            if (localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
            {
                // 当我们还没有选定的语言时，这个模式在应用程序启动时设置一次
                // 所以我们默认使用“en”作为语言值，稍后它将被替换为工作语言代码
                var code = "en";
                return $"{{{DHRoutingDefaults.RouteValue.Language}:maxlength(2):{DHRoutingDefaults.LanguageParameterTransformer}={code}}}";
            }
        }

        return string.Empty;
    }
}