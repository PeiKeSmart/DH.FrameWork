using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace SKIT.FlurlHttpClient.ByteDance.DouyinOpen
{
    public static class DouyinOpenClientExecuteVideoExtensions
    {
        /// <summary>
        /// <para>异步调用 [POST] /api/douyin/v1/video/upload_video 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/video-management/douyin/create-video/upload-video </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.VideoUploadResponse> ExecuteVideoUploadAsync(this DouyinOpenClient client, Models.VideoUploadRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Post, "api", "douyin", "v1", "video", "upload_video")
                .WithHeader("access-token", request.AccessToken)
                .SetQueryParam("open_id", request.OpenId);

            if (request.VideoFileName == null)
                request.VideoFileName = Guid.NewGuid().ToString("N").ToLower() + ".mp4";

            if (request.VideoContentType == null)
                request.VideoContentType = "video/mp4";

            using var httpContent = Utilities.FileHttpContentBuilder.Build(fileName: request.VideoFileName, fileBytes: request.VideoFileBytes, fileContentType: request.VideoContentType!, formDataName: "video");
            return await client.SendRequestAsync<Models.VideoUploadResponse>(flurlReq, httpContent: httpContent, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [POST] /api/douyin/v1/video/init_video_part_upload 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/video-management/douyin/create-video/video-part-upload-init </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.VideoPartInitializeResponse> ExecuteVideoPartInitializeAsync(this DouyinOpenClient client, Models.VideoPartInitializeRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Post, "api", "douyin", "v1", "video", "init_video_part_upload")
                .WithHeader("access-token", request.AccessToken)
                .SetQueryParam("open_id", request.OpenId);

            return await client.SendFlurlRequestAsFormUrlEncodedAsync<Models.VideoPartInitializeResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [POST] /api/douyin/v1/video/upload_video_part 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/video-management/douyin/create-video/video-part-upload </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.VideoPartUploadResponse> ExecuteVideoPartUploadAsync(this DouyinOpenClient client, Models.VideoPartUploadRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Post, "api", "douyin", "v1", "video", "upload_video_part")
                .WithHeader("access-token", request.AccessToken)
                .SetQueryParam("open_id", request.OpenId)
                .SetQueryParam("upload_id", request.UploadId)
                .SetQueryParam("part_number", request.PartNumber);

            if (request.VideoFileName == null)
                request.VideoFileName = Guid.NewGuid().ToString("N").ToLower() + ".mp4";

            if (request.VideoContentType == null)
                request.VideoContentType = "video/mp4";

            using var httpContent = Utilities.FileHttpContentBuilder.Build(fileName: request.VideoFileName, fileBytes: request.VideoFileBytes, fileContentType: request.VideoContentType!, formDataName: "video");
            return await client.SendRequestAsync<Models.VideoPartUploadResponse>(flurlReq, httpContent: httpContent, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [POST] /api/douyin/v1/video/complete_video_part_upload 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/video-management/douyin/create/slice-accomplish-upload </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.VideoPartCompleteResponse> ExecuteVideoPartCompleteAsync(this DouyinOpenClient client, Models.VideoPartCompleteRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Post, "api", "douyin", "v1", "video", "complete_video_part_upload")
                .WithHeader("access-token", request.AccessToken)
                .SetQueryParam("open_id", request.OpenId)
                .SetQueryParam("upload_id", request.UploadId);

            return await client.SendFlurlRequestAsFormUrlEncodedAsync<Models.VideoPartCompleteResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [POST] /api/douyin/v1/video/create_video 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/video-management/douyin/create-video/video-create </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.VideoCreateResponse> ExecuteVideoCreateAsync(this DouyinOpenClient client, Models.VideoCreateRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Post, "api", "douyin", "v1", "video", "create_video")
                .WithHeader("access-token", request.AccessToken)
                .SetQueryParam("open_id", request.OpenId);

            return await client.SendFlurlRequestAsFormUrlEncodedAsync<Models.VideoCreateResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [GET] /api/douyin/v1/video/video_list/ 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/video-management/douyin/search-video/account-video-list </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.VideoListResponse> ExecuteVideoListAsync(this DouyinOpenClient client, Models.VideoListRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Get, "api", "douyin", "v1", "video", "video_list/")
                .WithHeader("access-token", request.AccessToken)
                .SetQueryParam("open_id", request.OpenId)
                .SetQueryParam("cursor", request.PageCursor)
                .SetQueryParam("count", request.PageSize);

            return await client.SendFlurlRequestAsFormUrlEncodedAsync<Models.VideoListResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [POST] /api/douyin/v1/video/video_data/ 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/video-management/douyin/search-video/video-data </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.VideoDataResponse> ExecuteVideoDataAsync(this DouyinOpenClient client, Models.VideoDataRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Post, "api", "douyin", "v1", "video", "video_data/")
                .WithHeader("access-token", request.AccessToken)
                .SetQueryParam("open_id", request.OpenId); ;

            return await client.SendFlurlRequestAsFormUrlEncodedAsync<Models.VideoDataResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [POST] /video/source 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/video-management/douyin/search-video/douyin-video-origin </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.VideoSourceResponse> ExecuteVideoSourceAsync(this DouyinOpenClient client, Models.VideoSourceRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Post, "video", "source")
                .SetQueryParam("open_id", request.OpenId)
                .SetQueryParam("access_token", request.AccessToken);

            return await client.SendFlurlRequestAsFormUrlEncodedAsync<Models.VideoSourceResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }

        #region Image
        /// <summary>
        /// <para>异步调用 [POST] /api/douyin/v1/video/upload_image 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/video-management/douyin/create-image-text/image-upload </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.VideoUploadImageResponse> ExecuteVideoUploadImageAsync(this DouyinOpenClient client, Models.VideoUploadImageRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Post, "api", "douyin", "v1", "video", "upload_image")
                .WithHeader("access-token", request.AccessToken)
                .SetQueryParam("open_id", request.OpenId);

            if (request.ImageFileName == null)
                request.ImageFileName = Guid.NewGuid().ToString("N").ToLower() + ".png";

            if (request.ImageContentType == null)
                request.ImageContentType = "image/png";

            using var httpContent = Utilities.FileHttpContentBuilder.Build(fileName: request.ImageFileName, fileBytes: request.ImageFileBytes, fileContentType: request.ImageContentType!, formDataName: "image");
            return await client.SendRequestAsync<Models.VideoUploadImageResponse>(flurlReq, httpContent: httpContent, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [POST] /api/douyin/v1/video/create_image_text 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/video-management/douyin/create-image-text/create-image-text </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.VideoCreateImageTextResponse> ExecuteVideoCreateImageTextAsync(this DouyinOpenClient client, Models.VideoCreateImageTextRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Post, "api", "douyin", "v1", "video", "create_image_text")
                .WithHeader("access-token", request.AccessToken)
                .SetQueryParam("open_id", request.OpenId);

            return await client.SendFlurlRequestAsFormUrlEncodedAsync<Models.VideoCreateImageTextResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }
        #endregion

        #region Search
        /// <summary>
        /// <para>异步调用 [GET] /video/search 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/search-management/keywords-video-list/keywords-video </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.VideoSearchResponse> ExecuteVideoSearchAsync(this DouyinOpenClient client, Models.VideoSearchRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Get, "video", "search")
                .SetQueryParam("open_id", request.OpenId)
                .SetQueryParam("access_token", request.AccessToken)
                .SetQueryParam("cursor", request.PageCursor)
                .SetQueryParam("count", request.PageSize)
                .SetQueryParam("keyword", request.Keyword);

            return await client.SendFlurlRequestAsFormUrlEncodedAsync<Models.VideoSearchResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [GET] /video/search/comment/list 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/search-management/keywords-video-comment-management/comment-list </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.VideoSearchCommentListResponse> ExecuteVideoSearchCommentListAsync(this DouyinOpenClient client, Models.VideoSearchCommentListRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Get, "video", "search", "comment", "list")
                .SetQueryParam("open_id", request.OpenId)
                .SetQueryParam("access_token", request.AccessToken)
                .SetQueryParam("cursor", request.PageCursor)
                .SetQueryParam("count", request.PageSize)
                .SetQueryParam("sec_item_id", request.SecurityItemId);

            return await client.SendFlurlRequestAsFormUrlEncodedAsync<Models.VideoSearchCommentListResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [GET] /video/search/comment/reply/list 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/search-management/keywords-video-comment-management/comment-reply-list </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.VideoSearchCommentReplyListResponse> ExecuteVideoSearchCommentReplyListAsync(this DouyinOpenClient client, Models.VideoSearchCommentReplyListRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Get, "video", "search", "comment", "reply", "list")
                .SetQueryParam("open_id", request.OpenId)
                .SetQueryParam("access_token", request.AccessToken)
                .SetQueryParam("cursor", request.PageCursor)
                .SetQueryParam("count", request.PageSize)
                .SetQueryParam("sec_item_id", request.SecurityItemId)
                .SetQueryParam("comment_id", request.CommentId);

            return await client.SendFlurlRequestAsFormUrlEncodedAsync<Models.VideoSearchCommentReplyListResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [POST] /video/search/comment/reply 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/search-management/keywords-video-comment-management/comment-reply </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.VideoSearchCommentReplyResponse> ExecuteVideoSearchCommentReplyAsync(this DouyinOpenClient client, Models.VideoSearchCommentReplyRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Post, "video", "search", "comment", "reply")
                .SetQueryParam("open_id", request.OpenId)
                .SetQueryParam("access_token", request.AccessToken);

            return await client.SendFlurlRequestAsFormUrlEncodedAsync<Models.VideoSearchCommentReplyResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }
        #endregion
    }
}
