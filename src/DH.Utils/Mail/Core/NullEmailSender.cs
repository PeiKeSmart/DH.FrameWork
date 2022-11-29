﻿using DH.Mail.Configs;

using System.Net.Mail;

namespace DH.Mail.Core;

/// <summary>
/// 空电子邮件发送器
/// </summary>
public class NullEmailSender : EmailSenderBase
{
    /// <summary>
    /// 初始化一个<see cref="NullEmailSender"/>类型的实例
    /// </summary>
    /// <param name="provider">电子邮件配置提供器</param>
    public NullEmailSender(IEmailConfigProvider provider) : base(provider)
    {
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="mail">邮件</param>
    protected override void SendEmail(MailMessage mail)
    {
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="mail">邮件</param>
    /// <returns></returns>
    protected override Task SendEmailAsync(MailMessage mail)
    {
        return Task.FromResult(0);
    }
}