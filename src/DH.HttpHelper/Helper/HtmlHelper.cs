using DG.HttpHelper.Enum;
using DG.HttpHelper.Item;
using DG.HttpHelper.Static;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DG.HttpHelper.Helper
{
    /// <summary>
    /// Html操作相关
    /// </summary>
    internal class HtmlHelper
    {
        /// <summary>
        /// 获取所有的A链接
        /// </summary>
        /// <param name="html">要分析的Html代码</param>
        /// <returns>返回一个List存储所有的A标签</returns>
        internal static List<AItem> GetAList(string html, string host = "deng-haocom")
        {
            List<AItem> list = null;
            string ra = RegexString.Alist;
            if (Regex.IsMatch(html, ra, RegexOptions.IgnoreCase))
            {
                list = new List<AItem>();
                foreach (Match item in Regex.Matches(html, ra, RegexOptions.IgnoreCase))
                {
                    AItem a = null;
                    try
                    {
                        a = new AItem()
                        {
                            Href = item.Groups[1].Value,
                            Text = item.Groups[2].Value,
                            Html = item.Value,
                            Type = AType.Text,
                            attr = 0,
                            is_blank = false,
                            is_title = false,
                            is_nofollow = false
                        };
                        if (Regex.IsMatch(a.Html, RegexString.ImgList, RegexOptions.IgnoreCase))
                        {
                            a.Type = AType.Img;
                        }
                        if (Regex.IsMatch(a.Html, "target=[\"']_blank[\"']", RegexOptions.IgnoreCase))
                        {
                            a.is_blank = true;
                        }
                        if (Regex.IsMatch(a.Html, "title=[\"']([^<]*)[\"']", RegexOptions.IgnoreCase))
                        {
                            a.is_title = true;
                        }
                        if (Regex.IsMatch(a.Html, "rel=[\"']nofollow[\"']", RegexOptions.IgnoreCase))
                        {
                            a.is_nofollow = true;
                        }
                        if (host != "sufeinet")
                        {
                            if (string.IsNullOrWhiteSpace(a.Href))
                            {
                                a.attr = 2;
                            }
                            else if (a.Href == "#")
                            {
                                a.attr = 2;
                            }
                            else if (a.Href.StartsWith("//") || a.Href.StartsWith("http://") || a.Href.StartsWith("https://"))
                            {
                                string hhost = a.Href != null ? new Uri(a.Href).Host : string.Empty;
                                if (hhost == host)
                                {
                                    a.attr = 0;
                                }
                                else
                                {
                                    a.attr = 1;
                                }
                            }
                            else
                            {
                                a.attr = 0;
                            }

                        }
                    }
                    catch { a = null; }
                    if (a != null)
                    {
                        list.Add(a);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 获取所有的Img标签
        /// </summary>
        /// <param name="html">要分析的Html代码</param>
        /// <returns>返回一个List存储所有的Img标签</returns>
        internal static List<ImgItem> GetImgList(string html)
        {
            List<ImgItem> list = null;
            string ra = RegexString.ImgList;
            if (Regex.IsMatch(html, ra, RegexOptions.IgnoreCase))
            {
                list = new List<ImgItem>();
                foreach (Match item in Regex.Matches(html, ra, RegexOptions.IgnoreCase))
                {
                    ImgItem a = null;
                    try
                    {
                        a = new ImgItem()
                        {
                            Src = item.Groups[1].Value,
                            Html = item.Value
                        };
                    }
                    catch { a = null; }

                    string retitle = "title=[\"']([^<]*)[\"']";
                    if (Regex.IsMatch(a.Html, retitle, RegexOptions.IgnoreCase))
                    {
                        a.title = Regex.Match(a.Html, retitle, RegexOptions.IgnoreCase).Groups[1].Value.Trim();
                    }
                    string realt = "alt=[\"']([^<]*)[\"']";
                    if (Regex.IsMatch(a.Html, realt, RegexOptions.IgnoreCase))
                    {
                        a.alt = Regex.Match(a.Html, realt, RegexOptions.IgnoreCase).Groups[1].Value.Trim();
                    }
                    if (a != null)
                    {
                        list.Add(a);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 过滤html标签
        /// </summary>
        /// <param name="html">html的内容</param>
        /// <returns>处理后的文本</returns>
        internal static string StripHTML(string html)
        {
            string src = string.Empty, title = string.Empty;
            foreach (Match item in Regex.Matches(html, "alt[\\s\\t\\r\\n]*=[\"']([\\S\\s]*?)[\"']"))
            {
                src += item.Groups[1].Value;
            }
            foreach (Match item in Regex.Matches(html, "title[\\s\\t\\r\\n]*=[\"']([\\S\\s]*?)[\"']"))
            {
                title += item.Groups[1].Value;
            }

            html = Regex.Replace(html, "<nscript[\\s\\S]*?>[\\s\\S]*?</nscript>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            html = Regex.Replace(html, "<style[\\s\\S]*?>[\\s\\S]*?</style>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            html = Regex.Replace(html, "<script[\\s\\S]*?>[\\s\\S]*?</script>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            html = Regex.Replace(html, "<script[\\s\\S]*?>[\\s\\S]*?</script>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            html = Regex.Replace(html, "</p(?:\\s*)>(?:\\s*)<p(?:\\s*)>", "\n\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            html += src + title;
            html = Regex.Replace(html, "", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            html = Regex.Replace(html, "\"", "''", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            html = Regex.Replace(html, "<[^>]+>", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            return html.Replace("\r\n", "\n").Replace("\n", "").Replace(" ", "").Replace("	", "");
        }
        /// <summary>
        /// 过滤html中所有的换行符号
        /// </summary>
        /// <param name="html">html的内容</param>
        /// <returns>处理后的文本</returns>
        internal static string ReplaceNewLine(string html)
        {
            return Regex.Replace(html, RegexString.NewLine, string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
        /// <summary>
        /// 提取Html字符串中两字符之间的数据
        /// </summary>
        /// <param name="html">源Html</param>
        /// <param name="s">开始字符串</param>
        /// <param name="e">结束字符串</param>
        /// <returns></returns>
        internal static string GetBetweenHtml(string html, string s, string e)
        {
            string rx = string.Format("{0}{1}{2}", s, RegexString.AllHtml, e);
            if (Regex.IsMatch(html, rx, RegexOptions.IgnoreCase))
            {
                Match match = Regex.Match(html, rx, RegexOptions.IgnoreCase);
                if (match != null && match.Groups.Count > 0)
                {
                    return match.Groups[1].Value.Trim();
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 提取网页Title
        /// </summary>
        /// <param name="html">Html</param>
        /// <returns>返回Title</returns>
        internal static string GetHtmlTitle(string html)
        {
            if (Regex.IsMatch(html, RegexString.HtmlTitle, RegexOptions.IgnoreCase))
            {
                return Regex.Match(html, RegexString.HtmlTitle, RegexOptions.IgnoreCase).Groups[1].Value.Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 提取网页Keywords
        /// </summary>
        /// <param name="html">Html</param>
        /// <returns>返回Keywords</returns>
        internal static string GetHtmlKeywords(string html)
        {
            if (Regex.IsMatch(html, RegexString.HtmlKeywords, RegexOptions.IgnoreCase))
            {
                return Regex.Match(html, RegexString.HtmlKeywords, RegexOptions.IgnoreCase).Groups[1].Value.Trim();
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 提取网页Description
        /// </summary>
        /// <param name="html">Html</param>
        /// <returns>返回Description</returns>
        internal static string GetHtmlDescription(string html)
        {
            if (Regex.IsMatch(html, RegexString.HtmlDescription, RegexOptions.IgnoreCase))
            {
                return Regex.Match(html, RegexString.HtmlDescription, RegexOptions.IgnoreCase).Groups[1].Value.Trim();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
