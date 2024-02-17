﻿using DG.HttpHelper.BaseBll;
using DG.HttpHelper.Helper;
using DG.HttpHelper.Item;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.Text;

namespace DG.HttpHelper
{
    /// <summary>
    /// gethtml方法异步调用的委托
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public delegate void ResultHandler(HttpResult item);

    /// <summary>
    /// Http帮助类
    /// </summary>
    public class HttpHelper
    {
        #region Private Obj

        /// <summary>
        /// HttpHelperBLL
        /// </summary>
        private HttpHelperBll bll = new HttpHelperBll();
        /// <summary>
        /// gethtml方法异步调用的委托
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private delegate HttpResult GethtmlHandler(HttpItem item);
        #endregion

        #region HttpHelper
        /// <summary>
        /// 根据相传入的数据，得到相应页面数据
        /// </summary>
        /// <param name="item">参数类对象</param>
        /// <returns>返回HttpResult类型</returns>
        public HttpResult GetHtml(HttpItem item)
        {
            return bll.GetHtml(item);
        }
        /// <summary>
        /// 根据Url获取图片
        /// </summary>
        /// <param name="item">HttpItem参数</param>
        /// <returns>返回图片，错误为NULL</returns>
        public Image GetImage(HttpItem item)
        {
            return bll.GetImage(item);
        }
        /// <summary>
        /// 快速请求方法FastRequest（极速请求不接收数据,只做提交）不返回Header、Cookie、Html
        /// </summary>
        /// <param name="item">参数类对象</param>
        /// <returns>返回HttpResult类型</returns>
        public HttpResult FastRequest(HttpItem item)
        {

            return bll.FastRequest(item);
        }
        #endregion

        #region Cookie
        /// <summary>
        /// 根据字符生成Cookie和精简串，将排除path,expires,domain以及重复项
        /// </summary>
        /// <param name="strcookie">Cookie字符串</param>
        /// <returns>精简串</returns>
        public static string GetSmallCookie(string strcookie)
        {
            return HttpCookieHelper.GetSmallCookie(strcookie);
        }
        /// <summary>
        /// 将字符串Cookie转为CookieCollection
        /// </summary>
        /// <param name="strcookie">Cookie字符串</param>
        /// <returns>List-CookieItem</returns>
        public static CookieCollection StrCookieToCookieCollection(string strcookie)
        {
            return HttpCookieHelper.StrCookieToCookieCollection(strcookie);
        }
        /// <summary>
        /// 将CookieCollection转为字符串Cookie
        /// </summary>
        /// <param name="cookie">Cookie字符串</param>
        /// <returns>strcookie</returns>
        public static string CookieCollectionToStrCookie(CookieCollection cookie)
        {
            return HttpCookieHelper.CookieCollectionToStrCookie(cookie);
        }
        /// <summary>
        /// 自动合并两个Cookie的值返回更新后结果 
        /// </summary>
        /// <param name="cookie1">Cookie1</param>
        /// <param name="cookie2">Cookie2</param>
        /// <returns>返回更新后的Cookie</returns>
        public static string GetMergeCookie(string cookie1, string cookie2)
        {
            return HttpCookieHelper.GetMergeCookie(cookie1, cookie2);
        }
        #endregion

        #region URL

        /// <summary>
        /// 使用指定的编码对象将 URL 编码的字符串转换为已解码的字符串。
        /// </summary>
        /// <param name="text">指定的字符串</param>
        /// <param name="encoding">指定编码默认为Default</param>
        /// <returns>解码后字符串</returns>
        public static string URLDecode(string text, Encoding encoding = null)
        {
            return HttpUrlHelper.URLDecode(text, encoding);
        }
        /// <summary>
        /// 使用指定的编码对象对 URL 字符串进行编码。
        /// </summary>
        /// <param name="text">指定的字符串</param>
        /// <param name="encoding">指定编码默认为Default</param>
        /// <returns>转码后字符串</returns>
        public static string URLEncode(string text, Encoding encoding = null)
        {
            return HttpUrlHelper.URLEncode(text, encoding);
        }
        /// <summary>
        /// 将Url参数字符串转为一个Key和Value的集合
        /// </summary>
        /// <param name="str">要转为集合的字符串</param>
        /// <returns>NameValueCollection</returns>
        public static NameValueCollection GetNameValueCollection(string str)
        {
            return HttpUrlHelper.GetNameValueCollection(str);
        }
        /// <summary>
        /// 提取网站主机部分就是host
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>host</returns>
        public static string GetUrlHost(string url)
        {
            return HttpUrlHelper.GetUrlHost(url);
        }
        /// <summary>
        /// 提取网址对应的IP地址
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>返回Url对应的IP地址</returns>
        public static string GetUrlIp(string url)
        {
            return HttpUrlHelper.GetUrlIp(url);
        }
        #endregion

