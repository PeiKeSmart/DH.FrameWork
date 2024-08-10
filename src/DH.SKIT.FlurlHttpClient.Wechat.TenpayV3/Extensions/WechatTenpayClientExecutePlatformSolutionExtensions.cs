using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;

namespace SKIT.FlurlHttpClient.Wechat.TenpayV3
{
    public static class WechatTenpayClientExecutePlatformSolutionExtensions
    {
        #region MerchantTransfer
        /// <summary>
        /// <para>异步调用 [POST] /platsolution/mch-transfer/batches/apply 接口。</para>
        /// <para>
        /// REF: <br/>
        /// <![CDATA[ https://pay.weixin.qq.com/docs/partner/apis/platsolution-mch-transfer/transfer-batch/transfer-batch-apply.html ]]>
        /// </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.ApplyPlatformSolutionMerchantTransferBatchResponse> ExecuteApplyPlatformSolutionMerchantTransferBatchAsync(this WechatTenpayClient client, Models.ApplyPlatformSolutionMerchantTransferBatchRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateFlurlRequest(request, HttpMethod.Post, "platsolution", "mch-transfer", "batches", "apply");

            return await client.SendFlurlRequestAsJsonAsync<Models.ApplyPlatformSolutionMerchantTransferBatchResponse>(flurlReq, data: request, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// <para>异步调用 [GET] /platsolution/mch-transfer/batches/out-batch-no/{out_batch_no} 接口。</para>
        /// <para>
        /// REF: <br/>
        /// <![CDATA[ https://pay.weixin.qq.com/docs/partner/apis/platsolution-mch-transfer/transfer-batch/transfer-batch-get-by-out-code.html ]]>
        /// </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.GetPlatformSolutionMerchantTransferBatchByOutBatchNumberResponse> ExecuteGetPlatformSolutionMerchantTransferBatchByOutBatchNumberAsync(this WechatTenpayClient client, Models.GetPlatformSolutionMerchantTransferBatchByOutBatchNumberRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateFlurlRequest(request, HttpMethod.Get, "platsolution", "mch-transfer", "batches", "out-batch-no", request.OutBatchNumber)
                .SetQueryParam("sub_mchid", request.SubMerchantId)
                .SetQueryParam("need_query_detail", request.RequireQueryDetail ? "true" : "false")
                .SetQueryParam("detail_state", request.DetailState)
                .SetQueryParam("offset", request.Offset)
                .SetQueryParam("limit", request.Limit);

            return await client.SendFlurlRequestAsJsonAsync<Models.GetPlatformSolutionMerchantTransferBatchByOutBatchNumberResponse>(flurlReq, data: request, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// <para>异步调用 [GET] /platsolution/mch-transfer/batches/batch-id/{batch_id} 接口。</para>
        /// <para>
        /// REF: <br/>
        /// <![CDATA[ https://pay.weixin.qq.com/docs/partner/apis/platsolution-mch-transfer/transfer-batch/transfer-batch-get-by-id.html ]]>
        /// </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.GetPlatformSolutionMerchantTransferBatchByBatchIdResponse> ExecuteGetPlatformSolutionMerchantTransferBatchByBatchIdAsync(this WechatTenpayClient client, Models.GetPlatformSolutionMerchantTransferBatchByBatchIdRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateFlurlRequest(request, HttpMethod.Get, "platsolution", "mch-transfer", "batches", "batch-id", request.BatchId)
                .SetQueryParam("sub_mchid", request.SubMerchantId)
                .SetQueryParam("need_query_detail", request.RequireQueryDetail ? "true" : "false")
                .SetQueryParam("detail_state", request.DetailState)
                .SetQueryParam("offset", request.Offset)
                .SetQueryParam("limit", request.Limit);

            return await client.SendFlurlRequestAsJsonAsync<Models.GetPlatformSolutionMerchantTransferBatchByBatchIdResponse>(flurlReq, data: request, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// <para>异步调用 [GET] /platsolution/mch-transfer/batches/out-batch-no/{out_batch_no}/details/out-detail-no/{out_detail_no} 接口。</para>
        /// <para>
        /// REF: <br/>
        /// <![CDATA[ https://pay.weixin.qq.com/docs/partner/apis/platsolution-mch-transfer/transfer-batch/transfer-batch-get-detail-by-out-no.html ]]>
        /// </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.GetPlatformSolutionMerchantTransferBatchDetailByOutDetailNumberResponse> ExecuteGetPlatformSolutionMerchantTransferBatchDetailByOutDetailNumberAsync(this WechatTenpayClient client, Models.GetPlatformSolutionMerchantTransferBatchDetailByOutDetailNumberRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateFlurlRequest(request, HttpMethod.Get, "platsolution", "mch-transfer", "batches", "out-batch-no", request.OutBatchNumber, "details", "out-detail-no", request.OutDetailNumber)
                .SetQueryParam("sub_mchid", request.SubMerchantId);

            return await client.SendFlurlRequestAsJsonAsync<Models.GetPlatformSolutionMerchantTransferBatchDetailByOutDetailNumberResponse>(flurlReq, data: request, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// <para>异步调用 [GET] /platsolution/mch-transfer/batches/batch-id/{batch_id}/details/detail-id/{detail_id} 接口。</para>
        /// <para>
        /// REF: <br/>
        /// <![CDATA[ https://pay.weixin.qq.com/docs/partner/apis/platsolution-mch-transfer/transfer-batch/get-transfer-detail-by-no.html ]]>
        /// </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.GetPlatformSolutionMerchantTransferBatchDetailByDetailIdResponse> ExecuteGetPlatformSolutionMerchantTransferBatchDetailByDetailIdAsync(this WechatTenpayClient client, Models.GetPlatformSolutionMerchantTransferBatchDetailByDetailIdRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateFlurlRequest(request, HttpMethod.Get, "platsolution", "mch-transfer", "batches", "out-batch-no", request.BatchId, "details", "detail-id", request.DetailId)
                .SetQueryParam("sub_mchid", request.SubMerchantId);

            return await client.SendFlurlRequestAsJsonAsync<Models.GetPlatformSolutionMerchantTransferBatchDetailByDetailIdResponse>(flurlReq, data: request, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// <para>异步调用 [POST] /platsolution/mch-transfer/reservation/apply 接口。</para>
        /// <para>
        /// REF: <br/>
        /// <![CDATA[ https://pay.weixin.qq.com/docs/partner/apis/platsolution-mch-transfer/transfer-reservation/transfer-reservation-apply.html ]]>
        /// </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.ApplyPlatformSolutionMerchantTransferReservationResponse> ExecuteApplyPlatformSolutionMerchantTransferReservationAsync(this WechatTenpayClient client, Models.ApplyPlatformSolutionMerchantTransferReservationRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateFlurlRequest(request, HttpMethod.Post, "platsolution", "mch-transfer", "reservation", "apply");

            return await client.SendFlurlRequestAsJsonAsync<Models.ApplyPlatformSolutionMerchantTransferReservationResponse>(flurlReq, data: request, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// <para>异步调用 [GET] /platsolution/mch-transfer/reservation/out-reservation-no/{out_reservation_no} 接口。</para>
        /// <para>
        /// REF: <br/>
        /// <![CDATA[ https://pay.weixin.qq.com/docs/partner/apis/platsolution-mch-transfer/transfer-reservation/transfer-reservation-get-by-out-no.html ]]>
        /// </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.GetPlatformSolutionMerchantTransferReservationByOutReservationNumberResponse> ExecuteGetPlatformSolutionMerchantTransferReservationByOutReservationNumberAsync(this WechatTenpayClient client, Models.GetPlatformSolutionMerchantTransferReservationByOutReservationNumberRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateFlurlRequest(request, HttpMethod.Get, "platsolution", "mch-transfer", "reservation", "out-reservation-no", request.OutReservationNumber)
                .SetQueryParam("sub_mchid", request.SubMerchantId);

            return await client.SendFlurlRequestAsJsonAsync<Models.GetPlatformSolutionMerchantTransferReservationByOutReservationNumberResponse>(flurlReq, data: request, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// <para>异步调用 [GET] /platsolution/mch-transfer/reservation/reservation-id/{reservation_id} 接口。</para>
        /// <para>
        /// REF: <br/>
        /// <![CDATA[ https://pay.weixin.qq.com/docs/partner/apis/platsolution-mch-transfer/transfer-reservation/transfer-reservation-get-by-id.html ]]>
        /// </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.GetPlatformSolutionMerchantTransferReservationByReservationIdResponse> ExecuteGetPlatformSolutionMerchantTransferReservationByReservationIdAsync(this WechatTenpayClient client, Models.GetPlatformSolutionMerchantTransferReservationByReservationIdRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateFlurlRequest(request, HttpMethod.Get, "platsolution", "mch-transfer", "reservation", "reservation-id", request.ReservationId)
                .SetQueryParam("sub_mchid", request.SubMerchantId);

            return await client.SendFlurlRequestAsJsonAsync<Models.GetPlatformSolutionMerchantTransferReservationByReservationIdResponse>(flurlReq, data: request, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// <para>异步调用 [POST] /platsolution/mch-transfer/reservation/out-reservation-no/{out_reservation_no}/close 接口。</para>
        /// <para>
        /// REF: <br/>
        /// <![CDATA[ https://pay.weixin.qq.com/docs/partner/apis/platsolution-mch-transfer/transfer-reservation/transfer-reservation-close.html ]]>
        /// </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.ClosePlatformSolutionMerchantTransferReservationResponse> ExecuteClosePlatformSolutionMerchantTransferReservationAsync(this WechatTenpayClient client, Models.ClosePlatformSolutionMerchantTransferReservationRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateFlurlRequest(request, HttpMethod.Post, "platsolution", "mch-transfer", "reservation", "out-reservation-no", request.OutReservationNumber, "close");

            return await client.SendFlurlRequestAsJsonAsync<Models.ClosePlatformSolutionMerchantTransferReservationResponse>(flurlReq, data: request, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        #endregion
    }
}
