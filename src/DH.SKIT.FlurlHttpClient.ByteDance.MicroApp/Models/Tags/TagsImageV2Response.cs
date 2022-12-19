﻿namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.Models
{
    /// <summary>
    /// <para>表示 [POST] /v2/tags/image 接口的响应。</para>
    /// </summary>
    public class TagsImageV2Response : ByteDanceMicroAppResponse
    {
        public static class Types
        {
            public class Result
            {
                public static class Types
                {
                    public class Predict
                    {
                        /// <summary>
                        /// 获取或设置置信度服务或目标。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("target")]
                        [System.Text.Json.Serialization.JsonPropertyName("target")]
                        public string Target { get; set; } = default!;

                        /// <summary>
                        /// 获取或设置置信度模型或标签。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("model_name")]
                        [System.Text.Json.Serialization.JsonPropertyName("model_name")]
                        public string ModelName { get; set; } = default!;

                        /// <summary>
                        /// 获取或设置置信度概率（范围：0～1）。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("prob")]
                        [System.Text.Json.Serialization.JsonPropertyName("prob")]
                        [System.Text.Json.Serialization.JsonNumberHandling(System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString)]
                        public double Probability { get; set; }
                    }
                }

                /// <summary>
                /// 获取或设置检测结果状态码。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("code")]
                [System.Text.Json.Serialization.JsonPropertyName("code")]
                public int Code { get; set; }

                /// <summary>
                /// 获取或设置检测结果消息。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("msg")]
                [System.Text.Json.Serialization.JsonPropertyName("msg")]
                public string? Message { get; set; }

                /// <summary>
                /// 获取或设置检测数据 ID。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("data_id")]
                [System.Text.Json.Serialization.JsonPropertyName("data_id")]
                public string? DataId { get; set; }

                /// <summary>
                /// 获取或设置检测任务 ID。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("task_id")]
                [System.Text.Json.Serialization.JsonPropertyName("task_id")]
                public string TaskId { get; set; } = default!;

                /// <summary>
                /// 获取或设置置信度列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("predicts")]
                [System.Text.Json.Serialization.JsonPropertyName("predicts")]
                public Types.Predict[] PredictList { get; set; } = default!;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty("code")]
        [System.Text.Json.Serialization.JsonPropertyName("code")]
        public override long ErrorCode { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty("exception")]
        [System.Text.Json.Serialization.JsonPropertyName("exception")]
        public override string? ErrorMessage { get; set; }

        /// <summary>
        /// 获取或设置错误 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("error_id")]
        [System.Text.Json.Serialization.JsonPropertyName("error_id")]
        public string? ErrorId { get; set; }

        /// <summary>
        /// 获取或设置检测结果列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("data")]
        [System.Text.Json.Serialization.JsonPropertyName("data")]
        public Types.Result[] ResultList { get; set; } = default!;

        public override bool IsSuccessful()
        {
            return base.IsSuccessful() && string.IsNullOrEmpty(ErrorId);
        }
    }
}
