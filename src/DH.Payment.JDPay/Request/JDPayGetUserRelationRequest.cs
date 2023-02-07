﻿using System.Collections.Generic;
using DH.Payment.JDPay.Response;

namespace DH.Payment.JDPay.Request
{
    /// <summary>
    /// 用户关系查询接口
    /// </summary>
    public class JDPayGetUserRelationRequest : IJDPayRequest<JDPayGetUserRelationResponse>
    {
        /// <summary>
        /// 商户用户标识
        /// </summary>
        public string UserId { get; set; }

        #region IJDPayRequest Members

        private string ApiVersion = "V2.0";

        public string GetRequestUrl()
        {
            return "https://paygate.jd.com/service/getUserRelation";
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
                { "userId", UserId }
            };
            return parameters;
        }

        #endregion
    }
}
