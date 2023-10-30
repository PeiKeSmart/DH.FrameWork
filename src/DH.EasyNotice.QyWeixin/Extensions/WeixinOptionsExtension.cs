using EasyNotice.Extensions;
using EasyNotice.QyWeixin.Options;
using EasyNotice.QyWeixin.Provider;

using Microsoft.Extensions.DependencyInjection;

namespace EasyNotice.QyWeixin.Extensions;

public class WeixinOptionsExtension : IEasyNoticeOptionsExtension {
    private readonly Action<WeixinOptions> configure;

    public WeixinOptionsExtension(Action<WeixinOptions> configure)
    {
        this.configure = configure;
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddOptions();
        services.AddOptions<WeixinOptions>().Configure(configure);
        services.AddTransient<IWeixinProvider, WeixinProvider>();
    }
}