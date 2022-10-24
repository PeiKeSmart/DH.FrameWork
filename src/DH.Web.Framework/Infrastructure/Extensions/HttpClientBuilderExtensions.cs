using DH.Core.Domain.Security;

using Microsoft.Extensions.DependencyInjection;

using System.Net;

namespace DH.Web.Framework.Infrastructure.Extensions
{
    /// <summary>
    /// 表示IHttpClientBuilder的扩展
    /// </summary>
    public static class HttpClientBuilderExtensions
    {
        /// <summary>
        /// 为HTTP客户端配置代理连接（如果启用）
        /// </summary>
        /// <param name="httpClientBuilder">用于配置HttpClient的生成器</param>
        public static void WithProxy(this IHttpClientBuilder httpClientBuilder)
        {
            httpClientBuilder.ConfigurePrimaryHttpMessageHandler(provider =>
            {
                var handler = new HttpClientHandler();

                // 是否启用代理
                var proxySettings = provider.GetService<ProxySettings>();
                if (!proxySettings?.Enabled ?? true)
                    return handler;

                // 配置代理连接
                var webProxy = new WebProxy($"{proxySettings.Address}:{proxySettings.Port}", proxySettings.BypassOnLocal);
                if (!string.IsNullOrEmpty(proxySettings.Username) && !string.IsNullOrEmpty(proxySettings.Password))
                {
                    webProxy.UseDefaultCredentials = false;
                    webProxy.Credentials = new NetworkCredential
                    {
                        UserName = proxySettings.Username,
                        Password = proxySettings.Password
                    };
                }
                else
                {
                    webProxy.UseDefaultCredentials = true;
                    webProxy.Credentials = CredentialCache.DefaultCredentials;
                }

                // 配置HTTP客户端处理程序
                handler.UseDefaultCredentials = webProxy.UseDefaultCredentials;
                handler.Proxy = webProxy;
                handler.PreAuthenticate = proxySettings.PreAuthenticate;

                return handler;
            });
        }
    }
}
