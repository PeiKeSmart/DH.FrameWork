using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayIserviceMindvJobsbyuserQueryResponse.
    /// </summary>
    public class AlipayIserviceMindvJobsbyuserQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 用户填写的任务id列表
        /// </summary>
        [JsonPropertyName("job_ids")]
        public List<long> JobIds { get; set; }
    }
}
