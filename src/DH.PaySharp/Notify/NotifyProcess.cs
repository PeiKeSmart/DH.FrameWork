﻿using DH.PaySharp.Utils;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DH.PaySharp
{
    /// <summary>
    /// 网关通知的处理类，通过对返回数据的分析识别网关类型
    /// </summary>
    internal static class NotifyProcess
    {
        /// <summary>
        /// 是否是Xml格式数据
        /// </summary>
        /// <returns></returns>
        private static bool IsXmlData => HttpUtil.ContentType == "text/xml" || HttpUtil.ContentType == "application/xml";

        /// <summary>
        /// 是否是GET请求
        /// </summary>
        /// <returns></returns>
        private static bool IsGetRequest => HttpUtil.RequestType == "GET";

        /// <summary>
        /// 获取网关
        /// </summary>
        /// <param name="gateways">网关列表</param>
        /// <returns></returns>
        public static async Task<BaseGateway> GetGatewayAsync(IGateways gateways)
        {
            var gatewayData = await ReadNotifyDataAsync();
            BaseGateway gateway = null;

            foreach (var item in gateways.GetList())
            {
                if (ExistParameter(item.NotifyVerifyParameter, gatewayData))
                {
                    if (item.Merchant.AppId == gatewayData
                        .GetStringValue(item.NotifyVerifyParameter.FirstOrDefault()))
                    {
                        gateway = item;
                        break;
                    }
                }
            }

            if (gateway is null)
            {
                gateway = new NullGateway();
            }

            gateway.GatewayData = gatewayData;
            return gateway;
        }

        /// <summary>
        /// 网关参数数据项中是否存在指定的所有参数名
        /// </summary>
        /// <param name="parmaName">参数名数组</param>
        /// <param name="gatewayData">网关数据</param>
        public static bool ExistParameter(string[] parmaName, GatewayData gatewayData)
        {
            var compareCount = 0;
            foreach (var item in parmaName)
            {
                if (gatewayData.Exists(item))
                {
                    compareCount++;
                }
            }

            return compareCount == parmaName.Length;
        }

        /// <summary>
        /// 读取网关发回的数据
        /// </summary>
        /// <returns></returns>
        public static async Task<GatewayData> ReadNotifyDataAsync()
        {
            var gatewayData = new GatewayData();
            if (IsGetRequest)
            {
                gatewayData.FromUrl(HttpUtil.QueryString);
            }
            else
            {
                if (IsXmlData)
                {
                    var reader = new StreamReader(HttpUtil.Body);
                    var xmlData = await reader.ReadToEndAsync();
                    gatewayData.FromXml(xmlData);
                }
                else
                {
                    try
                    {
                        gatewayData.FromForm(HttpUtil.Form);
                    }
                    catch { }
                }
            }

            return gatewayData;
        }
    }
}
