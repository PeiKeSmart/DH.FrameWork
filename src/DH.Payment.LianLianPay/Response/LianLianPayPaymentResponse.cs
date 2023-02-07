using Newtonsoft.Json;

namespace DG.Payment.LianLianPay.Response
{
    /// <summary>
    /// 付款申请
    /// </summary>
    public class LianLianPayPaymentResponse : LianLianPayResponse
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
        /// 原请求中商户订单号。
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
        /// 商户编号是商户在连连支付支付平台上开设的商户号码，为18位数字，如：201304121000001004。
        /// </summary>
        [JsonProperty("oid_partner")]
        public string OidPartner { get; set; }

        /// <summary>
        /// 连连支付单号。
        /// 全局唯一。
        /// 如： 2011030900001098。
        /// </summary>
        [JsonProperty("oid_paybill")]
        public string OidPaybill { get; set; }

        /// <summary>
        /// RetCode 为4002, 4003, 4004 时返回
        /// 确认码。
        /// 用于确认付款API。
        /// </summary>
        [JsonProperty("confirm_code")]
        public string ConfirmCode { get; set; }
    }
}
