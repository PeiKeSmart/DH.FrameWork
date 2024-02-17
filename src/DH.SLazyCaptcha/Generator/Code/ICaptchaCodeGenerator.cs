namespace DH.SLazyCaptcha.Generator.Code;

public interface ICaptchaCodeGenerator {
    (string renderText, string code) Generate(int length);
}