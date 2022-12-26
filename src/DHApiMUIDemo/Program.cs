using DH;
using DH.Api.MUI;
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

DHSetting.Current.IsApiItem = true;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins()
                .WithHeaders()
                .WithMethods();
        });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "����", Version = "v1" });
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "�û�", Version = "v1" });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "DHApiMUIDemo.xml"), true);
});

// ʹ�û���
builder.AddCube(builder.Configuration, builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

//app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseFytApiUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "����", "v1");
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "�û�", "v2");
});
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

// ʹ�û���
app.UseCube(builder.Configuration, builder.Environment);


app.Run();