using Microsoft.AspNetCore.Html;

using System.Reflection;

namespace DH.Web.Framework.UI;

/// <summary>
/// 表示HTML帮助程序
/// </summary>
public partial interface IDHHtmlHelper {
    /// <summary>
    /// 将title元素添加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">Title part</param>
    void AddTitleParts(string part);

    /// <summary>
    /// 将title元素附加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">Title part</param>
    void AppendTitleParts(string part);

    /// <summary>
    /// 生成所有标题部分
    /// </summary>
    /// <param name="addDefaultTitle">指示是否插入默认标题的值</param>
    /// <param name="part">标题部分</param>
    /// <returns>表示异步操作的任务
    /// 任务结果包含生成的HTML字符串</returns>
    Task<IHtmlContent> GenerateTitleAsync(bool addDefaultTitle = true, string part = "");

    /// <summary>
    /// 将元描述元素添加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">元描述部分</param>
    void AddMetaDescriptionParts(string part);

    /// <summary>
    /// 将元描述元素附加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">元描述部分</param>
    void AppendMetaDescriptionParts(string part);

    /// <summary>
    /// 生成所有描述部分
    /// </summary>
    /// <param name="part">元描述部分</param>
    /// <returns>表示异步操作的任务
    /// 任务结果包含生成的HTML字符串</returns>
    Task<IHtmlContent> GenerateMetaDescriptionAsync(string part = "");

    /// <summary>
    /// 将meta关键字元素添加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">元关键字部分</param>
    void AddMetaKeywordParts(string part);

    /// <summary>
    /// 将meta关键字元素附加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">元关键字部分</param>
    void AppendMetaKeywordParts(string part);

    /// <summary>
    /// 生成所有关键字部分
    /// </summary>
    /// <param name="part">元关键字部分</param>
    /// <returns>表示异步操作的任务
    /// 任务结果包含生成的HTML字符串</returns>
    Task<IHtmlContent> GenerateMetaKeywordsAsync(string part = "");

    /// <summary>
    /// 添加脚本元素
    /// </summary>
    /// <param name="location">脚本元素的位置</param>
    /// <param name="src">脚本路径（缩小版本）</param>
    /// <param name="debugSrc">脚本路径（完整调试版本）。如果为空，则将使用缩小版本</param>
    /// <param name="excludeFromBundle">指示是否从绑定中排除此脚本的值</param>
    void AddScriptParts(ResourceLocation location, string src, string debugSrc = "", bool excludeFromBundle = false);

    /// <summary>
    /// 追加脚本元素
    /// </summary>
    /// <param name="location">脚本元素的位置</param>
    /// <param name="src">脚本路径（缩小版本）</param>
    /// <param name="debugSrc">脚本路径（完整调试版本）。如果为空，则将使用缩小版本</param>
    /// <param name="excludeFromBundle">指示是否从绑定中排除此脚本的值</param>
    void AppendScriptParts(ResourceLocation location, string src, string debugSrc = "", bool excludeFromBundle = false);

    /// <summary>
    /// 生成所有脚本部分
    /// </summary>
    /// <param name="location">脚本元素的位置</param>
    /// <returns>生成的HTML字符串</returns>
    IHtmlContent GenerateScripts(ResourceLocation location);

    /// <summary>
    /// 添加内联脚本元素
    /// </summary>
    /// <param name="location">脚本元素的位置</param>
    /// <param name="script">Script</param>
    void AddInlineScriptParts(ResourceLocation location, string script);

    /// <summary>
    /// 附加内联脚本元素
    /// </summary>
    /// <param name="location">脚本元素的位置</param>
    /// <param name="script">Script</param>
    void AppendInlineScriptParts(ResourceLocation location, string script);

    /// <summary>
    /// 生成所有内联脚本部分
    /// </summary>
    /// <param name="location">脚本元素的位置</param>
    /// <returns>生成的HTML字符串</returns>
    IHtmlContent GenerateInlineScripts(ResourceLocation location);

