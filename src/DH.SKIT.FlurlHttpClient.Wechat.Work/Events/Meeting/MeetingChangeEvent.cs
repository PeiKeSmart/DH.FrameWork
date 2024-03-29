namespace SKIT.FlurlHttpClient.Wechat.Work.Events
{
    /// <summary>
    /// <para>表示 EVENT.meeting_change 事件的数据。</para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/99081 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/99082 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98333 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98337 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98341 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98345 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98348 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98352 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98353 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98354 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98355 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98393 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98394 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98395 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98396 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98397 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98771 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98773 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98774 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98775 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98398 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98399 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98400 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98401 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98402 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98404 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98781 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98782 </para>
    /// <para>REF: https://developer.work.weixin.qq.com/document/path/98783 </para>
    /// </summary>
    public class MeetingChangeEvent : WechatWorkEvent, WechatWorkEvent.Serialization.IXmlSerializable
    {
        public static class Types
        {
            public class OperatedUser
            {
                /// <summary>
                /// 获取或设置成员账号。
                /// </summary>
                [System.Xml.Serialization.XmlElement("UserId", IsNullable = true)]
                public string? UserId { get; set; }

                /// <summary>
                /// 获取或设置会中临时 ID。
                /// </summary>
                [System.Xml.Serialization.XmlElement("TmpOpenId", IsNullable = true)]
                public string? TempOpenId { get; set; }

                /// <summary>
                /// 获取或设置成员角色。
                /// </summary>
                [System.Xml.Serialization.XmlElement("UserRole", IsNullable = true)]
                public int? Role { get; set; }
            }

            public class WarmUpInfo
            {
                /// <summary>
                /// 获取或设置暖场图片 URL。
                /// </summary>
                [System.Xml.Serialization.XmlElement("WarmUpPicture", IsNullable = true)]
                public string? WarmUpPictureUrl { get; set; }

                /// <summary>
                /// 获取或设置暖场视频 URL。
                /// </summary>
                [System.Xml.Serialization.XmlElement("WarmUpVideo", IsNullable = true)]
                public string? WarmUpVideoUrl { get; set; }

                /// <summary>
                /// 获取或设置上传结果。
                /// </summary>
                [System.Xml.Serialization.XmlElement("UploadStatus")]
                public int UploadStatus { get; set; }

                /// <summary>
                /// 获取或设置错误描述。
                /// </summary>
                [System.Xml.Serialization.XmlElement("ErrorMsg", IsNullable = true)]
                public string? ErrorMessage { get; set; }
            }

            public class UploadInfo
            {
                /// <summary>
                /// 获取或设置素材类型。
                /// </summary>
                [System.Xml.Serialization.XmlElement("MediumType")]
                public int MediumType { get; set; }

                /// <summary>
                /// 获取或设置素材 URL。
                /// </summary>
                [System.Xml.Serialization.XmlElement("MediumUrl")]
                public string MediumUrl { get; set; } = default!;

                /// <summary>
                /// 获取或设置上传结果。
                /// </summary>
                [System.Xml.Serialization.XmlElement("UploadStatus")]
                public int UploadStatus { get; set; }

                /// <summary>
                /// 获取或设置错误描述。
                /// </summary>
                [System.Xml.Serialization.XmlElement("ErrorMsg", IsNullable = true)]
                public string? ErrorMessage { get; set; }
            }

            public class MRAInfo
            {
                /// <summary>
                /// 获取或设置信令协议。
                /// </summary>
                [System.Xml.Serialization.XmlElement("Protocol")]
                public int Protocol { get; set; }

                /// <summary>
                /// 获取或设置信令。
                /// </summary>
                [System.Xml.Serialization.XmlElement("DialString")]
                public string DialogString { get; set; } = default!;
            }
        }

        /// <summary>
        /// 获取或设置变更类型。
        /// </summary>
        [System.Xml.Serialization.XmlElement("ChangeType")]
        public string ChangeType { get; set; } = default!;

        /// <summary>
        /// 获取或设置会议 ID。
        /// </summary>
        [System.Xml.Serialization.XmlElement("MeetingId")]
        public string MeetingId { get; set; } = default!;

        /// <summary>
        /// 获取或设置会议室 ID。
        /// </summary>
        [System.Xml.Serialization.XmlElement("MeetingRoomId", IsNullable = true)]
        public string? MeetingRoomId { get; set; }

        /// <summary>
        /// 获取或设置操作者会中临时 ID。
        /// </summary>
        [System.Xml.Serialization.XmlElement("FromUserTmpOpenId", IsNullable = true)]
        public string? FromUserTempOpenId { get; set; }

        /// <summary>
        /// 获取或设置被操作者信息。
        /// </summary>
        [System.Xml.Serialization.XmlElement("OperatedUser", IsNullable = true)]
        public Types.OperatedUser? OperatedUser { get; set; }

        /// <summary>
        /// 获取或设置暖场信息。
        /// </summary>
        [System.Xml.Serialization.XmlElement("WarmUpInfo", IsNullable = true)]
        public Types.WarmUpInfo? WarmUpInfo { get; set; }

        /// <summary>
        /// 获取或设置上传信息列表。
        /// </summary>
        [System.Xml.Serialization.XmlElement("UploadInfo", Type = typeof(Types.UploadInfo), IsNullable = true)]
        public Types.UploadInfo[]? UploadInfoList { get; set; }

        /// <summary>
        /// 获取或设置报名 ID。
        /// </summary>
        [System.Xml.Serialization.XmlElement("EnrollId", IsNullable = true)]
        public string? EnrollId { get; set; }

        /// <summary>
        /// 获取或设置 Rooms MRA 信息。
        /// </summary>
        [System.Xml.Serialization.XmlElement("MraAddress", IsNullable = true)]
        public Types.MRAInfo? RoomsMRAInfo { get; set; }

        /// <summary>
        /// 获取或设置 Rooms 应答结果。
        /// </summary>
        [System.Xml.Serialization.XmlElement("RoomResponseStatus", IsNullable = true)]
        public int? RoomsResponseStatus { get; set; }
    }
}
