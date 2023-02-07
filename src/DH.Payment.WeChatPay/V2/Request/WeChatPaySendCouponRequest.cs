﻿using System.Collections.Generic;
using DH.Payment.WeChatPay.V2.Response;

namespace DH.Payment.WeChatPay.V2.Request
{
    /// <summary>
    /// 发放代金券 (普通商户)
    /// </summary>
    public class WeChatPaySendCouponRequest : IWeChatPayCertRequest<WeChatPaySendCouponResponse>
    {
        /// <summary>
        /// 代金券批次id
        /// </summary>
        public string CouponStockId { get; set; }

        /// <summary>
        /// openid记录数
        /// </summary>
        public string OpenIdCount { get; set; }

        /// <summary>
        /// 商户单据号
        /// </summary>
        public string PartnerTradeNo { get; set; }

        /// <summary>
        /// 用户openid
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string OpUserId { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 协议版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 协议类型
        /// </summary>
        public string Type { get; set; }

        #region IWeChatPayCertRequest Members

        private string requestUrl = "https://api.mch.weixin.qq.com/mmpaymkttransfers/send_coupon";
        private WeChatPaySignType signType = WeChatPaySignType.MD5;

        public string GetRequestUrl()
        {
            return requestUrl;
        }

        public void SetRequestUrl(string url)
        {
            requestUrl = url;
        }

        public IDictionary<string, string> GetParameters()
        {
            var parameters = new WeChatPayDictionary
            {
                { "coupon_stock_id", CouponStockId },
                { "openid_count", OpenIdCount },
                { "partner_trade_no", PartnerTradeNo },
                { "openid", OpenId },
                { "op_user_id", OpUserId },
                { "device_info", DeviceInfo },
                { "version", Version },
                { "type", Type }
            };
            return parameters;
        }

        public WeChatPaySignType GetSignType()
        {
            return signType;
        }

        public void SetSignType(WeChatPaySignType signType)
        {
            this.signType = signType switch
            {
                WeChatPaySignType.MD5 => signType,
                _ => throw new WeChatPayException("api only support MD5!"),
            };
        }

        public void PrimaryHandler(WeChatPayDictionary sortedTxtParams, WeChatPayOptions options)
        {
            sortedTxtParams.Add(WeChatPayConsts.nonce_str, WeChatPayUtility.GenerateNonceStr());
            sortedTxtParams.Add(WeChatPayConsts.appid, options.AppId);
            sortedTxtParams.Add(WeChatPayConsts.mch_id, options.MchId);

            sortedTxtParams.Add(WeChatPayConsts.sign, WeChatPaySignature.SignWithKey(sortedTxtParams, options.APIKey, signType));
        }

        #endregion
    }
}
