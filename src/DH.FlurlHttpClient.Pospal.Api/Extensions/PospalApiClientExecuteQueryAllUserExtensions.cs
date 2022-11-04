using Flurl.Http;

using NewLife;

namespace DG.FlurlHttpClient.Pospal.Api
{
    using SKIT.FlurlHttpClient.Common;

    public static class PospalApiClientExecuteQueryAllUserExtensions
    {
        /// <summary>
        /// <para>异步调用 [POST] /pospal-api2/openapi/v1/userOpenApi/queryAllUser 接口。</para>
        /// <para>REF: http://pospal.cn/openplatform/userapi.html </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.QueryAllUserResponse> ExecuteQueryAllUserAsync(this PospalApiClient client, Models.QueryAllUserRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Post, "pospal-api2", "openapi", "v1", "openNotificationOpenApi", "queryPushUrl")
                ;

            DateTime ORIGINDATE = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            long timeStamp = Convert.ToInt64(DateTime.Now.Subtract(ORIGINDATE).TotalMilliseconds);

            if (!client.Header.ContainsKey("time-stamp"))
            {
                client.Header.Add("time-stamp", timeStamp);
            }
            else
            {
                client.Header["time-stamp"] = timeStamp;
            }
            if (!client.Header.ContainsKey("data-signature"))
            {
                client.Header.Add("data-signature", (client.Credentials.AppKey + JsonHelper.ToJson(request)).MD5());
            }
            else
            {
                client.Header["data-signature"] = (client.Credentials.AppKey + JsonHelper.ToJson(request)).MD5();
            }

            return await client.SendRequestWithJsonAsync<Models.QueryAllUserResponse>(flurlReq, header: client.Header, data: request, cancellationToken: cancellationToken);
        }
    }
}
