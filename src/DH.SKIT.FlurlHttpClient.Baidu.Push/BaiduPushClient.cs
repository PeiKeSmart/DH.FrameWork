using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;

namespace SKIT.FlurlHttpClient.Baidu.Push
{
    /// <summary>
    /// 一个百度云推送 API HTTP 客户端。
    /// </summary>
    public class BaiduPushClient : CommonClientBase, ICommonClient
    {
        /// <summary>
        /// 获取当前客户端使用的百度云推送凭证。
        /// </summary>
        public Settings.Credentials Credentials { get; }

        /// <summary>
        /// 用指定的配置项初始化 <see cref="BaiduPushClient"/> 类的新实例。
        /// </summary>
        /// <param name="options">配置项。</param>
        public BaiduPushClient(BaiduPushClientOptions options)
            : base()
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            Credentials = new Settings.Credentials(options);

            FlurlClient.BaseUrl = options.Endpoint ?? BaiduPushEndpoints.DEFAULT;
            FlurlClient.Headers.Remove(FlurlHttpClient.Constants.HttpHeaders.UserAgent);
            FlurlClient.WithHeader(FlurlHttpClient.Constants.HttpHeaders.UserAgent, options.UserAgent);
            FlurlClient.WithTimeout(TimeSpan.FromMilliseconds(options.Timeout));

            Interceptors.Add(new Interceptors.BaiduPushRequestSignatureInterceptor(
                apiKey: options.ApiKey,
                apiSecretKey: options.SecretKey
            ));
        }

        /// <summary>
        /// 用指定的百度云推送 API Key 和 Secret Key 初始化 <see cref="BaiduTranslateClient"/> 类的新实例。
        /// </summary>
        /// <param name="apiKey">百度云推送 API Key。</param>
        /// <param name="secretKey">百度云推送 Secret Key。</param>
        public BaiduPushClient(string apiKey, string secretKey)
            : this(new BaiduPushClientOptions() { ApiKey = apiKey, SecretKey = secretKey })
        {
        }

        /// <summary>
        /// 使用当前客户端生成一个新的 <see cref="IFlurlRequest"/> 对象。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="method"></param>
        /// <param name="urlSegments"></param>
        /// <returns></returns>
        public IFlurlRequest CreateRequest(BaiduPushRequest request, HttpMethod method, params object[] urlSegments)
        {
            IFlurlRequest flurlRequest = FlurlClient.Request(urlSegments).WithVerb(method);

            if (request.Timeout != null)
            {
                flurlRequest.WithTimeout(TimeSpan.FromMilliseconds(request.Timeout.Value));
            }

            if (request.DeviceType != null)
            {
                if (method == HttpMethod.Get)
                    flurlRequest.SetQueryParam("device_type", request.DeviceType.Value);
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
            where T : BaiduPushResponse, new()
        {
            if (flurlRequest == null) throw new ArgumentNullException(nameof(flurlRequest));

            try
            {
                using IFlurlResponse flurlResponse = await base.SendRequestAsync(flurlRequest, httpContent, cancellationToken);
                return await WrapResponseWithJsonAsync<T>(flurlResponse, cancellationToken);
            }
            catch (FlurlHttpTimeoutException ex)
            {
                throw new Exceptions.BaiduPushRequestTimeoutException(ex.Message, ex);
            }
            catch (FlurlHttpException ex)
            {
                throw new BaiduPushException(ex.Message, ex);
            }
        }

        /// <summary>
        /// 异步发起请求。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="flurlRequest"></param>
        /// <param name="formdata"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<T> SendRequestWithFormUrlEncodedAsync<T>(IFlurlRequest flurlRequest, IDictionary<string, IConvertible?>? formdata = null, CancellationToken cancellationToken = default)
            where T : BaiduPushResponse, new()
        {
            if (flurlRequest == null) throw new ArgumentNullException(nameof(flurlRequest));

            HttpContent? httpContent = null;
            if (formdata != null)
            {
                if (!flurlRequest.Headers.Contains(Constants.HttpHeaders.ContentType))
                {
                    flurlRequest.WithHeader(Constants.HttpHeaders.ContentType, "application/x-www-form-urlencoded;charset=utf-8");
                }

                IDictionary<string, string> tmpDict = formdata
                    .Where(e => e.Value != null)
                    .ToDictionary(k => k.Key, v => v.Value!.ToString()!);
                httpContent = new FormUrlEncodedContent(tmpDict);
            }

            try
            {
                using IFlurlResponse flurlResponse = await base.SendRequestAsync(flurlRequest, httpContent, cancellationToken).ConfigureAwait(false);
                return await WrapResponseWithJsonAsync<T>(flurlResponse).ConfigureAwait(false);
            }
            catch (FlurlHttpException ex)
            {
                throw new BaiduPushException(ex.Message, ex);
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
        public async Task<T> SendRequestWithFormUrlEncodedAsync<T>(IFlurlRequest flurlRequest, object? data = null, CancellationToken cancellationToken = default)
            where T : BaiduPushResponse, new()
        {
            bool isSimpleRequest = data == null ||
                flurlRequest.Verb == HttpMethod.Get ||
                flurlRequest.Verb == HttpMethod.Head ||
                flurlRequest.Verb == HttpMethod.Options;
            if (isSimpleRequest)
            {
                return await SendRequestAsync<T>(flurlRequest, null, cancellationToken);
            }
            else
            {
                string tmpJson = JsonSerializer.Serialize(data);
                IDictionary<string, IConvertible?> formdata = new FlurlNewtonsoftJsonSerializer()
                    .Deserialize<IDictionary<string, string?>>(tmpJson)
                    .ToDictionary(k => k.Key, v => v.Value as IConvertible);
                return await SendRequestWithFormUrlEncodedAsync<T>(flurlRequest, formdata, cancellationToken);
            }
        }
    }
}
