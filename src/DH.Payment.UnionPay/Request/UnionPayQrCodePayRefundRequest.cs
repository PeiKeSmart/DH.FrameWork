﻿using System.Collections.Generic;
using DH.Payment.UnionPay.Response;

namespace DH.Payment.UnionPay.Request
{
    /// <summary>
    /// 二维码支付(V2.2) 退货类（退货）
    /// </summary>
    public class UnionPayQrCodePayRefundRequest : IUnionPayRequest<UnionPayQrCodePayRefundResponse>
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
        /// 接入机构代码
        /// </summary>
        public string AccInsCode { get; set; }

        /// <summary>
        /// 原交易查询流水号
        /// </summary>
        public string OrigQryId { get; set; }

        /// <summary>
        /// 原交易商户订单号
        /// </summary>
        public string OrigOrderId { get; set; }

        /// <summary>
        /// 原交易商户发送交易时间
        /// </summary>
        public string OrigTxnTime { get; set; }

        /// <summary>
        /// 保留域
        /// </summary>
        public string Reserved { get; set; }

        /// <summary>
        /// 分账域
        /// </summary>
        public string AccSplitData { get; set; }

        /// <summary>
        /// 请求方保留域
        /// </summary>
        public string ReqReserved { get; set; }

        /// <summary>
        /// 终端号
        /// </summary>
        public string TermId { get; set; }

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
                { "txnType", TxnType },
                { "txnSubType", TxnSubType },
                { "channelType", ChannelType },
                { "backUrl", BackUrl },
                { "txnAmt", TxnAmt },
                { "orderId", OrderId  },
                { "subMerId", SubMerId },
                { "subMerAbbr", SubMerAbbr },
                { "subMerName", SubMerName },
                { "accInsCode", AccInsCode },
                { "origQryId", OrigQryId },
                { "origOrderId", OrigOrderId },
                { "origTxnTime", OrigTxnTime },
                { "reserved", Reserved },
                { "accSplitData", AccSplitData },
                { "reqReserved", ReqReserved },
                { "termId", TermId }
            };
            return parameters;
        }

        public string GetRequestUrl(bool isTest)
        {
            return isTest ? "https://gateway.test.95516.com/gateway/api/backTransReq.do" : "https://gateway.95516.com/gateway/api/backTransReq.do";
        }

        public bool HasEncryptCertId()
        {
            return false;
        }

        #endregion
    }
}
