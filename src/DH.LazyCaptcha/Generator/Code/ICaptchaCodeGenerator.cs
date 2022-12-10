namespace DH.LazyCaptcha.Generator.Code
{
    public interface ICaptchaCodeGenerator
    {
        (string renderText, string code) Generate(int length);
    }
}
