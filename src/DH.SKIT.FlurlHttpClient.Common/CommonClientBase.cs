using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace SKIT.FlurlHttpClient
{
    /// <summary>
    /// SKIT.FlurlHttpClient 客户端基类。
    /// </summary>
    public abstract class CommonClientBase : ICommonClient
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public FlurlHttpCallInterceptorCollection Interceptors { get; }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public ISerializer JsonSerializer
        {
            get { return FlurlClient.Settings?.JsonSerializer ?? FlurlHttp.GlobalSettings.JsonSerializer; }
        }

        /// <summary>
        /// 获取当前客户端使用的 <see cref="IFlurlClient"/> 对象。
        /// </summary>
        protected IFlurlClient FlurlClient { get; }

        /// <summary>
        ///
        /// </summary>
        protected CommonClientBase()
        {
            Interceptors = new FlurlHttpCallInterceptorCollection();
            FlurlClient = new FlurlClient();
            FlurlClient.Configure(flurlSettings =>
            {
                flurlSettings.JsonSerializer = new FlurlSystemTextJsonSerializer();
                flurlSettings.BeforeCallAsync = async (flurlCall) =>
                {
                    for (int i = 0, len = Interceptors.Count; i < len; i++)
                    {
                        await Interceptors[i].BeforeCallAsync(flurlCall);
                    }
                };
                flurlSettings.AfterCallAsync = async (flurlCall) =>
                {
                    for (int i = Interceptors.Count - 1; i >= 0; i--)
                    {
                        await Interceptors[i].AfterCallAsync(flurlCall);
                    }
                };
            });
        }

        /// <inheritdoc/>
        public void Configure(Action<CommonClientSettings> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            FlurlClient.Configure(flurlClientSettings =>
            {
                CommonClientSettings settings = new CommonClientSettings(flurlClientSettings);
                configure.Invoke(settings);

                flurlClientSettings.Timeout = settings.ConnectionRequestTimeout;
                flurlClientSettings.ConnectionLeaseTimeout = settings.ConnectionLeaseTimeout;
                flurlClientSettings.JsonSerializer = settings.JsonSerializer;
                flurlClientSettings.UrlEncodedSerializer = settings.UrlEncodedSerializer;
                flurlClientSettings.HttpClientFactory = settings.FlurlHttpClientFactory;
            });
        }

        private IFlurlRequest WrapRequest(IFlurlRequest flurlRequest)
        {
            return flurlRequest
                .WithClient(FlurlClient)
                .AllowAnyHttpStatus();
        }

        /// <summary>
        /// 异步发起请求。
        /// </summary>
        /// <param name="flurlRequest"></param>
        /// <param name="httpContent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual async Task<IFlurlResponse> SendRequestAsync(IFlurlRequest flurlRequest, HttpContent? httpContent = null, CancellationToken cancellationToken = default)
        {
            if (flurlRequest == null) throw new ArgumentNullException(nameof(flurlRequest));

            return await WrapRequest(flurlRequest).SendAsync(flurlRequest.Verb, httpContent, cancellationToken);
        }

        /// <summary>
        /// 异步发起请求。
        /// </summary>
        /// <param name="flurlRequest"></param>
        /// <param name="httpContent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual async Task<IFlurlResponse> SendRequestAsync(IFlurlRequest flurlRequest, IDictionary<String, Object>? header = null, HttpContent? httpContent = null, CancellationToken cancellationToken = default)
        {
            if (flurlRequest == null) throw new ArgumentNullException(nameof(flurlRequest));

            if (header != null)
            {
                foreach (var item in header)
                {
                    flurlRequest.WithHeader(item.Key, item.Value);
                }
            }

            return await WrapRequest(flurlRequest).SendAsync(flurlRequest.Verb, httpContent, cancellationToken);
        }

        /// <summary>
        /// 异步发起请求。
        /// <para>注意：对于非简单请求，如果未指定请求标头 Content-Type，将默认使用 "application/json" 作为其值。</para>
        /// </summary>
        /// <param name="flurlRequest"></param>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual async Task<IFlurlResponse> SendRequestWithJsonAsync(IFlurlRequest flurlRequest, object? data = null, CancellationToken cancellationToken = default)
        {
            if (flurlRequest == null) throw new ArgumentNullException(nameof(flurlRequest));

            if (data != null)
            {
                if (!flurlRequest.Headers.Contains(Constants.HttpHeaders.ContentType))
                {
                    flurlRequest.WithHeader(Constants.HttpHeaders.ContentType, "application/json");
                }
            }

            return await WrapRequest(flurlRequest).SendJsonAsync(flurlRequest.Verb, data: data, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 异步发起请求。
        /// <para>指定请求标头 `Content-Type` 为 `application/json`。</para>
        /// </summary>
        /// <param name="flurlRequest"></param>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        protected virtual async Task<IFlurlResponse> SendRequestWithJsonAsync(IFlurlRequest flurlRequest, IDictionary<String, Object>? header = null, object? data = null, CancellationToken cancellationToken = default)
        {
            if (flurlRequest == null) throw new ArgumentNullException(nameof(flurlRequest));

            if (data != null)
            {
                if (header != null)
                {
                    foreach (var item in header)
                    {
                        flurlRequest.WithHeader(item.Key, item.Value);
                    }
                }

                if (!flurlRequest.Headers.GetAll(Constants.HttpHeaders.ContentType).Any())
                {
                    flurlRequest.WithHeader(Constants.HttpHeaders.ContentType, "application/json");
                }
            }

            return await WrapRequest(flurlRequest).SendJsonAsync(flurlRequest.Verb, data: data, cancellationToken: cancellationToken);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="flurlResponse"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task<TResponse> WrapResponseAsync<TResponse>(IFlurlResponse flurlResponse, CancellationToken cancellationToken = default)
            where TResponse : ICommonResponse, new()
        {
            Task<byte[]> task = flurlResponse.GetBytesAsync();
            Task taskWithCt = await Task.WhenAny(task, Task.Delay(Timeout.Infinite, cancellationToken));
            if (taskWithCt == task)
            {
                TResponse result = new TResponse();
                result.RawBytes = await task;
                result.RawStatus = flurlResponse.StatusCode;
                result.RawHeaders = new ReadOnlyDictionary<string, string>(
                    flurlResponse.Headers
                        .GroupBy(e => e.Name)
                        .ToDictionary(
                            k => k.Key,
                            v => string.Join(",", v.Select(e => e.Value)),
                            StringComparer.OrdinalIgnoreCase
                        )
                );
                return result;
            }
            else
            {
                cancellationToken.ThrowIfCancellationRequested();
                throw new OperationCanceledException("Infinite delay task completed.");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="flurlResponse"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task<TResponse> WrapResponseWithJsonAsync<TResponse>(IFlurlResponse flurlResponse, CancellationToken cancellationToken = default)
            where TResponse : ICommonResponse, new()
        {
            TResponse tmp = await WrapResponseAsync<TResponse>(flurlResponse, cancellationToken);
            byte tmpb1 = tmp.RawBytes.SkipWhile(b => b <= 32).FirstOrDefault(),
                 tmpb2 = tmp.RawBytes.Reverse().SkipWhile(b => b <= 32).FirstOrDefault();
            bool jsonable = (tmpb1 == 91 && tmpb2 == 93) || (tmpb1 == 123 && tmpb2 == 125); // "[...]" or "{...}"

            TResponse result;
            if (jsonable)
            {
                string? contentType = flurlResponse.Headers.GetAll(Constants.HttpHeaders.ContentType).FirstOrDefault();
                string? charset = MediaTypeHeaderValue.TryParse(contentType, out var mediaType) ? mediaType.CharSet : null;
                string json = (string.IsNullOrEmpty(charset) ? Encoding.UTF8 : Encoding.GetEncoding(charset)).GetString(tmp.RawBytes);

                result = JsonSerializer.Deserialize<TResponse>(json);
                result.RawStatus = tmp.RawStatus;
                result.RawHeaders = tmp.RawHeaders;
                result.RawBytes = tmp.RawBytes;
            }
            else
            {
                result = tmp;
            }

            return result;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual void Dispose()
        {
            FlurlClient?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
