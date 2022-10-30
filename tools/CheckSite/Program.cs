using DH.WebHook;

using NewLife;
using NewLife.Agent;
using NewLife.Log;
using NewLife.Reflection;
using NewLife.Threading;

using RestSharp;

using System.Reflection;

namespace CheckSite;

class Program
{
    static void Main(String[] args)
    {
        if (WebHookSetting.Current.DingTalkSendUrl.IsNullOrWhiteSpace())
        {
            WebHookSetting.Current.DingTalkSendUrl = "https://oapi.dingtalk.com/robot/send?access_token=c0a66acf134d4f08581c8c3d834dfc1155f8c90b2b8955e0d7c244ce305a301a";
            WebHookSetting.Current.Save();
        }

        new MyService().Main(args);
    }
}

/// <summary>服务类。名字可以自定义</summary>
class MyService : ServiceBase
{
    public MyService()
    {
        ServiceName = "CheckSiteService";
        DisplayName = "网站状态检测服务";
        Description = "湖北登灏科技有限公司网站状态检测服务";

        // 注册菜单，在控制台菜单中按 t 可以执行Test函数，主要用于临时处理数据

        AddMenu('t', "服务器信息", ShowMachineInfo);
    }

    /// <summary>
    /// 定时器
    /// </summary>
    private TimerX? _timer;

    /// <summary>
    /// 定时器
    /// </summary>
    private TimerX? _timer1;

    /// <summary>
    /// 上次执行的时间
    /// </summary>
    private DateTime LastTime = DateTime.Now;

    /// <summary>开始服务</summary>
    /// <param name="reason"></param>
    protected override void StartWork(string reason)
    {
        _timer = new TimerX(DoSchedule, null, 1_000, 60_000, "CheckSite") { Async = true };
        _timer1 = new TimerX(DoSchedule1, null, 10_000, 60_000, "CheckState") { Async = false };
    }

    private void DoSchedule1(object? state)
    {
        if (_timer == null)
        {
            XTrace.WriteLine("逻辑计时器失效");
            _timer = new TimerX(DoSchedule, null, 1_000, 60_000, "CheckSite") { Async = true };
        }
        if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 23)
        {
            if (LastTime < DateTime.Now.AddMinutes(-2))
            {
                _timer = new TimerX(DoSchedule, null, 1_000, 60_000, "CheckSite") { Async = true };
            }
        }
    }

    private async Task DoSchedule(object state)
    {
        if (_timer1 == null)
        {
            XTrace.WriteLine("校验计时器失效");
            _timer1 = new TimerX(DoSchedule1, null, 10_000, 60_000, "CheckSite1") { Async = true };
        }

        XTrace.WriteLine("执行开始");
        if (DateTime.Now.Hour > 23 && DateTime.Now.Hour < 8)
        {
            _timer!.Period = 600_000;
            XTrace.WriteLine("晚上不执行请求");
            return;
        }
        LastTime = DateTime.Now;
        var site = Site.GetAll();
        foreach (var row in site)
        {
            if (row.SiteUrl.IsNullOrWhiteSpace())
                continue;

            var client = new RestClient(row.SiteUrl!);
            var request = new RestRequest("", Method.Get);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var content = $"测试推送----{row.SiteName}出现错误：500,网站打开报错";
                XTrace.WriteLine(content);
                DingTalkRobot.OapiRobotText(content, new List<string>(), false);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var content = $"测试推送----{row.SiteName}出现错误：404,网站打开文件不存在";
                XTrace.WriteLine(content);
                DingTalkRobot.OapiRobotText(content, new List<string>(), false);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response.Content!.Contains("\"code\":2"))
                {
                    var content = $"测试推送----{row.SiteName}出现错误：500,网站打开报错";
                    XTrace.WriteLine(content);
                    DingTalkRobot.OapiRobotText(content, new List<string>(), false);
                }
            }
            else if ((Int32)response.StatusCode == 0)
            {
                XTrace.WriteLine($"{response.StatusCode}_{row.SiteUrl}");

                var content = $"测试推送----{row.SiteName}出现错误：网站故障，请尽快处理!";
                XTrace.WriteLine(content);
                DingTalkRobot.OapiRobotText(content, new List<string>(), false);
            }
        }
        XTrace.WriteLine("执行成功");
    }

    /// <summary>停止服务</summary>
    /// <param name="reason"></param>
    protected override void StopWork(string reason)
    {
        _timer = null;
        _timer?.Dispose();

        _timer1 = null;
        _timer1?.Dispose();

        base.StopWork(reason);
    }

    public void ShowMachineInfo()
    {
        XTrace.WriteLine("FullPath:{0}", ".".GetFullPath());
        XTrace.WriteLine("BasePath:{0}", ".".GetBasePath());
        XTrace.WriteLine("TempPath:{0}", Path.GetTempPath());

        var mi = MachineInfo.Current ?? MachineInfo.RegisterAsync().Result;

        foreach (var pi in mi.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            XTrace.WriteLine("{0}:\t{1}", pi.Name, mi.GetValue(pi));
        }
    }
}