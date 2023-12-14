using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Reflection;
using NewLife.IoT.Drivers;
using NewLife.IoT.ThingModels;
using NewLife.IoT.ThingSpecification;

namespace NewLife.NetPing.Drivers;

/// <summary>
/// 设备网络心跳驱动
/// </summary>
/// <remarks>
/// IoT驱动，通过Ping探测到目标设备的网络情况，并收集延迟数据
/// </remarks>
[Driver("NetPing")]
[DisplayName("设备网络心跳")]
public class NetPingDriver : DriverBase<Node, NetPingParameter>
{
    #region 方法
    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="node">节点对象，可存储站号等信息，仅驱动自己识别</param>
    /// <param name="points">点位集合，Address属性地址示例：D100、C100、W100、H100</param>
    /// <returns></returns>
    public override IDictionary<String, Object> Read(INode node, IPoint[] points)
    {
        var dic = new Dictionary<String, Object>();

        if (points == null || points.Length == 0) return dic;

        var p = node.Parameter as NetPingParameter;
        foreach (var point in points)
        {
            if (!point.Address.IsNullOrEmpty())
            {
                try
                {
                    var reply = new Ping().Send(point.Address, p.Timeout);
                    if (reply.Status == IPStatus.Success)
                        dic[point.Name] = reply.RoundtripTime;
                    if (p.RetrieveStatus)
                        dic[point.Name + "-Status"] = reply.Status + "";
                }
                catch (Exception ex)
                {
                    dic[point.Name + "-Status"] = ex.GetTrue().Message;
                }
            }
        }

        return dic;
    }

    /// <summary>发现本地节点</summary>
    /// <returns></returns>
    public override ThingSpec GetSpecification()
    {
        var type = GetType();
        var spec = new ThingSpec
        {
            Profile = new Profile
            {
                Version = type.Assembly.GetName().Version + "",
                ProductKey = type.GetCustomAttribute<DriverAttribute>().Name
            }
        };

        var points = new List<PropertySpec>();
        var extends = new List<PropertyExtend>();

        // 所有网关地址和DNS地址
        var gaddrs = new List<String>();
        var daddrs = new List<String>();
        var gi = 1;
        var di = 1;
        foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
        {
            var ipps = item.GetIPProperties();
            foreach (var elm in ipps.GatewayAddresses)
            {
                var ip = elm.Address + "";
                if (!gaddrs.Contains(ip))
                {
                    var name = "Gateway";
                    if (gi > 1) name += gi++;
                    var ps = PropertySpec.Create(name, $"{item.Name}网关", "int", 0);
                    ps.DataType.Specs = new DataSpecs { Unit = "ms", UnitName = "毫秒" };
                    points.Add(ps);
                    extends.Add(new PropertyExtend { Id = name, Address = ip });
                    gaddrs.Add(ip);
                }
            }
            foreach (var elm in ipps.DnsAddresses)
            {
                if (!elm.IsIPv4()) continue;

                var ip = elm + "";
                if (!daddrs.Contains(ip))
                {
                    var name = "Dns";
                    if (di > 1) name += di++;
                    var ps = PropertySpec.Create(name, $"{item.Name}DNS", "int", 0);
                    ps.DataType.Specs = new DataSpecs { Unit = "ms", UnitName = "毫秒" };
                    points.Add(ps);
                    extends.Add(new PropertyExtend { Id = name, Address = ip });
                    daddrs.Add(ip);
                }
            }
        }

        spec.Properties = points.ToArray();

        return spec;
    }
    #endregion
}