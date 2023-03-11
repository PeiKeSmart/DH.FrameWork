using DH.Core;
using DH.Core.Configuration;
using DH.Core.Domain.Seo;
using DH.Services.Localization;
using DH.Web.Framework.Mvc.Routing;
using DH.Web.Framework.WebOptimizer;
using DH.Web.Framework.WebOptimizer.Processors;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;

using WebOptimizer;

namespace DH.Web.Framework.UI;

/// <summary>
/// 表示HTML助手实现
/// </summary>
public partial class DHHtmlHelper : IDHHtmlHelper
{
    #region Fields

    private readonly AppSettings _appSettings;
    private readonly HtmlEncoder _htmlEncoder;
    private readonly IActionContextAccessor _actionContextAccessor;
    private readonly IAssetPipeline _assetPipeline;
    private readonly IUrlHelperFactory _urlHelperFactory;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly SeoSettings _seoSettings;

    private readonly Dictionary<ResourceLocation, List<ScriptReferenceMeta>> _scriptParts = new();
    private readonly Dictionary<ResourceLocation, List<string>> _inlineScriptParts = new();
    private readonly List<CssReferenceMeta> _cssParts = new();

    private readonly List<string> _canonicalUrlParts = new();
    private readonly List<string> _headCustomParts = new();
    private readonly List<string> _metaDescriptionParts = new();
    private readonly List<string> _metaKeywordParts = new();
    private readonly List<string> _pageCssClassParts = new();
    private readonly List<string> _titleParts = new();

    private string _activeAdminMenuSystemName;
    private string _editPageUrl;

    #endregion

    #region Ctor

    public DHHtmlHelper(AppSettings appSettings,
        HtmlEncoder htmlEncoder,
        IActionContextAccessor actionContextAccessor,
        IAssetPipeline assetPipeline,
        IUrlHelperFactory urlHelperFactory,
        IWebHostEnvironment webHostEnvironment,
        SeoSettings seoSettings)
    {
        _appSettings = appSettings;
        _htmlEncoder = htmlEncoder;
        _actionContextAccessor = actionContextAccessor;
        _assetPipeline = assetPipeline;
        _urlHelperFactory = urlHelperFactory;
        _webHostEnvironment = webHostEnvironment;
        _seoSettings = seoSettings;
    }

    #endregion

    #region Utils

    private IAsset CreateCssAsset(string bundleKey, string[] assetFiles) {
        var asset = _assetPipeline.AddBundle(bundleKey, $"{MimeTypes.TextCss}; charset=UTF-8", assetFiles)
            .EnforceFileExtensions(".css")
            .AdjustRelativePaths()
            .AddResponseHeader(HeaderNames.XContentTypeOptions, "nosniff");

        // 为了更精确地记录问题文件，我们在连接之前缩小它们
        asset.Processors.Add(new DHCssMinifier());

        asset.Concatenate();

        return asset;
    }

    private IAsset CreateJavaScriptAsset(string bundleKey, string[] assetFiles) {
        var asset = _assetPipeline.AddBundle(bundleKey, $"{MimeTypes.TextJavascript}; charset=UTF-8", assetFiles)
                    .EnforceFileExtensions(".js", ".es5", ".es6")
                    .AddResponseHeader(HeaderNames.XContentTypeOptions, "nosniff");

        // 为了更精确地记录问题文件，我们在连接之前缩小它们
        asset.Processors.Add(new DHJsMinifier());

        asset.Concatenate();

        return asset;
    }

    private static string GetAssetKey(string[] keys, string suffix) {
        if (keys is null || keys.Length == 0)
            throw new ArgumentNullException(nameof(keys));

        var hashInput = string.Join(',', keys);

        using var sha = MD5.Create();
        var input = sha.ComputeHash(Encoding.Unicode.GetBytes(hashInput));

        var key = string.Concat(WebEncoders.Base64UrlEncode(input));

        if (!string.IsNullOrEmpty(suffix))
            key += suffix;

        return key.ToLower();
    }

