using Microsoft.Extensions.DependencyInjection;

namespace DH.LazyCaptcha
{
    internal class CaptchaBuilder : ICaptchaBuilder
    {
        public CaptchaBuilder(IServiceCollection services)
        {
            Services = services;
        }
        public IServiceCollection Services { get; }
    }
}
