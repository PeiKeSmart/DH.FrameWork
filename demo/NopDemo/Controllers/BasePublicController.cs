using DH.Web.Framework.Controllers;

using Microsoft.AspNetCore.Mvc;

namespace NopDemo.Controllers
{
    public abstract partial class BasePublicController : BaseController
    {
        protected virtual IActionResult InvokeHttp404()
        {
            Response.StatusCode = 404;
            return new EmptyResult();
        }
    }
}
