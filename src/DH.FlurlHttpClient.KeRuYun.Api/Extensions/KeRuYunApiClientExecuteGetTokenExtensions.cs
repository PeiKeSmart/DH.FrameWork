using DH.Timing;

using Flurl.Http;

using NewLife.Collections;

using System;
using System.Reflection;

namespace DH.FlurlHttpClient.KeRuYun.Api.Extensions;

public static class KeRuYunApiClientExecuteGetTokenExtensions
{
    /// <summary>
    /// <para>异步调用 [POST] /open/v1/token/get 接口。</para>
    /// <para>REF: https://open.keruyun.com/docs/zh/vMeUEXQBzPVmqdQuNVgi.html </para>
    /// </summary>
    /// <param name="client"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<Models.TokenGetResponse> ExecuteGetTokenAsync(this KeRuYunApiClient client, Models.TokenGetRequest request, CancellationToken cancellationToken = default)
    {
        if (client is null) throw new ArgumentNullException(nameof(client));
        if (request is null) throw new ArgumentNullException(nameof(request));

        IFlurlRequest flurlReq = client
            .CreateRequest(request, HttpMethod.Get, "pospal-api2", "openapi", "v1", "openNotificationOpenApi", "queryPushUrl")
            ;

        IDictionary<string, string> queryDic = new Dictionary<string, string>();
        queryDic.Add("appKey", client.Credentials.AppKey);
        queryDic.Add("shopIdenty", request.ShopIdenty.SafeString());
        queryDic.Add("timestamp", UnixTime.ToTimestamp().SafeString());
        queryDic.Add("version", client.Credentials.Version);

        var build = Pool.StringBuilder.Get();
        foreach (var item in queryDic)
        {
            build.Append($"{item.Key}{item.Value}");
        }
        var signBuild = build.Put(true) + client.Credentials.APPSecret;
        var sign = Encrypt.Sha256(signBuild);

        flurlReq.SetQueryParams(new
        {
            appKey = client.Credentials.AppKey,
            shopIdenty = request.ShopIdenty,
            Version = client.Credentials.Version,
            timestamp = UnixTime.ToTimestamp()
        });

        return await client.SendRequestWithJsonAsync<Models.TokenGetResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
    }
}
