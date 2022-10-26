using DH.Web.Framework.Components;

using Microsoft.AspNetCore.Mvc;

namespace TestPlugin.Components
{
    [ViewComponent(Name = "WidgetsNivoSlider")]
    public class WidgetsNivoSliderViewComponent : DHViewComponent
    {
        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            return View("~/Plugins/TestPlugin/Views/PublicInfo.cshtml");
        }
    }
}
