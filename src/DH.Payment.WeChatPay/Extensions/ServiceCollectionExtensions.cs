using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWeChatPay(this IServiceCollection services)
        {
            services.AddHttpClient(DH.Payment.WeChatPay.V2.WeChatPayClient.Name);
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, DH.Payment.WeChatPay.V2.WeChatPayHttpMessageHandlerBuilderFilter>());
            services.AddSingleton<DH.Payment.WeChatPay.V2.WeChatPayClientCertificateManager>();
            services.AddSingleton<DH.Payment.WeChatPay.V2.IWeChatPayClient, DH.Payment.WeChatPay.V2.WeChatPayClient>();
            services.AddSingleton<DH.Payment.WeChatPay.V2.IWeChatPayNotifyClient, DH.Payment.WeChatPay.V2.WeChatPayNotifyClient>();

            services.AddHttpClient(DH.Payment.WeChatPay.V3.WeChatPayClient.Name);
            services.AddSingleton<DH.Payment.WeChatPay.V3.WeChatPayPlatformCertificateManager>();
            services.AddSingleton<DH.Payment.WeChatPay.V3.IWeChatPayClient, DH.Payment.WeChatPay.V3.WeChatPayClient>();
            services.AddSingleton<DH.Payment.WeChatPay.V3.IWeChatPayNotifyClient, DH.Payment.WeChatPay.V3.WeChatPayNotifyClient>();
        }
    }
}
