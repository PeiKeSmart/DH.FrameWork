using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppJfUserinstinfoQueryResponse.
    /// </summary>
    public class AlipayEbppJfUserinstinfoQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 机构信息列表
        /// </summary>
        [JsonPropertyName("user_charge_inst_info_list")]
        public List<EbppUserChargeInstInfo> UserChargeInstInfoList { get; set; }
    }
}
