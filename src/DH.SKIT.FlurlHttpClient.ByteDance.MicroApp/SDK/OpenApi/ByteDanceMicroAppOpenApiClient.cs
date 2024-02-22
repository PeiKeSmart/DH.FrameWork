using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;

namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.SDK.OpenApi
{
    /// <summary>
    /// 一个字节小程序服务商平台 API HTTP 客户端。
    /// </summary>
    public class ByteDanceMicroAppOpenApiClient : CommonClientBase, ICommonClient
    {
        /// <summary>
        /// 获取当前客户端使用的字节小程序凭证。
        /// </summary>
        public Settings.Credentials Credentials { get; }

        /// <summary>
        /// 用指定的配置项初始化 <see cref="ByteDanceMicroAppOpenApiClient"/> 类的新实例。
        /// </summary>
        /// <param name="options">配置项。</param>
        public ByteDanceMicroAppOpenApiClient(ByteDanceMicroAppOpenApiClientOptions options)
            : base()
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            Credentials = new Settings.Credentials(options);

            FlurlClient.BaseUrl = options.Endpoints ?? ByteDanceMicroAppOpenApiEndpoints.DEFAULT;
            FlurlClient.WithTimeout(TimeSpan.FromMilliseconds(options.Timeout));
        }

        /// <summary>
        /// 用指定的字节小程序服务商 AppId、字节小程序服务商 AppSecret 初始化 <see cref="ByteDanceMicroAppOpenApiClient"/> 类的新实例。
        /// </summary>
        /// <param name="componentAppId">字节小程序服务商 AppId。</param>
        /// <param name="componentAppSecret">字节小程序服务商 AppSecret。</param>
        public ByteDanceMicroAppOpenApiClient(string componentAppId, string componentAppSecret)
            : this(new ByteDanceMicroAppOpenApiClientOptions() { ComponentAppId = componentAppId, ComponentAppSecret = componentAppSecret })
        {
        }

        /// <summary>
        /// 使用当前客户端生成一个新的 <see cref="IFlurlRequest"/> 对象。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="method"></param>
        /// <param name="urlSegments"></param>
        /// <returns></returns>
        public IFlurlRequest CreateRequest(ByteDanceMicroAppOpenApiRequest request, HttpMethod method, params object[] urlSegments)
        {
            IFlurlRequest flurlRequest = FlurlClient.Request(urlSegments).WithVerb(method);

            if (request.ComponentAppId == null)
            {
                request.ComponentAppId = Credentials.ComponentAppId;
            }

            return flurlRequest;
        }

        /// <summary>
        /// 异步发起请求。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="flurlRequest"></param>
        /// <param name="httpContent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<T> SendRequestAsync<T>(IFlurlRequest flurlRequest, HttpContent? httpContent = null, CancellationToken cancellationToken = default)
            where T : ByteDanceMicroAppOpenApiResponse, new()
        {
            if (flurlRequest == null) throw new ArgumentNullException(nameof(flurlRequest));

            try
            {
                using IFlurlResponse flurlResponse = await base.SendFlurlRequestAsync(flurlRequest, httpContent, cancellationToken);
                return await WrapFlurlResponseAsJsonAsync<T>(flurlResponse, cancellationToken);
            }
            catch (FlurlHttpTimeoutException ex)
            {
                throw new Exceptions.ByteDanceMicroAppRequestTimeoutException(ex.Message, ex);
            }
            catch (FlurlHttpException ex)
            {
                throw new ByteDanceMicroAppException(ex.Message, ex);
            }
        }

        /// <summary>
        /// 异步发起请求。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="flurlRequest"></param>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<T> SendRequestWithJsonAsync<T>(IFlurlRequest flurlRequest, object? data = null, CancellationToken cancellationToken = default)
            where T : ByteDanceMicroAppOpenApiResponse, new()
        {
            if (flurlRequest == null) throw new ArgumentNullException(nameof(flurlRequest));

            try
            {
                bool isSimpleRequest = data == null ||
                    flurlRequest.Verb == HttpMethod.Get ||
                    flurlRequest.Verb == HttpMethod.Head ||
                    flurlRequest.Verb == HttpMethod.Options;
                using IFlurlResponse flurlResponse = isSimpleRequest ?
                    await base.SendFlurlRequestAsync(flurlRequest, null, cancellationToken) :
                    await base.SendFlurlRequestAsJsonAsync(flurlRequest, data, cancellationToken);
                return await WrapFlurlResponseAsJsonAsync<T>(flurlResponse, cancellationToken);
            }
            catch (FlurlHttpTimeoutException ex)
            {
                throw new Exceptions.ByteDanceMicroAppRequestTimeoutException(ex.Message, ex);
            }
            catch (FlurlHttpException ex)
            {
                throw new ByteDanceMicroAppException(ex.Message, ex);
            }
        }
    }
}