    /// <summary>
    /// 获取或创建优化管道的资源。
    /// </summary>
    /// <param name="bundlePath">已登记路线</param>
    /// <param name="createAsset">创建捆绑包的函数</param>
    /// <param name="sourceFiles">要优化的源的相对文件名；如果未指定，将使用<paramref name="bundlePath"/></param>
    /// <returns>The bundle</returns>
    private IAsset GetOrCreateBundle(string bundlePath, Func<string, string[], IAsset> createAsset, params string[] sourceFiles) {
        if (string.IsNullOrEmpty(bundlePath))
            throw new ArgumentNullException(nameof(bundlePath));

        if (createAsset is null)
            throw new ArgumentNullException(nameof(createAsset));

        if (sourceFiles.Length == 0)
            sourceFiles = new[] { bundlePath };

        //从生成的URL中删除基路径（如果存在）
        var pathBase = _actionContextAccessor.ActionContext?.HttpContext.Request.PathBase ?? PathString.Empty;
        sourceFiles = sourceFiles.Select(src => src.RemoveApplicationPathFromRawUrl(pathBase)).ToArray();

        if (!_assetPipeline.TryGetAssetFromRoute(bundlePath, out var bundleAsset)) {
            bundleAsset = createAsset(bundlePath, sourceFiles);
        }
        else if (bundleAsset.SourceFiles.Count != sourceFiles.Length || !bundleAsset.SourceFiles.SequenceEqual(sourceFiles)) {
            bundleAsset.SourceFiles.Clear();
            foreach (var source in sourceFiles)
                bundleAsset.TryAddSourceFile(source);
        }

        return bundleAsset;
    }

    #endregion

    #region Methods

    /// <summary>
    /// 将title元素添加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">标题部分</param>
    public virtual void AddTitleParts(string part) {
        if (string.IsNullOrEmpty(part))
            return;

        _titleParts.Add(part);
    }

    /// <summary>
    /// 将title元素附加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">标题部分</param>
    public virtual void AppendTitleParts(string part) {
        if (string.IsNullOrEmpty(part))
            return;

        _titleParts.Insert(0, part);
    }

    /// <summary>
    /// 生成所有标题部分
    /// </summary>
    /// <param name="addDefaultTitle">指示是否插入默认标题的值</param>
    /// <param name="part">标题部分</param>
    /// <returns>A表示异步操作的任务
    /// 任务结果包含生成的HTML字符串</returns>
    public virtual async Task<IHtmlContent> GenerateTitleAsync(bool addDefaultTitle = true, string part = "") {
        AppendTitleParts(part);

        await Task.CompletedTask;

        var specificTitle = string.Join(_seoSettings.PageTitleSeparator, _titleParts.AsEnumerable().Reverse().ToArray());
        string result;
        if (!string.IsNullOrEmpty(specificTitle)) {
            if (addDefaultTitle)
                // 站点名称+页面标题
                switch (_seoSettings.PageTitleSeoAdjustment) {
                    case PageTitleSeoAdjustment.PagenameAfterStorename: {
                            result = string.Join(_seoSettings.PageTitleSeparator, _seoSettings.DefaultTitle, specificTitle);
                        }
                        break;
                    case PageTitleSeoAdjustment.StorenameAfterPagename:
                    default: {
                            result = string.Join(_seoSettings.PageTitleSeparator, specificTitle, _seoSettings.DefaultTitle);
                        }
                        break;
                }
            else
                // 仅页面标题
                result = specificTitle;
        }
        else
            // 仅站点名称
            result = _seoSettings.DefaultTitle;

        return new HtmlString(_htmlEncoder.Encode(result ?? string.Empty));
    }

    /// <summary>
    /// 将元描述元素添加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">元描述部分</param>
    public virtual void AddMetaDescriptionParts(string part) {
        if (string.IsNullOrEmpty(part))
            return;

        _metaDescriptionParts.Add(part);
    }

    /// <summary>
    /// 将元描述元素附加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">元描述部分</param>
    public virtual void AppendMetaDescriptionParts(string part) {
        if (string.IsNullOrEmpty(part))
            return;

        _metaDescriptionParts.Insert(0, part);
    }

