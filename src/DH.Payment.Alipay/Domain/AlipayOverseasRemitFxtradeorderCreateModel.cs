﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOverseasRemitFxtradeorderCreateModel Data Structure.
    /// </summary>
    public class AlipayOverseasRemitFxtradeorderCreateModel : AlipayObject
    {
        /// <summary>
        /// the unique id on block chain generated by the caller to represent this remit operation
        /// </summary>
        [JsonPropertyName("bc_remit_id")]
        public string BcRemitId { get; set; }

        /// <summary>
        /// currency pair
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string CurrencyPair { get; set; }

        /// <summary>
        /// extended info
        /// </summary>
        [JsonPropertyName("extend_info")]
        public string ExtendInfo { get; set; }

        /// <summary>
        /// fx trade orderId
        /// </summary>
        [JsonPropertyName("fx_trade_order_id")]
        public string FxTradeOrderId { get; set; }

        /// <summary>
        /// fx trade side
        /// </summary>
        [JsonPropertyName("fx_trade_side")]
        public string FxTradeSide { get; set; }

        /// <summary>
        /// the Mid of the receiver allocated by net.
        /// </summary>
        [JsonPropertyName("receiver_mid")]
        public string ReceiverMid { get; set; }

        /// <summary>
        /// the Mid of the sender allocated by net.
        /// </summary>
        [JsonPropertyName("sender_mid")]
        public string SenderMid { get; set; }

        /// <summary>
        /// { "currency":"CNY", "value":"100000" }
        /// </summary>
        [JsonPropertyName("trans_amount")]
        public Money TransAmount { get; set; }
    }
}
