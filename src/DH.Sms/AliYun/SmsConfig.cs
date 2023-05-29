namespace DG.Sms.AliYun
{
    /// <summary>
    /// AliYun短信配置
    /// </summary>
    public class SmsConfig
    {
        /// <summary>
        /// 密钥Id
        /// </summary>
        public string AccessKeyId { get; set; }

        /// <summary>
        /// 密钥密码
        /// </summary>
        public string AccessKeySecret { get; set; }

        /// <summary>
        /// 短信签名名称
        /// </summary>
        public string SignName { get; set; }
    }
}
