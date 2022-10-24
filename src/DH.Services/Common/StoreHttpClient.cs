using DH.Core;

namespace DH.Services.Common
{
    /// <summary>
    /// 表示请求当前存储的HTTP客户端
    /// </summary>
    public partial class StoreHttpClient
    {
        #region Fields

        private readonly HttpClient _httpClient;

        #endregion

        #region Ctor

        public StoreHttpClient(HttpClient client,
            IWebHelper webHelper)
        {
            // 配置客户端
            client.BaseAddress = new Uri(webHelper.GetStoreLocation());

            _httpClient = client;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 保持当前存储站点的活动状态
        /// </summary>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含异步任务，其结果确定请求已完成
        /// </returns>
        public virtual async Task KeepAliveAsync()
        {
            await _httpClient.GetStringAsync(DHCommonDefaults.KeepAlivePath);
        }

        #endregion
    }
}
