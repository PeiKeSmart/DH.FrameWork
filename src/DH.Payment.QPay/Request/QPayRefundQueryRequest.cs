﻿using System.Collections.Generic;
using DH.Payment.QPay.Response;
using DH.Payment.QPay.Utility;

namespace DH.Payment.QPay.Request
{
    /// <summary>
    /// 退款查询 (普通商户 / 服务商)
    /// </summary>
    public class QPayRefundQueryRequest : IQPayRequest<QPayRefundQueryResponse>
    {
        /// <summary>
        /// 子商户应用ID
        /// </summary>
        public string SubAppId { get; set; }

        /// <summary>
        /// 子商户号
        /// </summary>
        public string SubMchId { get; set; }

        /// <summary>
        /// QQ钱包退款单号
        /// </summary>
        public string RefundId { get; set; }

        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string OutRefundNo { get; set; }

        /// <summary>
        /// QQ钱包订单号
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        #region IQPayRequest Members

        public string GetRequestUrl()
        {
            return "https://qpay.qq.com/cgi-bin/pay/qpay_refund_query.cgi";
        }

        public IDictionary<string, string> GetParameters()
        {
            var parameters = new QPayDictionary
            {
                { "refund_id", RefundId },
                { "out_refund_no", OutRefundNo },
                { "transaction_id", TransactionId },
                { "out_trade_no", OutTradeNo }
            };
            return parameters;
        }

        public void PrimaryHandler(QPayOptions options, QPayDictionary sortedTxtParams)
        {
            sortedTxtParams.Add(QPayConsts.NONCE_STR, QPayUtility.GenerateNonceStr());
            sortedTxtParams.Add(QPayConsts.APPID, options.AppId);
            sortedTxtParams.Add(QPayConsts.SUB_APPID, options.SubAppId);
            sortedTxtParams.Add(QPayConsts.MCH_ID, options.MchId);
            sortedTxtParams.Add(QPayConsts.SUB_MCH_ID, options.SubMchId);

            sortedTxtParams.Add(QPayConsts.SIGN, QPaySignature.SignWithKey(sortedTxtParams, options.Key));
        }

        public bool GetNeedCheckSign()
        {
            return true;
        }

        #endregion
    }
}
