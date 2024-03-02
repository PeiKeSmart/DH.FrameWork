using DH.Extensions;
using DH.Security;

using NewLife;
using NewLife.Configuration;
using NewLife.Security;

using System.ComponentModel;

using XCode.Configuration;

namespace DH;

/// <summary>基类设置</summary>
[DisplayName("基类设置")]
//[XmlConfigFile("Config/DHSetting.config", 10000)]
[Config("DHSetting")]
public class DHSetting : Config<DHSetting>
{
    #region 静态
    static DHSetting() => Provider = new DbConfigProvider { UserId = 0, Category = "DH" };
    #endregion

    /// <summary>是否启用调试。默认true</summary>
    [Description("调试")]
    [Category("通用")]
    public Boolean Debug { get; set; } = true;

    /// <summary>显示运行时间</summary>
    [Description("显示运行时间")]
    [Category("通用")]
    public Boolean ShowRunTime { get; set; } = true;

    /// <summary>当前版本号</summary>
    [Description("当前版本号")]
    public String CurrentVersion { get; set; } = "1.00";

    /// <summary>升级状态。版本号_是否成功(0 or 1)</summary>
    [Description("升级状态")]
    public String UpdateInfo { get; set; } = "";

    /// <summary>站点Id</summary>
    [Description("站点Id")]
    public Int32 SiteId { get; set; } = 1;

    /// <summary>是否Api项目或者单页项目。默认false</summary>
    [Description("是否Api项目")]
    [Category("通用")]
    public Boolean IsApiOrSpaItem { get; set; } = false;

    /// <summary>系统初始化控制参数</summary>
    [Description("系统初始化控制参数,系统是否安装,true：已安装，false：未安装")]
    public Boolean IsInstalled { get; set; } = false;

    /// <summary>上传目录。默认Uploads</summary>
    [Description("上传目录。默认Uploads")]
    [Category("通用")]
    public String UploadPath { get; set; } = "Uploads";

    /// <summary>
    /// 是否允许限流
    /// </summary>
    [Description("是否允许限流")]
    public Boolean AllowRateLimter { get; set; }

    /// <summary>
    /// 管理后台路由
    /// </summary>
    [Description("管理后台路由")]
    public String AdminArea { get; set; } = "Admin";

    /// <summary>当前系统域名</summary>
    [Description("当前系统域名")]
    public String CurDomainUrl { get; set; } = "http://localhost:5000";

    /// <summary>会话超时。单点登录后会话超时时间，该时间内可借助Cookie登录，默认0s</summary>
    [Description("会话超时。单点登录后会话超时时间，该时间内可借助Cookie登录，默认0s")]
    [Category("用户登录")]
    public Int32 SessionTimeout { get; set; } = 0;

    /// <summary>
    /// 是否全站启用SSL
    /// </summary>
    /// <summary>是否全站启用SSL</summary>
    [Description("是否全站启用SSL")]
    public Boolean AllSslEnabled { get; set; }

    /// <summary>
    /// HttpsRequirement是否启用SSL。0为不处理，1为重定向（永久）到页面的HTTPS版本，2为（永久）重定向到页面的HTTP版本
    /// </summary>
    /// <summary>HttpsRequirement是否启用SSL。0为不处理，1为重定向（永久）到页面的HTTPS版本，2为（永久）重定向到页面的HTTP版本</summary>
    [Description("HttpsRequirement是否启用SSL。0为不处理，1为重定向（永久）到页面的HTTPS版本，2为（永久）重定向到页面的HTTP版本")]
    public Int32 SslEnabled { get; set; } = 0;

    /// <summary>
    /// 是否为管理系统
    /// </summary>
    [Description("是否为管理系统")]
    public Boolean IsOnlyManager { get; set; } = true;

    /// <summary>
    /// 接口授权值缓存过期时间，秒
    /// </summary>
    [Description("接口授权值缓存过期时间，秒")]
    public Int32 SignatureExpire { get; set; } = 300;

    /// <summary>
    /// 流量统计代码
    /// </summary>
    [Description("流量统计代码")]
    public String Statistical { get; set; }

    /// <summary>工作台页面。进入后台的第一个内容页</summary>
    [Description("工作台页面。进入后台的第一个内容页")]
    [Category("界面配置")]
    public String StartPage { get; set; }

    /// <summary>星尘Web。星尘控制台地址，支持直达调用链 /trace?id={traceId} 或 /graph?id={traceId}</summary>
    [Description("星尘Web。星尘控制台地址，支持直达调用链 /trace?id={traceId} 或 /graph?id={traceId}")]
    [Category("界面配置")]
    public String StarWeb { get; set; }

    /// <summary>默认角色。默认普通用户</summary>
    [Description("默认角色。默认普通用户")]
    [Category("用户登录")]
    public String DefaultRole { get; set; } = "普通用户";

