namespace DH.Core.Domain.Security
{
    /// <summary>
    /// 表示reCAPTCHA的类型
    /// </summary>
    public enum CaptchaType
    {
        /// <summary>
        /// 使用reCAPTCHA v2复选框
        /// </summary>
        CheckBoxReCaptchaV2 = 10,

        /// <summary>
        /// 使用reCAPTCHA v3
        /// </summary>
        ReCaptchaV3 = 20,
    }
}
