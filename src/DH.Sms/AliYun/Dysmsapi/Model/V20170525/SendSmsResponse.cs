using Aliyun.Acs.Core;

namespace DH.Sms.AliYun.Dysmsapi.Model.V20170525
{
    public class SendSmsResponse : AcsResponse
    {
        public new string RequestId { get; set; }

        public string BizId { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
    }
}
