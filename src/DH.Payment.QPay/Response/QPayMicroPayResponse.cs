﻿using System.Xml.Serialization;

namespace DH.Payment.QPay.Response
{
    [XmlRoot("xml")]
    public class QPayMicroPayResponse : QPayResponse
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        [XmlElement("return_code")]
        public string ReturnCode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        [XmlElement("return_msg")]
        public string ReturnMsg { get; set; }

        /// <summary>
        /// 手Q CGI原始错误码
        /// </summary>
        [XmlElement("retcode")]
        public string RetCode { get; set; }

        /// <summary>
        /// 手Q CGI原始错误信息
        /// </summary>
        [XmlElement("retmsg")]
        public string RetMsg { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        [XmlElement("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 子商户应用ID
        /// </summary>
        [XmlElement("sub_appid")]
        public string SubAppId { get; set; }

        /// <summary>
        /// 商户号ID
        /// </summary>
        [XmlElement("mch_id")]
        public string MchId { get; set; }

        /// <summary>
        /// 子商户号
        /// </summary>
        [XmlElement("sub_mch_id")]
        public string SubMchId { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [XmlElement("sign")]
        public string Sign { get; set; }

        /// <summary>
        /// 业务结果
        /// </summary>
        [XmlElement("result_code")]
        public string ResultCode { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        [XmlElement("err_code")]
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误描述	
        /// </summary>
        [XmlElement("err_code_des")]
        public string ErrCodeDes { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        [XmlElement("nonce_str")]
        public string NonceStr { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        [XmlElement("device_info")]
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 交易场景
        /// </summary>
        [XmlElement("trade_type")]
        public string TradeType { get; set; }

        /// <summary>
        /// 交易状态
        /// </summary>
        [XmlElement("trade_state")]
        public string TradeState { get; set; }

        /// <summary>
        /// 付款银行
        /// </summary>
        [XmlElement("bank_type")]
        public string BankType { get; set; }

        /// <summary>
        /// 货币种类
        /// </summary>
        [XmlElement("fee_type")]
        public string FeeType { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        [XmlElement("total_fee")]
        public int TotalFee { get; set; }

        /// <summary>
        /// 用户支付金额	
        /// </summary>
        [XmlElement("cash_fee")]
        public int CashFee { get; set; }

        /// <summary>
        /// QQ钱包优惠金额	
        /// </summary>
        [XmlElement("coupon_fee")]
        public int CouponFee { get; set; }

        /// <summary>
        /// QQ钱包订单号	
        /// </summary>
        [XmlElement("transaction_id")]
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户订单号	
        /// </summary>
        [XmlElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 附加数据	
        /// </summary>
        [XmlElement("attach")]
        public string Attach { get; set; }

        /// <summary>
        /// 支付完成时间	
        /// </summary>
        [XmlElement("time_end")]
        public string TimeEnd { get; set; }

        /// <summary>
        /// 交易状态描述
        /// </summary>
        [XmlElement("trade_state_desc")]
        public string TradeStateDesc { get; set; }

        /// <summary>
        /// 用户标识	
        /// </summary>
        [XmlElement("openid")]
        public string OpenId { get; set; }
    }
}
