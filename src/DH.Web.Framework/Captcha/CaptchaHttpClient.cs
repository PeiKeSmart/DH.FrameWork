using DH.Core;
using DH.Core.Domain.Security;
using DH.Services.Security;
using DH.Web.Framework.Security;

using Microsoft.Net.Http.Headers;

using Newtonsoft.Json;

namespace DH.Web.Framework.Captcha
{
    /// <summary>
    /// 表示请求reCAPTCHA服务的HTTP客户端
    /// </summary>
    public partial class CaptchaHttpClient
    {
        #region Fields

        private readonly CaptchaSettings _captchaSettings;
        private readonly HttpClient _httpClient;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public CaptchaHttpClient(CaptchaSettings captchaSettings,
            HttpClient client,
            IWebHelper webHelper)
        {
            _captchaSettings = captchaSettings;
            _httpClient = client;
            _webHelper = webHelper;

            // 配置客户端
            client.BaseAddress = new Uri(captchaSettings.ReCaptchaApiUrl);
            client.DefaultRequestHeaders.Add(HeaderNames.UserAgent, $"dh-{DHVersion.CURRENT_VERSION}");

            if (captchaSettings.ReCaptchaRequestTimeout is int timeout && timeout > 0)
                client.Timeout = TimeSpan.FromSeconds(timeout);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 验证reCAPTCHA
        /// </summary>
        /// <param name="responseValue">响应值</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含异步任务，其结果包含reCAPTCHA服务的响应
        /// </returns>
        public virtual async Task<CaptchaResponse> ValidateCaptchaAsync(string responseValue)
        {
            // 准备请求的URL
            var url = string.Format(DHSecurityDefaults.RecaptchaValidationPath,
                _captchaSettings.ReCaptchaPrivateKey,
                responseValue,
                _webHelper.GetCurrentIpAddress());

            //get response
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<CaptchaResponse>(response);

        }

        #endregion
    }
}
