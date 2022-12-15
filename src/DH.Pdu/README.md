###  **DH.Pdu** 

是一个用于C#的PDU编码及解码的功能库，支持net core7.0。

###  **安装** 

您可以使用以下命令从Nuget安装：

```
Install-Package DH.Pdu
```

或者通过Visual Studio包管理器。

###  **使用** 

1.编码


```
TextMessage textMessage = new TextMessage
{
    DataEncoding = DataEncoding.UCS2_16bit,
    Text = "你好"
};
SMSSubmit sms = new SMSSubmit
{
    MessageToSend = textMessage,
    ValidityPeriod = new TimeSpan(5, 0, 0, 0),
    PhoneNumber = "18888888888"
};

List<byte[]> messageList = sms.GetPDUList();
foreach (var messagePart in item.MessageList.ToArray())  //数据太长会分成多项
{
    // 将messagePart转成十六进制即为PDU格式的数据
}
```

2.解码


```
SMSType smsType = SMSBase.GetSMSType(要解码的十六进制数据);
switch (smsType)
{
    case SMSType.SMS:
        SMS sms = new SMS();
        SMS.Fetch(sms, ref source);
        Console.WriteLine(sms.PhoneNumber + sms.Message);

        break;
    case SMSType.StatusReport:
        SMSStatusReport statusReport = new SMSStatusReport();
        SMSStatusReport.Fetch(statusReport, ref source);

        Console.WriteLine("测试" + statusReport.PhoneNumber + statusReport.ReportStatus);
        break;
}
```
