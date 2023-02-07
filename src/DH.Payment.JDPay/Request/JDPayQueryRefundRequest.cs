﻿using System.Collections.Generic;
using DG.Payment.JDPay.Response;

namespace DG.Payment.JDPay.Request
{
    /// <summary>
    /// 正单号退款查询接口
    /// </summary>
    public class JDPayQueryRefundRequest : IJDPayRequest<JDPayQueryRefundResponse>
    {
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string TradeNum { get; set; }

        /// <summary>
        /// 原交易流水号
        /// </summary>
        public string OTradeNum { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public string TradeType { get; set; }


        #region IJDPayRequest Members

        private string ApiVersion = "V2.0";

        public string GetRequestUrl()
        {
            return "https://paygate.jd.com/service/queryRefund";
        }

        public void SetApiVersion(string apiVersion)
        {
            ApiVersion = apiVersion;
        }

        public string GetApiVersion()
        {
            return ApiVersion;
        }

        public IDictionary<string, string> GetParameters()
        {
            var parameters = new JDPayDictionary
            {
                { "tradeNum", TradeNum },
                { "oTradeNum", OTradeNum },
                { "tradeType", TradeType }
            };
            return parameters;
        }

        #endregion
    }
}
