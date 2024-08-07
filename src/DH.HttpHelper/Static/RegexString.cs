﻿using System;

namespace DG.HttpHelper.Static
{
    /// <summary>
    /// 正则表达式静态类
    /// </summary>
    internal class RegexString
    {
        /// <summary>
        /// 获取所有的A链接
        /// </summary>
        internal static readonly string Alist = "<a[^<]*href=[\"']([^<\"']*)[\"'][^<]*>([^<]*)</a>";
        /// <summary>
        /// 获取所有的Img标签
        /// </summary>
        internal static readonly string ImgList = "<img[^<]*src=[\"']([^<]*)[\"'][^<]*>";
        /// <summary>
        /// 所有的Nscript
        /// </summary>
        internal static readonly string Nscript = "<nscript[\\s\\S]*?>[\\s\\S]*?</nscript>";
        /// <summary>
        /// 所有的Style
        /// </summary>
        internal static readonly string Style = "<style[\\s\\S]*?>[\\s\\S]*?</style>";     
        /// <summary>
        /// 所有的Script
        /// </summary>
        internal static readonly string Script = "<script[\\s\\S]*?>[\\s\\S]*?</script>";
        /// <summary>
        /// 所有的Html
        /// </summary>
        internal static readonly string Html = "<[\\s\\S]*?>";
        /// <summary>
        /// 换行符号
        /// </summary>
        internal static readonly string NewLine = Environment.NewLine;
        /// <summary>
        ///获取网页编码
        /// </summary>
        internal static readonly string Enconding = "<meta[^<]*charset=([^<]*)[\"']";
        /// <summary>
        /// 所有Html
        /// </summary>
        internal static readonly string AllHtml = "([\\s\\S]*?)";
        /// <summary>
        /// title
        /// </summary>
        internal static readonly string HtmlTitle = "<title>([\\s\\S]*?)</title>";
        /// <summary>
        /// Keywords
        /// </summary>
        internal static readonly string HtmlKeywords = "<meta[^<]*name=[\"']keywords[\"'][^<]*content=[\"']([^<]*)[\"'][^<]*>";
        /// <summary>
        /// Description 
        /// </summary>
        internal static readonly string HtmlDescription = "<meta[^<]*name=[\"']description[\"'][^<]*content=[\"']([^<]*)[\"'][^<]*>";
    }
}
