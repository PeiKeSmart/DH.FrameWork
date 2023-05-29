using Aliyun.Acs.Core;

namespace DG.Sms.AliYun.Dysmsapi.Model.V20170525
{
    public class SendBatchSmsResponse : AcsResponse
    {
        public new string RequestId { get; set; }

        public string BizId { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
    }
}