    /// <summary>
    /// 生成所有描述部分
    /// </summary>
    /// <param name="part">元描述部分</param>
    /// <returns>表示异步操作的任务
    /// 任务结果包含生成的HTML字符串</returns>
    public virtual async Task<IHtmlContent> GenerateMetaDescriptionAsync(string part = "") {
        AppendMetaDescriptionParts(part);

        await Task.CompletedTask;

        var metaDescription = string.Join(", ", _metaDescriptionParts.AsEnumerable().Reverse().ToArray());
        var result = !string.IsNullOrEmpty(metaDescription)
            ? metaDescription
            : _seoSettings.DefaultMetaDescription;

        return new HtmlString(_htmlEncoder.Encode(result ?? string.Empty));
    }

    /// <summary>
    /// 将meta关键字元素添加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">元关键字部分</param>
    public virtual void AddMetaKeywordParts(string part) {
        if (string.IsNullOrEmpty(part))
            return;

        _metaKeywordParts.Add(part);
    }

    /// <summary>
    /// 将meta关键字元素附加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">元关键字部分</param>
    public virtual void AppendMetaKeywordParts(string part) {
        if (string.IsNullOrEmpty(part))
            return;

        _metaKeywordParts.Insert(0, part);
    }

    /// <summary>
    /// 生成所有关键字部分
    /// </summary>
    /// <param name="part">元关键字部分</param>
    /// <returns>表示异步操作的任务
    /// 任务结果包含生成的HTML字符串</returns>
    public virtual async Task<IHtmlContent> GenerateMetaKeywordsAsync(string part = "") {
        AppendMetaKeywordParts(part);

        await Task.CompletedTask;

        var metaKeyword = string.Join(", ", _metaKeywordParts.AsEnumerable().Reverse().ToArray());
        var result = !string.IsNullOrEmpty(metaKeyword)
            ? metaKeyword
            : _seoSettings.DefaultMetaKeywords;

        return new HtmlString(_htmlEncoder.Encode(result ?? string.Empty));
    }

    /// <summary>
    /// 添加脚本元素
    /// </summary>
    /// <param name="location">脚本元素的位置</param>
    /// <param name="src">脚本路径（缩小版本）</param>
    /// <param name="debugSrc">脚本路径（完整调试版本）。如果为空，则将使用缩小版本</param>
    /// <param name="excludeFromBundle">指示是否从绑定中排除此脚本的值</param>
    public virtual void AddScriptParts(ResourceLocation location, string src, string debugSrc = "", bool excludeFromBundle = false) {
        if (!_scriptParts.ContainsKey(location))
            _scriptParts.Add(location, new List<ScriptReferenceMeta>());

        if (string.IsNullOrEmpty(src))
            return;

        if (!string.IsNullOrEmpty(debugSrc) && _webHostEnvironment.IsDevelopment())
            src = debugSrc;

        if (_actionContextAccessor.ActionContext == null)
            throw new ArgumentNullException(nameof(_actionContextAccessor.ActionContext));

        var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

        _scriptParts[location].Add(new ScriptReferenceMeta {
            ExcludeFromBundle = excludeFromBundle,
            IsLocal = urlHelper.IsLocalUrl(src),
            Src = urlHelper.Content(src)
        });
    }

    /// <summary>
    /// 追加脚本元素
    /// </summary>
    /// <param name="location">脚本元素的位置</param>
    /// <param name="src">脚本路径（缩小版本）</param>
    /// <param name="debugSrc">脚本路径（完整调试版本）。如果为空，则将使用缩小版本</param>
    /// <param name="excludeFromBundle">指示是否从绑定中排除此脚本的值</param>
    public virtual void AppendScriptParts(ResourceLocation location, string src, string debugSrc = "", bool excludeFromBundle = false) {
        if (!_scriptParts.ContainsKey(location))
            _scriptParts.Add(location, new List<ScriptReferenceMeta>());

        if (string.IsNullOrEmpty(src))
            return;

        if (!string.IsNullOrEmpty(debugSrc) && _webHostEnvironment.IsDevelopment())
            src = debugSrc;

        if (_actionContextAccessor.ActionContext == null)
            throw new ArgumentNullException(nameof(_actionContextAccessor.ActionContext));

        var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

        _scriptParts[location].Insert(0, new ScriptReferenceMeta {
            ExcludeFromBundle = excludeFromBundle,
            IsLocal = urlHelper.IsLocalUrl(src),
            Src = urlHelper.Content(src)
        });
    }

