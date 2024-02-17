using DH.Sms.Providers;

namespace DH.Sms;

public class SmsTool {
    internal static ISmsProvider GetProvider(SmsProvider _typ)
    {
        switch (_typ)
        {
            case SmsProvider.Alibaba:
                return new AlibabaSms();
            case SmsProvider.Tencent:
                return new TencentSms();
        }
        return null;
    }

    public static ISmsProvider New(SmsProvider _typ)
    {
        return GetProvider(_typ);
    }
}