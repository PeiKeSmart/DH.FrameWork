using DH.SLazyCaptcha.Generator.Image.Option;

namespace DH.SLazyCaptcha.Generator;

/// <summary>
/// 验证码生成器
/// </summary>
public interface ICaptchaImageGenerator {
    byte[] Generate(string text, CaptchaImageGeneratorOption option);
}