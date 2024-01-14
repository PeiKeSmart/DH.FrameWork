using Aliyun.Acs.Core.Transform;
using DH.Sms.AliYun.Dysmsapi.Model.V20170525;
using System.Collections.Generic;

namespace DH.Sms.AliYun.Dysmsapi.Transform.V20170525
{
    public class QuerySendDetailsResponseUnmarshaller
    {
        public static QuerySendDetailsResponse Unmarshall(UnmarshallerContext context)
        {
            QuerySendDetailsResponse querySendDetailsResponse = new QuerySendDetailsResponse();

            querySendDetailsResponse.HttpResponse = context.HttpResponse;
            querySendDetailsResponse.RequestId = context.StringValue("QuerySendDetails.RequestId");
            querySendDetailsResponse.Code = context.StringValue("QuerySendDetails.Code");
            querySendDetailsResponse.Message = context.StringValue("QuerySendDetails.Message");
            querySendDetailsResponse.TotalCount = context.StringValue("QuerySendDetails.TotalCount");

            List<QuerySendDetailsResponse.QuerySendDetails_SmsSendDetailDTO> querySendDetailsResponse_smsSendDetailDTOs
                = new List<QuerySendDetailsResponse.QuerySendDetails_SmsSendDetailDTO>();
            for (int i = 0; i < context.Length("QuerySendDetails.SmsSendDetailDTOs.Length"); i++)
            {
                QuerySendDetailsResponse.QuerySendDetails_SmsSendDetailDTO smsSendDetailDTO =
                    new QuerySendDetailsResponse.QuerySendDetails_SmsSendDetailDTO();
                smsSendDetailDTO.PhoneNum =
                    context.StringValue("QuerySendDetails.SmsSendDetailDTOs[" + i + "].PhoneNum");
                smsSendDetailDTO.SendStatus =
                    context.LongValue("QuerySendDetails.SmsSendDetailDTOs[" + i + "].SendStatus");
                smsSendDetailDTO.ErrCode = context.StringValue("QuerySendDetails.SmsSendDetailDTOs[" + i + "].ErrCode");
                smsSendDetailDTO.TemplateCode =
                    context.StringValue("QuerySendDetails.SmsSendDetailDTOs[" + i + "].TemplateCode");
                smsSendDetailDTO.Content = context.StringValue("QuerySendDetails.SmsSendDetailDTOs[" + i + "].Content");
                smsSendDetailDTO.SendDate =
                    context.StringValue("QuerySendDetails.SmsSendDetailDTOs[" + i + "].SendDate");
                smsSendDetailDTO.ReceiveDate =
                    context.StringValue("QuerySendDetails.SmsSendDetailDTOs[" + i + "].ReceiveDate");
                smsSendDetailDTO.OutId = context.StringValue("QuerySendDetails.SmsSendDetailDTOs[" + i + "].OutId");

                querySendDetailsResponse_smsSendDetailDTOs.Add(smsSendDetailDTO);
            }

            querySendDetailsResponse.SmsSendDetailDTOs = querySendDetailsResponse_smsSendDetailDTOs;

            return querySendDetailsResponse;
        }
    }
}
