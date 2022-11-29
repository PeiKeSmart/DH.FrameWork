using System.ComponentModel;

namespace DH.Mail;

/// <summary>
/// Email配置
/// </summary>
[DisplayName("Email配置")]
public class Email
{
    /// <summary>
    /// 服务器地址
    /// </summary>
    public string Host { get; set; } = "smtp.exmail.qq.com";
    /// <summary>
    /// 服务器端口
    /// </summary>
    public int Port { get; set; } = 465;
    /// <summary>
    /// 邮箱账号
    /// </summary>
    public string UserName { get; set; } = "services@gicisky.net";
    /// <summary>
    /// 邮箱密码
    /// </summary>
    public string Password { get; set; } = "wE28noPizWXLDShr";
    /// <summary>
    /// 发送邮箱
    /// </summary>
    public string From { get; set; } = "services@gicisky.net";
    /// <summary>
    /// 发送邮箱的昵称
    /// </summary>
    public string FromName { get; set; } = "模块邦";
    /// <summary>
    /// 是否启用SSL,0为否,1为是
    /// </summary>
    public Boolean IsSSL { get; set; } = true;
}