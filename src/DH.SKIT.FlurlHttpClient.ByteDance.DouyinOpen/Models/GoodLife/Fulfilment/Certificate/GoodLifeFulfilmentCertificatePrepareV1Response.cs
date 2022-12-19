namespace SKIT.FlurlHttpClient.ByteDance.DouyinOpen.Models
{
    /// <summary>
    /// <para>表示 [GET] /goodlife/v1/fulfilment/certificate/prepare 接口的响应。</para>
    /// </summary>
    public class GoodLifeFulfilmentCertificatePrepareV1Response : DouyinOpenResponse<GoodLifeFulfilmentCertificatePrepareV1Response.Types.Data>
    {
        public static class Types
        {
            public class Data : DouyinOpenResponseData
            {
                public static class Types
                {
                    public class Certificate
                    {
                        public static class Types
                        {
                            public class Amount
                            {
                                /// <summary>
                                /// 获取或设置券原始金额（单位：分）。
                                /// </summary>
                                [Newtonsoft.Json.JsonProperty("original")]
                                [System.Text.Json.Serialization.JsonPropertyName("original")]
                                public int OriginalAmount { get; set; }

                                /// <summary>
                                /// 获取或设置用户实付金额（单位：分）。
                                /// </summary>
                                [Newtonsoft.Json.JsonProperty("pay")]
                                [System.Text.Json.Serialization.JsonPropertyName("pay")]
                                public int PayAmount { get; set; }

                                /// <summary>
                                /// 获取或设置券实付金额（单位：分）。
                                /// </summary>
                                [Newtonsoft.Json.JsonProperty("coupon_pay")]
                                [System.Text.Json.Serialization.JsonPropertyName("coupon_pay")]
                                public int CouponPayAmount { get; set; }

                                /// <summary>
                                /// 获取或设置商家营销金额（单位：分）。
                                /// </summary>
                                [Newtonsoft.Json.JsonProperty("merchant_ticket")]
                                [System.Text.Json.Serialization.JsonPropertyName("merchant_ticket")]
                                public int MerchantTicketAmount { get; set; }

                                /// <summary>
                                /// 获取或设置支付优惠金额（单位：分）。
                                /// </summary>
                                [Newtonsoft.Json.JsonProperty("payment_discount")]
                                [System.Text.Json.Serialization.JsonPropertyName("payment_discount")]
                                public int PaymentDiscountAmount { get; set; }
                            }

                            public class SKU
                            {
                                /// <summary>
                                /// 获取或设置 SKU ID。
                                /// </summary>
                                [Newtonsoft.Json.JsonProperty("sku_id")]
                                [System.Text.Json.Serialization.JsonPropertyName("sku_id")]
                                public string SKUId { get; set; } = default!;

                                /// <summary>
                                /// 获取或设置团购名称。
                                /// </summary>
                                [Newtonsoft.Json.JsonProperty("title")]
                                [System.Text.Json.Serialization.JsonPropertyName("title")]
                                public string Title { get; set; } = default!;

                                /// <summary>
                                /// 获取或设置团购类型。
                                /// </summary>
                                [Newtonsoft.Json.JsonProperty("groupon_type")]
                                [System.Text.Json.Serialization.JsonPropertyName("groupon_type")]
                                public int GrouponType { get; set; }

                                /// <summary>
                                /// 获取或设置团购市场价（单位：分）。
                                /// </summary>
                                [Newtonsoft.Json.JsonProperty("market_price")]
                                [System.Text.Json.Serialization.JsonPropertyName("market_price")]
                                public int MarketPrice { get; set; }

                                /// <summary>
                                /// 获取或设置售卖开始时间戳。
                                /// </summary>
                                [Newtonsoft.Json.JsonProperty("sold_start_time")]
                                [System.Text.Json.Serialization.JsonPropertyName("sold_start_time")]
                                public long StartTimestamp { get; set; }

                                /// <summary>
                                /// 获取或设置 SKU 外部 ID。
                                /// </summary>
                                [Newtonsoft.Json.JsonProperty("third_sku_id")]
                                [System.Text.Json.Serialization.JsonPropertyName("third_sku_id")]
                                public string? OutSKUId { get; set; }
                            }
                        }

                        /// <summary>
                        /// 获取或设置加密券码。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("encrypted_code")]
                        [System.Text.Json.Serialization.JsonPropertyName("encrypted_code")]
                        public string EncryptedCode { get; set; } = default!;

                        /// <summary>
                        /// 获取或设置券码有效期时间戳。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("expire_time")]
                        [System.Text.Json.Serialization.JsonPropertyName("expire_time")]
                        public long ExpireTimestamp { get; set; }

                        /// <summary>
                        /// 获取或设置金额信息。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("amount")]
                        [System.Text.Json.Serialization.JsonPropertyName("amount")]
                        public Types.Amount Amount { get; set; } = default!;

                        /// <summary>
                        /// 获取或设置团购 SKU 信息。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("sku")]
                        [System.Text.Json.Serialization.JsonPropertyName("sku")]
                        public Types.SKU SKU { get; set; } = default!;
                    }
                }

                /// <summary>
                /// 获取或设置一次验券的标识。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("verify_token")]
                [System.Text.Json.Serialization.JsonPropertyName("verify_token")]
                public string VerifyToken { get; set; } = default!;

                /// <summary>
                /// 获取或设置可用团购券列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("certificates")]
                [System.Text.Json.Serialization.JsonPropertyName("certificates")]
                public Types.Certificate[] CertificateList { get; set; } = default!;
            }
        }
    }
}
