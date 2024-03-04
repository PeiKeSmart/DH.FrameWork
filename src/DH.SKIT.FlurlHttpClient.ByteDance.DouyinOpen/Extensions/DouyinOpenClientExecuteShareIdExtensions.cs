using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace SKIT.FlurlHttpClient.ByteDance.DouyinOpen
{
    public static class DouyinOpenClientExecuteShareIdExtensions
    {
        /// <summary>
        /// <para>异步调用 [GET] /share-id 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/video-management/douyin/search-video/video-share-result </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.ShareIdResponse> ExecuteShareIdAsync(this DouyinOpenClient client, Models.ShareIdRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Get, "share-id")
                .SetQueryParam("access_token", request.AccessToken);

            if (request.RequireCallback != null)
                flurlReq.SetQueryParam("need_callback", request.RequireCallback.Value);

            if (request.SourceStyleId != null)
                flurlReq.SetQueryParam("access_token", request.SourceStyleId);

            if (request.DefaultHashTag != null)
                flurlReq.SetQueryParam("default_hashtag", request.DefaultHashTag);

            if (request.LinkParameter != null)
                flurlReq.SetQueryParam("link_param", request.LinkParameter);

            return await client.SendFlurlRequestAsFormUrlEncodedAsync<Models.ShareIdResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }
    }
}
