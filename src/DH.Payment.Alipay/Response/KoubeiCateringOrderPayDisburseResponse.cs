﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCateringOrderPayDisburseResponse.
    /// </summary>
    public class KoubeiCateringOrderPayDisburseResponse : AlipayResponse
    {
        /// <summary>
        /// 优惠明细列表
        /// </summary>
        [JsonPropertyName("discount_details")]
        public List<PosDiscountDetail> DiscountDetails { get; set; }

        /// <summary>
        /// 外部支付订单号,唯一标识本次支付的requestID
        /// </summary>
        [JsonPropertyName("out_pay_no")]
        public string OutPayNo { get; set; }

        /// <summary>
        /// 买家实付金额
        /// </summary>
        [JsonPropertyName("pay_amount")]
        public string PayAmount { get; set; }

        /// <summary>
        /// 口碑内部支付订单号，和外部支付订单号一一映射
        /// </summary>
        [JsonPropertyName("pay_no")]
        public string PayNo { get; set; }

        /// <summary>
        /// 买家交易支付成功时间
        /// </summary>
        [JsonPropertyName("pay_time")]
        public string PayTime { get; set; }

        /// <summary>
        /// 商家实收金额
        /// </summary>
        [JsonPropertyName("receipt_amount")]
        public string ReceiptAmount { get; set; }

        /// <summary>
        /// PROCESS-支付中，PAY-已支付。  如果是支付中状态，需要做支付结果轮询
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// 订单付款金额，以元为单位，精确到分
        /// </summary>
        [JsonPropertyName("total_amount")]
        public string TotalAmount { get; set; }

        /// <summary>
        /// 支付宝交易号
        /// </summary>
        [JsonPropertyName("trade_no")]
        public string TradeNo { get; set; }

        /// <summary>
        /// 买家支付宝账号
        /// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }
}
