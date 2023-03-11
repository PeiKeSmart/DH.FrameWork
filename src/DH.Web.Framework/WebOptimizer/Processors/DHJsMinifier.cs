using DH.Core;
using DH.Core.Infrastructure;

using NewLife.Log;

using NUglify;
using NUglify.JavaScript;

using WebOptimizer;

using XCode.Membership;

namespace DH.Web.Framework.WebOptimizer.Processors;

/// <summary>
/// 表示处理javascript资产的处理器类
/// </summary>
/// <remarks>已从WebOptimizer实施添加日志记录</remarks>
public partial class DHJsMinifier : Processor {
    #region Methods

    /// <summary>
    /// 在指定的配置上执行处理器。
    /// </summary>
    /// <param name="context">用于对WebOptimizer.IAsset实例执行处理的上下文</param>
    public override async Task ExecuteAsync(IAssetContext context) {
        var content = new Dictionary<string, byte[]>();

        foreach (var key in context.Content.Keys) {
            if (key.EndsWith(".min")) {
                content[key] = context.Content[key];
                continue;
            }

            var input = context.Content[key].AsString();
            var result = Uglify.Js(input, new CodeSettings { TermSemicolons = true });

            var minified = result.Code;

            if (result.HasErrors) {
                var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                var workContext = EngineContext.Current.Resolve<IWorkContext>();

                // 获取当前客户
                var currentCustomer = workContext.GetCurrentCustomer();

                var message = $"JavaScript minification: {key}" + new Exception(string.Join(Environment.NewLine, result.Errors));
                XTrace.Log.Error(message);
                LogProvider.Provider?.WriteLog("系统", "错误", false, message, currentCustomer.User.ID, currentCustomer.User.Name, webHelper.GetCurrentIpAddress());
            }

            content[key] = minified.AsByteArray();
        }

        context.Content = content;

        await Task.CompletedTask;
    }

    #endregion

}
