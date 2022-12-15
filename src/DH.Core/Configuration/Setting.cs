using DH.Extensions;
using DH.Security;

using NewLife.Security;
using NewLife.Xml;

using System.ComponentModel;

namespace DH;

/// <summary>基类设置</summary>
[DisplayName("基类设置")]
[XmlConfigFile("Config/DHSetting.config", 10000)]
public class DHSetting : XmlConfig<DHSetting>
{
    /// <summary>是否启用调试。默认true</summary>
    [Description("调试")]
    [Category("通用")]
    public Boolean Debug { get; set; } = true;

    /// <summary>系统初始化控制参数</summary>
    [Description("系统初始化控制参数,系统是否安装,true：已安装，false：未安装")]
    public Boolean IsInstalled { get; set; }

    /// <summary>
    /// 是否允许限流
    /// </summary>
    [Description("是否允许限流")]
    public Boolean AllowRateLimter { get; set; }

    /// <summary>
    /// 是否全站启用SSL
    /// </summary>
    /// <summary>是否全站启用SSL</summary>
    [Description("是否全站启用SSL")]
    public Boolean AllSslEnabled { get; set; }

    /// <summary>
    /// API接口是否启用SSL
    /// </summary>
    /// <summary>API接口是否启用SSL</summary>
    [Description("API接口是否启用SSL")]
    public Boolean SslEnabled { get; set; }

    /// <summary>
    /// 流量统计代码
    /// </summary>
    [Description("流量统计代码")]
    public String Statistical { get; set; }

    /// <summary>工作台页面。进入后台的第一个内容页</summary>
    [Description("工作台页面。进入后台的第一个内容页")]
    [Category("界面配置")]
    public String StartPage { get; set; }

    /// <summary>默认角色。默认普通用户</summary>
    [Description("默认角色。默认普通用户")]
    [Category("用户登录")]
    public String DefaultRole { get; set; } = "普通用户";

    /// <summary>密码强度。*表示无限制，默认8位起，数字大小写字母和符号</summary>
    [Description("密码强度。*表示无限制，默认8位起，数字大小写字母和符号")]
    [Category("用户登录")]
    public String PaswordStrength { get; set; } = @"^(?=.*\d.*)(?=.*[a-z].*)(?=.*[A-Z].*)(?=.*[^(0-9a-zA-Z)].*).{8,32}$";

    /// <summary>登录失败次数。短时间内，相同用户或IP地址连续登录错误次数达到该值后禁止登录或提供验证码，默认6</summary>
    [Description("登录失败次数。短时间内，相同用户或IP地址连续登录错误次数达到该值后禁止登录或提供验证码，默认6")]
    [Category("用户登录")]
    public Int32 MaxLoginError { get; set; } = 6;

    /// <summary>登录失败次数。短时间内，相同用户或IP地址连续登录错误次数达到该值后禁止登录，默认300</summary>
    [Description("登录封禁时间。触发风控禁止登录后的禁止时间，默认300秒")]
    [Category("用户登录")]
    public Int32 LoginForbiddenTime { get; set; } = 300;

    /// <summary>
    /// Jwt配置
    /// </summary>
    [Description("Jwt配置")]
    public JwtOptions JwtOptions { get; set; } = new JwtOptions { Secret = "5efefbv1j67uqrono0xdmx4y0il5dn5y7b72tlb3imba677ht1p1xlfcnh36mk5u3xzjktfara25podhy85apfplun7oslbe1m20c148p5d519kja5wvg7lmn5v4a5ou", Issuer = "ding_identity", Audience = "ding_client", AccessExpireMinutes = 120, RefreshExpireMinutes = 43200, ThrowEnabled = false };


    #region 方法
    /// <summary>实例化</summary>
    public DHSetting() { }

    /// <summary>加载时触发</summary>
    protected override void OnLoaded()
    {
        if (StartPage.IsNullOrEmpty()) StartPage =
            // 避免出现生成 "/Admin/Admin/Index/Main" 这样的情况
            //NewLife.Web.HttpContext.Current?.Request.PathBase.ToString().EnsureEnd("/") + 
            "/Admin/Home/Main";

        if (DefaultRole.IsNullOrEmpty() || DefaultRole == "4") DefaultRole = "普通用户";

        if (JwtOptions.Secret.IsNullOrEmpty() || JwtOptions.Secret.Split(':').Length != 2) JwtOptions.Secret = $"HS256:{Rand.NextString(16)}";

        if (PaswordStrength.IsNullOrEmpty()) PaswordStrength = @"^(?=.*\d.*)(?=.*[a-z].*)(?=.*[A-Z].*)(?=.*[^(0-9a-zA-Z)].*).{8,32}$";
        if (MaxLoginError <= 0) MaxLoginError = 6;
        if (LoginForbiddenTime <= 0) LoginForbiddenTime = 300;

        base.OnLoaded();
    }
    #endregion
}
