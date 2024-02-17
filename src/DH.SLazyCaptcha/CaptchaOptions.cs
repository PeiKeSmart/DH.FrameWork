using DH.SLazyCaptcha.Generator.Extensions;
using DH.SLazyCaptcha.Generator.Image.Option;

namespace DH.SLazyCaptcha;

public class CaptchaOptions {
    private const int DEFAULT_CODE_LENGTH = 4;
    private CaptchaType _captchaType = CaptchaType.DEFAULT;

    /// <summary>
    /// 验证码类型
    /// </summary>
    public virtual CaptchaType CaptchaType
    {
        get { return _captchaType; }
        set
        {
            if (value.IsArithmetic())
            {
                // 当是4时，长度重置为2。不过四位数的运算用户体验不佳，因此不必在意这个bug。
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
    /// 验证码长度
    /// </summary>
    public virtual int CodeLength { get; set; } = DEFAULT_CODE_LENGTH;

    /// <summary>
    /// 过期时长
    /// </summary>
    public virtual int ExpirySeconds { get; set; } = 60;

    /// <summary>
    /// code比较是否忽略大小写
    /// </summary>
    public virtual bool IgnoreCase { get; set; } = true;

    /// <summary>
    /// 存储键前缀
    /// </summary>
    public virtual string StoreageKeyPrefix { get; set; }

    /// <summary>
    /// 图片选项
    /// </summary>
    public virtual CaptchaImageGeneratorOption ImageOption { get; set; } = new CaptchaImageGeneratorOption();
}