    /// <summary>密码强度。*表示无限制，默认8位起，数字大小写字母和符号。简易版^(?=.*\\d.*)(?=.*[a-zA-Z].*).{8,32}$</summary>
    [Description("密码强度。*表示无限制，默认8位起，数字大小写字母和符号。简易版^(?=.*\\d.*)(?=.*[a-zA-Z].*).{8,32}$")]
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

    ///// <summary>Cookie模式。token的cookies默认模式（ -1 Unspecified，0 None，1 Lax，2 Strict）</summary>
    //[Description("Cookie模式。token的cookies默认模式（ -1 Unspecified，0 None，1 Lax，2 Strict）")]
    //[Category("通用")]
    //public Int32 SameSiteMode { get; set; } = -1;

    ///// <summary>Cookie域名。可用于把Cookie写到顶级域名，默认为空写当前域。写顶级域要求https，同时会导致普通http无法在本地域写同名键值</summary>
    //[Description("Cookie域名。可用于把Cookie写到顶级域名，默认为空写当前域。写顶级域要求https，同时会导致普通http无法在本地域写同名键值")]
    //[Category("通用")]
    //public String CookieDomain { get; set; }

    /// <summary>
    /// Sid缓存名称
    /// </summary>
    [Description("Sid缓存名称")]
    public String SidName { get; set; } = "sid";

    /// <summary>自动注册。默认true，SSO登录后，如果本地未登录，自动注册新用户。全局设置和OAuth应用设置只要有一个启用则表示使用</summary>
    [Description("自动注册。默认true，SSO登录后，如果本地未登录，自动注册新用户。全局设置和OAuth应用设置只要有一个启用则表示使用")]
    [Category("用户登录")]
    public Boolean AutoRegister { get; set; } = true;

    /// <summary>
    /// 是否允许获取请求和响应内容
    /// </summary>
    [Description("是否允许获取请求和响应内容")]
    public Boolean AllowRequestParams { get; set; }

    /// <summary>
    /// 允许获取请求和响应内容时排除的Url关键词，多个以逗号分隔
    /// </summary>
    [Description("允许获取请求和响应内容时排除的Url关键词，多个以逗号分隔")]
    public String ExcludeUrl { get; set; }

    /// <summary>
    /// 语言缓存名称
    /// </summary>
    [Description("语言缓存名称")]
    public String LangName { get; set; } = "lang";

    /// <summary>
    /// 是否禁用动态页面转静态页面
    /// </summary>
    [Description("是否禁用动态页面转静态页面")]
    public Boolean IsHtmlStaticDevelopmentMode { get; set; } = true;

    /// <summary>
    /// 动态页面转静态页面过期时间
    /// </summary>
    [Description("动态页面转静态页面过期时间")]
    public Int32 HtmlStaticExpireMinutes { get; set; } = 1;

    /// <summary>跨域授权地址</summary>
    [Description("跨域授权地址")]
    public String CORSUrl { get; set; } = "https://localhost:9091,http://localhost:9090";

    /// <summary>
    /// 是否检查接口检验
    /// </summary>
    [Description("是否检查接口检验")]
    public Boolean IsCheckApiSignature { get; set; } = true;

    /// <summary>
    /// App接口请求的Token值
    /// </summary>
    [Description("App接口请求的Token值")]
    public String ServerToken { get; set; } = "HlkTech20200429";

    /// <summary>
    /// 是否允许全局添加JWT授权，一般用于纯接口控制器
    /// </summary>
    [Description("是否允许全局添加JWT授权")]
    public Boolean IsAllowGlobalJWTAuthorize { get; set; }

    /// <summary>网站启动时监听端口</summary>
    [Description("网站启动时监听端口")]
    public String Urls { get; set; } = "http://*:9090;https://*:9091";

    /// <summary>机器人错误码。设置后拦截各种爬虫并返回相应错误，如404/500，默认0不拦截</summary>
    [Description("机器人错误码。设置后拦截各种爬虫并返回相应错误，如404/500，默认0不拦截")]
    [Category("通用")]
    public Int32 RobotError { get; set; }

    /// <summary>数据保留时间。审计日期与OAuth日志，默认30天</summary>
    [Description("数据保留时间。审计日期与OAuth日志，默认30天")]
    [Category("通用")]
    public Int32 DataRetention { get; set; } = 30;

    /// <summary>下拉选择框。使用Bootstrap，美观，但有呈现方面的性能损耗</summary>
    [Description("下拉选择框。使用Bootstrap，美观，但有呈现方面的性能损耗")]
    [Category("界面配置")]
    public Boolean BootstrapSelect { get; set; } = true;

    /// <summary>最大下拉个数。表单页关联下拉列表最大允许个数，默认50，超过时显示文本数字框</summary>
    [Description("最大下拉个数。表单页关联下拉列表最大允许个数，默认50，超过时显示文本数字框")]
    [Category("界面配置")]
    public Int32 MaxDropDownList { get; set; } = 50;

