using DH.Core.Infrastructure;
using DH.Entity;

using NewLife;
using NewLife.Reflection;

using System.Reflection;

namespace DH.Jobs;

/// <summary>
/// 调度器
/// </summary>
public class DHSchedulers {
    /// <summary>
    /// 扫描并添加作业
    /// </summary>
    /// <param name="assemblies">程序集列表</param>
    public async Task ScanJobsAsync(params Assembly[] assemblies)
    {
        List<IDHJob> jobs = [];
        if (assemblies != null && assemblies.Length > 0)
        {
            jobs = Helpers.Reflection.GetInstancesByInterface<IDHJob>(assemblies);
        }
        else
        {
            var _finder = Singleton<ITypeFinder>.Instance;

            jobs = _finder.FindClassesOfType<IDHJob>().Select(e => e.CreateInstance() as IDHJob).ToList();
        }

        if (jobs == null)
            return;

        foreach (var job in jobs)
        {
            if (job is not DHJobBase DhJob)
                throw new InvalidOperationException("DH.Job.DHJobBase派生");

            var Name = DhJob.GetJobName();
            if (Name.IsNullOrWhiteSpace())
            {
                throw new InvalidOperationException("作业名称为空");
            }

            var cron = DhJob.GetCron();
            if (cron.IsNullOrWhiteSpace())
            {
                throw new InvalidOperationException("Cron表达工为空");
            }

            var model = CronJob.FindByName(Name);
            if (model != null)
            {
                throw new InvalidOperationException("已存在指定名称的作业");
            }

            var executeMethod = job?.GetType().GetMethod("Execute");

            model = new CronJob();
            model.Name = Name;
            model.Cron = cron;
            model.DisplayName = executeMethod.GetDisplayName();
            model.Method = $"{executeMethod?.DeclaringType?.FullName}.{executeMethod?.Name}";
            model.Enable = true;
            model.Remark = executeMethod.GetDescription();
            model.Insert();

            await Task.CompletedTask;
        }
    }
}