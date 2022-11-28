using System.Security.Claims;
using System.Security.Principal;

namespace DH.Security.Principals;

/// <summary>
/// 未认证安全主体
/// </summary>
public class UnauthenticatedPrincipal : ClaimsPrincipal
{
    /// <summary>
    /// 身份标识s
    /// </summary>
    public override IIdentity Identity => UnauthenticatedIdentity.Instance;

    /// <summary>
    /// 初始化一个<see cref="UnauthenticatedPrincipal"/>类型的实例
    /// </summary>
    private UnauthenticatedPrincipal() { }

    /// <summary>
    /// 未认证安全主体
    /// </summary>
    public static readonly UnauthenticatedPrincipal Instance = new UnauthenticatedPrincipal();
}