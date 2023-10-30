﻿using EasyNotice.Email.Helpers;
using EasyNotice.Email.Models;
using EasyNotice.Email.Options;
using EasyNotice.Helpers;
using EasyNotice.Models;
using EasyNotice.Options;

using MailKit.Net.Smtp;
using MailKit.Security;

using Microsoft.Extensions.Options;

namespace EasyNotice.Email.Provider;

internal class EmailProvider : IEmailProvider {
    private readonly EmailOptions _emailOptions;
    private readonly NoticeOptions _noticeOptions;

    public EmailProvider(IOptions<EmailOptions> emailOptions, IOptions<NoticeOptions> noticeOptions)
    {
        _emailOptions = emailOptions.Value;
        _noticeOptions = noticeOptions.Value;
    }

    /// <summary>
    /// 发送异常消息
    /// </summary>
    public Task<EasyNoticeSendResponse> SendAsync(string title, Exception exception)
    {
        var request = new EmailSendRequest
        {
            Subject = title,
            Body = $"{exception.Message}{Environment.NewLine}{exception}"
        };

        return SendBaseAsync(request);
    }

    /// <summary>
    /// 发送普通消息
    /// </summary>
    public Task<EasyNoticeSendResponse> SendAsync(string title, string message)
    {
        var request = new EmailSendRequest
        {
            Subject = title,
            Body = message
        };

        return SendBaseAsync(request);
    }

    /// <summary>
    /// 发送消息公共方法
    /// </summary>
    private async Task<EasyNoticeSendResponse> SendBaseAsync(EmailSendRequest input)
    {
        var response = new EasyNoticeSendResponse();
        try
        {
            await IntervalHelper.IntervalExcuteAsync(async () =>
            {
                var message = EmailHelper.CreateMimeMessage(input, _emailOptions);
                using (SmtpClient client = new SmtpClient())
                {
                    client.CheckCertificateRevocation = false;
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Timeout = 10 * 1000;
                    await client.ConnectAsync(_emailOptions.Host, _emailOptions.Port, SecureSocketOptions.Auto);
                    await client.AuthenticateAsync(_emailOptions.FromAddress, _emailOptions.Password);
                    await client.SendAsync(message);
                }
            }, input.Subject, _noticeOptions.IntervalSeconds);

        }
        catch (Exception ex)
        {
            response.ErrMsg = $"邮件发送异常:{ex.Message}";
        }
        return response;
    }
}