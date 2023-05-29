using System;

namespace DG.Sms
{
    public class SmsOptions
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public Boolean IsEnabled { get; set; }

        /// <summary>
        /// 是否允许登录发送短信
        /// </summary>
        public Boolean SmsLogin { get; set; }

        /// <summary>
        /// 是否允许登录发送短信
        /// </summary>
        public Boolean SmsRegister { get; set; }

        /// <summary>
        /// 是否允找回密码发送短信
        /// </summary>
        public Boolean SmsPassword { get; set; }
    }
}
