using Newtonsoft.Json;

namespace DH.Payment.LianLianPay.Response
{
    /// <summary>
    /// 确认付款
    /// </summary>
    public class LianLianPayConfirmPaymentResponse : LianLianPayResponse
    {
        /// <summary>
        /// 请求结果代码 
        /// </summary>
        [JsonProperty("ret_code")]
        public string RetCode { get; set; }

        /// <summary>
        /// 请求结果描述
        /// </summary>
        [JsonProperty("ret_msg")]
        public string RetMsg { get; set; }

        /// <summary>
        /// 商户付款流水号
        /// </summary>
        [JsonProperty("no_order")]
        public string NoOrder { get; set; }

        /// <summary>
        /// 签名方式
        /// </summary>
        [JsonProperty("sign_type")]
        public string SignType { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [JsonProperty("sign")]
        public string Sign { get; set; }

        /// <summary>
        /// 商户编号
        /// </summary>
        [JsonProperty("oid_partner")]
        public string OidPartner { get; set; }

        /// <summary>
        /// 连连支付单号
        /// </summary>
        [JsonProperty("oid_paybill")]
        public string OidPaybill { get; set; }
    }
}
