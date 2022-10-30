using System.Collections.Generic;

namespace DH.WebHook.Model
{
    /// <summary>
    /// FeedCard类型
    /// </summary>
    public class FeedCardModel
    {
        /// <summary>
        /// 此消息类型为固定feedCard
        /// </summary>
        public string msgtype { get; set; }
        /// <summary>
        /// feedCard类型
        /// </summary>
        public feedCard feedCard { get; set; }
    }
    /// <summary>
    /// feedCard类型
    /// </summary>
    public class feedCard
    {
        /// <summary>
        /// link消息
        /// </summary>
        public List<links> links { get; set; }
    }
    /// <summary>
    /// link消息
    /// </summary>
    public class links
    {
        /// <summary>
        /// 单条信息文本
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 点击单条信息到跳转链接
        /// </summary>
        public string messageURL { get; set; }
        /// <summary>
        /// 单条信息后面图片的URL
        /// </summary>
        public string picURL { get; set; }
    }
}