    /// <summary>
    /// 生成所有脚本部分
    /// </summary>
    /// <param name="location">脚本元素的位置</param>
    /// <returns>生成的HTML字符串</returns>
    public virtual IHtmlContent GenerateScripts(ResourceLocation location) {
        if (!_scriptParts.ContainsKey(location) || _scriptParts[location] == null)
            return HtmlString.Empty;

        if (!_scriptParts.Any())
            return HtmlString.Empty;

        var result = new StringBuilder();
        var woConfig = _appSettings.Get<WebOptimizerConfig>();

        var httpContext = _actionContextAccessor.ActionContext.HttpContext;

        if (woConfig.EnableJavaScriptBundling && _scriptParts[location].Any(item => !item.ExcludeFromBundle)) {
            var sources = _scriptParts[location]
               .Where(item => !item.ExcludeFromBundle && item.IsLocal)
               .Select(item => item.Src)
               .Distinct().ToArray();

            var bundleKey = string.Concat("/js/", GetAssetKey(sources, woConfig.JavaScriptBundleSuffix), ".js");

            var bundleAsset = GetOrCreateBundle(bundleKey, CreateJavaScriptAsset, sources);

            var pathBase = _actionContextAccessor.ActionContext?.HttpContext.Request.PathBase ?? PathString.Empty;
            result.AppendFormat("<script type=\"{0}\" src=\"{1}{2}?v={3}\"></script>",
                MimeTypes.TextJavascript, pathBase, bundleAsset.Route, bundleAsset.GenerateCacheKey(httpContext, woConfig));
        }

        var scripts = _scriptParts[location]
            .Where(item => !woConfig.EnableJavaScriptBundling || item.ExcludeFromBundle || !item.IsLocal)
            .Distinct();

        foreach (var item in scripts) {
            if (!item.IsLocal) {
                result.AppendFormat("<script type=\"{0}\" src=\"{1}\"></script>", MimeTypes.TextJavascript, item.Src);
                result.Append(Environment.NewLine);
                continue;
            }

            var asset = GetOrCreateBundle(item.Src, CreateJavaScriptAsset);

            result.AppendFormat("<script type=\"{0}\" src=\"{1}?v={2}\"></script>",
                MimeTypes.TextJavascript, asset.Route, asset.GenerateCacheKey(httpContext, woConfig));

            result.Append(Environment.NewLine);
        }

        return new HtmlString(result.ToString());
    }

    /// <summary>
    /// 添加内联脚本元素
    /// </summary>
    /// <param name="location">脚本元素的位置</param>
    /// <param name="script">Script</param>
    public virtual void AddInlineScriptParts(ResourceLocation location, string script) {
        if (!_inlineScriptParts.ContainsKey(location))
            _inlineScriptParts.Add(location, new());

        if (string.IsNullOrEmpty(script))
            return;

        if (_inlineScriptParts[location].Contains(script))
            return;

        _inlineScriptParts[location].Add(script);
    }

    /// <summary>
    /// 附加内联脚本元素
    /// </summary>
    /// <param name="location">脚本元素的位置</param>
    /// <param name="script">脚本</param>
    public virtual void AppendInlineScriptParts(ResourceLocation location, string script) {
        if (!_inlineScriptParts.ContainsKey(location))
            _inlineScriptParts.Add(location, new());

        if (string.IsNullOrEmpty(script))
            return;

        if (_inlineScriptParts[location].Contains(script))
            return;

        _inlineScriptParts[location].Insert(0, script);
    }

    /// <summary>
    /// 生成所有内联脚本部分
    /// </summary>
    /// <param name="location">脚本元素的位置</param>
    /// <returns>生成的HTML字符串</returns>
    public virtual IHtmlContent GenerateInlineScripts(ResourceLocation location) {
        if (!_inlineScriptParts.ContainsKey(location) || _inlineScriptParts[location] == null)
            return HtmlString.Empty;

        if (!_inlineScriptParts.Any())
            return HtmlString.Empty;

        var result = new StringBuilder();
        foreach (var item in _inlineScriptParts[location]) {
            result.Append(item);
            result.Append(Environment.NewLine);
        }
        return new HtmlString(result.ToString());
    }