    /// <summary>抓取头像。是否抓取远程头像，默认true</summary>
    /// <summary>头像目录。设定后下载远程头像到本地，默认Avatars子目录，web上一级Avatars。清空表示不抓取</summary>
    [Description("头像目录。设定后下载远程头像到本地，默认Avatars子目录，web上一级Avatars。清空表示不抓取")]
    [Category("通用")]
    public String AvatarPath { get; set; } = "Avatars";

    /// <summary>静态资源目录。默认wwwroot</summary>
    [Description("静态资源目录。默认wwwroot")]
    [Category("通用")]
    public String WebRootPath { get; set; } = "wwwroot";

    /// <summary>允许密码登录。允许输入用户名密码进行登录</summary>
    [Description("允许密码登录。允许输入用户名密码进行登录")]
    [Category("用户登录")]
    public Boolean AllowLogin { get; set; } = true;

    /// <summary>
    /// 前台用户登录地址
    /// </summary>
    [Description("前台用户登录地址")]
    public String LoginUrl { get; set; } = "~/Login";

    /// <summary>万能验证码</summary>
    [Description("万能验证码。用于测试使用专门的验证码进行测试")]
    [Category("用户登录")]
    public String EnableUniversalCaptcha { get; set; } = Rand.NextString(4);

    /// <summary>万能验证码开启截止时间</summary>
    [Description("万能验证码开启截止时间")]
    [Category("用户登录")]
    public DateTime UniversalCaptchaEndTime { get; set; }

    /// <summary>
    /// 用于接口调测时使用的密码
    /// </summary>
    [Description("用于接口调测时使用的密码")]
    public String DebugPassWord { get; set; } = Rand.NextString(32);

    /// <summary>
    /// 禁止访问时间
    /// </summary>
    [Description("禁止访问时间")]
    public string BanAccessTime { get; set; }

    /// <summary>
    /// 禁止IP列表
    /// </summary>
    [Description("禁止IP列表")]
    public string BanAccessIP { get; set; } = "";

    /// <summary>
    /// 允许IP列表
    /// </summary>
    [Description("允许IP列表")]
    public string AllowAccessIP { get; set; } = "";

    /// <summary>
    /// 启用在线数统计
    /// </summary>
    [Description("启用在线数统计")]
    public Boolean EnableOnlineStatistics { get; set; } = false;

    /// <summary>
    /// 最大在线人数
    /// </summary>
    [Description("最大在线人数")]
    public int MaxOnlineCount { get; set; } = 10000;

    /// <summary>
    /// 在线人数缓存时间(单位为分钟,0代表即时数量)
    /// </summary>
    [Description("在线人数缓存时间(单位为分钟,0代表即时数量)")]
    public int OnlineCountExpire { get; set; } = 5;

    /// <summary>
    /// 更新用户在线时间间隔(单位为分钟,0代表不更新)
    /// </summary>
    [Description("更新用户在线时间间隔(单位为分钟,0代表不更新)")]
    public int UpdateOnlineTimeSpan { get; set; } = 2;

    /// <summary>
    /// 在线用户过期时间(单位为分钟)
    /// </summary>
    [Description("在线用户过期时间(单位为分钟)")]
    public int OnlineUserExpire { get; set; } = 8;

    /// <summary>
    /// 项目启动时间
    /// </summary>
    [Description("项目启动时间")]
    public DateTime StartTime { get; set; }

    #region 系统功能
    /// <summary>多租户。是否支持多租户，租户模式禁止访问系统管理，平台管理模式禁止访问租户页面</summary>
    [Description("多租户。是否支持多租户，租户模式禁止访问系统管理，平台管理模式禁止访问租户页面")]
    [Category("系统功能")]
    public Boolean EnableTenant { get; set; }

    /// <summary>用户在线。是否记录用户在线信息，0表示不记录，1表示仅记录已登录用户，2表示记录所有访客。默认2</summary>
    [Description("用户在线。是否记录用户在线信息，0表示不记录，1表示仅记录已登录用户，2表示记录所有访客。默认2")]
    [Category("系统功能")]
    public Int32 EnableUserOnline { get; set; } = 2;

    /// <summary>用户统计。是否统计用户访问，默认true</summary>
    [Description("用户统计。是否统计用户访问，默认true")]
    [Category("系统功能")]
    public Boolean EnableUserStat { get; set; } = true;
    #endregion

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

        if (JwtOptions.Secret.IsNullOrEmpty() || JwtOptions.Secret.Split(':').Length != 2) JwtOptions.Secret = $"HS256:{Rand.NextString(26)}";

        if (PaswordStrength.IsNullOrEmpty()) PaswordStrength = @"^(?=.*\d.*)(?=.*[a-z].*)(?=.*[A-Z].*)(?=.*[^(0-9a-zA-Z)].*).{8,32}$";
        if (MaxLoginError <= 0) MaxLoginError = 6;
        if (LoginForbiddenTime <= 0) LoginForbiddenTime = 300;

        base.OnLoaded();
    }
    #endregion
}
