﻿using System.Collections.Generic;

namespace SKIT.FlurlHttpClient.Wechat.Work.Models
{
    /// <summary>
    /// <para>表示 [POST] /cgi-bin/wedrive/space_acl_add 接口的请求。</para>
    /// </summary>
    public class CgibinWebDriveSpaceACLAddRequest : WechatWorkRequest
    {
        public static class Types
        {
            public class AuthorizedUser : CgibinWebDriveSpaceCreateRequest.Types.AuthorizedUser
            {
            }
        }

        /// <summary>
        /// 获取或设置操作者成员账号。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("userid")]
        [System.Text.Json.Serialization.JsonPropertyName("userid")]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置空间 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("spaceid")]
        [System.Text.Json.Serialization.JsonPropertyName("spaceid")]
        public string SpaceId { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置空间授权成员列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("auth_info")]
        [System.Text.Json.Serialization.JsonPropertyName("auth_info")]
        public IList<Types.AuthorizedUser> AuthorizedUserList { get; set; } = new List<Types.AuthorizedUser>();
    }
}
