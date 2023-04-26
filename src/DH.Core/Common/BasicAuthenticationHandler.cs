using DH.Entity;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NewLife;
using NewLife.Common;

using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace DH.Common;

public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions> {
    public BasicAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock)
    { }

    /// <summary>
    /// 标记是否认证成功以及返回认证过后的票据
    /// </summary>
    /// <returns></returns>
    protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.NoResult();

        string authHeader = Request.Headers["Authorization"];
        if (!authHeader.StartsWith("Basic ", System.StringComparison.OrdinalIgnoreCase))
            return AuthenticateResult.NoResult();

        string token = authHeader.Substring("Basic ".Length).Trim();
        string credentialString = Encoding.UTF8.GetString(System.Convert.FromBase64String(token));
        string[] credentials = credentialString.Split(':');

        if (credentials.Length != 2)
            return AuthenticateResult.Fail("More than two strings seperated by colons found");

        ClaimsPrincipal principal = await Task.Run(() => Options.SignIn(credentials[0], credentials[1]));

        if (principal != null)
        {
            AuthenticationTicket ticket = new AuthenticationTicket(principal, new AuthenticationProperties(), BasicAuthenticationDefaults.AuthenticationScheme);
            return AuthenticateResult.Success(ticket);
        }

        return AuthenticateResult.Fail("Wrong credentials supplied");
    }

    /// <summary>
    /// 用来处理禁用等结果
    /// </summary>
    /// <param name="properties"></param>
    /// <returns></returns>
    protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        Response.StatusCode = 403;
        return base.HandleForbiddenAsync(properties);
    }

    /// <summary>
    /// 接收上一步中传递的认证参数
    /// </summary>
    /// <param name="properties"></param>
    /// <returns></returns>
    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.StatusCode = 401;
        string headerValue = $"{BasicAuthenticationDefaults.AuthenticationScheme} realm=\"{Options.Realm}\"";
        Response.Headers.Append(Microsoft.Net.Http.Headers.HeaderNames.WWWAuthenticate, headerValue);
        return base.HandleChallengeAsync(properties);
    }
}

public class BasicAuthenticationOptions : AuthenticationSchemeOptions, IOptions<BasicAuthenticationOptions> {
    public IServiceCollection ServiceCollection { get; set; }

    public BasicAuthenticationOptions Value => this;

    public string Realm { get; set; }

    public ClaimsPrincipal SignIn(string userName, string password)
    {
        var model = UserE.FindByName(userName);
        if (model == null)
        {
            return null;
        }
        if (model.Password != password.MD5())
        {
            return null;
        }
        var identity = new ClaimsIdentity(BasicAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
        identity.AddClaim(new Claim(ClaimTypes.Name, model.Name));
        identity.AddClaim(new Claim(ClaimTypes.Sid, model.ID.ToString()));
        var principal = new ClaimsPrincipal(identity);
        return principal;
    }
}

public static class BasicAuthenticationDefaults {
    public const string AuthenticationScheme = "Basic";
}

public static class BasicAuthenticationExtensions {
    public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder)
        => builder.AddBasic(BasicAuthenticationDefaults.AuthenticationScheme, _ => { _.ServiceCollection = builder.Services; _.Realm = SysConfig.Current.Name; });

    public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, System.Action<BasicAuthenticationOptions> configureOptions)
        => builder.AddBasic(BasicAuthenticationDefaults.AuthenticationScheme, configureOptions);

    public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, string authenticationScheme, System.Action<BasicAuthenticationOptions> configureOptions)
        => builder.AddBasic(authenticationScheme, displayName: null, configureOptions: configureOptions);

    public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, string authenticationScheme, string displayName, System.Action<BasicAuthenticationOptions> configureOptions)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptions<BasicAuthenticationOptions>, BasicAuthenticationOptions>());
        return builder.AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
    }
}