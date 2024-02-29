﻿using DH.Entity;
using DH.Services.Jobs;

using Microsoft.Extensions.DependencyInjection;

using NewLife;
using NewLife.Caching;
using NewLife.Log;
using NewLife.Reflection;
using NewLife.Threading;

using System.Diagnostics;
using System.Reflection;

using IHostedService = Microsoft.Extensions.Hosting.IHostedService;

namespace DH.Services.Services;

/// <summary>作业扩展</summary>
public static class JobServiceExtersions {
    /// <summary>启用魔方CronJob</summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCubeJob(this IServiceCollection services)
    {
        // 注册作业服务，这些作业可以使用DI
        services.AddSingleton<SqlService>();
        services.AddSingleton<HttpService>();

        // 传统建议定时作业，可以不用注册
        //services.AddSingleton<BackupDbService>();
        BackupDbService.Init();

        // 定时作业调度服务
        services.AddHostedService<JobService>();

        // 扫描并添加ICubeJob作业
        Task.Run(JobService.ScanJobs);

        return services;
    }
}

/// <summary>定时作业服务</summary>
public class JobService : IHostedService {
    #region 核心控制

    private static readonly IList<MyJob> _jobs = new List<MyJob>();
    private readonly IServiceProvider _serviceProvider;
    private readonly ITracer _tracer;

    /// <summary>实例化作业服务</summary>
    /// <param name="serviceProvider"></param>
    /// <param name="tracer"></param>
    public JobService(IServiceProvider serviceProvider, ITracer tracer)
    {
        _tracer = tracer;
        _serviceProvider = serviceProvider;
    }

    private static TimerX _timer;
    /// <summary>启动</summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        // 定时检测作业参数的变更，如果管理界面修改了作业参数，需要唤醒定时器马上检查
        _timer = new TimerX(DoJob, null, 1_000, 600_000) { Async = true };

        return Task.CompletedTask;
    }

    /// <summary>停止</summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.TryDispose();

        // 避免释放过程中集合被修改
        _jobs?.ToArray().TryDispose();
        _jobs.Clear();

        return Task.CompletedTask;
    }

    /// <summary>唤醒作业调度，作业配置有变更</summary>
    public static void Wake() => _timer.SetNext(1_000);

    /// <summary>唤醒具体作业</summary>
    /// <param name="jobId"></param>
    /// <param name="ms"></param>
    public static void Wake(Int32 jobId, Int32 ms)
    {
        var job = _jobs.FirstOrDefault(e => e.Job.Id == jobId);
        job?.Wake(ms);
    }

    private void DoJob(Object state)
    {
        var list = CronJob.FindAll();
        foreach (var item in list)
        {
            var job = _jobs.FirstOrDefault(e => e.Job.Id == item.Id);
            if (job == null)
            {
                // 将ICacheProvider 改为IServiceProvider注入，避免没有星尘注册导致的Job注入错误
                job = new MyJob
                {
                    Job = item,
                    CacheProvider = _serviceProvider.GetService<ICacheProvider>(),
                    ServiceProvider = _serviceProvider,
                    Tracer = _tracer
                };
                _jobs.Add(job);
            }
            job.Job = item;

            try
            {
                if (item.Enable)
                    job.Start();
                else
                    job.Stop();
            }
            catch (Exception ex)
            {
                WriteLog("控制", false, $"作业[{item.Name}/{item.DisplayName}]失败，{ex.Message}", item);

                item.Enable = false;
                item.Update();
            }
        }

        //// 如果没有作业，10分钟跑一次
        //_timer.Period = list.Any(e => e.Enable) ? 60_000 : 3600_000;
    }
    #endregion

    #region 辅助
    /// <summary>扫描并添加ICubeJob作业</summary>
    public static void ScanJobs()
    {
        var jobs = CronJob.FindAll();

        foreach (var type in typeof(ICubeJob).GetAllSubclasses())
        {
            var name = type.Name;
            var att = type.GetCustomAttribute<CronJobAttribute>();
            if (att != null) name = att.Name;

            // 查找或新增作业，仅首次新增是设置Cron，后续在管理界面修改
            var job = jobs.FirstOrDefault(e => e.Name.EqualIgnoreCase(name));
            job ??= new CronJob
            {
                Name = name,
                Cron = att?.Cron,
                Enable = true,
                Remark = type.GetDescription(),
            };

            job.DisplayName = type.GetDisplayName();
            job.Method = type.FullName;
            if (job.Remark.IsNullOrEmpty()) job.Remark = type.GetDescription();

            job.Save();
        }
    }

    internal static void WriteLog(String action, Boolean success, String remark, CronJob job)
    {
        job ??= new CronJob();
        job.WriteLog(action, success, remark);
    }
    #endregion
}

/// <summary>定时作业项</summary>
internal class MyJob : IDisposable {
    public CronJob Job { get; set; }

