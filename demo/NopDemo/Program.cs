using Autofac.Extensions.DependencyInjection;

using DH.Core.Configuration;
using DH.Core.Domain;
using DH.Entity;
using DH.Web.Framework;
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

// 使用基类
builder.AddCube(builder.Configuration, builder.Environment);

var app = builder.Build();

// 使用基类
app.UseCube(builder.Configuration, builder.Environment);

app.Run();