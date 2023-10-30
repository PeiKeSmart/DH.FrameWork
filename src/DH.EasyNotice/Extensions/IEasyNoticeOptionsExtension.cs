using Microsoft.Extensions.DependencyInjection;

namespace EasyNotice.Extensions;

/// <summary>
/// options extension.
/// </summary>
public interface IEasyNoticeOptionsExtension {
    /// <summary>
    /// Adds the services.
    /// </summary>
    /// <param name="services">Services.</param>
    void AddServices(IServiceCollection services);
}