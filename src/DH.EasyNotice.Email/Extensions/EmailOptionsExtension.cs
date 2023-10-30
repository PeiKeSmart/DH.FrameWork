using EasyNotice.Email.Options;
using EasyNotice.Email.Provider;
using EasyNotice.Extensions;

using Microsoft.Extensions.DependencyInjection;

namespace EasyNotice.Email.Extensions;

internal class EmailOptionsExtension : IEasyNoticeOptionsExtension {
    private readonly Action<EmailOptions> configure;

    public EmailOptionsExtension(Action<EmailOptions> configure)
    {
        this.configure = configure;
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddOptions();
        services.AddOptions<EmailOptions>().Configure(configure);
        services.AddTransient<IEmailProvider, EmailProvider>();
    }
}