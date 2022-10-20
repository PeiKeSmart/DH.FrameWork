using Microsoft.AspNetCore.Mvc;

namespace HelloWorldPlugin.Controllers
{
    public class TestController : Controller
    {
        public IActionResult HelloWorld()
        {
            return View("~/Plugins/HelloWorldPlugin/Views/HelloWorld.cshtml");
        }
    }
}
