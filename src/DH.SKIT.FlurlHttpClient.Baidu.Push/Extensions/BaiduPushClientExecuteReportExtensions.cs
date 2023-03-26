using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;

namespace SKIT.FlurlHttpClient.Baidu.Push
{
    public static class BaiduPushClientExecuteReportExtensions
    {
        /// <summary>
        /// <para>异步调用 [GET] /report/query_msg_status 接口。</para>
        /// <para>REF: https://push.baidu.com/doc/restapi/restapi </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.ReportQueryMessageStatusResponse> ExecuteReportQueryMessageStatusAsync(this BaiduPushClient client, Models.ReportQueryMessageStatusRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Get, "report", "query_msg_status")
                .SetQueryParam("msg_id", client.JsonSerializer.Serialize(request.MessageIdList));

            return await client.SendRequestWithFormUrlEncodedAsync<Models.ReportQueryMessageStatusResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [GET] /report/query_timer_records 接口。</para>
        /// <para>REF: https://push.baidu.com/doc/restapi/restapi </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.ReportQueryTimerRecordsResponse> ExecuteReportQueryTimerRecordsAsync(this BaiduPushClient client, Models.ReportQueryTimerRecordsRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Get, "report", "query_timer_records")
                .SetQueryParam("timer_id", request.TimerId);

            if (request.Start != null)
                flurlReq.SetQueryParam("start", request.Start.Value);

            if (request.Limit != null)
                flurlReq.SetQueryParam("limit", request.Limit.Value);

            if (request.StartTimestamp != null)
                flurlReq.SetQueryParam("range_start", request.StartTimestamp.Value);

            if (request.EndTimestamp != null)
                flurlReq.SetQueryParam("range_end", request.EndTimestamp.Value);

            return await client.SendRequestWithFormUrlEncodedAsync<Models.ReportQueryTimerRecordsResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [GET] /report/query_topic_records 接口。</para>
        /// <para>REF: https://push.baidu.com/doc/restapi/restapi </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.ReportQueryTopicRecordsResponse> ExecuteReportQueryTopicRecordsAsync(this BaiduPushClient client, Models.ReportQueryTopicRecordsRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Get, "report", "query_topic_records")
                .SetQueryParam("topic_id", request.TopicId);

            if (request.Start != null)
                flurlReq.SetQueryParam("start", request.Start.Value);

            if (request.Limit != null)
                flurlReq.SetQueryParam("limit", request.Limit.Value);

            if (request.StartTimestamp != null)
                flurlReq.SetQueryParam("range_start", request.StartTimestamp.Value);

            if (request.EndTimestamp != null)
                flurlReq.SetQueryParam("range_end", request.EndTimestamp.Value);

            return await client.SendRequestWithFormUrlEncodedAsync<Models.ReportQueryTopicRecordsResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [GET] /report/statistic_device 接口。</para>
        /// <para>REF: https://push.baidu.com/doc/restapi/restapi </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.ReportStatisticDeviceResponse> ExecuteReportStatisticDeviceAsync(this BaiduPushClient client, Models.ReportStatisticDeviceRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Get, "report", "statistic_device");

            return await client.SendRequestWithFormUrlEncodedAsync<Models.ReportStatisticDeviceResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [GET] /report/statistic_topic 接口。</para>
        /// <para>REF: https://push.baidu.com/doc/restapi/restapi </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.ReportStatisticTopicResponse> ExecuteReportStatisticTopicAsync(this BaiduPushClient client, Models.ReportStatisticTopicRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Get, "report", "statistic_topic")
                .SetQueryParam("topic_id", request.TopicId);

            return await client.SendRequestWithFormUrlEncodedAsync<Models.ReportStatisticTopicResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }
    }
}
