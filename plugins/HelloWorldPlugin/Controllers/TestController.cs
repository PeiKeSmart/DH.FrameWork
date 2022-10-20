using Microsoft.AspNetCore.Mvc;

namespace HelloWorldPlugin.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Plugins/HelloWorldPlugin/Views/Test.cshtml");
        }
    }
}