    /// <summary>
    /// 添加CSS元素
    /// </summary>
    /// <param name="src">脚本路径（缩小版本）</param>
    /// <param name="debugSrc">脚本路径（完整调试版本）。如果为空，则将使用缩小版本</param>
    /// <param name="excludeFromBundle">指示是否从绑定中排除此样式表的值</param>
    public virtual void AddCssFileParts(string src, string debugSrc = "", bool excludeFromBundle = false) {
        if (string.IsNullOrEmpty(src))
            return;

        if (!string.IsNullOrEmpty(debugSrc) && _webHostEnvironment.IsDevelopment())
            src = debugSrc;

        if (_actionContextAccessor.ActionContext == null)
            throw new ArgumentNullException(nameof(_actionContextAccessor.ActionContext));

        var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

        _cssParts.Add(new CssReferenceMeta {
            ExcludeFromBundle = excludeFromBundle,
            IsLocal = urlHelper.IsLocalUrl(src),
            Src = urlHelper.Content(src)
        });
    }

    /// <summary>
    /// 附加CSS元素
    /// </summary>
    /// <param name="src">脚本路径（缩小版本）</param>
    /// <param name="debugSrc">脚本路径（完整调试版本）。如果为空，则将使用缩小版本</param>
    /// <param name="excludeFromBundle">指示是否从绑定中排除此样式表的值</param>
    public virtual void AppendCssFileParts(string src, string debugSrc = "", bool excludeFromBundle = false) {
        if (string.IsNullOrEmpty(src))
            return;

        if (!string.IsNullOrEmpty(debugSrc) && _webHostEnvironment.IsDevelopment())
            src = debugSrc;

        if (_actionContextAccessor.ActionContext == null)
            throw new ArgumentNullException(nameof(_actionContextAccessor.ActionContext));

        var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

        _cssParts.Insert(0, new CssReferenceMeta {
            ExcludeFromBundle = excludeFromBundle,
            IsLocal = urlHelper.IsLocalUrl(src),
            Src = urlHelper.Content(src)
        });
    }

    /// <summary>
    /// 生成所有CSS部分
    /// </summary>
    /// <returns>生成的HTML字符串</returns>
    public virtual IHtmlContent GenerateCssFiles() {
        if (_cssParts.Count == 0)
            return HtmlString.Empty;

        var result = new StringBuilder();

        var woConfig = _appSettings.Get<WebOptimizerConfig>();
        var httpContext = _actionContextAccessor.ActionContext.HttpContext;

        if (woConfig.EnableCssBundling && _cssParts.Any(item => !item.ExcludeFromBundle)) {
            var bundleSuffix = woConfig.CssBundleSuffix;

            if (CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft)
                bundleSuffix += ".rtl";

            var sources = _cssParts
                .Where(item => !item.ExcludeFromBundle && item.IsLocal)
                .Distinct()
                //remove the application path from the generated URL if exists
                .Select(item => item.Src).ToArray();

            var bundleKey = string.Concat("/css/", GetAssetKey(sources, bundleSuffix), ".css");

            var bundleAsset = GetOrCreateBundle(bundleKey, CreateCssAsset, sources);

            var pathBase = _actionContextAccessor.ActionContext?.HttpContext.Request.PathBase ?? PathString.Empty;
            result.AppendFormat("<link rel=\"stylesheet\" type=\"{0}\" href=\"{1}{2}?v={3}\" />",
                MimeTypes.TextCss, pathBase, bundleAsset.Route, bundleAsset.GenerateCacheKey(httpContext, woConfig));
        }

        var styles = _cssParts
                .Where(item => !woConfig.EnableCssBundling || item.ExcludeFromBundle || !item.IsLocal)
                .Distinct();

        if (_actionContextAccessor.ActionContext == null)
            throw new ArgumentNullException(nameof(_actionContextAccessor.ActionContext));

        foreach (var item in styles) {
            if (!item.IsLocal) {
                result.AppendFormat("<link rel=\"stylesheet\" type=\"{0}\" href=\"{1}\" />", MimeTypes.TextCss, item.Src);
                result.Append(Environment.NewLine);
                continue;
            }

            var asset = GetOrCreateBundle(item.Src, CreateCssAsset);

            result.AppendFormat("<link rel=\"stylesheet\" type=\"{0}\" href=\"{1}?v={2}\" />",
                MimeTypes.TextCss, asset.Route, asset.GenerateCacheKey(httpContext, woConfig));
            result.AppendLine();
        }

        return new HtmlString(result.ToString());
    }

