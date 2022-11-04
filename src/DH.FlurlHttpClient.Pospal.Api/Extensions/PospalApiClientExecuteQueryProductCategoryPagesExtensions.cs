using Flurl.Http;

using NewLife;

using SKIT.FlurlHttpClient.Common;

namespace DG.FlurlHttpClient.Pospal.Api.Extensions
{
    public static class PospalApiClientExecuteQueryProductCategoryPagesExtensions
    {
        /// <summary>
        /// <para>异步调用 [POST] /pospal-api2/openapi/v1/productOpenApi/queryProductCategoryPages 接口。</para>
        /// <para>REF: http://pospal.cn/openplatform/productapi.html </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.QueryProductCategoryPagesResponse> ExecuteQueryProductCategoryPagesAsync(this PospalApiClient client, Models.QueryProductCategoryPagesRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Post, "pospal-api2", "openapi", "v1", "productOpenApi", "queryProductCategoryPages")
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
            
            return await client.SendRequestWithJsonAsync<Models.QueryProductCategoryPagesResponse>(flurlReq, header: client.Header, data: request, cancellationToken: cancellationToken);
        }
    }
}
