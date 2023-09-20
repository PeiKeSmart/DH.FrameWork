using System.Collections.Generic;

namespace SKIT.FlurlHttpClient.Wechat.Work.Models
{
    /// <summary>
    /// <para>表示 [POST] /cgi-bin/meeting/webinar/enroll/delete 接口的请求。</para>
    /// </summary>
    public class CgibinMeetingWebinarEnrollDeleteRequest : CgibinMeetingEnrollDeleteRequest
    {
        public static new class Types
        {
            public class Enroll : CgibinMeetingEnrollDeleteRequest.Types.Enroll
            {
            }
        }

        /// <summary>
        /// 获取或设置报名列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("enroll_id_list")]
        [System.Text.Json.Serialization.JsonPropertyName("enroll_id_list")]
        public new IList<Types.Enroll> EnrollList { get; set; } = new List<Types.Enroll>();
    }
}
