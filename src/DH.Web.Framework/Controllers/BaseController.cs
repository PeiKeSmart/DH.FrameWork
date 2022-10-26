using DH.Core;
using DH.Core.Infrastructure;
using DH.Entity;
using DH.Services.Localization;
using DH.Web.Framework.Models;
using DH.Web.Framework.Mvc.Filters;
using DH.Web.Framework.UI;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;

using System.Collections;
using System.Net;
using System.Text.Encodings.Web;

namespace DH.Web.Framework.Controllers
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    [PublishModelEvents]
    public abstract partial class BaseController : Controller
    {
        #region 渲染

        /// <summary>
        /// 将组件渲染为字符串
        /// </summary>
        /// <param name="componentType">组件类型</param>
        /// <param name="arguments">参数</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含结果
        /// </returns>
        protected virtual async Task<string> RenderViewComponentToStringAsync(Type componentType, object arguments = null)
        {
            var helper = new DefaultViewComponentHelper(
                EngineContext.Current.Resolve<IViewComponentDescriptorCollectionProvider>(),
                HtmlEncoder.Default,
                EngineContext.Current.Resolve<IViewComponentSelector>(),
                EngineContext.Current.Resolve<IViewComponentInvokerFactory>(),
                EngineContext.Current.Resolve<IViewBufferScope>());

            using var writer = new StringWriter();
            var context = new ViewContext(ControllerContext, NullView.Instance, ViewData, TempData, writer, new HtmlHelperOptions());
            helper.Contextualize(context);
            var result = await helper.InvokeAsync(componentType, arguments);
            result.WriteTo(writer, HtmlEncoder.Default);
            await writer.FlushAsync();
            return writer.ToString();
        }

        /// <summary>
        /// 将局部视图渲染为字符串
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="model">模型</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含结果
        /// </returns>
        protected virtual async Task<string> RenderPartialViewToStringAsync(string viewName, object model)
        {
            // 获取Razor视图引擎
            var razorViewEngine = EngineContext.Current.Resolve<IRazorViewEngine>();

            // 创建操作上下文
            var actionContext = new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor, ModelState);

            // 如果未传递，则将视图名称设置为操作名称
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.ActionDescriptor.ActionName;

            // 设置模型
            ViewData.Model = model;

            // 尝试按名称获取视图
            var viewResult = razorViewEngine.FindView(actionContext, viewName, false);
            if (viewResult.View == null)
            {
                // 或尝试通过路径查看
                viewResult = razorViewEngine.GetView(null, viewName, false);
                if (viewResult.View == null)
                    throw new ArgumentNullException($"{viewName} view was not found");
            }
            await using var stringWriter = new StringWriter();
            var viewContext = new ViewContext(actionContext, viewResult.View, ViewData, TempData, stringWriter, new HtmlHelperOptions());

            await viewResult.View.RenderAsync(viewContext);
            return stringWriter.GetStringBuilder().ToString();
        }

        #endregion

        #region 消息

        /// <summary>
        /// 错误的JSON数据
        /// </summary>
        /// <param name="error">错误文本</param>
        /// <returns>错误的JSON数据</returns>
        protected JsonResult ErrorJson(string error)
        {
            return Json(new
            {
                error
            });
        }

        /// <summary>
        /// 错误的JSON数据
        /// </summary>
        /// <param name="errors">错误提示</param>
        /// <returns>错误的JSON数据</returns>
        protected JsonResult ErrorJson(object errors)
        {
            return Json(new
            {
                error = errors
            });
        }

        /// <summary>
        /// 显示“编辑”（管理）链接（在公共站点中）
        /// </summary>
        /// <param name="editPageUrl">编辑页面URL</param>
        protected virtual void DisplayEditLink(string editPageUrl)
        {
            var dhHtmlHelper = EngineContext.Current.Resolve<IDHHtmlHelper>();

            dhHtmlHelper.AddEditPageUrl(editPageUrl);
        }

        #endregion

        #region 本地化

        /// <summary>
        /// 为可本地化实体添加区域设置
        /// </summary>
        /// <typeparam name="TLocalizedModelLocal">Localizable model</typeparam>
        /// <param name="locales">Locales</param>
        protected virtual void AddLocales<TLocalizedModelLocal>(IList<TLocalizedModelLocal> locales) where TLocalizedModelLocal : ILocalizedLocaleModel
        {
            AddLocales(locales, null);
        }

        /// <summary>
        /// Add locales for localizable entities
        /// </summary>
        /// <typeparam name="TLocalizedModelLocal">Localizable model</typeparam>
        /// <param name="locales">Locales</param>
        /// <param name="configure">Configure action</param>
        protected virtual void AddLocales<TLocalizedModelLocal>(IList<TLocalizedModelLocal> locales, Action<TLocalizedModelLocal, int> configure) where TLocalizedModelLocal : ILocalizedLocaleModel
        {
            var list = Language.GetAllLanguages(showHidden: true);
            foreach (var language in list)
            {
                var locale = Activator.CreateInstance<TLocalizedModelLocal>();
                locale.LanguageId = language.Id;

                if (configure != null)
                    configure.Invoke(locale, locale.LanguageId);

                locales.Add(locale);
            }
        }


        #endregion

        #region Security

        /// <summary>
        /// Access denied view
        /// </summary>
        /// <returns>Access denied view</returns>
        protected virtual IActionResult AccessDeniedView()
        {
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();

            //return Challenge();
            return RedirectToAction("AccessDenied", "Security", new { pageUrl = webHelper.GetRawUrl(Request) });
        }

        /// <summary>
        /// Access denied JSON data for DataTables
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the access denied JSON data
        /// </returns>
        protected async Task<JsonResult> AccessDeniedDataTablesJson()
        {
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();

            return ErrorJson(localizationService.GetResource("Admin.AccessDenied.Description"));
        }

        #endregion

        #region Cards and tabs

        /// <summary>
        /// Save selected card name
        /// </summary>
        /// <param name="cardName">Card name to save</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request. Pass null to ignore</param>
        public virtual void SaveSelectedCardName(string cardName, bool persistForTheNextRequest = true)
        {
            //keep this method synchronized with
            //"GetSelectedCardName" method of \DH.Web.Framework\Extensions\HtmlExtensions.cs
            if (string.IsNullOrEmpty(cardName))
                throw new ArgumentNullException(nameof(cardName));

            const string dataKey = "dh.selected-card-name";
            if (persistForTheNextRequest)
            {
                TempData[dataKey] = cardName;
            }
            else
            {
                ViewData[dataKey] = cardName;
            }
        }

        /// <summary>
        /// Save selected tab name
        /// </summary>
        /// <param name="tabName">Tab name to save; empty to automatically detect it</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request. Pass null to ignore</param>
        public virtual void SaveSelectedTabName(string tabName = "", bool persistForTheNextRequest = true)
        {
            //default root tab
            SaveSelectedTabName(tabName, "selected-tab-name", null, persistForTheNextRequest);
            //child tabs (usually used for localization)
            //Form is available for POST only
            if (!Request.Method.Equals(WebRequestMethods.Http.Post, StringComparison.InvariantCultureIgnoreCase))
                return;

            foreach (var key in Request.Form.Keys)
                if (key.StartsWith("selected-tab-name-", StringComparison.InvariantCultureIgnoreCase))
                    SaveSelectedTabName(null, key, key["selected-tab-name-".Length..], persistForTheNextRequest);
        }

        /// <summary>
        /// Save selected tab name
        /// </summary>
        /// <param name="tabName">Tab name to save; empty to automatically detect it</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request. Pass null to ignore</param>
        /// <param name="formKey">Form key where selected tab name is stored</param>
        /// <param name="dataKeyPrefix">A prefix for child tab to process</param>
        protected virtual void SaveSelectedTabName(string tabName, string formKey, string dataKeyPrefix, bool persistForTheNextRequest)
        {
            // 保持此方法与
            // \DH.Web.Framework\Extensions\HtmlExtensions.cs中的方法"GetSelectedTabName"
            if (string.IsNullOrEmpty(tabName))
            {
                tabName = Request.Form[formKey];
            }

            if (string.IsNullOrEmpty(tabName))
                return;

            var dataKey = "dh.selected-tab-name";
            if (!string.IsNullOrEmpty(dataKeyPrefix))
                dataKey += $"-{dataKeyPrefix}";

            if (persistForTheNextRequest)
            {
                TempData[dataKey] = tabName;
            }
            else
            {
                ViewData[dataKey] = tabName;
            }
        }

        #endregion
    }
}
