using DH;
using DH.Core.Domain;
using DH.Entity;
using DH.Web.Framework;

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

// ʹ�û���
builder.AddCube(builder.Configuration, builder.Environment);

var app = builder.Build();

// ʹ�û���
app.UseCube(builder.Configuration, builder.Environment);

app.Run();