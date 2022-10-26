using DH.Web.Framework.Mvc.Filters;

namespace DH.Web.Framework.Controllers
{
    /// <summary>
    /// 插件的基本控制器
    /// </summary>
    [NotNullValidationMessage]
    public abstract partial class BasePluginController : BaseController
    {
    }
}
