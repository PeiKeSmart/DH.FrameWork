using Aliyun.Acs.Core;
using System.Collections.Generic;

namespace DH.Sms.AliYun.Dysmsapi.Model.V20170525
{
    public class QueryInterSmsIsoInfoResponse : AcsResponse
    {
        public new string RequestId { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }

        public List<QueryInterSmsIsoInfo_IsoSupportDTO> IsoSupportDTOs { get; set; }

        public class QueryInterSmsIsoInfo_IsoSupportDTO
        {
            public string CountryName { get; set; }

            public string CountryCode { get; set; }

            public string IsoCode { get; set; }
        }
    }
}
