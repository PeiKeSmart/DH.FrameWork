using DH.Core.Infrastructure;
using DH.VirtualFileSystem;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;

using NewLife;

using System.Text;

namespace DH.AMap;

/// <summary>
/// 代表用于在应用程序启动时配置框架的对象
/// </summary>
public class DHStartup : IDHStartup {
    /// <summary>
    /// 添加并配置任何中间件
    /// </summary>
    /// <param name="services">服务描述符集合</param>
    /// <param name="configuration">应用程序的配置</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        
    }

    /// <summary>
    /// 配置添加的中间件的使用
    /// </summary>
    /// <param name="application">用于配置应用程序的请求管道的生成器</param>
    public void Configure(IApplicationBuilder application)
    {

    }

    /// <summary>
    /// 配置虚拟文件系统
    /// </summary>
    /// <param name="options">虚拟文件配置</param>
    public void ConfigureVirtualFileSystem(DHVirtualFileSystemOptions options)
    {
    }

    /// <summary>
    /// 注册路由
    /// </summary>
    /// <param name="endpoints">路由生成器</param>
    public void UseDHEndpoints(IEndpointRouteBuilder endpoints)
    {
        // 高德密钥反向代码
        endpoints.Map("/_AMapService/v4/map/styles", async context =>
        {
            await ProxyRequest(context, "https://webapi.amap.com/v4/map/styles", "/_AMapService/");
        });

        endpoints.Map("/_AMapService/v3/vectormap", async context =>
        {
            await ProxyRequest(context, "https://fmap01.amap.com/v3/vectormap", "/_AMapService/");
        });

        endpoints.Map("/_AMapService/{**path}", async context =>
        {
            await ProxyRequest(context, "https://restapi.amap.com/", "/_AMapService/");
        });
    }

    private async Task ProxyRequest(HttpContext context, string targetUrl, String replaceUrl)
    {
        var queryString = context.Request.QueryString;
        var newQueryString = queryString.Add("jscode", AMapSetting.Current.AMapSecret ?? String.Empty);

        var targetUrlWithQueryString = targetUrl + context.Request.Path.Value?.Replace(replaceUrl, "") + newQueryString;

        using (var client = new HttpClient())
        {
            var method = context.Request.Method;

            if (method.EqualIgnoreCase("GET"))
            {
                var response = await client.GetAsync(targetUrlWithQueryString);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    context.Response.ContentType = response.Content.Headers.ContentType?.ToString();
                    await context.Response.WriteAsync(content);
                }
                else
                {
                    context.Response.StatusCode = (int)response.StatusCode;
                }
            }
            else if (method.EqualIgnoreCase("POST"))
            {
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                {
                    var requestBody = await reader.ReadToEndAsync();

                    // 使用 HttpClient 发送 POST 请求
                    using (var httpClient = new HttpClient())
                    {
                        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync(targetUrlWithQueryString, content);

                        if (response.IsSuccessStatusCode)
                        {
                            var content1 = await response.Content.ReadAsStringAsync();
                            context.Response.ContentType = response.Content.Headers.ContentType?.ToString();
                            await context.Response.WriteAsync(content1);
                        }
                        else
                        {
                            context.Response.StatusCode = (int)response.StatusCode;
                        }
                    }
                }
            }
            else
            {

            }
        }
    }

    /// <summary>
    /// 将区域路由写入数据库
    /// </summary>
    public void ConfigureArea()
    {

    }

    /// <summary>
    /// 调整菜单
    /// </summary>
    public void ChangeMenu()
    {

    }

    /// <summary>
    /// 升级处理逻辑
    /// </summary>
    public void Update()
    {

    }

    /// <summary>
    /// 获取此启动配置实现的顺序
    /// </summary>
    public int Order => 300;
}
