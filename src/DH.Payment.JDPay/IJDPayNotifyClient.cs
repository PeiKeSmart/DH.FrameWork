﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DH.Payment.JDPay
{
    /// <summary>
    /// JDPay 通知解析客户端。
    /// </summary>
    public interface IJDPayNotifyClient
    {
        /// <summary>
        /// 执行JDPay通知请求解析。
        /// </summary>
        /// <typeparam name="T">领域对象</typeparam>
        /// <param name="request">控制器的请求</param>
        /// <param name="options">配置选项</param>
        /// <returns>领域对象</returns>
        Task<T> ExecuteAsync<T>(HttpRequest request, JDPayOptions options) where T : JDPayNotify;
    }
}