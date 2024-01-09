using Microsoft.Extensions.DependencyInjection;

namespace DH.SLazyCaptcha;

internal class CaptchaBuilder : ICaptchaBuilder {
    public CaptchaBuilder(IServiceCollection services)
    {
        Services = services;
    }
    public IServiceCollection Services { get; }
}