    public ICacheProvider CacheProvider { get; set; }

    public IServiceProvider ServiceProvider { get; set; }

    public ITracer Tracer { get; set; }

    private TimerX _timer;
    private String _id;
    private Type _type;
    private MethodInfo _method;
    private Action<String> _action;

    ~MyJob() => Dispose();

    public void Dispose() => Stop();

    public void Start()
    {
        var job = Job;

        // 参数检查
        var expession = job.Cron;
        if (expession.IsNullOrEmpty()) throw new ArgumentNullException(nameof(job.Cron));

        var cmd = job.Method;
        if (cmd.IsNullOrEmpty()) throw new ArgumentNullException(nameof(job.Method));

        // 标识相同，不要处理
        var id = $"{expession}@{cmd}";
        if (id == _id && _timer != null) return;

        var cron = new Cron();
        if (!cron.Parse(expession)) throw new InvalidOperationException($"无效表达式 {expession}");

        // 找到类和方法
        _type = cmd.GetTypeEx();
        if (_type == null || !_type.As<ICubeJob>())
        {
            var p = cmd.LastIndexOf('.');
            if (p <= 0) throw new InvalidOperationException($"无效作业方法 {cmd}");

            _type = cmd[..p].GetTypeEx();
            _method = _type?.GetMethodEx(cmd[(p + 1)..]);
            if (_method == null) throw new InvalidOperationException($"无效作业方法 {cmd}");

            if (_method.IsStatic)
            {
                _action = _method.As<Action<String>>();
                if (_action == null) throw new InvalidOperationException($"无效作业方法 {cmd}");
            }
        }

        JobService.WriteLog("启用", true, $"作业[{job.Name}]，定时 {job.Cron}，方法 {job.Method}", job);

        // 实例化定时器，原定时器销毁
        _timer.TryDispose();
        _timer = new TimerX(DoJobWork, this, expession) { Async = true, Tracer = Tracer };

        job.NextTime = _timer.NextTime;
        job.Update();

        _id = id;
    }

    public void Stop()
    {
        if (_timer != null)
        {
            //using var span = Tracer?.NewSpan($"job:{Job}:Stop");

            JobService.WriteLog("停用", true, $"作业[{Job.Name}]", Job);

            _timer.TryDispose();
            _timer = null;
        }

        _id = null;
    }

    public void Wake(Int32 ms) => _timer?.SetNext(ms);

    private Boolean CheckRunning(CronJob job)
    {
        // 检查分布式锁，避免多节点重复执行
        var key = $"Job:{job.Id}";
        if (CacheProvider != null && !CacheProvider.Cache.Add(key, job.Name, 5)) return false;

        // 有时候可能并没有配置Redis，借助数据库事务实现去重，需要20230804版本的XCode
        using var tran = CronJob.Meta.CreateTrans();

        // 如果短时间内重复执行，跳过
        var job2 = CronJob.FindByKey(job.Id);
        if (job2 != null && job2.LastTime.AddSeconds(5) > DateTime.Now) return false;

        job2.LastTime = DateTime.Now;
        job2.Update();

        tran.Commit();

        return true;
    }

    private async void DoJobWork(Object state)
    {
        var job = Job;

        // 检查分布式锁，避免多节点重复执行
        if (!CheckRunning(job))
        {
            var set = DHSetting.Current;
            if (set.Debug)
                JobService.WriteLog(job.Name, false, "分布式锁检查失败，跳过执行", job);
            return;
        }

        job.LastTime = DateTime.Now;

        using var span = Tracer?.NewSpan($"job:{job}", job);
        var sw = Stopwatch.StartNew();
        var message = "";
        var success = true;
        try
        {
            if (_method != null && _method.IsStatic)
            {
                _action?.Invoke(job.Argument);
            }
            else
            {
                // 新功能IServiceProvider.CreateInstance可以在第二位创建对象，定时任务类就不需要注册到容器里面了
                var instance = ServiceProvider?.GetService(_type);
                instance ??= NewLife.Model.ModelExtension.CreateInstance(ServiceProvider, _type);
                instance ??= _type?.CreateInstance();
                if (instance is ICubeJob cubeJob)
                {
                    if (instance is CubeJobBase cubeJob2) cubeJob2.Job = job;

                    message = await cubeJob.Execute(job.Argument);
                }
                else
                {
                    _method?.Invoke(instance, new Object[] { job.Argument });
                }
            }
        }
        catch (Exception ex)
        {
            span?.SetError(ex, null);
            XTrace.WriteException(ex);

            success = false;
            message = ex.ToString();
        }
        sw.Stop();
        message += $" 耗时 {sw.Elapsed}";

        job.WriteLog(job.Name, success, message);

        job.NextTime = _timer.Cron.GetNext(_timer.NextTime);
        job.Update();
    }
}