using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;

namespace SKIT.FlurlHttpClient.ByteDance.DouyinOpen
{
    /// <summary>
    /// 一个抖音开放平台 API HTTP 客户端。
    /// </summary>
    public class DouyinOpenClient : CommonClientBase, ICommonClient
    {
        /// <summary>
        /// 获取当前客户端使用的抖音开放平台凭证。
        /// </summary>
        public Settings.Credentials Credentials { get; }

        /// <summary>
        /// 用指定的配置项初始化 <see cref="DouyinOpenClient"/> 类的新实例。
        /// </summary>
        /// <param name="options">配置项。</param>
        public DouyinOpenClient(DouyinOpenClientOptions options)
            : base()
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            Credentials = new Settings.Credentials(options);

            FlurlClient.BaseUrl = options.Endpoint ?? DouyinOpenEndpoints.DEFAULT;
            FlurlClient.WithTimeout(TimeSpan.FromMilliseconds(options.Timeout));
        }

        /// <summary>
        /// 用指定的抖音开放平台应用 Key、抖音开放平台应用密钥初始化 <see cref="DouyinOpenClient"/> 类的新实例。
        /// </summary>
        /// <param name="clientKey">抖音开放平台应用 Key。</param>
        /// <param name="clientSecret">抖音开放平台应用密钥。</param>
        public DouyinOpenClient(string clientKey, string clientSecret)
            : this(new DouyinOpenClientOptions() { ClientKey = clientKey, ClientSecret = clientSecret })
        {
        }

        /// <summary>
        /// 使用当前客户端生成一个新的 <see cref="IFlurlRequest"/> 对象。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="method"></param>
        /// <param name="urlSegments"></param>
        /// <returns></returns>
        public IFlurlRequest CreateRequest(DouyinOpenRequest request, HttpMethod method, params object[] urlSegments)
        {
            IFlurlRequest flurlRequest = FlurlClient.Request(urlSegments).WithVerb(method);

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
            where T : DouyinOpenResponse, new()
        {
            if (flurlRequest == null) throw new ArgumentNullException(nameof(flurlRequest));

            using IFlurlResponse flurlResponse = await base.SendFlurlRequestAsync(flurlRequest, httpContent, cancellationToken).ConfigureAwait(false);
            return await WrapFlurlResponseAsJsonAsync<T>(flurlResponse, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步发起请求。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="flurlRequest"></param>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<T> SendFlurlRequestAsJsonAsync<T>(IFlurlRequest flurlRequest, object? data = null, CancellationToken cancellationToken = default)
            where T : DouyinOpenResponse, new()
        {
            if (flurlRequest is null) throw new ArgumentNullException(nameof(flurlRequest));

            bool isSimpleRequest = data is null ||
                flurlRequest.Verb == HttpMethod.Get ||
                flurlRequest.Verb == HttpMethod.Head ||
                flurlRequest.Verb == HttpMethod.Options;
            using IFlurlResponse flurlResponse = isSimpleRequest ?
                await base.SendFlurlRequestAsync(flurlRequest, null, cancellationToken).ConfigureAwait(false) :
                await base.SendFlurlRequestAsJsonAsync(flurlRequest, data, cancellationToken).ConfigureAwait(false);
            return await WrapFlurlResponseAsJsonAsync<T>(flurlResponse, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步发起请求。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="flurlRequest"></param>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<T> SendFlurlRequestAsFormUrlEncodedAsync<T>(IFlurlRequest flurlRequest, object? data = null, CancellationToken cancellationToken = default)
            where T : DouyinOpenResponse, new()
        {
            if (flurlRequest is null) throw new ArgumentNullException(nameof(flurlRequest));

            bool isSimpleRequest = data is null ||
                flurlRequest.Verb == HttpMethod.Get ||
                flurlRequest.Verb == HttpMethod.Head ||
                flurlRequest.Verb == HttpMethod.Options;
            using IFlurlResponse flurlResponse = isSimpleRequest ?
                await base.SendFlurlRequestAsync(flurlRequest, null, cancellationToken).ConfigureAwait(false) :
                await base.SendFlurlRequestAsFormUrlEncodedAsync(flurlRequest, data, cancellationToken).ConfigureAwait(false);
            return await WrapFlurlResponseAsJsonAsync<T>(flurlResponse, cancellationToken).ConfigureAwait(false);
        }

    }
}
