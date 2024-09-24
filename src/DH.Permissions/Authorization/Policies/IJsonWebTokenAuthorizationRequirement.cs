using Microsoft.AspNetCore.Authorization;

using Pek.Security;

namespace DH.Permissions.Authorization.Policies;

/// <summary>
/// JWT授权请求
/// </summary>
public interface IJsonWebTokenAuthorizationRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// 设置校验函数
    /// </summary>
    /// <param name="func">校验函数</param>
    IJsonWebTokenAuthorizationRequirement SetValidateFunc(
        Func<IDictionary<string, string>, JwtOptions, bool> func);
}
