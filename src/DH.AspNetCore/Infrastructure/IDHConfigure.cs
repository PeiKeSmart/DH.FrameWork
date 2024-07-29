using Microsoft.AspNetCore.Builder;

namespace DH.AspNetCore.Infrastructure;

public interface IDHConfigure {
    /// <summary>
    /// 获取此启动配置实现的顺序
    /// </summary>
    int Order { get; }

    /// <summary>
    /// 配置使用添加的中间件
    /// </summary>
    /// <param name="application">用于配置应用程序的请求管道的生成器</param>
    void ConfigureMiddleware(IApplicationBuilder application);

    /// <summary>
    /// UseRouting前执行的数据
    /// </summary>
    /// <param name="application"></param>
    void BeforeRouting(IApplicationBuilder application);

    /// <summary>
    /// UseAuthentication或者UseAuthorization后面 Endpoints前执行的数据
    /// </summary>
    /// <param name="application"></param>
    void AfterAuth(IApplicationBuilder application);
}
