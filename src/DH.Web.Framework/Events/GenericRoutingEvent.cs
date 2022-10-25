using DH.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DH.Web.Framework.Events
{
    /// <summary>
    /// 表示在处理通用路由时和默认转换之前发生的事件
    /// </summary>
    public partial class GenericRoutingEvent
    {
        #region Ctor

        public GenericRoutingEvent(HttpContext httpContext, RouteValueDictionary values, UrlRecord urlRecord)
        {
            HttpContext = httpContext;
            RouteValues = values;
            UrlRecord = urlRecord;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取HTTP上下文
        /// </summary>
        public HttpContext HttpContext { get; private set; }

        /// <summary>
        /// 获取与当前匹配项关联的路由值
        /// </summary>
        public RouteValueDictionary RouteValues { get; private set; }

        /// <summary>
        /// 获取URL slug找到的记录
        /// </summary>
        public UrlRecord UrlRecord { get; private set; }

        /// <summary>
        /// 获取一个值，该值指示事件是否已处理，并且应在不进行进一步处理的情况下使用值
        /// </summary>
        public bool Handled { get; set; }

        #endregion
    }
}