    /// <summary>
    /// 将规范URL元素添加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">标准URL部分</param>
    /// <param name="withQueryString">是否将规范URL与查询字符串参数一起使用</param>
    public virtual void AddCanonicalUrlParts(string part, bool withQueryString = false) {
        if (string.IsNullOrEmpty(part))
            return;

        if (withQueryString) {
            //添加有序查询字符串参数
            var queryParameters = _actionContextAccessor.ActionContext.HttpContext.Request.Query.OrderBy(parameter => parameter.Key)
                .ToDictionary(parameter => parameter.Key, parameter => parameter.Value.ToString());
            part = QueryHelpers.AddQueryString(part, queryParameters);
        }

        _canonicalUrlParts.Add(part);
    }

    /// <summary>
    /// 将规范URL元素附加到<![CDATA[<head>]]>
    /// </summary>
    /// <param name="part">标准URL部分</param>
    public virtual void AppendCanonicalUrlParts(string part) {
        if (string.IsNullOrEmpty(part))
            return;

        _canonicalUrlParts.Insert(0, part);
    }

    /// <summary>
    /// 生成所有规范URL部分
    /// </summary>
    /// <returns>生成的HTML字符串</returns>
    public virtual IHtmlContent GenerateCanonicalUrls() {
        var result = new StringBuilder();
        foreach (var canonicalUrl in _canonicalUrlParts) {
            result.AppendFormat("<link rel=\"canonical\" href=\"{0}\" />", canonicalUrl);
            result.Append(Environment.NewLine);
        }
        return new HtmlString(result.ToString());
    }

    /// <summary>
    /// 将任何自定义元素添加到<![CDATA[<head>]]>元素
    /// </summary>
    /// <param name="part">整个元素。例如 <![CDATA[<meta name="msvalidate.01" content="123121231231313123123" />]]></param>
    public virtual void AddHeadCustomParts(string part) {
        if (string.IsNullOrEmpty(part))
            return;

        _headCustomParts.Add(part);
    }

    /// <summary>
    /// 将任何自定义元素附加到<![CDATA[<head>]]>元素
    /// </summary>
    /// <param name="part">整个元素。例如 <![CDATA[<meta name="msvalidate.01" content="123121231231313123123" />]]></param>
    public virtual void AppendHeadCustomParts(string part) {
        if (string.IsNullOrEmpty(part))
            return;

        _headCustomParts.Insert(0, part);
    }

    /// <summary>
    /// 生成所有自定义元素
    /// </summary>
    /// <returns>生成的HTML字符串</returns>
    public virtual IHtmlContent GenerateHeadCustom() {
        // 仅使用不同的行
        var distinctParts = _headCustomParts.Distinct().ToList();
        if (!distinctParts.Any())
            return HtmlString.Empty;

        var result = new StringBuilder();
        foreach (var path in distinctParts) {
            result.Append(path);
            result.Append(Environment.NewLine);
        }
        return new HtmlString(result.ToString());
    }

    /// <summary>
    /// 将CSS类添加到<![CDATA[<head>]]>元素
    /// </summary>
    /// <param name="part">CSS类</param>
    public virtual void AddPageCssClassParts(string part) {
        if (string.IsNullOrEmpty(part))
            return;

        _pageCssClassParts.Add(part);
    }

    /// <summary>
    /// 将CSS类附加到<![CDATA[<head>]]>元素
    /// </summary>
    /// <param name="part">CSS类</param>
    public virtual void AppendPageCssClassParts(string part) {
        if (string.IsNullOrEmpty(part))
            return;

        _pageCssClassParts.Insert(0, part);
    }

