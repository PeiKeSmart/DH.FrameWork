using Microsoft.Extensions.DependencyInjection;

namespace LettuceEncrypt;

/// <summary>
/// An interface for building extension methods to extend LettuceEncrypt configuration.
/// </summary>
public interface ILettuceEncryptServiceBuilder
{
    /// <summary>
    /// The service collection.
    /// </summary>
    IServiceCollection Services { get; }
}
