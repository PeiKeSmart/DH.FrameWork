﻿using DH.VirtualFileSystem;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DH.Core.Infrastructure;

/// <summary>
/// 表示应用程序启动时配置服务和中间件的对象
/// </summary>
public partial interface IDHStartup
{
    /// <summary>
    /// 添加并配置任何中间件
    /// </summary>
    /// <param name="services">服务描述符集合</param>
    /// <param name="configuration">应用程序的配置</param>
    /// <param name="startups">查找到的IDHStartup集合</param>
    void ConfigureServices(IServiceCollection services, IConfiguration configuration, IEnumerable<IDHStartup> startups, IWebHostEnvironment webHostEnvironment);

    /// <summary>
    /// 配置添加的中间件的使用
    /// </summary>
    /// <param name="application">用于配置应用程序的请求管道的生成器</param>
    /// <param name="typeFinder">类型处理器</param>
    void Configure(IApplicationBuilder application, ITypeFinder typeFinder);

    /// <summary>
    /// 配置虚拟文件系统
    /// </summary>
    /// <param name="options">虚拟文件配置</param>
    void ConfigureVirtualFileSystem(DHVirtualFileSystemOptions options);

    /// <summary>
    /// 将区域路由写入数据库
    /// </summary>
    void ConfigureArea();

    /// <summary>
    /// 调整菜单
    /// </summary>
    void ChangeMenu();

    /// <summary>
    /// 升级处理逻辑
    /// </summary>
    void Update();

    /// <summary>
    /// 获取此启动配置实现的顺序
    /// </summary>
    int Order { get; }
}
