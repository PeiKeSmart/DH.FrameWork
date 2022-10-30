namespace DH.WebHook.Model
{
    public class LinkModel
    {
        /// <summary>
        /// 消息类型，此时固定为:link
        /// </summary>
        public string msgtype { get; set; }
        /// <summary>
        /// link类型消息
        /// </summary>
        public link link { get; set; }
    }

    public class link
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 消息内容。如果太长只会部分展示
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 点击消息跳转的URL
        /// </summary>
        public string messageUrl { get; set; }
        /// <summary>
        /// 图片URL
        /// </summary>
        public string picUrl { get; set; }
    }
}
