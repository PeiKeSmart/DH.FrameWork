﻿using System.Runtime.Serialization;

namespace DH.WebHook.Models
{
    /// <summary>
    /// 文件消息请求
    /// </summary>
    [DataContract]
    public class FileMessageRequest : WeChatWorkRobotRequest
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        protected override string MessageType => Const.File;

        /// <summary>
        /// 文件id，通过下文的文件上传接口获取
        /// </summary>
        [DataMember(Name = "media_id", IsRequired = true)]
        public string MediaId { get; set; }

        /// <summary>
        /// 校验
        /// </summary>
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(MediaId))
                throw new ArgumentNullException(nameof(MediaId), "文件ID不能为空");
        }
    }
}
