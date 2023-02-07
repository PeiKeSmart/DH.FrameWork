﻿using Newtonsoft.Json;

namespace DG.Payment.UnionPay.Notify
{
    /// <summary>
    /// 无跳转支付(V2.2) 银联全渠道支付开通交易 - 前台通知
    /// </summary>
    public class UnionPayNoRedirectPayFrontOpenReturn : UnionPayResponse
    {
        /// <summary>
        /// 开通状态	
        /// </summary>
        [JsonProperty("activateStatus")]
        public string ActivateStatus { get; set; }

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
        /// 账号	
        /// </summary>
        [JsonProperty("accNo")]
        public string AccNo { get; set; }

        /// <summary>
        /// 标记化支付信息域	
        /// </summary>
        [JsonProperty("tokenPayData")]
        public string TokenPayData { get; set; }

        /// <summary>
        /// 发卡机构代码	
        /// </summary>
        [JsonProperty("issInsCode")]
        public string IssInsCode { get; set; }

        /// <summary>
        /// 签名公钥证书	
        /// </summary>
        [JsonProperty("signPubKeyCert")]
        public string SignPubKeyCert { get; set; }

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
        /// 接入类型	
        /// </summary>
        [JsonProperty("accessType")]
        public string AccessType { get; set; }

        /// <summary>
        /// 请求方保留域	
        /// </summary>
        [JsonProperty("reqReserved")]
        public string ReqReserved { get; set; }

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
        /// 保留域	
        /// </summary>
        [JsonProperty("reserved")]
        public string Reserved { get; set; }

        /// <summary>
        /// 风控信息域	
        /// </summary>
        [JsonProperty("riskRateInfo")]
        public string RiskRateInfo { get; set; }

        /// <summary>
        /// 小额临时支付信息域	
        /// </summary>
        [JsonProperty("temporaryPayInfo")]
        public string TemporaryPayInfo { get; set; }

        /// <summary>
        /// 银行卡验证信息及身份信息	
        /// </summary>
        [JsonProperty("customerInfo")]
        public string CustomerInfo { get; set; }
    }
}
