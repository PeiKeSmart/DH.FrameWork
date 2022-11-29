using DH.Mail.Abstractions;

using System.Net.Mail;

namespace DH.Mail.Smtp;

/// <summary>
/// 基于SMTP的电子邮件发送器
/// </summary>
public interface ISmtpEmailSender : IEmailSender
{
    /// <summary>
    /// 生成SMTP客户端
    /// </summary>
    /// <returns></returns>
    SmtpClient BuildClient();
}