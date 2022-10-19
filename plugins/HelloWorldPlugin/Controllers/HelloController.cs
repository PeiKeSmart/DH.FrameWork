using Microsoft.AspNetCore.Mvc;

using PluginCore;

namespace HelloWorldPlugin.Controllers
{
    [Route("api/plugins/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {

        public ActionResult Get()
        {
            SettingsModel settingsModel = PluginSettingsModelFactory.Create<SettingsModel>("HelloWorldPlugin");
            string str = $"Hello PluginCore ! {settingsModel.Hello}";

            return Ok(str);
        }

    }
}
