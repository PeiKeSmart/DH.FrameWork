using DH.Swagger;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace DH.AspNetCore.MVC;

/// <summary>
/// 自定义路由 /api/{version}/[controler]/[action]
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class DHCustomRouteAttribute : RouteAttribute, IApiDescriptionGroupNameProvider {
    /// <summary>
    /// 分组名称,是来实现接口 IApiDescriptionGroupNameProvider
    /// </summary>
    public String GroupName { get; set; }

    /// <summary>
    /// 自定义路由构造函数，继承基类路由
    /// </summary>
    /// <param name="version"></param>
    public DHCustomRouteAttribute(String version) : base($"/Api/{version}/[controller]/[action]")
    {
    }

    /// <summary>
    /// 自定义版本+路由构造函数，继承基类路由
    /// </summary>
    /// <param name="actionName"></param>
    /// <param name="version"></param>
    public DHCustomRouteAttribute(String version, string actionName = "") : base($"/Api/{version}/[controller]/{actionName}")
    {
        GroupName = version;
    }

    /// <summary>
    /// 自定义版本+路由构造函数，继承基类路由
    /// </summary>
    /// <param name="actionName"></param>
    /// <param name="version"></param>
    /// <param name="PrefixName"></param>
    public DHCustomRouteAttribute(String version, string PrefixName, string actionName = "") : base($"/Api/{PrefixName}/{version}/[controller]/{actionName}")
    {
        GroupName = version;
    }
}
