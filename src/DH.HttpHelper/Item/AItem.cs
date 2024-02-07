using DG.HttpHelper.Enum;

namespace DG.HttpHelper.Item
{
    /// <summary>
    /// A连接对象
    /// </summary>
    public class AItem
    {
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// 链接文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 链接的图片，如果是文本链接则为空
        /// </summary>
        public ImgItem Img { get; set; }
        /// <summary>
        /// 整个连接Html
        /// </summary>
        public string Html { get; set; }
        /// <summary>
        /// A链接的类型
        /// </summary>
        public AType Type { get; set; }
        /// <summary>
        /// A链接的属性  0是内连链，1是外链，2不是链接
        /// </summary>
        public int attr { get; set; }
        /// <summary>
        /// 是否_blank链接
        /// </summary>
        public bool is_blank { get; set; }
        /// <summary>
        /// 是否写title
        /// </summary>
        public bool is_title { get; set; }
        /// <summary>
        /// nofolly
        /// </summary>
       public bool is_nofollow { get; set; }
}
}
