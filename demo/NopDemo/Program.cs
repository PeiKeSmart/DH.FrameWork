using Autofac.Extensions.DependencyInjection;

using DH.Core.Configuration;
using DH.Core.Domain;
using DH.Entity;
using DH.Web.Framework.Infrastructure.Extensions;
using DH.Webs;

using NewLife.Log;

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

// ��Ӧ�ó�����ӷ������÷����ṩ��
builder.Services.ConfigureApplicationServices(builder, builder.Environment);

var app = builder.Build();

// ���þ�̬Http�����ķ�����
app.UseStaticHttpContext();

// ����Ӧ�ó���HTTP����ܵ�
app.ConfigureRequestPipeline();
app.StartEngine();

app.Run();