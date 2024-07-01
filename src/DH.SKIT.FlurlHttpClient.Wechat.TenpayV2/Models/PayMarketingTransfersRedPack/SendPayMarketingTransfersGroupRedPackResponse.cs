using System;

namespace SKIT.FlurlHttpClient.Wechat.TenpayV2.Models
{
    /// <summary>
    /// <para>表示 [POST] /mmpaymkttransfers/sendgroupredpack 接口的响应。</para>
    /// </summary>
    [Obsolete("相关接口或字段于 2024-06-05 下线。")]
    public class SendPayMarketingTransfersGroupRedPackResponse : WechatTenpaySignableResponse
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty("mch_id")]
        [System.Text.Json.Serialization.JsonPropertyName("mch_id")]
#pragma warning disable CS8618
#pragma warning disable CS8765
        public override string MerchantId { get; set; }
#pragma warning restore CS8765
#pragma warning restore CS8618

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty("wxappid")]
        [System.Text.Json.Serialization.JsonPropertyName("wxappid")]
#pragma warning disable CS8618
#pragma warning disable CS8765
        public override string AppId { get; set; }
#pragma warning restore CS8765
#pragma warning restore CS8618

        /// <summary>
        /// 获取或设置商户订单号。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("mch_billno")]
        [System.Text.Json.Serialization.JsonPropertyName("mch_billno")]
        public string MerchantBillNumber { get; set; } = default!;

        /// <summary>
        /// 获取或设置商户付款单号。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("partner_trade_no")]
        [System.Text.Json.Serialization.JsonPropertyName("partner_trade_no")]
        public string PartnerTradeNumber { get; set; } = default!;

        /// <summary>
        /// 获取或设置接受红包的种子用户 OpenId。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("re_openid")]
        [System.Text.Json.Serialization.JsonPropertyName("re_openid")]
        public string OpenId { get; set; } = default!;

        /// <summary>
        /// 获取或设置红包金额（单位：分）。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("total_amount")]
        [System.Text.Json.Serialization.JsonPropertyName("total_amount")]
        [System.Text.Json.Serialization.JsonNumberHandling(System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString)]
        public int TotalAmount { get; set; }

        /// <summary>
        /// 获取或设置微信红包订单号。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("send_listid")]
        [System.Text.Json.Serialization.JsonPropertyName("send_listid")]
        public string PaymentNumber { get; set; } = default!;
    }
}
