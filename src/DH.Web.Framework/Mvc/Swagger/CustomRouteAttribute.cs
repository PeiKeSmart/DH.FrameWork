using DH.AspNetCore;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace DH.Web.Framework.Mvc.Swagger;

/// <summary>
/// 自定义路由 /api/{version}/[controler]/[action]
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class CustomRouteAttribute : RouteAttribute, IApiDescriptionGroupNameProvider
{

    /// <summary>
    /// 分组名称,是来实现接口 IApiDescriptionGroupNameProvider
    /// </summary>
    public String GroupName { get; set; }

    /// <summary>
    /// 自定义路由构造函数，继承基类路由
    /// </summary>
    /// <param name="actionName"></param>
    public CustomRouteAttribute(string actionName = "[action]") : base("/api/{version}/[controller]/" + actionName)
    {
    }

    /// <summary>
    /// 自定义版本+路由构造函数，继承基类路由
    /// </summary>
    /// <param name="actionName"></param>
    /// <param name="version"></param>
    public CustomRouteAttribute(ApiVersions version, string actionName = "") : base($"/api/{version}/[controller]/{actionName}")
    {
        GroupName = version.ToString();
    }

    /// <summary>
    /// 自定义版本+路由构造函数，继承基类路由
    /// </summary>
    /// <param name="actionName"></param>
    /// <param name="version"></param>
    /// <param name="PrefixName"></param>
    public CustomRouteAttribute(ApiVersions version, string PrefixName, string actionName = "") : base($"/api/{PrefixName}/{version}/[controller]/{actionName}")
    {
        GroupName = version.ToString();
    }
}