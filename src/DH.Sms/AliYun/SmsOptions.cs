namespace DG.Sms.AliYun
{
    /// <summary>
    /// 短信配置
    /// </summary>
    public class SmsOptions
    {
        /// <summary>
        /// 阿里云短信配置
        /// </summary>
        public SmsConfig AliSmsOptions { get; set; } = new SmsConfig();
    }
}
