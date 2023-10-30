using EasyNotice.Dingtalk.Options;
using EasyNotice.Dingtalk.Provider;
using EasyNotice.Extensions;

using Microsoft.Extensions.DependencyInjection;

namespace EasyNotice.Dingtalk.Extensions;

public class DingtalkOptionsExtension : IEasyNoticeOptionsExtension {
    private readonly Action<DingtalkOptions> configure;

    public DingtalkOptionsExtension(Action<DingtalkOptions> configure)
    {
        this.configure = configure;
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddOptions();
        services.AddOptions<DingtalkOptions>().Configure(configure);
        services.AddTransient<IDingtalkProvider, DingtalkProvider>();
    }
}