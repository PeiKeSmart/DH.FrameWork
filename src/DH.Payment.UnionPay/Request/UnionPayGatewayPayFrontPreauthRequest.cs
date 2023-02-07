﻿using System.Collections.Generic;
using DH.Payment.UnionPay.Response;

namespace DH.Payment.UnionPay.Request
{
    /// <summary>
    /// 网关支付(V2.2) 预授权接口
    /// </summary>
    public class UnionPayGatewayPayFrontPreauthRequest : IUnionPayRequest<UnionPayGatewayPayFrontPreauthResponse>
    {
        /// <summary>
        /// 产品类型
        /// </summary>
        public string BizType { get; set; }

        /// <summary>
        /// 订单发送时间
        /// </summary>
        public string TxnTime { get; set; }

        /// <summary>
        /// 后台通知地址
        /// </summary>
        public string BackUrl { get; set; }

        /// <summary>
        /// 交易币种
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        public string TxnAmt { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public string TxnType { get; set; }

        /// <summary>
        /// 交易子类
        /// </summary>
        public string TxnSubType { get; set; }

        /// <summary>
        /// 渠道类型
        /// </summary>
        public string ChannelType { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 订单描述
        /// </summary>
        public string OrderDesc { get; set; }

        /// <summary>
        /// 二级商户代码
        /// </summary>
        public string SubMerId { get; set; }

        /// <summary>
        /// 二级商户简称
        /// </summary>
        public string SubMerAbbr { get; set; }

        /// <summary>
        /// 二级商户名称
        /// </summary>
        public string SubMerName { get; set; }

        /// <summary>
        /// 前台通知地址
        /// </summary>
        public string FrontUrl { get; set; }

        /// <summary>
        /// 银行卡验证信息及身份信息
        /// </summary>
        public string CustomerInfo { get; set; }

        /// <summary>
        /// 有卡交易信息域
        /// </summary>
        public string CardTransData { get; set; }

        /// <summary>
        /// 预付卡通道
        /// </summary>
        public string AccountPayChannel { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string AccNo { get; set; }

        /// <summary>
        /// 账号类型(卡介质)
        /// </summary>
        public string AccType { get; set; }

        /// <summary>
        /// 保留域
        /// </summary>
        public string Reserved { get; set; }

        /// <summary>
        /// 持卡人IP
        /// </summary>
        public string CustomerIp { get; set; }

        /// <summary>
        /// 订单接收超时时间
        /// </summary>
        public string OrderTimeOut { get; set; }

        /// <summary>
        /// 发卡机构代码
        /// </summary>
        public string IssInsCode { get; set; }

        /// <summary>
        /// 分账域
        /// </summary>
        public string AccSplitData { get; set; }

        /// <summary>
        /// 风控信息域
        /// </summary>
        public string RiskRateInfo { get; set; }

        /// <summary>
        /// 默认支付方式
        /// </summary>
        public string DefaultPayType { get; set; }

        /// <summary>
        /// 请求方保留域
        /// </summary>
        public string ReqReserved { get; set; }

        /// <summary>
        /// 失败交易前台跳转地址
        /// </summary>
        public string FrontFailUrl { get; set; }

        /// <summary>
        /// 支持支付方式
        /// </summary>
        public string SupPayType { get; set; }

        /// <summary>
        /// 支付超时时间
        /// </summary>
        public string PayTimeOut { get; set; }

        /// <summary>
        /// 终端号
        /// </summary>
        public string TermId { get; set; }

        /// <summary>
        /// 终端信息域
        /// </summary>
        public string UserMac { get; set; }

        #region IUnionPayRequest

        private string version = string.Empty;

        public string GetApiVersion()
        {
            return version;
        }

        public void SetApiVersion(string version)
        {
            this.version = version;
        }

        public IDictionary<string, string> GetParameters()
        {
            var parameters = new UnionPayDictionary
            {
                { "bizType", BizType },
                { "txnTime", TxnTime },
                { "backUrl", BackUrl },
                { "currencyCode", CurrencyCode },
                { "txnAmt", TxnAmt },
                { "txnType", TxnType },
                { "txnSubType", TxnSubType },
                { "channelType", ChannelType },
                { "orderId", OrderId },
                { "orderDesc", OrderDesc },
                { "subMerId", SubMerId },
                { "subMerAbbr", SubMerAbbr },
                { "subMerName", SubMerName },
                { "issInsCode", IssInsCode },
                { "frontUrl", FrontUrl },
                { "customerInfo", CustomerInfo },
                { "cardTransData", CardTransData },
                { "accountPayChannel", AccountPayChannel },
                { "accNo", AccNo },
                { "accType", AccType },
                { "reserved", Reserved },
                { "customerIp", CustomerIp },
                { "orderTimeout", OrderTimeOut },
                { "accSplitData", AccSplitData },
                { "riskRateInfo", RiskRateInfo },
                { "defaultPayType", DefaultPayType },
                { "reqReserved", ReqReserved },
                { "frontFailUrl", FrontFailUrl },
                { "supPayType", SupPayType },
                { "payTimeout", PayTimeOut },
                { "termId", TermId },
                { "userMac", UserMac }
            };
            return parameters;
        }

        public string GetRequestUrl(bool isTest)
        {
            return isTest ? "https://gateway.test.95516.com/gateway/api/frontTransReq.do" : "https://gateway.95516.com/gateway/api/frontTransReq.do";
        }

        public bool HasEncryptCertId()
        {
            return true;
        }

        #endregion
    }
}
