using DH.SLazyCaptcha.Generator;
using DH.SLazyCaptcha.Generator.Code;
using DH.SLazyCaptcha.Storage;

namespace DH.SLazyCaptcha;

public class CaptchaService {
    private readonly CaptchaOptions CaptchaOptions;
    private readonly IStorage Storage;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="captchaOptions"></param>
    public CaptchaService(CaptchaOptions captchaOptions, IStorage storage)
    {
        CaptchaOptions = captchaOptions;
        Storage = storage;
    }

    /// <summary>
    /// 生成验证码
    /// </summary>
    /// <param name="captchaId">验证码id</param>
    /// <param name="expirySeconds">缓存时间，未设定则使用配置时间</param>
    /// <returns></returns>
    public virtual CaptchaData Generate(string captchaId, int? expirySeconds = null)
    {
        var _captchaCodeGenerator = GetCodeGenerator();
        var _captchaImageGenerator = GetImmageGenerator();

        var (renderText, code) = _captchaCodeGenerator.Generate(CaptchaOptions.CodeLength);
        var image = _captchaImageGenerator.Generate(renderText, CaptchaOptions.ImageOption);
        expirySeconds = expirySeconds.HasValue ? expirySeconds.Value : CaptchaOptions.ExpirySeconds;
        Storage.Set(captchaId, code, DateTime.Now.AddSeconds(expirySeconds.Value).ToUniversalTime());

        return new CaptchaData(captchaId, code, image);
    }

    protected virtual ICaptchaImageGenerator GetImmageGenerator()
    {
        return new DefaultCaptchaImageGenerator();
    }

    protected virtual ICaptchaCodeGenerator GetCodeGenerator()
    {
        return new DefaultCaptchaCodeGenerator(CaptchaOptions.CaptchaType);
    }

    /// <summary>
    /// 校验
    /// </summary>
    /// <param name="captchaId">验证码id</param>
    /// <param name="code">用户输入的验证码</param>
    /// <param name="removeIfSuccess">校验成功时是否移除缓存(用于多次验证)</param>
    /// <returns></returns>
    public virtual bool Validate(string captchaId, string code, bool removeIfSuccess = true, bool removeIfFail = true)
    {
        var val = Storage.Get(captchaId);
        var comparisonType = CaptchaOptions.IgnoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
        var success = !string.IsNullOrWhiteSpace(code) &&
                      !string.IsNullOrWhiteSpace(val) &&
                      string.Equals(val, code, comparisonType);

        if ((!success && removeIfFail) || (success && removeIfSuccess))
        {
            Storage.Remove(captchaId);
        }

        return success;
    }
}