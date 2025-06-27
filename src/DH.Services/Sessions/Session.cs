using IdentityModel;

using Pek;

namespace DH.Services.Sessions;

/// <summary>
/// 用户会话
/// </summary>
public class Session : ISession {
    /// <summary>
    /// 用户标识
    /// </summary>
    public string UserId
    {
        get
        {
            var result = Pek.Helpers.DHWeb.Identity.GetValue(JwtClaimTypes.Subject);
            return string.IsNullOrWhiteSpace(result)
                ? Pek.Helpers.DHWeb.Identity.GetValue(System.Security.Claims.ClaimTypes.NameIdentifier)
                : result;
        }
    }

    /// <summary>
    /// 是否认证
    /// </summary>
    public bool IsAuthenticated => Pek.Helpers.DHWeb.Identity.IsAuthenticated;

    /// <summary>
    /// 用户会话
    /// </summary>
    public static readonly ISession Instance = new Session();

    /// <summary>
    /// 空用户会话
    /// </summary>
    public static readonly ISession Null = NullSession.Instance;
}