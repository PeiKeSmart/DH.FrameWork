﻿using System;

namespace SKIT.FlurlHttpClient.ByteDance.DouyinOpen.Models
{
    /// <summary>
    /// <para>表示 [GET] /hotsearch/sentences 接口的响应。</para>
    /// </summary>
    public class HotSearchSentencesResponse : DouyinOpenResponse<HotSearchSentencesResponse.Types.Data>
    {
        public static class Types
        {
            public class Data : DouyinOpenResponseData
            {
                public static class Types
                {
                    public class HotSearchSentence
                    {
                        /// <summary>
                        /// 获取或设置热点词。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("sentence")]
                        [System.Text.Json.Serialization.JsonPropertyName("sentence")]
                        public string Sentence { get; set; } = default!;

                        /// <summary>
                        /// 获取或设置热度等级。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("hot_level")]
                        [System.Text.Json.Serialization.JsonPropertyName("hot_level")]
                        public int HotLevel { get; set; }
                    }
                }

                /// <summary>
                /// 获取或设置刷新时间。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("active_time")]
                [System.Text.Json.Serialization.JsonPropertyName("active_time")]
                public DateTimeOffset ActiveTime { get; set; }

                /// <summary>
                /// 获取或设置热点词列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("list")]
                [System.Text.Json.Serialization.JsonPropertyName("list")]
                public Types.HotSearchSentence[] HotSearchSentenceList { get; set; } = default!;
            }
        }
    }
}
