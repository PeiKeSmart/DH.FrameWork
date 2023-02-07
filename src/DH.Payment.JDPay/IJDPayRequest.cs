﻿using System.Collections.Generic;

namespace DG.Payment.JDPay
{
    /// <summary>
    /// JDPay 请求接口。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IJDPayRequest<T> where T : JDPayResponse
    {
        /// <summary>
        /// API接口地址
        /// </summary>
        /// <returns></returns>
        string GetRequestUrl();

        /// <summary>
        /// 获取API版本
        /// </summary>
        /// <returns></returns>
        string GetApiVersion();

        /// <summary>
        /// 设置API版本
        /// </summary>
        /// <param name="apiVersion"></param>
        void SetApiVersion(string apiVersion);

        /// <summary>
        /// 获取所有的Key-Value形式的文本请求参数字典。其中：
        /// Key: 请求参数名
        /// Value: 请求参数文本值
        /// </summary>
        /// <returns>文本请求参数字典</returns>
        IDictionary<string, string> GetParameters();
    }
}
