using Flurl.Http;

using NewLife.Log;

using SKIT.FlurlHttpClient;
using SKIT.FlurlHttpClient.Constants;

using System.Reflection.PortableExecutable;

namespace DG.FlurlHttpClient.Pospal.Api
{
    /// <summary>
    /// 一个银豹 API HTTP 客户端。
    /// </summary>
    public class PospalApiClient : CommonClientBase, ICommonClient
    {
        /// <summary>
        /// 获取当前客户端使用的开放平台服务凭证。
        /// </summary>
        public Settings.Credentials Credentials { get; }

        /// <summary>
        /// 请求头
        /// </summary>
        public IDictionary<String, Object> Header { get; set; }

        /// <summary>
        /// 用指定的配置项初始化 <see cref="PospalApiClient"/> 类的新实例。
        /// </summary>
        /// <param name="options">配置项。</param>
        public PospalApiClient(PospalApiClientOptions options)
            : base()
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            Credentials = new Settings.Credentials(options);

            Header = new Dictionary<String, Object>();
            Header[HttpHeaders.UserAgent] = "openApi";
            Header[HttpHeaders.ContentType] = "application/json; charset=utf-8";
            Header[HttpHeaders.AcceptEncoding] = "gzip,deflate";

            FlurlClient.BaseUrl = options.Endpoints ?? PospalApiEndpoints.DEFAULT;
            FlurlClient.WithTimeout(TimeSpan.FromMilliseconds(options.Timeout));
        }

        /// <summary>
        /// 用指定的银豹 AppId 和银豹 AppKey 初始化 <see cref="PospalApiClient"/> 类的新实例。
        /// </summary>
        /// <param name="appId">银豹 AppId。</param>
        /// <param name="appkey">银豹 AppKey。</param>
        public PospalApiClient(string appId, string appkey)
            : this(new PospalApiClientOptions() { AppId = appId, AppKey = appkey })
        {
        }

        /// <summary>
        /// 使用当前客户端生成一个新的 <see cref="IFlurlRequest"/> 对象。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="method"></param>
        /// <param name="urlSegments"></param>
        /// <returns></returns>
        public IFlurlRequest CreateRequest(PospalApiRequest request, HttpMethod method, params object[] urlSegments)
        {
            IFlurlRequest flurlRequest = FlurlClient.Request(urlSegments).WithVerb(method);

            if (request.Timeout != null)
            {
                flurlRequest.WithTimeout(TimeSpan.FromMilliseconds(request.Timeout.Value));
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
            where T : PospalApiResponse, new()
        {
            if (flurlRequest == null) throw new ArgumentNullException(nameof(flurlRequest));

            try
            {
                using IFlurlResponse flurlResponse = await base.SendRequestAsync(flurlRequest, httpContent, cancellationToken);
                return await WrapResponseWithJsonAsync<T>(flurlResponse, cancellationToken);
            }
            catch (FlurlHttpTimeoutException ex)
            {
                throw new Exceptions.PospalApiRequestTimeoutException(ex.Message, ex);
            }
            catch (FlurlHttpException ex)
            {
                throw new PospalApiException(ex.Message, ex);
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
            where T : PospalApiResponse, new()
        {
            if (flurlRequest == null) throw new ArgumentNullException(nameof(flurlRequest));

            try
            {
                bool isSimpleRequest = data == null ||
                    flurlRequest.Verb == HttpMethod.Get ||
                    flurlRequest.Verb == HttpMethod.Head ||
                    flurlRequest.Verb == HttpMethod.Options;
                using IFlurlResponse flurlResponse = isSimpleRequest ?
                    await base.SendRequestAsync(flurlRequest, null, cancellationToken) :
                    await base.SendRequestWithJsonAsync(flurlRequest, data, cancellationToken);
                return await WrapResponseWithJsonAsync<T>(flurlResponse, cancellationToken);
            }
            catch (FlurlHttpTimeoutException ex)
            {
                throw new Exceptions.PospalApiRequestTimeoutException(ex.Message, ex);
            }
            catch (FlurlHttpException ex)
            {
                throw new PospalApiException(ex.Message, ex);
            }
        }

        /// <summary>
        /// 异步发起请求。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="flurlRequest"></param>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public async Task<T> SendRequestWithJsonAsync<T>(IFlurlRequest flurlRequest, IDictionary<String, Object>? header = null, object? data = null, CancellationToken cancellationToken = default)
            where T : PospalApiResponse, new()
        {
            if (flurlRequest == null) throw new ArgumentNullException(nameof(flurlRequest));

            try
            {
                bool isSimpleRequest = data == null ||
                    flurlRequest.Verb == HttpMethod.Get ||
                    flurlRequest.Verb == HttpMethod.Head ||
                    flurlRequest.Verb == HttpMethod.Options;
                using IFlurlResponse flurlResponse = isSimpleRequest ?
                    await base.SendRequestAsync(flurlRequest, header, null, cancellationToken) :
                    await base.SendRequestWithJsonAsync(flurlRequest, header, data, cancellationToken);

                return await WrapResponseWithJsonAsync<T>(flurlResponse, cancellationToken);
            }
            catch (FlurlHttpTimeoutException ex)
            {
                throw new Exceptions.PospalApiRequestTimeoutException(ex.Message, ex);
            }
            catch (FlurlHttpException ex)
            {
                throw new PospalApiException(ex.Message, ex);
            }
        }

    }
}
