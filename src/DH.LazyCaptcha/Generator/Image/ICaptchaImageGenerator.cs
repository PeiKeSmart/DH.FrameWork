using DH.LazyCaptcha.Generator.Image.Option;

namespace DH.LazyCaptcha.Generator.Image
{
    /// <summary>
    /// 验证码生成器
    /// </summary>
    public interface ICaptchaImageGenerator
    {
        byte[] Generate(string text, CaptchaImageGeneratorOption option);
    }
}
