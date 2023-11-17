using System.Net;
using System.Net.NetworkInformation;

namespace DH.Helpers;

/// <summary>
/// 网络端帮助类
/// </summary>
public static class NetHelper {

    /// <summary>
    /// 判断端口是否被占用
    /// </summary>
    /// <param name="port"></param>
    /// <returns></returns>
    public static bool PortInUse(int port)
    {
        bool inUse = false;

        IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
        IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

        foreach (IPEndPoint endPoint in ipEndPoints)
        {
            if (endPoint.Port == port)
            {
                inUse = true;
                break;
            }
        }

        return inUse;
    }

}
