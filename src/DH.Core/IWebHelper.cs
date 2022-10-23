using Microsoft.AspNetCore.Http;

namespace DH.Core
{
    /// <summary>
    /// 表示web助手
    /// </summary>
    public partial interface IWebHelper
    {
        /// <summary>
        /// 获取URL引用者（如果存在）
        /// </summary>
        /// <returns>URL referrer</returns>
        string GetUrlReferrer();

        /// <summary>
        /// 从HTTP上下文获取IP地址
        /// </summary>
        /// <returns>IP地址字符串</returns>
        string GetCurrentIpAddress();

        /// <summary>
        /// 获取此网页URL
        /// </summary>
        /// <param name="includeQueryString">指示是否包含查询字符串的值</param>
        /// <param name="useSsl">指示是否获取SSL安全页面URL的值。传递null以自动确定</param>
        /// <param name="lowercaseUrl">指示是否小写URL的值</param>
        /// <returns>页面URL</returns>
        string GetThisPageUrl(bool includeQueryString, bool? useSsl = null, bool lowercaseUrl = false);

        /// <summary>
        /// 获取一个值，该值指示当前连接是否受保护
        /// </summary>
        /// <returns>如果安全，则为True，否则为false</returns>
        bool IsCurrentConnectionSecured();

        /// <summary>
        /// 获取存储主机位置
        /// </summary>
        /// <param name="useSsl">是否获取SSL安全URL</param>
        /// <returns>存储主机位置</returns>
        string GetStoreHost(bool useSsl);

        /// <summary>
        /// 获取系统Url
        /// </summary>
        /// <param name="useSsl">是否获取SSL安全URL；传递null以自动确定</param>
        /// <returns>系统Url</returns>
        string GetStoreLocation(bool? useSsl = null);

        /// <summary>
        /// 如果请求的资源是CMS引擎不需要处理的典型资源之一，则返回true。
        /// </summary>
        /// <returns>如果请求以静态资源文件为目标，则为True。</returns>
        bool IsStaticResource();

        /// <summary>
        /// 修改URL的查询字符串
        /// </summary>
        /// <param name="url">要修改的url</param>
        /// <param name="key">查询要添加的参数键</param>
        /// <param name="values">查询要添加的参数值</param>
        /// <returns>带有传递的查询参数的新URL</returns>
        string ModifyQueryString(string url, string key, params string[] values);

        /// <summary>
        /// 从URL中删除查询参数
        /// </summary>
        /// <param name="url">要修改的url</param>
        /// <param name="key">查询要删除的参数键</param>
        /// <param name="value">查询要删除的参数值；传递null以删除具有指定键的所有查询参数</param>
        /// <returns>没有传递查询参数的新URL</returns>
        string RemoveQueryString(string url, string key, string value = null);

        /// <summary>
        /// 按名称获取查询字符串值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="name">查询参数名称</param>
        /// <returns>查询字符串值</returns>
        T QueryString<T>(string name);

        /// <summary>
        /// 重新启动应用程序域
        /// </summary>
        void RestartAppDomain();

        /// <summary>
        /// 获取一个值，该值指示客户端是否正在重定向到新位置
        /// </summary>
        bool IsRequestBeingRedirected { get; }

        /// <summary>
        /// 获取或设置一个值，该值指示客户端是否正在使用POST重定向到新位置
        /// </summary>
        bool IsPostBeingDone { get; set; }

        /// <summary>
        /// 获取当前HTTP请求协议
        /// </summary>
        string GetCurrentRequestProtocol();

        /// <summary>
        /// 获取指定的HTTP请求URI是否引用本地主机。
        /// </summary>
        /// <param name="req">HTTP请求</param>
        /// <returns>如果HTTP请求URI引用本地主机，则为True</returns>
        bool IsLocalRequest(HttpRequest req);

        /// <summary>
        /// 获取请求的原始路径和完整查询
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <returns>原始URL</returns>
        string GetRawUrl(HttpRequest request);

        /// <summary>
        /// 获取是否使用AJAX发出请求
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <returns>结果</returns>
        bool IsAjaxRequest(HttpRequest request);
    }
}
