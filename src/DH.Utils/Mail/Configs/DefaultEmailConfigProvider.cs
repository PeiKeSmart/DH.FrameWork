using Microsoft.Extensions.Options;

namespace DH.Mail.Configs;

/// <summary>
/// 电子邮件默认配置提供器
/// </summary>
public class DefaultEmailConfigProvider : IEmailConfigProvider
{
    /// <summary>
    /// 配置
    /// </summary>
    private readonly EmailConfig _config;

    /// <summary>
    /// 初始化一个<see cref="DefaultEmailConfigProvider"/>类型的实例
    /// </summary>
    /// <param name="config">电子邮件配置</param>
    public DefaultEmailConfigProvider(EmailConfig config)
    {
        _config = config;
    }

    public DefaultEmailConfigProvider(IOptionsMonitor<Email> options)
    {
        _config = new EmailConfig();
        _config.Host = options.CurrentValue.Host;
        _config.Port = options.CurrentValue.Port;
        _config.DisplayName = options.CurrentValue.FromName;
        _config.FromAddress = options.CurrentValue.From;
        _config.UserName = options.CurrentValue.UserName;
        _config.Password = options.CurrentValue.Password;
        _config.EnableSsl = options.CurrentValue.IsSSL;
    }

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <returns></returns>
    public EmailConfig GetConfig()
    {
        return _config;
    }

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <returns></returns>
    public Task<EmailConfig> GetConfigAsync()
    {
        return Task.FromResult(_config);
    }
}