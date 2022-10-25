namespace DH.Services.Helpers
{
    /// <summary>
    /// UserAgent帮助接口
    /// </summary>
    public partial interface IUserAgentHelper
    {
        /// <summary>
        /// 获取一个值，该值指示该请求是否由搜索引擎（网络爬虫）发出
        /// </summary>
        /// <returns>结果</returns>
        bool IsSearchEngine();

        /// <summary>
        /// 获取一个值，指示该请求是否由移动设备发出
        /// </summary>
        /// <returns></returns>
        bool IsMobileDevice();
    }
}
