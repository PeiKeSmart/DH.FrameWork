namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.ExtendedSDK.Webcast.Models
{
    /// <summary>
    /// <para>表示 [POST] /card/get_all_cards_for_user 接口的响应。</para>
    /// </summary>
    public class WebcastCardGetAllCardsForUserResponse : DouyinMicroAppWebcastResponse
    {
        public static class Types
        {
            public class Card
            {
                public static class Types
                {
                    public class CardValue : WebcastCardDefaultGetResponse.Types.CardValue
                    {
                        /// <summary>
                        /// 获取或设置小程序页面路径。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("url")]
                        [System.Text.Json.Serialization.JsonPropertyName("url")]
                        public string PagePath { get; set; } = default!;
                    }
                }

                /// <summary>
                /// 获取或设置小程序页面路径。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("url")]
                [System.Text.Json.Serialization.JsonPropertyName("url")]
                public string PagePath { get; set; } = default!;

                /// <summary>
                /// 获取或设置讲解卡类型。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("card_type")]
                [System.Text.Json.Serialization.JsonPropertyName("card_type")]
                public int CardType { get; set; }

                /// <summary>
                /// 获取或设置讲解卡内容。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("value")]
                [System.Text.Json.Serialization.JsonPropertyName("value")]
                public Types.CardValue CardValue { get; set; } = default!;

                /// <summary>
                /// 获取或设置审核状态。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("status")]
                [System.Text.Json.Serialization.JsonPropertyName("status")]
                public int Status { get; set; }

                /// <summary>
                /// 获取或设置审核意见。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("audit_opinion")]
                [System.Text.Json.Serialization.JsonPropertyName("audit_opinion")]
                public string? AuditOpinion { get; set; }

                /// <summary>
                /// 获取或设置达人 UID。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("user_id")]
                [System.Text.Json.Serialization.JsonPropertyName("user_id")]
                public long? UserUID { get; set; }
            }
        }

        /// <summary>
        /// 获取或设置讲解卡列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("card_list")]
        [System.Text.Json.Serialization.JsonPropertyName("card_list")]
        public Types.Card[] CardList { get; set; } = default!;
    }
}
