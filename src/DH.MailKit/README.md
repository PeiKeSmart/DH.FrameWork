
```
//注册邮件
            services.AddMailKit(optionBuilder =>
            {
                optionBuilder.EmailConfig = new Ding.Net.Mail.Configs.EmailConfig()
                {
                    Host = SiteSetting.Current.Email.Host,
                    Port = SiteSetting.Current.Email.Port,
                    DisplayName = SiteSetting.Current.Email.FromName,
                    FromAddress = SiteSetting.Current.Email.From,
                    UserName = SiteSetting.Current.Email.UserName,
                    Password = SiteSetting.Current.Email.Password,
                    EnableSsl = SiteSetting.Current.Email.IsSSL
                };
                optionBuilder.MailKitConfig = new Ding.MailKit.Configs.MailKitConfig();
            });
```


```
var EmailsService = Ioc.Create<IEmailsService>();

await EmailsService.SendWebcomeEmail(UserInfo.Email);  // 发送邮件
```
