using LettuceEncrypt;
using LettuceEncrypt.Acme;
using LettuceEncrypt.Internal;
using LettuceEncrypt.Internal.AcmeStates;
using LettuceEncrypt.Internal.IO;
using LettuceEncrypt.Internal.PfxBuilder;
using McMaster.AspNetCore.Kestrel.Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 用于配置Lettuce Encrypt服务的Helper方法。
/// </summary>
public static class LettuceEncryptServiceCollectionExtensions
{
    /// <summary>
    /// 添加将自动为此服务器生成HTTPS证书的服务。
    /// 默认情况下，它使用Let's Encrypt (<see href="https://letsencrypt.org/">https://letsencrypt.org/</see>).
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static ILettuceEncryptServiceBuilder AddLettuceEncrypt(this IServiceCollection services)
        => services.AddLettuceEncrypt(_ => { });

    /// <summary>
    /// 添加将自动为此服务器生成HTTPS证书的服务。
    /// 默认情况下，它使用Let's Encrypt(<see href="https://letsencrypt.org/">https://letsencrypt.org/</see>).
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure">用于配置选项的回调</param>
    /// <returns></returns>
    public static ILettuceEncryptServiceBuilder AddLettuceEncrypt(this IServiceCollection services,
        Action<LettuceEncryptOptions> configure)
    {
        // 疑似因此导致部署localhost的https无法正常使用,增加配置项
        services.AddTransient<IConfigureOptions<KestrelServerOptions>, KestrelOptionsSetup>();

        services.TryAddSingleton<ICertificateAuthorityConfiguration, DefaultCertificateAuthorityConfiguration>();

        services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<LettuceEncryptOptions>, OptionsValidation>());

        services
            .AddSingleton<CertificateSelector>()
            .AddSingleton<IServerCertificateSelector>(s => s.GetRequiredService<CertificateSelector>())
            .AddSingleton<IConsole>(PhysicalConsole.Singleton)
            .AddSingleton<IClock, SystemClock>()
            .AddSingleton<TermsOfServiceChecker>()
            .AddSingleton<IHostedService, StartupCertificateLoader>()
            .AddSingleton<ICertificateSource, DeveloperCertLoader>()
            .AddSingleton<IHostedService, AcmeCertificateLoader>()
            .AddSingleton<AcmeCertificateFactory>()
            .AddSingleton<AcmeClientFactory>()
            .AddSingleton<IHttpChallengeResponseStore, InMemoryHttpChallengeResponseStore>()
            .AddSingleton<X509CertStore>()
            .AddSingleton<ICertificateSource>(x => x.GetRequiredService<X509CertStore>())
            .AddSingleton<ICertificateRepository>(x => x.GetRequiredService<X509CertStore>())
            .AddSingleton<HttpChallengeResponseMiddleware>()
            .AddSingleton<TlsAlpnChallengeResponder>()
            .AddSingleton<IStartupFilter, HttpChallengeStartupFilter>()
            .AddSingleton<IDnsChallengeProvider, NoOpDnsChallengeProvider>();

        services.AddSingleton<IConfigureOptions<LettuceEncryptOptions>>(s =>
        {
            var config = s.GetService<IConfiguration?>();
            return new ConfigureOptions<LettuceEncryptOptions>(options => config?.Bind("LettuceEncrypt", options));
        });

        services.Configure(configure);

        // 状态机应该在自己的作用域中运行
        services.AddScoped<AcmeStateMachineContext>();

        services.AddSingleton(TerminalState.Singleton);

        // 状态应始终是瞬态的
        services
            .AddTransient<ServerStartupState>()
            .AddTransient<CheckForRenewalState>()
            .AddTransient<BeginCertificateCreationState>();

        // PfxBuilderFactory是无状态的，因此不需要临时注册
        services.AddSingleton<IPfxBuilderFactory, PfxBuilderFactory>();

        return new LettuceEncryptServiceBuilder(services);
    }
}
