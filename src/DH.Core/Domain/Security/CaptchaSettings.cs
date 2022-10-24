using DH.Core.Configuration;

namespace DH.Core.Domain.Security
{
    /// <summary>
    /// CAPTCHA设置
    /// </summary>
    public partial class CaptchaSettings : ISettings
    {
        /// <summary>
        /// 是否启用CAPTCHA？
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// reCAPTCHA类型
        /// </summary>
        public CaptchaType CaptchaType { get; set; }

        /// <summary>
        /// 指示是否应在登录页面上显示CAPTCHA的值
        /// </summary>
        public bool ShowOnLoginPage { get; set; }

        /// <summary>
        /// 指示是否应在注册页面上显示CAPTCHA的值
        /// </summary>
        public bool ShowOnRegistrationPage { get; set; }

        /// <summary>
        /// 指示是否应在联系人页面上显示CAPTCHA的值
        /// </summary>
        public bool ShowOnContactUsPage { get; set; }

        /// <summary>
        /// 指示CAPTCHA是否应显示在愿望列表页面上的值
        /// </summary>
        public bool ShowOnEmailWishlistToFriendPage { get; set; }

        /// <summary>
        /// 指示CAPTCHA是否应显示在“向朋友发送电子邮件”页面上的值
        /// </summary>
        public bool ShowOnEmailProductToFriendPage { get; set; }

        /// <summary>
        /// 指示CAPTCHA是否应显示在“评论博客”页面上的值
        /// </summary>
        public bool ShowOnBlogCommentPage { get; set; }

        /// <summary>
        /// 指示是否应在“评论新闻”页面上显示CAPTCHA的值
        /// </summary>
        public bool ShowOnNewsCommentPage { get; set; }

        /// <summary>
        /// 指示是否应在产品评论页面上显示CAPTCHA的值
        /// </summary>
        public bool ShowOnProductReviewPage { get; set; }

        /// <summary>
        /// 指示是否应在“申请供应商帐户”页面上显示CAPTCHA的值
        /// </summary>
        public bool ShowOnApplyVendorPage { get; set; }

        /// <summary>
        /// 指示是否应在“忘记密码”页面上显示CAPTCHA的值
        /// </summary>
        public bool ShowOnForgotPasswordPage { get; set; }

        /// <summary>
        /// 指示是否应在论坛上显示CAPTCHA的值
        /// </summary>
        public bool ShowOnForum { get; set; }

        /// <summary>
        /// 一个值，指示是否应在客户结账页面上显示CAPTCHA
        /// </summary>
        public bool ShowOnCheckoutPageForGuests { get; set; }

        /// <summary>
        /// 基本reCAPTCHA API URL
        /// </summary>
        public string ReCaptchaApiUrl { get; set; }
        /// <summary>
        /// reCAPTCHA公钥
        /// </summary>
        public string ReCaptchaPublicKey { get; set; }

        /// <summary>
        /// reCAPTCHA私钥
        /// </summary>
        public string ReCaptchaPrivateKey { get; set; }

        /// <summary>
        /// reCAPTCHA V3得分阈值
        /// </summary>
        public decimal ReCaptchaV3ScoreThreshold { get; set; }

        /// <summary>
        /// reCAPTCHA主题
        /// </summary>
        public string ReCaptchaTheme { get; set; }

        /// <summary>
        /// reCAPTCHA请求超时前的时间长度（秒）
        /// </summary>
        public int? ReCaptchaRequestTimeout { get; set; }
        /// <summary>
        /// reCAPTCHA默认语言
        /// </summary>
        public string ReCaptchaDefaultLanguage { get; set; }

        /// <summary>
        /// 指示是否应自动设置reCAPTCHA语言的值
        /// </summary>
        public bool AutomaticallyChooseLanguage { get; set; }
    }
}
