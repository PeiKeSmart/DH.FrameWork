using Microsoft.Extensions.DependencyInjection;

namespace DH.SLazyCaptcha;

public interface ICaptchaBuilder {
    IServiceCollection Services { get; }
}