using Flurl.Http;

using SKIT.FlurlHttpClient;

namespace DG.FlurlHttpClient.Pospal.Api;

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
    /// 用指定的配置项初始化 <see cref="PospalApiClientOptions"/> 类的新实例。
    /// </summary>
    /// <param name="options">配置项。</param>
    public PospalApiClient(PospalApiClientOptions options)
        : this(options, null)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="httpClient"></param>
    /// <param name="disposeClient"></param>
    internal protected PospalApiClient(PospalApiClientOptions options, HttpClient? httpClient, bool disposeClient = true)
        : base(httpClient, disposeClient)
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
    /// 使用当前客户端生成一个新的 <see cref="IFlurlRequest"/> 对象。
    /// </summary>
    /// <param name="request"></param>
    /// <param name="httpMethod"></param>
    /// <param name="urlSegments"></param>
    /// <returns></returns>
    public IFlurlRequest CreateFlurlRequest(PospalApiRequest request, HttpMethod httpMethod, params object[] urlSegments)
    {
        IFlurlRequest flurlRequest = base.CreateFlurlRequest(request, httpMethod, urlSegments);

        if (request.AppId is null)
        {
            request.AppId = Credentials.AppId;
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
    public async Task<T> SendFlurlRequestAsync<T>(IFlurlRequest flurlRequest, HttpContent? httpContent = null, CancellationToken cancellationToken = default)
        where T : PospalApiResponse, new()
    {
        if (flurlRequest is null) throw new ArgumentNullException(nameof(flurlRequest));

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
        where T : PospalApiResponse, new()
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
        where T : PospalApiResponse, new()
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
                await base.SendFlurlRequestAsync(flurlRequest, header, null, cancellationToken) :
                await base.SendFlurlRequestAsJsonAsync(flurlRequest, header, data, cancellationToken);

            return await WrapFlurlResponseAsJsonAsync<T>(flurlResponse, cancellationToken);
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
