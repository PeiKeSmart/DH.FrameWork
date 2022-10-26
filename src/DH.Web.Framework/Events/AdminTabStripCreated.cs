using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DH.Web.Framework.Events
{
    /// <summary>
    /// 管理选项卡条创建的事件
    /// </summary>
    public partial class AdminTabStripCreated
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="helper">HTML Helper</param>
        /// <param name="tabStripName">Tabstrip name</param>
        public AdminTabStripCreated(IHtmlHelper helper, string tabStripName)
        {
            Helper = helper;
            TabStripName = tabStripName;
            BlocksToRender = new List<IHtmlContent>();
        }

        /// <summary>
        /// HTML Helper
        /// </summary>
        public IHtmlHelper Helper { get; private set; }
        /// <summary>
        /// TabStripName
        /// </summary>
        public string TabStripName { get; private set; }
        /// <summary>
        /// Blocks to render
        /// </summary>
        public IList<IHtmlContent> BlocksToRender { get; set; }
    }
}
