using DH.Core;
using DH.Core.Domain.Catalog;
using DH.Core.Domain.Localization;
using DH.Core.Events;
using DH.Core.Http;
using DH.Entity;
using DH.Services.Seo;
using DH.Web.Framework.Events;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace DH.Web.Framework.Mvc.Routing
{
    /// <summary>
    /// 表示分段路由处理器
    /// </summary>
    public partial class SlugRouteTransformer : DynamicRouteValueTransformer
    {
        #region Fields

        private readonly CatalogSettings _catalogSettings;
        private readonly IEventPublisher _eventPublisher;
        private readonly IStoreContext _storeContext;
        private readonly IUrlRecordService _urlRecordService;
        private readonly LocalizationSettings _localizationSettings;

        #endregion

        #region Ctor

        public SlugRouteTransformer(CatalogSettings catalogSettings,
            IEventPublisher eventPublisher,
            IStoreContext storeContext,
            IUrlRecordService urlRecordService,
            LocalizationSettings localizationSettings)
        {
            _catalogSettings = catalogSettings;
            _eventPublisher = eventPublisher;
            _storeContext = storeContext;
            _urlRecordService = urlRecordService;
            _localizationSettings = localizationSettings;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 根据传递的URL记录转换路由值
        /// </summary>
        /// <param name="httpContext">HTTP上下文</param>
        /// <param name="values">与当前匹配项关联的路由值</param>
        /// <param name="urlRecord">URL slug找到的记录</param>
        /// <param name="catalogPath">URL目录路径</param>
        /// <returns>表示异步操作的任务</returns>
        protected virtual void SingleSlugRouting(HttpContext httpContext, RouteValueDictionary values, UrlRecord urlRecord, string catalogPath)
        {
            // 如果URL记录未激活，让我们查找最新的记录
            var slug = urlRecord.IsActive
                ? urlRecord.Slug
                : _urlRecordService.GetActiveSlug(urlRecord.EntityId, urlRecord.EntityName, urlRecord.LanguageId);
            if (string.IsNullOrEmpty(slug))
                return;

            if (!urlRecord.IsActive || !string.IsNullOrEmpty(catalogPath))
            {
                // 永久重定向到具有活动单段的新URL
                InternalRedirect(httpContext, values, $"/{slug}", true);
                return;
            }

            // 确保slug与当前语言相同，
            // 否则，当客户选择一种新的语言，但slug保持不变时，它可能会导致一些问题
            if (_localizationSettings.SeoFriendlyUrlsForLanguagesEnabled && values.TryGetValue(DHRoutingDefaults.RouteValue.Language, out var langValue))
            {
                var store = _storeContext.GetCurrentStore();
                var languages = Language.GetAllLanguages();
                var language = languages
                    .FirstOrDefault(lang => lang.Status && lang.UniqueSeoCode.Equals(langValue?.ToString(), StringComparison.InvariantCultureIgnoreCase))
                    ?? languages.FirstOrDefault();

                var slugLocalized = _urlRecordService.GetActiveSlug(urlRecord.EntityId, urlRecord.EntityName, language.Id);
                if (!string.IsNullOrEmpty(slugLocalized) && !slugLocalized.Equals(slug, StringComparison.InvariantCultureIgnoreCase))
                {
                    // 我们应该在上面进行验证，因为一些实体没有标准（Id=0）语言的SeName（例如新闻、博客帖子）

                    // 重定向到当前语言的页面
                    InternalRedirect(httpContext, values, $"/{language.UniqueSeoCode}/{slugLocalized}", false);
                    return;
                }
            }

            // 既然我们在这里，slug就没问题了，所以处理URL
            switch (urlRecord.EntityName)
            {
                //case var name when name.Equals(nameof(Product), StringComparison.InvariantCultureIgnoreCase):
                //    RouteToAction(values, "Product", "ProductDetails", slug, (DHRoutingDefaults.RouteValue.ProductId, urlRecord.EntityId));
                //    return;

                //case var name when name.Equals(nameof(ProductTag), StringComparison.InvariantCultureIgnoreCase):
                //    RouteToAction(values, "Catalog", "ProductsByTag", slug, (NopRoutingDefaults.RouteValue.ProductTagId, urlRecord.EntityId));
                //    return;

                //case var name when name.Equals(nameof(Category), StringComparison.InvariantCultureIgnoreCase):
                //    RouteToAction(values, "Catalog", "Category", slug, (NopRoutingDefaults.RouteValue.CategoryId, urlRecord.EntityId));
                //    return;

                //case var name when name.Equals(nameof(Manufacturer), StringComparison.InvariantCultureIgnoreCase):
                //    RouteToAction(values, "Catalog", "Manufacturer", slug, (NopRoutingDefaults.RouteValue.ManufacturerId, urlRecord.EntityId));
                //    return;

                //case var name when name.Equals(nameof(Vendor), StringComparison.InvariantCultureIgnoreCase):
                //    RouteToAction(values, "Catalog", "Vendor", slug, (NopRoutingDefaults.RouteValue.VendorId, urlRecord.EntityId));
                //    return;

                //case var name when name.Equals(nameof(NewsItem), StringComparison.InvariantCultureIgnoreCase):
                //    RouteToAction(values, "News", "NewsItem", slug, (NopRoutingDefaults.RouteValue.NewsItemId, urlRecord.EntityId));
                //    return;

                //case var name when name.Equals(nameof(BlogPost), StringComparison.InvariantCultureIgnoreCase):
                //    RouteToAction(values, "Blog", "BlogPost", slug, (NopRoutingDefaults.RouteValue.BlogPostId, urlRecord.EntityId));
                //    return;

                //case var name when name.Equals(nameof(Topic), StringComparison.InvariantCultureIgnoreCase):
                //    RouteToAction(values, "Topic", "TopicDetails", slug, (NopRoutingDefaults.RouteValue.TopicId, urlRecord.EntityId));
                //    return;
            }
        }

        ///// <summary>
        ///// 尝试转换路由值，假设传递的URL记录是产品类型
        ///// </summary>
        ///// <param name="httpContext">HTTP context</param>
        ///// <param name="values">The route values associated with the current match</param>
        ///// <param name="urlRecord">Record found by the URL slug</param>
        ///// <param name="catalogPath">URL catalog path</param>
        ///// <returns>
        ///// A task that represents the asynchronous operation
        ///// The task result contains a value whether the route values were processed
        ///// </returns>
        //protected virtual async Task<bool> TryProductCatalogRoutingAsync(HttpContext httpContext, RouteValueDictionary values, UrlRecord urlRecord, string catalogPath)
        //{
        //    //ensure it's a product URL record
        //    if (!urlRecord.EntityName.Equals(nameof(Product), StringComparison.InvariantCultureIgnoreCase))
        //        return false;

        //    //if the product URL structure type is product seName only, it will be processed later by a single slug
        //    if (_catalogSettings.ProductUrlStructureTypeId == (int)ProductUrlStructureType.Product)
        //        return false;

        //    //get active slug for the product
        //    var slug = urlRecord.IsActive
        //        ? urlRecord.Slug
        //        : await _urlRecordService.GetActiveSlugAsync(urlRecord.EntityId, urlRecord.EntityName, urlRecord.LanguageId);
        //    if (string.IsNullOrEmpty(slug))
        //        return false;

        //    //try to get active catalog (e.g. category or manufacturer) seName for the product
        //    var catalogSeName = string.Empty;
        //    var isCategoryProductUrl = _catalogSettings.ProductUrlStructureTypeId == (int)ProductUrlStructureType.CategoryProduct;
        //    if (isCategoryProductUrl)
        //    {
        //        var productCategory = (await _categoryService.GetProductCategoriesByProductIdAsync(urlRecord.EntityId)).LastOrDefault();
        //        var category = await _categoryService.GetCategoryByIdAsync(productCategory?.CategoryId ?? 0);
        //        catalogSeName = category is not null ? await _urlRecordService.GetSeNameAsync(category) : string.Empty;
        //    }
        //    var isManufacturerProductUrl = _catalogSettings.ProductUrlStructureTypeId == (int)ProductUrlStructureType.ManufacturerProduct;
        //    if (isManufacturerProductUrl)
        //    {
        //        var productManufacturer = (await _manufacturerService.GetProductManufacturersByProductIdAsync(urlRecord.EntityId)).FirstOrDefault();
        //        var manufacturer = await _manufacturerService.GetManufacturerByIdAsync(productManufacturer?.ManufacturerId ?? 0);
        //        catalogSeName = manufacturer is not null ? await _urlRecordService.GetSeNameAsync(manufacturer) : string.Empty;
        //    }
        //    if (string.IsNullOrEmpty(catalogSeName))
        //        return false;

        //    //get URL record by the specified catalog path
        //    var catalogUrlRecord = await _urlRecordService.GetBySlugAsync(catalogPath);
        //    if (catalogUrlRecord is null ||
        //        (isCategoryProductUrl && !catalogUrlRecord.EntityName.Equals(nameof(Category), StringComparison.InvariantCultureIgnoreCase)) ||
        //        (isManufacturerProductUrl && !catalogUrlRecord.EntityName.Equals(nameof(Manufacturer), StringComparison.InvariantCultureIgnoreCase)) ||
        //        !urlRecord.IsActive)
        //    {
        //        //permanent redirect to new URL with active catalog seName and active slug
        //        InternalRedirect(httpContext, values, $"/{catalogSeName}/{slug}", true);
        //        return true;
        //    }

        //    //ensure the catalog seName and slug are the same for the current language
        //    if (_localizationSettings.SeoFriendlyUrlsForLanguagesEnabled && values.TryGetValue(NopRoutingDefaults.RouteValue.Language, out var langValue))
        //    {
        //        var store = await _storeContext.GetCurrentStoreAsync();
        //        var languages = await _languageService.GetAllLanguagesAsync(storeId: store.Id);
        //        var language = languages
        //            .FirstOrDefault(lang => lang.Published && lang.UniqueSeoCode.Equals(langValue?.ToString(), StringComparison.InvariantCultureIgnoreCase))
        //            ?? languages.FirstOrDefault();

        //        var slugLocalized = await _urlRecordService.GetActiveSlugAsync(urlRecord.EntityId, urlRecord.EntityName, language.Id);
        //        var catalogSlugLocalized = await _urlRecordService.GetActiveSlugAsync(catalogUrlRecord.EntityId, catalogUrlRecord.EntityName, language.Id);
        //        if ((!string.IsNullOrEmpty(slugLocalized) && !slugLocalized.Equals(slug, StringComparison.InvariantCultureIgnoreCase)) ||
        //            (!string.IsNullOrEmpty(catalogSlugLocalized) && !catalogSlugLocalized.Equals(catalogUrlRecord.Slug, StringComparison.InvariantCultureIgnoreCase)))
        //        {
        //            //redirect to localized URL for the current language
        //            var activeSlug = !string.IsNullOrEmpty(slugLocalized) ? slugLocalized : slug;
        //            var activeCatalogSlug = !string.IsNullOrEmpty(catalogSlugLocalized) ? catalogSlugLocalized : catalogUrlRecord.Slug;
        //            InternalRedirect(httpContext, values, $"/{language.UniqueSeoCode}/{activeCatalogSlug}/{activeSlug}", false);
        //            return true;
        //        }
        //    }

        //    //ensure the specified catalog path is equal to the active catalog seName
        //    //we do it here after localization check to avoid double redirect
        //    if (!catalogSeName.Equals(catalogUrlRecord.Slug, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        //permanent redirect to new URL with active catalog seName and active slug
        //        InternalRedirect(httpContext, values, $"/{catalogSeName}/{slug}", true);
        //        return true;
        //    }

        //    //all is ok, so select the appropriate action
        //    RouteToAction(values, "Product", "ProductDetails", slug,
        //        (NopRoutingDefaults.RouteValue.ProductId, urlRecord.EntityId), (NopRoutingDefaults.RouteValue.CatalogSeName, catalogSeName));
        //    return true;
        //}

        /// <summary>
        /// Transform route values to redirect the request
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <param name="values">The route values associated with the current match</param>
        /// <param name="path">Path</param>
        /// <param name="permanent">Whether the redirect should be permanent</param>
        protected virtual void InternalRedirect(HttpContext httpContext, RouteValueDictionary values, string path, bool permanent)
        {
            values[DHRoutingDefaults.RouteValue.Controller] = "Common";
            values[DHRoutingDefaults.RouteValue.Action] = "InternalRedirect";
            values[DHRoutingDefaults.RouteValue.Url] = $"{httpContext.Request.PathBase}{path}{httpContext.Request.QueryString}";
            values[DHRoutingDefaults.RouteValue.PermanentRedirect] = permanent;
            httpContext.Items[DHHttpDefaults.GenericRouteInternalRedirect] = true;
        }

        /// <summary>
        /// Transform route values to set controller, action and action parameters
        /// </summary>
        /// <param name="values">The route values associated with the current match</param>
        /// <param name="controller">Controller name</param>
        /// <param name="action">Action name</param>
        /// <param name="slug">URL slug</param>
        /// <param name="parameters">Action parameters</param>
        protected virtual void RouteToAction(RouteValueDictionary values, string controller, string action, string slug, params (string Key, object Value)[] parameters)
        {
            values[DHRoutingDefaults.RouteValue.Controller] = controller;
            values[DHRoutingDefaults.RouteValue.Action] = action;
            values[DHRoutingDefaults.RouteValue.SeName] = slug;
            foreach (var (key, value) in parameters)
            {
                values[key] = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 创建一组转换后的路由值，用于选择操作
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <param name="routeValues">The route values associated with the current match</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the set of values
        /// </returns>
        public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary routeValues)
        {
            //get values to transform for action selection
            var values = new RouteValueDictionary(routeValues);
            if (values is null)
                return values;

            if (!values.TryGetValue(DHRoutingDefaults.RouteValue.SeName, out var slug))
                return values;

            // 通过URL slug查找记录
            if (_urlRecordService.GetBySlug(slug.ToString()) is not UrlRecord urlRecord)
                return values;

            // 允许第三方处理程序根据找到的URL记录选择操作
            var routingEvent = new GenericRoutingEvent(httpContext, values, urlRecord);
            await _eventPublisher.PublishAsync(routingEvent);
            if (routingEvent.Handled)
                return values;

            // 然后尝试根据找到的URL记录和目录路径选择操作
            var catalogPath = values.TryGetValue(DHRoutingDefaults.RouteValue.CatalogSeName, out var catalogPathValue)
                ? catalogPathValue.ToString()
                : string.Empty;
            //if (await TryProductCatalogRoutingAsync(httpContext, values, urlRecord, catalogPath))
            //    return values;

            // 最后，仅通过URL记录选择操作
            SingleSlugRouting(httpContext, values, urlRecord, catalogPath);

            return values;
        }

        #endregion
    }
}
