using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using NewLife;
using NewLife.Log;

using Pek.Infrastructure;
using Pek.Swagger;

using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace DH.Swagger;

public static class NetProSwaggerServiceExtensions
{
    public static IServiceCollection AddNetProSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        if (!configuration.GetValue<bool>("SwaggerOption:Enabled", false))
        {
            XTrace.WriteLine($"DH Swagger 已关闭");
            return services;
        }
        else
            XTrace.WriteLine($"DH Swagger 已启用");

        services
            .Configure<OpenApiInfo>(configuration.GetSection("SwaggerOption"));

        var info = services.BuildServiceProvider().GetService<IOptionsMonitor<OpenApiInfo>>().CurrentValue;

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.DescribeAllParametersInCamelCase();//请求参数转小写
            c.OperationFilter<SwaggerFileUploadFilter>();//add file fifter component
            c.OperationFilter<SwaggerDefaultValueFilter>();//add webapi  default value of parameter
            c.OperationFilter<CustomerHeaderParameter>();//add default header
            c.OperationFilter<CustomerQueryParameter>();//add default query

            //batch find xml file of swagger
            var basePath = AppDomain.CurrentDomain.BaseDirectory;//get app root path
            List<string> xmlComments = GetXmlComments();
            xmlComments.ForEach(r =>
            {
                string filePath = Path.Combine(basePath, r);
                if (File.Exists(filePath))
                {
                    c.IncludeXmlComments(filePath, true); // 默认的第二个参数是false，这个是controller的注释，记得修改
                }
            });

            var typeFinder = Singleton<ITypeFinder>.Instance;
            var DHSwaggers = typeFinder.FindClassesOfType<IDHSwagger>();
            var list = DHSwaggers
            .Select(swagger => (IDHSwagger)Activator.CreateInstance(swagger));

            // 遍历出全部的版本，做文档信息展示
            typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
            {
                version = version.Replace('_', '.');

                var swagger = list.FirstOrDefault(r => r.ApiVersions.ToString().Replace('_', '.').EqualIgnoreCase(version));

                var infos = new OpenApiInfo
                {
                    Version = version,
                    Title = info.Title,
                    Description = info.Description + swagger?.Description,
                    //TermsOfService = "None",
                    Contact = info.Contact,// new OpenApiContact { Email = "Email", Name = "Name", Url = new Uri("http://www.github.com") },
                    License = info.License,//new OpenApiLicense { Url = new Uri("http://www.github.com"), Name = "LicenseName" },
                };

                c.SwaggerDoc(version, infos);
                c.OrderActionsBy(o => o.RelativePath);
            });

            c.IgnoreObsoleteActions();

            // Jwt Bearer 认证，必须是 oauth2
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                Name = "Authorization",//jwt默认的参数名称
                In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                Type = SecuritySchemeType.ApiKey
            });

            // 开启加权小锁
            c.OperationFilter<AddResponseHeadersFilter>();
            c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

            // 在header中添加token，传递到后台
            c.OperationFilter<SecurityRequirementsOperationFilter>();

            c.EnableAnnotations(); // 启用 Swagger 注释
        });
        services.AddSwaggerGenNewtonsoftSupport();

        return services;
    }

    /// <summary>
    /// 所有xml默认当作swagger文档注入swagger
    /// </summary>
    /// <returns></returns>
    private static List<string> GetXmlComments()
    {
        //var pattern = $"^{netProOption.ProjectPrefix}.*({netProOption.ProjectSuffix}|Domain)$";
        //List<string> assemblyNames = ReflectionHelper.GetAssemblyNames(pattern);
        List<string> assemblyNames = AppDomain.CurrentDomain.GetAssemblies().Select(s => s.GetName().Name).ToList();
        List<string> xmlFiles = new List<string>();
        assemblyNames.ForEach(r =>
        {
            string fileName = $"{r}.xml";
            xmlFiles.Add(fileName);
        });
        return xmlFiles;
    }
}

public static class NetProSwaggerMiddlewareExtensions
{
    public static IApplicationBuilder UseNetProSwagger(
     this IApplicationBuilder application)
    {
        var configuration = application.ApplicationServices.GetService(typeof(IConfiguration)) as IConfiguration;

        XTrace.WriteLine($"DH Swagger Middleware:{configuration.GetValue<bool>("SwaggerOption:Enabled", false)}");

        if (configuration.GetValue<bool>("SwaggerOption:Enabled", false))
        {
            var openApiInfo = configuration.GetSection("SwaggerOption").Get<OpenApiInfo>();

            application.UseSwagger(c =>
            {
                c.RouteTemplate = $"{configuration.GetValue<string>("SwaggerOption:DescEndpoint", "")}docs/" + "{documentName}/docs.json";//使中间件服务生成Swagger作为JSON端点
                //c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Info.Description = httpReq.Path);//请求过滤处理
                c.PreSerializeFilters.Add((doc, _) => { doc.Servers?.Clear(); }); // 清空Servers Url
            });

            application.UseSwaggerUI(c =>
            {
                c.DocExpansion(DocExpansion.List);
                c.EnableDeepLinking();
                c.EnableFilter();
                c.MaxDisplayedTags(5);
                c.ShowExtensions();
                c.EnableValidator();
                //c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Head);

                c.RoutePrefix = $"{configuration.GetValue("SwaggerOption:RoutePrefix", "swagger")}";//设置文档首页根路径

                //自定义右上角版本切换
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    version = version.Replace('_', '.');
                    c.SwaggerEndpoint($"/{configuration.GetValue<string>("SwaggerOption:DescEndpoint", "")}docs/{version}/docs.json", $"{version}");//此处配置要和UseSwagger的RouteTemplate匹配
                });
                //c.SwaggerEndpoint("https://petstore.swagger.io/v2/swagger.json", "petstore.swagger");//远程swagger示例   

                #region
                if (configuration.GetValue("SwaggerOption:Theme", 0) == 1)
                    c.IndexStream = () => typeof(NetProSwaggerMiddlewareExtensions).GetTypeInfo().Assembly.GetManifestResourceStream("DH.Swagger.IndexDark.html");
                else if (configuration.GetValue("SwaggerOption:Theme", 0) == 2)
                {
                    c.IndexStream = () => typeof(NetProSwaggerMiddlewareExtensions).GetTypeInfo().Assembly.GetManifestResourceStream("DH.Swagger.wwwroot.swagger.index.html");
                }
                else
                {
                    c.IndexStream = () => typeof(NetProSwaggerMiddlewareExtensions).GetTypeInfo().Assembly.GetManifestResourceStream("DH.Swagger.index.html");
                }
                #endregion
                
            });
        }

        return application;
    }
}

public class DGSwaggerMiddleware
{
    private readonly RequestDelegate _next;

    public DGSwaggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        await _next(context);
        return;
    }
}
