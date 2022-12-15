using DH.Core.Domain.Localization;
using DH.Core.Infrastructure;
using DH.Web.Framework.Themes;

using Microsoft.AspNetCore.Mvc;

using NewLife.Log;

using ScuiDemo.Models;

using System.Diagnostics;

namespace ScuiDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var localizationSettings = EngineContext.Current.Resolve<LocalizationSettings>();
            XTrace.WriteLine($"测试获取数据：{localizationSettings.SeoFriendlyUrlsForLanguagesEnabled}");

            var ThemeContext = EngineContext.Current.Resolve<IThemeContext>();
            XTrace.WriteLine($"测试获取数据：{ThemeContext.GetWorkingThemeNameAsync().Result}");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}