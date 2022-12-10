using Microsoft.Extensions.DependencyInjection;

namespace DH.LazyCaptcha
{
    public interface ICaptchaBuilder
    {
        IServiceCollection Services { get; }
    }
}
