using Microsoft.AspNetCore.Mvc;

namespace NopDemo.Controllers
{
    [AutoValidateAntiforgeryToken]
    public partial class CommonController : BasePublicController
    {
        // 找不到网页
        public virtual IActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            Response.ContentType = "text/html";

            return View();
        }
    }
}
