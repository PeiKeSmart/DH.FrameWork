namespace SKIT.FlurlHttpClient.Wechat.Api.Models
{
    /// <summary>
    /// <para>表示 [POST] /promoter/addrole 接口的响应。</para>
    /// </summary>
    public class PromoterAddRoleResponse : WechatApiResponse
    {
        /// <summary>
        /// 获取或设置角色 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("role_id")]
        [System.Text.Json.Serialization.JsonPropertyName("role_id")]
        public int RoleId { get; set; }

        /// <summary>
        /// 获取或设置角色名称。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("name")]
        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        /// <summary>
        /// 获取或设置角色描述。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("desc")]
        [System.Text.Json.Serialization.JsonPropertyName("desc")]
        public string Description { get; set; } = default!;
    }
}
