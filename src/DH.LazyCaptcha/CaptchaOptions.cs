﻿using DH.LazyCaptcha.Generator.Image.Option;

namespace DH.LazyCaptcha
{
    public class CaptchaOptions
    {
        private const int DEFAULT_CODE_LENGTH = 4;
        private CaptchaType _captchaType = CaptchaType.DEFAULT;

        /// <summary>
        /// 验证码类型
        /// </summary>
        public CaptchaType CaptchaType
        {
            get { return _captchaType; }
            set
            {
                if (value.IsArithmetic())
                {
                    if (this.CodeLength == DEFAULT_CODE_LENGTH)
                    {
                        this.CodeLength = 2;
                    }
                }

                if (value.ContainsChinese())
                {
                    this.ImageOption.FontFamily = DefaultFontFamilys.Instance.Kaiti;
                }

                _captchaType = value;
            }
        }

        /// <summary>
        /// 验证码长度 当CaptchaType为ARITHMETIC, ARITHMETIC_ZH时， 长度代表乘数个数
        /// </summary>
        public int CodeLength { get; set; } = DEFAULT_CODE_LENGTH;

        /// <summary>
        /// 过期时长
        /// </summary>
        public int ExpirySeconds { get; set; } = 60;

        /// <summary>
        /// code比较是否忽略大小写
        /// </summary>
        public bool IgnoreCase { get; set; } = true;

        /// <summary>
        /// 存储键前缀
        /// </summary>
        public string StoreageKeyPrefix { get; set; }

        /// <summary>
        /// 图片选项
        /// </summary>
        public CaptchaImageGeneratorOption ImageOption { get; set; } = new CaptchaImageGeneratorOption();
    }
}
