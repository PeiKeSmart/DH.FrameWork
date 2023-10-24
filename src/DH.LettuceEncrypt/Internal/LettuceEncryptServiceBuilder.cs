using Microsoft.Extensions.DependencyInjection;

namespace LettuceEncrypt.Internal;

internal class LettuceEncryptServiceBuilder : ILettuceEncryptServiceBuilder
{
    public LettuceEncryptServiceBuilder(IServiceCollection services)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public IServiceCollection Services { get; }
}
