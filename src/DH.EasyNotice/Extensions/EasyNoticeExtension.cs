using EasyNotice.Options;

using Microsoft.Extensions.DependencyInjection;

namespace EasyNotice.Extensions;

public static class EasyNoticeExtension {
    public static IServiceCollection AddEasyNotice(this IServiceCollection services, Action<NoticeOptions> configure)
    {
        var options = new NoticeOptions();
        configure?.Invoke(options);

        services.AddOptions();
        services.AddOptions<NoticeOptions>().Configure(configure);

        foreach (var serviceExtension in options.Extensions)
        {
            serviceExtension.AddServices(services);
        }
        return services;
    }
}