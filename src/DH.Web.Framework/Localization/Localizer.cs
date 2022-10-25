using Microsoft.Extensions.Localization;

namespace DH.Web.Framework.Localization
{
    /// <summary>
    /// 资源定位器
    /// </summary>
    /// <param name="text">文本</param>
    /// <param name="args">文本的参数</param>
    /// <returns>本地化字符串</returns>
    public delegate LocalizedString Localizer(string text, params object[] args);
}
