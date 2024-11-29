using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

using AutoMapper;

using DH.Core.Infrastructure;
using DH.Entity;
using DH.Extensions;

using IP2Region.Net.Abstractions;
using IP2Region.Net.XDB;

using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Exceptions;
using MaxMind.GeoIP2.Model;
using MaxMind.GeoIP2.Responses;

using Microsoft.AspNetCore.Http;

using NewLife;

using Pek;
using Pek.Models;
using Pek.Timing;

using Polly;

using StackExchange.Profiling.Internal;

using TimeZoneConverter;

namespace DH.Services.Common;

public static class CommonHelpers
{
    /// <summary>
    /// 系统设定
    /// </summary>
    public static ConcurrentDictionary<string, string> SystemSettings { get; set; } = new();

    /// <summary>
    /// 根据指定的ResourceKey属性获取资源字符串。
    /// </summary>
    /// <param name="resourceKey">代表ResourceKey的字符串</param>
    /// <returns>代表请求的资源字符串的翻译内容</returns>
    public static String GetResource(String resourceKey)
    {
        return LocaleStringResource.GetResource(resourceKey);
    }

    /// <summary>
    /// 校验码校验
    /// </summary>
    /// <param name="ImgCheckCode"></param>
    /// <returns></returns>
    public static DResult CheckCode(String ImgCheckCode)
    {
        var result = new DResult();

        var systemCheckCode = Pek.Webs.HttpContext.Current.Session.GetString("ybbcode");
        if (systemCheckCode.IsNullOrEmpty())
        {
            result.msg = GetResource("图片验证码过期");
            result.code = -1;
            return result;
        }

        if (systemCheckCode.ToLower() != ImgCheckCode.ToLower())
        {
            // 生成随机验证码，强制使验证码过期（一交提交必须更改验证码）
            Pek.Webs.HttpContext.Current.Session.SetString("ybbcode", Guid.NewGuid().ToString());
            result.msg = GetResource("图片验证码错误");
            result.code = -1;
            return result;
        }

        result.success = true;
        return result;
    }

    /// <summary>
    /// 检查短信/邮箱验证码是否超过60秒
    /// </summary>
    /// <param name="Account">手机号码/邮箱</param>
    /// <returns></returns>
    public static DResult CheckSendCodeTime(String Account)
    {
        var result = new DResult();

        var model = SendLog.FindLastByAccount(Account);

        if (model != null)
        {
            if (model.CreateTime.AddSeconds(60) > DateTime.Now)
            {
                var seconds = Math.Abs(DateTimeUtil.StrDateDiffSeconds(model.CreateTime.ToString(), 60));

                result.code = -2;
                result.data = seconds;
                result.msg = String.Format(GetResource("请等待{0}秒后重试"), seconds);
                return result;
            }
        }

        result.success = true;
        return result;
    }

    /// <summary>
    /// 敏感词
    /// </summary>
    public static string BanRegex { get; set; }

    /// <summary>
    /// 审核词
    /// </summary>
    public static string ModRegex { get; set; }

    /// <summary>
    /// 全局禁止IP
    /// </summary>
    public static string DenyIP { get; set; }

    /// <summary>
    /// ip白名单
    /// </summary>
    public static List<string> IPWhiteList { get; set; }

    /// <summary>
    /// 每IP错误的次数统计
    /// </summary>
    public static ConcurrentDictionary<string, int> IPErrorTimes { get; set; } = new();

    /// <summary>
    /// IP黑名单地址段
    /// </summary>
    public static Dictionary<string, string> DenyIPRange { get; set; }

    /// <summary>
    /// 判断IP地址是否被黑名单
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static bool IsDenyIpAddress(this string ip)
    {
        if (IPWhiteList.Contains(ip))
        {
            return false;
        }

        return DenyIP.Contains(ip) || DenyIPRange.AsParallel().Any(kv => kv.Key.StartsWith(ip.Split('.')[0]) && ip.IpAddressInRange(kv.Key, kv.Value));
    }

