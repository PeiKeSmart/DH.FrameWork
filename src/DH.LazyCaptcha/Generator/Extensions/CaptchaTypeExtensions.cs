namespace DH.LazyCaptcha
{
    public static class CaptchaTypeExtensions
    {
        public static bool ContainsChinese(this CaptchaType captchaType)
        {
            return captchaType == CaptchaType.ARITHMETIC_ZH ||
                   captchaType == CaptchaType.NUMBER_ZH_CN ||
                   captchaType == CaptchaType.NUMBER_ZH_HK ||
                   captchaType == CaptchaType.CHINESE;
        }

        public static bool IsArithmetic(this CaptchaType captchaType)
        {
            return captchaType == CaptchaType.ARITHMETIC || captchaType == CaptchaType.ARITHMETIC_ZH;
        }
    }
}
