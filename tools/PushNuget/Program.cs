using NewLife;
using NewLife.Log;
using NewLife.Threading;

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

namespace PushNuget;

internal class Program
{
    private static FileInfo[]? Infos; //要上传的文件

    private static List<FileInfo>? PushFile; //实际要上传的文件

    private static ConcurrentDictionary<FileInfo, DateTime> PushList = new ConcurrentDictionary<FileInfo, DateTime>();  // 上传的文件列表

    private static int _count = 0;  // 处理过的文件数量

    private static int _filescount; // 所有文件数量

    private static int jishi = 60;

    private static bool isjishi = false;

    /// <summary>
    /// 全局定时器
    /// </summary>
    public static TimerX? GlobalTimer { get; private set; }

    /// <summary>
    /// 全局定时器
    /// </summary>
    public static TimerX? GlobalTimer1 { get; private set; }

    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        XTrace.UseConsole();
        GlobalTimer = new TimerX(GlobalScheduledTasks, null, 1000, 1000);
        GlobalTimer1 = new TimerX(GlobalScheduledTasks1, null, 1000, 1000);

        Console.Title = args.Length > 0 ? args[0] : @"上传到Nuget";

        var fileInfos1 = "../".AsDirectory().GetAllFiles("*.snupkg");
        foreach (var item in fileInfos1)
        {
            item.Delete();
        }

        var fileInfos = "../".AsDirectory().GetAllFiles("*.nupkg");  // 获取所有的nupkg文件
        Infos = fileInfos as FileInfo[] ?? fileInfos.ToArray();
        PushFile = Infos.ToList();
        if (!Infos.Any())
        {
            Console.WriteLine(@"没有发现要上传的NuGet文件");
            isjishi = true;
            Close(false);
            return;
        }

        _filescount = Infos.Count();

        foreach (var item in Infos)
        {
            if (item.Name.Contains(".symbols.nupkg"))
            {
                item.Delete();
                PushFile.Remove(item);
                _filescount--;
                continue;
            }
        }
        Thread.Sleep(1000);

        Push();

        Console.ReadKey();
    }

    protected static void Push()
    {
        try
        {
            PushList.TryAdd(PushFile![_count], DateTime.Now);
            "cmd".Run($"/k dotnet nuget push -s {Setting.Current.Source} -k {Setting.Current.Key} ../{PushFile[_count].Name} --skip-duplicate", 10000, WriteLog);
        }
        catch (Exception ex)
        {
            XTrace.WriteException(ex);
        }
    }

    protected static void WriteLog(string? msg)
    {
        if (msg?.IndexOf("error: ") > -1 || msg?.IndexOf("已推送包") > -1 || msg?.IndexOf("已存在包") > -1 || msg?.IndexOf("was pushed.") > -1)
        {
            Interlocked.Increment(ref _count);

            XTrace.WriteLine(msg);
            if (_count != _filescount)
            {
                Push();
            }
            else
            {
                Console.WriteLine($@"已上传文件数：{_count},总文件数：{_filescount}");

                if (_count == _filescount)
                {
                    GlobalTimer1?.Dispose();
                    isjishi = true;
                    Close(true);
                }
                else
                {
                    Console.WriteLine($@"上传的文件数和实际文件数不一致，请检查后重新执行工具。");
                }
            }
            return;
        }
        XTrace.WriteLine(msg!);
    }

    protected static void Close(bool existFile)
    {
        Console.WriteLine("60秒后即将关闭");
        Thread.Sleep(60_000);

        if (existFile)
        {
            foreach (var row in PushList)
            {
                Console.WriteLine($@"删除 {row.Key.Name}");
                row.Key.Delete();
            }
        }

        // 使用代码退出，状态代码为0
        Environment.Exit(0);

        //var process = Process.GetProcessesByName("PushNuget");
        //foreach (var p in process)
        //{
        //    if (!p.CloseMainWindow())
        //    {
        //        p.Kill();
        //    }
        //}
    }

    private static void GlobalScheduledTasks(object? state)
    {
        if (isjishi)
        {
            XTrace.WriteLine(jishi.ToString());
            jishi--;
        }
    }

    private static void GlobalScheduledTasks1(object? state)
    {
        foreach (var row in PushList.ToArray())
        {
            if (row.Value.AddMinutes(2) < DateTime.Now)
            {
                row.Key.Delete();
                PushList.TryRemove(row.Key, out _);
            }
        }
    }
}