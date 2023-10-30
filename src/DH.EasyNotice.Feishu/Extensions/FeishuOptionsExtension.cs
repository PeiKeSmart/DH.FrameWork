using EasyNotice.Feishu.Provider;
using EasyNotice.Options;

using Microsoft.Extensions.DependencyInjection;

namespace EasyNotice.Extensions;

public class FeishuOptionsExtension : IEasyNoticeOptionsExtension {
    private readonly Action<FeishuOptions> configure;

    public FeishuOptionsExtension(Action<FeishuOptions> configure)
    {
        this.configure = configure;
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddOptions();
        services.AddOptions<FeishuOptions>().Configure(configure);
        services.AddTransient<IFeishuProvider, FeishuProvider>();
    }
}