    /// <summary>
    /// 生成所有标题部分
    /// </summary>
    /// <param name="part">CSS类</param>
    /// <returns>生成的字符串</returns>
    public virtual string GeneratePageCssClasses(string part = "") {
        AppendPageCssClassParts(part);

        var result = string.Join(" ", _pageCssClassParts.AsEnumerable().Reverse().ToArray());

        if (string.IsNullOrEmpty(result))
            return string.Empty;

        return _htmlEncoder.Encode(result);
    }

    /// <summary>
    /// 指定“编辑页面”URL
    /// </summary>
    /// <param name="url">URL</param>
    public virtual void AddEditPageUrl(string url) {
        _editPageUrl = url;
    }

    /// <summary>
    /// 获取“编辑页”URL
    /// </summary>
    /// <returns>URL</returns>
    public virtual string GetEditPageUrl() {
        return _editPageUrl;
    }

    /// <summary>
    /// 指定应选择（展开）的管理菜单项的系统名称
    /// </summary>
    /// <param name="systemName">系统名称</param>
    public virtual void SetActiveMenuItemSystemName(string systemName) {
        _activeAdminMenuSystemName = systemName;
    }

    /// <summary>
    /// 获取应选择（展开）的管理菜单项的系统名称
    /// </summary>
    /// <returns>系统名称</returns>
    public virtual string GetActiveMenuItemSystemName() {
        return _activeAdminMenuSystemName;
    }

    /// <summary>
    /// 获取与呈现此页面的请求关联的路由名称
    /// </summary>
    /// <param name="handleDefaultRoutes">一个值，指示是否使用引擎信息生成名称，除非另有指定</param>
    /// <returns>路由名称</returns>
    public virtual string GetRouteName(bool handleDefaultRoutes = false) {
        var actionContext = _actionContextAccessor.ActionContext;

        if (actionContext is null)
            return string.Empty;

        var httpContext = actionContext.HttpContext;
        var routeName = httpContext.GetEndpoint()?.Metadata.GetMetadata<RouteNameMetadata>()?.RouteName ?? string.Empty;

        if (!string.IsNullOrEmpty(routeName) && routeName != "areaRoute")
            return routeName;

        //然后尝试获取一个通用名称（实际上它是一个操作名称，而不是路由）
        if (httpContext.GetRouteValue(DHRoutingDefaults.RouteValue.SeName) is not null &&
            httpContext.GetRouteValue(DHRoutingDefaults.RouteValue.Action) is string actionName)
            return actionName;

        if (handleDefaultRoutes)
            return actionContext.ActionDescriptor switch {
                ControllerActionDescriptor controllerAction => string.Concat(controllerAction.ControllerName, controllerAction.ActionName),
                CompiledPageActionDescriptor compiledPage => string.Concat(compiledPage.AreaName, compiledPage.ViewEnginePath.Replace("/", "")),
                PageActionDescriptor pageAction => string.Concat(pageAction.AreaName, pageAction.ViewEnginePath.Replace("/", "")),
                _ => actionContext.ActionDescriptor.DisplayName?.Replace("/", "") ?? string.Empty
            };

        return routeName;
    }

    #endregion

    #region Nested classes

    /// <summary>
    /// JS文件元数据
    /// </summary>
    private partial record ScriptReferenceMeta {
        /// <summary>
        /// 指示是否从绑定中排除脚本的值
        /// </summary>
        public bool ExcludeFromBundle { get; init; }

        /// <summary>
        /// 指示src是否为本地的值
        /// </summary>
        public bool IsLocal { get; init; }

        /// <summary>
        /// 生产用Src
        /// </summary>
        public string Src { get; init; }
    }

    /// <summary>
    /// CSS文件元数据
    /// </summary>
    private partial record CssReferenceMeta {
        /// <summary>
        /// 指示是否从绑定中排除脚本的值
        /// </summary>
        public bool ExcludeFromBundle { get; init; }

        /// <summary>
        /// 生产用Src
        /// </summary>
        public string Src { get; init; }

        /// <summary>
        /// 指示Src是否为本地的值
        /// </summary>
        public bool IsLocal { get; init; }
    }

    #endregion
}
