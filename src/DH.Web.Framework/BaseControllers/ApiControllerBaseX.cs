using DH.Web.Framework.Mvc.Filters;

using Microsoft.AspNetCore.Mvc;

namespace DH.Web.Framework;

/// <summary>
/// WebApi控制器
/// </summary>
[ApiController]
[HttpsRequirement]
public class ApiControllerBaseX : ControllerBaseX
{
}
