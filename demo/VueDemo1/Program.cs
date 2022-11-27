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
builder.Configuration.SetConfig();  // ����Settings�ļ����з����������ļ�

// ��Ӧ�ó�����ӷ������÷����ṩ��
builder.Services.ConfigureApplicationServices(builder);

var app = builder.Build();

app.UseDHVueUI();  // Vue·��

// ����Ӧ�ó���HTTP����ܵ�
app.ConfigureRequestPipeline();
app.StartEngine();

app.Run();