﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace DH.Payment.Alipay
{
    /// <summary>
    /// Alipay 通知客户端
    /// </summary>
    public interface IAlipayNotifyClient
    {
        /// <summary>
        /// 执行 Alipay 通知请求解析。
        /// </summary>
        /// <typeparam name="T">领域对象</typeparam>
        /// <param name="request">控制器的请求</param>
        /// <param name="options">配置选项</param>
        /// <returns>领域对象</returns>
        Task<T> ExecuteAsync<T>(Microsoft.AspNetCore.Http.HttpRequest request, AlipayOptions options) where T : AlipayNotify;

        /// <summary>
        /// 执行 Alipay 通知请求解析。
        /// </summary>
        /// <typeparam name="T">领域对象</typeparam>
        /// <param name="request">控制器的请求</param>
        /// <param name="options">配置选项</param>
        /// <returns>领域对象</returns>
        Task<T> CertificateExecuteAsync<T>(Microsoft.AspNetCore.Http.HttpRequest request, AlipayOptions options) where T : AlipayNotify;

        /// <summary>
        /// 获取通知参数
        /// </summary>
        /// <param name="request"></param>
        Task<IDictionary<string, string>> GetParametersAsync(Microsoft.AspNetCore.Http.HttpRequest request);

        /// <summary>
        /// 执行 Alipay 通知请求解析。
        /// </summary>
        /// <typeparam name="T">领域对象</typeparam>
        /// <param name="parameters">通知参数</param>
        /// <param name="options">配置选项</param>
        /// <returns>领域对象</returns>
        Task<T> ExecuteAsync<T>(IDictionary<string, string> parameters, AlipayOptions options) where T : AlipayNotify;

        /// <summary>
        /// 执行 Alipay 通知请求解析。
        /// </summary>
        /// <typeparam name="T">领域对象</typeparam>
        /// <param name="parameters">通知参数</param>
        /// <param name="options">配置选项</param>
        /// <returns>领域对象</returns>
        Task<T> CertificateExecuteAsync<T>(IDictionary<string, string> parameters, AlipayOptions options) where T : AlipayNotify;
    }
}
