using Aliyun.Acs.Core.Transform;
using DH.Sms.AliYun.Dysmsapi.Model.V20170525;
using System.Collections.Generic;

namespace DH.Sms.AliYun.Dysmsapi.Transform.V20170525
{
    public class QueryInterSmsIsoInfoResponseUnmarshaller
    {
        public static QueryInterSmsIsoInfoResponse Unmarshall(UnmarshallerContext context)
        {
            QueryInterSmsIsoInfoResponse queryInterSmsIsoInfoResponse = new QueryInterSmsIsoInfoResponse();

            queryInterSmsIsoInfoResponse.HttpResponse = context.HttpResponse;
            queryInterSmsIsoInfoResponse.RequestId = context.StringValue("QueryInterSmsIsoInfo.RequestId");
            queryInterSmsIsoInfoResponse.Code = context.StringValue("QueryInterSmsIsoInfo.Code");
            queryInterSmsIsoInfoResponse.Message = context.StringValue("QueryInterSmsIsoInfo.Message");

            List<QueryInterSmsIsoInfoResponse.QueryInterSmsIsoInfo_IsoSupportDTO>
                queryInterSmsIsoInfoResponse_isoSupportDTOs =
                    new List<QueryInterSmsIsoInfoResponse.QueryInterSmsIsoInfo_IsoSupportDTO>();
            for (int i = 0; i < context.Length("QueryInterSmsIsoInfo.IsoSupportDTOs.Length"); i++)
            {
                QueryInterSmsIsoInfoResponse.QueryInterSmsIsoInfo_IsoSupportDTO isoSupportDTO =
                    new QueryInterSmsIsoInfoResponse.QueryInterSmsIsoInfo_IsoSupportDTO();
                isoSupportDTO.CountryName =
                    context.StringValue("QueryInterSmsIsoInfo.IsoSupportDTOs[" + i + "].CountryName");
                isoSupportDTO.CountryCode =
                    context.StringValue("QueryInterSmsIsoInfo.IsoSupportDTOs[" + i + "].CountryCode");
                isoSupportDTO.IsoCode = context.StringValue("QueryInterSmsIsoInfo.IsoSupportDTOs[" + i + "].IsoCode");

                queryInterSmsIsoInfoResponse_isoSupportDTOs.Add(isoSupportDTO);
            }

            queryInterSmsIsoInfoResponse.IsoSupportDTOs = queryInterSmsIsoInfoResponse_isoSupportDTOs;

            return queryInterSmsIsoInfoResponse;
        }
    }
}