    /// <summary>
    /// 添加CSS元素
    /// </summary>
    /// <param name="src">脚本路径（缩小版本）</param>
    /// <param name="debugSrc">脚本路径（完整调试版本）。如果为空，则将使用缩小版本</param>
    /// <param name="excludeFromBundle">指示是否从绑定中排除此样式表的值</param>
    void AddCssFileParts(string src, string debugSrc = "", bool excludeFromBundle = false);

    /// <summary>
    /// 附加CSS元素
    /// </summary>
    /// <param name="src">脚本路径（缩小版本）</param>
    /// <param name="debugSrc">脚本路径（完整调试版本）。如果为空，则将使用缩小版本</param>
    /// <param name="excludeFromBundle">指示是否从绑定中排除此样式表的值</param>
    void AppendCssFileParts(string src, string debugSrc = "", bool excludeFromBundle = false);

    /// <summary>
    /// 生成所有CSS部分
    /// </summary>
    /// <returns>生成的HTML字符串</returns>
    IHtmlContent GenerateCssFiles();

    /// <summary>
    /// 将规范URL元素添加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">标准URL部分</param>
    /// <param name="withQueryString">是否将规范URL与查询字符串参数一起使用</param>
    void AddCanonicalUrlParts(string part, bool withQueryString = false);

    /// <summary>
    /// 将规范URL元素附加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">标准URL部分</param>
    void AppendCanonicalUrlParts(string part);

    /// <summary>
    /// 生成所有规范URL部分
    /// </summary>
    /// <returns>生成的HTML字符串</returns>
    IHtmlContent GenerateCanonicalUrls();

    /// <summary>
    /// 向元素<![CDATA[<head>]]>中添加任何自定义元素
    /// </summary>
    /// <param name="part">整个元素。例如<![CDATA[<meta name="msvalidate.01" content="123121231231313123123" />]]></param>
    void AddHeadCustomParts(string part);

    /// <summary>
    /// 将任何自定义元素附加到<![CDATA[<head>]]>元素
    /// </summary>
    /// <param name="part">整个元素。例如<![CDATA[<meta name="msvalidate.01" content="123121231231313123123" />]]></param>
    void AppendHeadCustomParts(string part);

    /// <summary>
    /// 生成所有自定义元素
    /// </summary>
    /// <returns>生成的HTML字符串</returns>
    IHtmlContent GenerateHeadCustom();

    /// <summary>
    /// 将CSS类添加到<![CDATA[<head>]]>元素
    /// </summary>
    /// <param name="part">CSS class</param>
    void AddPageCssClassParts(string part);

    /// <summary>
    /// 将CSS类附加到<![CDATA[<head>]]>元素
    /// </summary>
    /// <param name="part">CSS class</param>
    void AppendPageCssClassParts(string part);

    /// <summary>
    /// 生成所有标题部分
    /// </summary>
    /// <param name="part">CSS类</param>
    /// <returns>生成的字符串</returns>
    string GeneratePageCssClasses(string part = "");

    /// <summary>
    /// 指定“编辑页面”URL
    /// </summary>
    /// <param name="url">URL</param>
    void AddEditPageUrl(string url);

    /// <summary>
    /// 获取“编辑页”URL
    /// </summary>
    /// <returns>URL</returns>
    string GetEditPageUrl();

    /// <summary>
    /// 指定应选择（展开）的管理菜单项的系统名称
    /// </summary>
    /// <param name="systemName">系统名称</param>
    void SetActiveMenuItemSystemName(string systemName);

    /// <summary>
    /// 获取应选择（展开）的管理菜单项的系统名称
    /// </summary>
    /// <returns>系统名称</returns>
    string GetActiveMenuItemSystemName();

    /// <summary>
    /// 获取与呈现此页面的请求关联的路由名称
    /// </summary>
    /// <param name="handleDefaultRoutes">一个值，指示是否使用引擎信息生成名称，除非另有指定</param>
    /// <returns>路由名称</returns>
    string GetRouteName(bool handleDefaultRoutes = false);
}
