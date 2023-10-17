using DH.Core.Configuration;

namespace DH.Services.Configuration
{
    /// <summary>
    /// 设置服务接口
    /// </summary>
    public partial interface ISettingService
    {
        /// <summary>
        /// 加载设置
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="storeId">应加载设置的站点标识符</param>
        /// <returns></returns>
        T LoadSetting<T>(int storeId = 0) where T : ISettings, new();

        /// <summary>
        /// 加载设置
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="storeId">应加载设置的站点标识符</param>
        /// <returns></returns>
        ISettings LoadSetting(Type type, int storeId = 0);
    }
}
