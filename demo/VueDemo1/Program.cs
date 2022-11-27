using Autofac.Extensions.DependencyInjection;

using DH.Configs;
using DH.Core.Configuration;
using DH.Core.Domain;
using DH.Entity;
using DH.Web.Framework.Infrastructure.Extensions;

using NewLife.Log;

using Autofac.Extensions.DependencyInjection;

using DH.Core.Configuration;
using DH.Core.Domain;
using DH.Entity;
using DH.Web.Framework.Infrastructure.Extensions;

using NewLife.Log;
using VueCliMiddleware.PartUI;

var set = DHSetting.Current;
if (set.Debug)
{
    XTrace.UseConsole();
}

if (!set.IsInstalled)
{
    set.IsInstalled = true;

    Setting.SaveSetting(new StoreInformationSettings
    {
        DefaultStoreTheme = "DefaultClean",
    });

    set.Save();
}

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Configuration.AddJsonFile(DHConfigurationDefaults.AppSettingsFilePath, true, true);
if (!string.IsNullOrEmpty(builder.Environment?.EnvironmentName))
{
    var path = string.Format(DHConfigurationDefaults.AppSettingsEnvironmentFilePath, builder.Environment.EnvironmentName);
    builder.Configuration.AddJsonFile(path, true, true);
}
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.SetConfig();  // 可在Settings文件夹中放入多个配置文件

// 向应用程序添加服务并配置服务提供商
builder.Services.ConfigureApplicationServices(builder);

var app = builder.Build();

app.UseDHVueUI();  // Vue路径

// 配置应用程序HTTP请求管道
app.ConfigureRequestPipeline();
app.StartEngine();

app.Run();