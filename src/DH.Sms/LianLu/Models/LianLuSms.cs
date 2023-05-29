﻿using NewLife;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DG.Sms
{
    /// <summary>
    /// 短信配置
    /// </summary>
    [DisplayName("短信配置")]
    public class LianLuSms : SmsOptions
    {
        private String url = "http://47.110.199.86:8081";
        /// <summary>
        /// 短信网关地址
        /// </summary>
        [Description("短信网关地址")]
        public string Url
        {
            get => url;
            set
            {
                if (!value.IsNullOrEmpty())
                {
                    url = value;
                }
            }
        }

        /// <summary>
        /// 短信AccessKeyId
        /// </summary>
        [Description("短信AccessKeyId")]
        public string AccessKeyId { get; set; }

        /// <summary>
        /// 短信AccessKeySecret
        /// </summary>
        [Description("短信AccessKeySecret")]
        public string AccessKeySecret { get; set; }

        /// <summary>
        /// 短信签名
        /// </summary>
        [Description("短信签名")]
        public string passKey { get; set; } = "模块邦";
    }
}
