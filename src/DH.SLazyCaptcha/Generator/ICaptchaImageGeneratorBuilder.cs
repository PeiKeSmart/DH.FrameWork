using DH.SLazyCaptcha.Generator.Image.Option;

namespace DH.SLazyCaptcha.Generator;

public interface ICaptchaImageOptionBuilder {
    CaptchaImageGeneratorOption Build();
}