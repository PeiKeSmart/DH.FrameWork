﻿using Newtonsoft.Json;

namespace DH.Payment.UnionPay.Response
{
    /// <summary>
    /// 手机支付控件(V2.2) 交易状态查询接口 - 应答报文
    /// </summary>
    public class UnionPayMobileControlPayQueryResponse : UnionPayResponse
    {
        /// <summary>
        /// 查询流水号
        /// </summary>
        [JsonProperty("queryId")]
        public string QueryId { get; set; }

        /// <summary>
        /// 交易传输时间
        /// </summary>
        [JsonProperty("traceTime")]
        public string TraceTime { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        [JsonProperty("txnType")]
        public string TxnType { get; set; }

        /// <summary>
        /// 交易子类
        /// </summary>
        [JsonProperty("txnSubType")]
        public string TxnSubType { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [JsonProperty("signature")]
        public string Signature { get; set; }

        /// <summary>
        /// 签名方法
        /// </summary>
        [JsonProperty("signMethod")]
        public string SignMethod { get; set; }

        /// <summary>
        /// 清算币种
        /// </summary>
        [JsonProperty("settleCurrencyCode")]
        public string SettleCurrencyCode { get; set; }

        /// <summary>
        /// 清算金额
        /// </summary>
        [JsonProperty("settleAmt")]
        public string SettleAmt { get; set; }

        /// <summary>
        /// 清算日期
        /// </summary>
        [JsonProperty("settleDate")]
        public string SettleDate { get; set; }

        /// <summary>
        /// 系统跟踪号
        /// </summary>
        [JsonProperty("traceNo")]
        public string TraceNo { get; set; }

        /// <summary>
        /// 应答码
        /// </summary>
        [JsonProperty("respCode")]
        public string RespCode { get; set; }

        /// <summary>
        /// 应答信息
        /// </summary>
        [JsonProperty("respMsg")]
        public string RespMsg { get; set; }

        /// <summary>
        /// 兑换日期
        /// </summary>
        [JsonProperty("exchangeDate")]
        public string ExchangeDate { get; set; }

        /// <summary>
        /// 交易币种
        /// </summary>
        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        [JsonProperty("txnAmt")]
        public string TxnAmt { get; set; }

        /// <summary>
        /// 签名公钥证书
        /// </summary>
        [JsonProperty("signPubKeyCert")]
        public string SignPubKeyCert { get; set; }

        /// <summary>
        /// 清算汇率
        /// </summary>
        [JsonProperty("exchangeRate")]
        public string ExchangeRate { get; set; }

        /// <summary>
        /// 收单机构代码
        /// </summary>
        [JsonProperty("acqInsCode")]
        public string AcqInsCode { get; set; }

        /// <summary>
        /// 有卡交易信息域
        /// </summary>
        [JsonProperty("cardTransData")]
        public string CardTransData { get; set; }

        /// <summary>
        /// 原交易应答码
        /// </summary>
        [JsonProperty("origRespCode")]
        public string OrigRespCode { get; set; }

        /// <summary>
        /// 原交易应答信息
        /// </summary>
        [JsonProperty("origRespMsg")]
        public string OrigRespMsg { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [JsonProperty("accNo")]
        public string AccNo { get; set; }

        /// <summary>
        /// 支付卡类型
        /// </summary>
        [JsonProperty("payCardType")]
        public string PayCardType { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// 编码方式
        /// </summary>
        [JsonProperty("encoding")]
        public string Encoding { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        [JsonProperty("bizType")]
        public string BizType { get; set; }

        /// <summary>
        /// 订单发送时间
        /// </summary>
        [JsonProperty("txnTime")]
        public string TxnTime { get; set; }

        /// <summary>
        /// 接入类型
        /// </summary>
        [JsonProperty("accessType")]
        public string AccessType { get; set; }

        /// <summary>
        /// 商户代码
        /// </summary>
        [JsonProperty("merId")]
        public string MerId { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// 请求方保留域
        /// </summary>
        [JsonProperty("reqReserved")]
        public string ReqReserved { get; set; }
    }
}
