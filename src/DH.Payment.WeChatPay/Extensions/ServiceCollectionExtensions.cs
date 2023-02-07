using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWeChatPay(this IServiceCollection services)
        {
            services.AddHttpClient(DG.Payment.WeChatPay.V2.WeChatPayClient.Name);
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, DG.Payment.WeChatPay.V2.WeChatPayHttpMessageHandlerBuilderFilter>());
            services.AddSingleton<DG.Payment.WeChatPay.V2.WeChatPayClientCertificateManager>();
            services.AddSingleton<DG.Payment.WeChatPay.V2.IWeChatPayClient, DG.Payment.WeChatPay.V2.WeChatPayClient>();
            services.AddSingleton<DG.Payment.WeChatPay.V2.IWeChatPayNotifyClient, DG.Payment.WeChatPay.V2.WeChatPayNotifyClient>();

            services.AddHttpClient(DG.Payment.WeChatPay.V3.WeChatPayClient.Name);
            services.AddSingleton<DG.Payment.WeChatPay.V3.WeChatPayPlatformCertificateManager>();
            services.AddSingleton<DG.Payment.WeChatPay.V3.IWeChatPayClient, DG.Payment.WeChatPay.V3.WeChatPayClient>();
            services.AddSingleton<DG.Payment.WeChatPay.V3.IWeChatPayNotifyClient, DG.Payment.WeChatPay.V3.WeChatPayNotifyClient>();
        }
    }
}