        #region HTML
        /// <summary>
        /// 获取所有的A链接
        /// </summary>
        /// <param name="html">要分析的Html代码</param>
        /// <param name="host">host</param>
        /// <returns>返回一个List存储所有的A标签</returns>
        public static List<AItem> GetAList(string html,string host= "deng-haocom")
        {
            return HtmlHelper.GetAList(html,host);
        }
        /// <summary>
        /// 获取所有的Img标签
        /// </summary>
        /// <param name="html">要分析的Html代码</param>
        /// <returns>返回一个List存储所有的Img标签</returns>
        public static List<ImgItem> GetImgList(string html)
        {
            return HtmlHelper.GetImgList(html);
        }
        /// <summary>
        /// 过滤html标签
        /// </summary>
        /// <param name="html">html的内容</param>
        /// <returns>处理后的文本</returns>
        public static string StripHTML(string html)
        {
            return HtmlHelper.StripHTML(html);
        }
        /// <summary>
        /// 过滤html中所有的换行符号
        /// </summary>
        /// <param name="html">html的内容</param>
        /// <returns>处理后的文本</returns>
        public static string ReplaceNewLine(string html)
        {
            return HtmlHelper.ReplaceNewLine(html);
        }

        /// <summary>
        /// 提取Html字符串中两字符之间的数据
        /// </summary>
        /// <param name="html">源Html</param>
        /// <param name="s">开始字符串</param>
        /// <param name="e">结束字符串</param>
        /// <returns></returns>
        public static string GetBetweenHtml(string html, string s, string e)
        {
            return HtmlHelper.GetBetweenHtml(html, s, e);
        }
        /// <summary>
        /// 提取网页Title
        /// </summary>
        /// <param name="html">Html</param>
        /// <returns>返回Title</returns>
        public static string GetHtmlTitle(string html)
        {
            return HtmlHelper.GetHtmlTitle(html);
        }
        /// <summary>
        /// Keywords
        /// </summary>
        /// <param name="html">Html</param>
        /// <returns>返回Keywords</returns>
        public static string GetHtmlKeywords(string html)
        {
            return HtmlHelper.GetHtmlKeywords(html);
        }
        /// <summary>
        /// 提取网页Description
        /// </summary>
        /// <param name="html">Html</param>
        /// <returns>返回Description</returns>
        public static string GetHtmlDescription(string html)
        {
            return HtmlHelper.GetHtmlDescription(html);
        }
        #endregion

        #region JavaScript
        /// <summary>
        /// 直接调用JS方法并获取返回的值
        /// </summary>
        /// <param name="strJs">要执行的JS代码</param>
        /// <param name="main">要调用的方法名</param>
        /// <returns>执行结果</returns>
        public static string JavaScriptEval(string strJs, string main)
        {
            return ExecJsHelper.JavaScriptEval(strJs, main);
        }

        #endregion

        #region Image
        /// <summary>
        /// 将字节数组转为图片
        /// </summary>
        /// <param name=" b">字节数组</param>
        /// <returns>返回图片</returns>
        public static System.Drawing.Image GetImage(byte[] b)
        {
            return ImageHelper.ByteToImage(b);
        }
        #endregion

        #region Encoding
        /// <summary>
        /// 将字节数组转为字符串
        /// </summary>
        /// <param name="b">字节数组</param>
        /// <param name="e">编码，默认为Default</param>
        /// <returns>字符串</returns>
        public static string ByteToString(byte[] b, Encoding e = null)
        {
            return EncodingHelper.ByteToString(b, e);
        }
        /// <summary>
        /// 将字符串转为字节数组
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="e">编码，默认为Default</param>
        /// <returns>字节数组</returns>
        public static byte[] StringToByte(string s, Encoding e = null)
        {
            return EncodingHelper.StringToByte(s, e);
        }
        #endregion

        #region Base64

        /// <summary>
        /// 将Base64编码解析成字符串
        /// </summary>
        /// <param name="strbase">要解码的string字符</param>
        /// <param name="encoding">字符编码方案</param>
        /// <returns>字符串</returns>
        public static string Base64ToString(string strbase, Encoding encoding)
        {
            return Base64Helper.Base64ToString(strbase, encoding);
        }
        /// <summary>
        /// 将字节数组为Base64编码
        /// </summary>
        /// <param name="bytebase">要编码的byte[]</param>
        /// <returns>base字符串</returns>
        public static string ByteToBase64(byte[] bytebase)
        {
            return Base64Helper.StringToBase64(bytebase);
        }
        /// <summary>
        /// 将字符串转为Base64编码
        /// </summary>
        /// <param name="str">要编码的string字符</param>
        /// <param name="encoding">字符编码方案</param>
        /// <returns>base字符串</returns>
        public static string StringToBase64(string str, Encoding encoding)
        {
            return Base64Helper.StringToBase64(str, encoding);
        }
        #endregion
    }
}
