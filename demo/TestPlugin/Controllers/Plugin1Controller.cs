using DH.Web.Framework.Controllers;

using Microsoft.AspNetCore.Mvc;

namespace TestPlugin.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class Plugin1Controller : BasePluginController
    {
        public IActionResult HelloWorld()
        {
            return View("~/Plugins/TestPlugin/Views/HelloWorld.cshtml");
        }
    }
}
