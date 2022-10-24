namespace DH.Services.Localization
{
    /// <summary>
    /// 本地化管理器接口
    /// </summary>
    public partial interface ILocalizationService
    {
        /// <summary>
        /// 按语言标识符获取所有区域设置字符串资源
        /// </summary>
        /// <param name="languageId">语言标识符</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含区域设置字符串资源
        /// </returns>
        Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(int languageId);

        /// <summary>
        ///基于指定的ResourceKey属性获取资源字符串。
        /// </summary>
        /// <param name="resourceKey">表示ResourceKey的字符串.</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含表示请求的资源字符串的字符串.
        /// </returns>
        Task<string> GetResourceAsync(string resourceKey);

        /// <summary>
        /// 基于指定的ResourceKey属性获取资源字符串。
        /// </summary>
        /// <param name="resourceKey">表示ResourceKey的字符串.</param>
        /// <param name="languageId">语言标识符</param>
        /// <param name="logIfNotFound">一个值，指示如果未找到区域设置字符串资源，是否记录错误</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="returnEmptyIfNotFound">一个值，指示如果未找到资源且默认值设置为空字符串，是否返回空字符串</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含表示请求的资源字符串的字符串.
        /// </returns>
        Task<string> GetResourceAsync(string resourceKey, int languageId,
            bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false);
    }
}
