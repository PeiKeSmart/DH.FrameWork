using Aliyun.Acs.Core;
using System.Collections.Generic;

namespace DG.Sms.AliYun.Dysmsapi.Model.V20170525
{
    public class QuerySendDetailsResponse : AcsResponse
    {
        public new string RequestId { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }

        public string TotalCount { get; set; }

        public List<QuerySendDetails_SmsSendDetailDTO> SmsSendDetailDTOs { get; set; }

        public class QuerySendDetails_SmsSendDetailDTO
        {
            public string PhoneNum { get; set; }

            public long? SendStatus { get; set; }

            public string ErrCode { get; set; }

            public string TemplateCode { get; set; }

            public string Content { get; set; }

            public string SendDate { get; set; }

            public string ReceiveDate { get; set; }

            public string OutId { get; set; }
        }
    }
}
