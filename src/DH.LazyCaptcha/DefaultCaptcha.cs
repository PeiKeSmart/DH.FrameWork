﻿using DH.LazyCaptcha.Generator.Code;
using DH.LazyCaptcha.Generator.Image;
using DH.LazyCaptcha.Storage;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DH.LazyCaptcha
{
    /// <summary>
    /// 默认验证码服务
    /// </summary>
    public class DefaultCaptcha : ICaptcha
    {
        protected CaptchaOptions _options;
        protected IStorage _storage;
        protected ICaptchaCodeGenerator _captchaCodeGenerator;
        protected ICaptchaImageGenerator _captchaImageGenerator;

        public DefaultCaptcha(IOptionsSnapshot<CaptchaOptions> options, IStorage storage)
        {
            _options = options.Value;
            _storage = storage;

            ChangeOptions(_options);

            _captchaCodeGenerator = new DefaultCaptchaCodeGenerator(_options.CaptchaType);
            _captchaImageGenerator = new DefaultCaptchaImageGenerator();
        }

        // 选项更新
        protected virtual void ChangeOptions(CaptchaOptions options)
        {

        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="captchaId">验证码id</param>
        /// <param name="expirySeconds">缓存时间，未设定则使用配置时间</param>
        /// <returns></returns>
        public virtual CaptchaData Generate(string captchaId, int? expirySeconds = null)
        {
            var (renderText, code) = _captchaCodeGenerator.Generate(_options.CodeLength);
            var image = _captchaImageGenerator.Generate(renderText, _options.ImageOption);
            expirySeconds = expirySeconds.HasValue ? expirySeconds.Value : _options.ExpirySeconds;
            _storage.Set(captchaId, code, DateTime.Now.AddSeconds(expirySeconds.Value).ToUniversalTime());

            return new CaptchaData(captchaId, code, image);
        }

        /// <summary>
        /// 使用session及固定Key
        /// </summary>
        /// <returns></returns>
        public virtual CaptchaData Generate()
        {
            var captchaId = "ybbcode";

            var (renderText, code) = _captchaCodeGenerator.Generate(_options.CodeLength);
            var image = _captchaImageGenerator.Generate(renderText, _options.ImageOption);

            Pek.Webs.HttpContext.Current.Session.SetString(captchaId, code);

            return new CaptchaData(captchaId, code, image);
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="captchaId">验证码id</param>
        /// <param name="code">用户输入的验证码</param>
        /// <param name="removeIfSuccess">校验成功时是否移除缓存(用于多次验证)</param>
        /// <returns></returns>
        public virtual bool Validate(string captchaId, string code, bool removeIfSuccess = true)
        {
            var val = _storage.Get(captchaId);
            var comparisonType = _options.IgnoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
            var success = !string.IsNullOrWhiteSpace(code) &&
              !string.IsNullOrWhiteSpace(val) &&
              string.Equals(val, code, comparisonType);

            if (!success || (success && removeIfSuccess))
            {
                _storage.Remove(captchaId);
            }

            return success;
        }
    }
}
