﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayFundJointaccountMemberUnbindModel Data Structure.
    /// </summary>
    public class AlipayFundJointaccountMemberUnbindModel : AlipayObject
    {
        /// <summary>
        /// 账本id
        /// </summary>
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; }

        /// <summary>
        /// 授权协议号
        /// </summary>
        [JsonPropertyName("agreement_no")]
        public string AgreementNo { get; set; }

        /// <summary>
        /// 场景码
        /// </summary>
        [JsonPropertyName("biz_scene")]
        public string BizScene { get; set; }

        /// <summary>
        /// 成员账号： identity_type是ALIPAY_USER_ID填支付宝会员ID（2088开头）； 是ALIPAY_LOGON_ID 填支付宝登录号
        /// </summary>
        [JsonPropertyName("identity")]
        public string Identity { get; set; }

        /// <summary>
        /// 账号类型，目前支持如下类型： 1、ALIPAY_USER_ID 支付宝的会员ID 2、ALIPAY_LOGON_ID：支付宝登录号，支持邮箱和手机号格式
        /// </summary>
        [JsonPropertyName("identity_type")]
        public string IdentityType { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// 产品码
        /// </summary>
        [JsonPropertyName("product_code")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 成员当前状态： 邀请中（PROCESSING）、正常（NORMAL）
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
