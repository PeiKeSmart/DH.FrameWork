using Aliyun.Acs.Core.Transform;

namespace DG.Sms.AliYun.Dysmsapi.Model.V20170525
{
    public class SendBatchSmsResponseUnmarshaller
    {
        public static SendBatchSmsResponse Unmarshall(UnmarshallerContext context)
        {
            SendBatchSmsResponse sendBatchSmsResponse = new SendBatchSmsResponse
            {
                HttpResponse = context.HttpResponse,
                RequestId = context.StringValue("SendBatchSms.RequestId"),
                BizId = context.StringValue("SendBatchSms.BizId"),
                Code = context.StringValue("SendBatchSms.Code"),
                Message = context.StringValue("SendBatchSms.Message")
            };

            return sendBatchSmsResponse;
        }
    }
}