    /// <summary>
    /// 是否是禁区
    /// </summary>
    /// <param name="ips"></param>
    /// <returns></returns>
    public static bool IsInDenyArea(this string ips)
    {
        var denyAreas = SystemSettings.GetOrAdd("DenyArea", "").Split(new[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
        if (denyAreas.Any())
        {
            foreach (var item in ips.Split(','))
            {
                var pos = DHGetIPLocation(item);
                return pos.Contains(denyAreas) || denyAreas.Intersect(pos.Split("|")).Any();
            }
        }

        return false;
    }

    //private static readonly DbSearcher IPSearcher = new(Path.Combine(AppContext.BaseDirectory + "/Data", "ip2region.db"));
    private static readonly ISearcher IPSearcher = new Searcher(CachePolicy.Content, Path.Combine(AppContext.BaseDirectory + "/Data", "ip2region.xdb"));
    public static readonly DatabaseReader MaxmindReader = new(Path.Combine(AppContext.BaseDirectory + "/Data", "GeoLite2-City.mmdb").Replace("//", "/"));
    private static readonly DatabaseReader MaxmindAsnReader = new(Path.Combine(AppContext.BaseDirectory + "/Data", "GeoLite2-ASN.mmdb"));

    public static AsnResponse GetIPAsn(this string ip)
    {
        if (ip.IsPrivateIP())
        {
            return new AsnResponse();
        }

        return Policy<AsnResponse>.Handle<AddressNotFoundException>().Fallback(new AsnResponse()).Execute(() => MaxmindAsnReader.Asn(ip));
    }

    public static AsnResponse GetIPAsn(this IPAddress ip)
    {
        return Policy<AsnResponse>.Handle<AddressNotFoundException>().Fallback(new AsnResponse()).Execute(() => MaxmindAsnReader.Asn(ip));
    }

    private static CityResponse GetCityResp(IPAddress ip)
    {
        return Policy<CityResponse>.Handle<AddressNotFoundException>().Fallback(new CityResponse()).Execute(() => MaxmindReader.City(ip));
    }

    public static string DHGetIPLocation(this string ips)
    {
        var (location, network) = DHGetIPLocation(IPAddress.Parse(ips));
        return location + "|" + network;
    }

    public static IPLocation GetIPLocation(this string ip)
    {
        var b = IPAddress.TryParse(ip, out var ipAddress);
        if (b)
        {
            return GetIPLocation(ipAddress);
        }

        throw new ArgumentException("不能将" + ip + "转换成IP地址");
    }

    public static IPLocation GetIPLocation(this IPAddress ip)
    {
        if (ip.IsPrivateIP())
        {
            return new IPLocation("内网", null, null, "内网IP", null);
        }

        var city = GetCityResp(ip);
        var asn = GetIPAsn(ip);
        var countryName = city.Country.Names.GetValueOrDefault("zh-CN") ?? city.Country.Name;
        var cityName = city.City.Names.GetValueOrDefault("zh-CN") ?? city.City.Name;
        switch (ip.AddressFamily)
        {
            case AddressFamily.InterNetworkV6 when ip.IsIPv4MappedToIPv6:
                ip = ip.MapToIPv4();
                goto case AddressFamily.InterNetwork;
            case AddressFamily.InterNetwork:
                var parts = IPSearcher.Search(ip.ToString())?.Split('|');
                if (parts != null)
                {
                    var network = parts[^1] == "0" ? asn.AutonomousSystemOrganization : parts[^1] + "/" + asn.AutonomousSystemOrganization;
                    parts[0] = parts[0] != "0" ? parts[0] : countryName;
                    parts[3] = parts[3] != "0" ? parts[3] : cityName;
                    return new IPLocation(parts[0], parts[2], parts[3], network?.Trim('/'), asn.AutonomousSystemNumber)
                    {
                        Address2 = countryName + cityName,
                        Coodinate = city.Location
                    };
                }

                goto default;
            default:
                return new IPLocation(countryName, null, cityName, asn.AutonomousSystemOrganization, asn.AutonomousSystemNumber)
                {
                    Coodinate = city.Location
                };
        }
    }

    public static (string location, string network) DHGetIPLocation(this IPAddress ip, String JoinString = "")
    {
        switch (ip.AddressFamily)
        {
            case AddressFamily.InterNetwork when ip.IsPrivateIP():
            case AddressFamily.InterNetworkV6 when ip.IsPrivateIP():
                return ("内网", "内网IP");
            case AddressFamily.InterNetworkV6 when ip.IsIPv4MappedToIPv6:
                ip = ip.MapToIPv4();
                goto case AddressFamily.InterNetwork;
            case AddressFamily.InterNetwork:
                var parts = IPSearcher.Search(ip.ToString())?.Split('|');
                if (parts != null)
                {
                    var asn = GetIPAsn(ip);
                    var network = parts[^1] == "0" ? asn.AutonomousSystemOrganization : parts[^1];
                    var location = parts[..^1].Where(s => s != "0").Distinct().Join(JoinString);
                    return (location, network + $"(AS{asn.AutonomousSystemNumber})");
                }

                goto default;
            default:
                var cityResp = Policy<CityResponse>.Handle<AddressNotFoundException>().Fallback(new CityResponse()).Execute(() => MaxmindReader.City(ip));
                var asnResp = GetIPAsn(ip);
                return (cityResp.Country.Names.GetValueOrDefault("zh-CN") + JoinString + cityResp.City.Names.GetValueOrDefault("zh-CN"), asnResp.AutonomousSystemOrganization + $"(AS{asnResp.AutonomousSystemNumber})");
        }
    }

    public static (String location, String network, String number) DHGetIPLocations(this IPAddress ip, String JoinString = "")
    {
        switch (ip.AddressFamily)
        {
            case AddressFamily.InterNetwork when ip.IsPrivateIP():
            case AddressFamily.InterNetworkV6 when ip.IsPrivateIP():
                return ("内网", "内网IP", "");
            case AddressFamily.InterNetworkV6 when ip.IsIPv4MappedToIPv6:
                ip = ip.MapToIPv4();
                goto case AddressFamily.InterNetwork;
            case AddressFamily.InterNetwork:
                var parts = IPSearcher.Search(ip.ToString())?.Split('|');
                if (parts != null)
                {
                    var asn = GetIPAsn(ip);
                    var network = parts[^1] == "0" ? asn.AutonomousSystemOrganization : parts[^1];
                    var regions = parts[..^1].Where(s => s != "0").Distinct();
                    var location = regions.Join(JoinString);
                    return (location, network, $"(AS{asn.AutonomousSystemNumber})");
                }

                goto default;
            default:
                {
                    var cityResp = Policy<CityResponse>.Handle<AddressNotFoundException>().Fallback(new CityResponse()).Execute(() => MaxmindReader.City(ip));
                    var asnResp = GetIPAsn(ip);

                    var regions = new List<String> { cityResp.Country.Names.GetValueOrDefault("zh-CN"), cityResp.City.Names.GetValueOrDefault("zh-CN") };

                    return (regions.Join(JoinString), asnResp.AutonomousSystemOrganization, $"(AS{asnResp.AutonomousSystemNumber})");
                }
        }
    }

    /// <summary>
    /// 获取ip所在时区
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static string GetClientTimeZone(this IPAddress ip)
    {
        switch (ip.AddressFamily)
        {
            case AddressFamily.InterNetwork when ip.IsPrivateIP():
            case AddressFamily.InterNetworkV6 when ip.IsPrivateIP():
                return "Asia/Shanghai";
            default:
                var resp = Policy<CityResponse>.Handle<AddressNotFoundException>().Fallback(new CityResponse()).Execute(() => MaxmindReader.City(ip));
                return resp.Location.TimeZone ?? "Asia/Shanghai";
        }
    }

    /// <summary>
    /// 类型映射
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static T Mapper<T>(this object source) where T : class
    {
        var mapper = EngineContext.Current.Resolve<IMapper>();
        return mapper.Map<T>(source);
    }

    ///// <summary>
    ///// 清理html的img标签的除src之外的其他属性
    ///// </summary>
    ///// <param name="html"></param>
    ///// <returns></returns>
    //public static async Task<string> ClearImgAttributes(this string html)
    //{
    //    var context = BrowsingContext.New(AngleSharp.Configuration.Default);
    //    var doc = await context.OpenAsync(req => req.Content(html));
    //    var nodes = doc.DocumentElement.GetElementsByTagName("img");
    //    var allows = new[] { "src", "data-original", "width", "style", "class" };
    //    foreach (var node in nodes)
    //    {
    //        for (var i = 0; i < node.Attributes.Length; i++)
    //        {
    //            if (allows.Contains(node.Attributes[i].Name))
    //            {
    //                continue;
    //            }

    //            node.RemoveAttribute(node.Attributes[i].Name);
    //        }
    //    }

    //    return doc.Body.InnerHtml;
    //}

    ///// <summary>
    ///// 将html的img标签的src属性名替换成data-original
    ///// </summary>
    ///// <param name="html"></param>
    ///// <param name="title"></param>
    ///// <returns></returns>
    //public static async Task<string> ReplaceImgAttribute(this string html, string title)
    //{
    //    var context = BrowsingContext.New(AngleSharp.Configuration.Default);
    //    var doc = await context.OpenAsync(req => req.Content(html));
    //    var nodes = doc.DocumentElement.GetElementsByTagName("img");
    //    foreach (var node in nodes)
    //    {
    //        if (node.HasAttribute("src"))
    //        {
    //            string src = node.Attributes["src"].Value;
    //            node.RemoveAttribute("src");
    //            node.SetAttribute("data-original", src);
    //            node.SetAttribute("alt", SystemSettings["Title"]);
    //            node.SetAttribute("title", title);
    //        }
    //    }

    //    return doc.Body.InnerHtml;
    //}

    ///// <summary>
    ///// 获取文章摘要
    ///// </summary>
    ///// <param name="html"></param>
    ///// <param name="length">截取长度</param>
    ///// <param name="min">摘要最少字数</param>
    ///// <returns></returns>
    //public static async Task<string> GetSummary(this string html, int length = 150, int min = 10)
    //{
    //    var context = BrowsingContext.New(AngleSharp.Configuration.Default);
    //    var doc = await context.OpenAsync(req => req.Content(html));
    //    var summary = doc.DocumentElement.GetElementsByTagName("p").FirstOrDefault(n => n.TextContent.Length > min)?.TextContent ?? "没有摘要";
    //    if (summary.Length > length)
    //    {
    //        return summary[..length] + "...";
    //    }

    //    return summary;
    //}

    public static string TrimQuery(this string path)
    {
        return path.Split('&').Where(s => s.Split('=', StringSplitOptions.RemoveEmptyEntries).Length == 2).Join("&");
    }

    /// <summary>
    /// 添加水印
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static Stream AddWatermark(this Stream stream)
    {
        if (!string.IsNullOrEmpty(SystemSettings.GetOrAdd("Watermark", string.Empty)))
        {
            try
            {
                // 使用Magick.NET
                //var watermarker = new ImageWatermarker(stream)
                //{
                //    SkipWatermarkForSmallImages = true,
                //    SmallImagePixelsThreshold = 90000
                //};
                //return watermarker.AddWatermark(SystemSettings["Watermark"], Color.LightGray, WatermarkPosition.BottomRight, 30);
            }
            catch
            {
                //
            }
        }
        return stream;
    }

    /// <summary>
    /// 转换时区
    /// </summary>
    /// <param name="time">UTC时间</param>
    /// <param name="zone">时区id</param>
    /// <returns></returns>
    public static DateTime ToTimeZone(this in DateTime time, string zone)
    {
        return TimeZoneInfo.ConvertTime(time, TZConvert.GetTimeZoneInfo(zone));
    }

    /// <summary>
    /// 转换时区
    /// </summary>
    /// <param name="time">UTC时间</param>
    /// <param name="zone">时区id</param>
    /// <param name="format">时间格式字符串</param>
    /// <returns></returns>
    public static string ToTimeZoneF(this in DateTime time, string zone, string format = "yyyy-MM-dd HH:mm:ss")
    {
        return ToTimeZone(time, zone).ToString(format);
    }
}

public class IPLocation
{
    public IPLocation(string country, string province, string city, string isp, long? asn)
    {
        Country = country?.Trim('0');
        Province = province?.Trim('0');
        City = city?.Trim('0');
        ISP = isp;
        ASN = asn;
    }

    public string Country { get; set; }

    public string Province { get; set; }

    public string City { get; set; }

    public string ISP { get; set; }

    public long? ASN { get; set; }

    public string Address => new[] { Country, Province, City }.Where(s => !string.IsNullOrEmpty(s)).Distinct().Join("");

    public string Address2 { get; set; }

    public string Network => ASN.HasValue ? ISP + "(AS" + ASN + ")" : ISP;

    public Location Coodinate { get; set; }

    public override string ToString()
    {
        string address = Address;
        string network = Network;
        if (string.IsNullOrWhiteSpace(address))
        {
            address = "未知地区";
        }

        if (string.IsNullOrWhiteSpace(network))
        {
            network = "未知网络";
        }

        return new[] { address, Address2, network }.Where(s => !string.IsNullOrEmpty(s)).Distinct().Join("|");
    }

    public static implicit operator string(IPLocation entry)
    {
        return entry.ToString();
    }

    public void Deconstruct(out string location, out string network, out string info)
    {
        location = new[] { Address, Address2 }.Where(s => !string.IsNullOrEmpty(s)).Distinct().Join("|");
        network = Network;
        info = ToString();
    }

    public bool Contains(string s)
    {
        return ToString().Contains(s, StringComparison.CurrentCultureIgnoreCase);
    }

    public bool Contains(params string[] s)
    {
        return ToString().Contains(s);
    }
}