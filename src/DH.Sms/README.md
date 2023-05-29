
```
//阿里云短信
            services.AddAliSms(o =>
            {
                o.AliSmsOptions.AccessKeyId = SiteSetting.Current.Sms.AccessKeyId;
                o.AliSmsOptions.AccessKeySecret = SiteSetting.Current.Sms.AccessKeySecret;
                o.AliSmsOptions.SignName = SiteSetting.Current.Sms.passKey;
            });
```


```
var SmsService = Ioc.Create<ISmsService>();
await SmsService.SendAsync(UserInfo.Mobile, "SMS_137580016", "{\"code\":\"" + "1234" + "\"}", "yourOutId");
```
