using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

using NewLife.Log;
using NewLife;
using DH.Entity;

namespace DH.Web.Framework.Mvc.Routing
{
    public class TranslationTransformer : DynamicRouteValueTransformer
    {
        public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            if (values == null)
            {
                values = new RouteValueDictionary();

                var model = DynamicRoute.FindByRegexInfo("/");
                if (model != null && model.Enable)
                {
                    values["controller"] = model.Controller;
                    values["action"] = model.Action;
                    values["area"] = model.Area;
                }
            }

            return new ValueTask<RouteValueDictionary>(values);
        }
    }
}
