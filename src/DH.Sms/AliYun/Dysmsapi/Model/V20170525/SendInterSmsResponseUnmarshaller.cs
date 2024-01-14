using Aliyun.Acs.Core.Transform;

namespace DH.Sms.AliYun.Dysmsapi.Model.V20170525
{
    public class SendInterSmsResponseUnmarshaller
    {
        public static SendInterSmsResponse Unmarshall(UnmarshallerContext context)
        {
            SendInterSmsResponse sendInterSmsResponse = new SendInterSmsResponse
            {
                HttpResponse = context.HttpResponse,
                RequestId = context.StringValue("SendInterSms.RequestId"),
                BizId = context.StringValue("SendInterSms.BizId"),
                Code = context.StringValue("SendInterSms.Code"),
                Message = context.StringValue("SendInterSms.Message")
            };

            return sendInterSmsResponse;
        }
    }
}
