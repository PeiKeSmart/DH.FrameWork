using XCode;

namespace DH.Services.Common
{
    /// <summary>
    /// 通用属性服务接口
    /// </summary>
    public partial interface IGenericAttributeService
    {
        /// <summary>
        /// 获取实体的属性
        /// </summary>
        /// <typeparam name="TPropType">属性类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="key">键</param>
        /// <param name="storeId">加载特定于某个站点的值；传递0以加载为所有站点共享的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>
        /// 任务结果包含属性
        /// </returns>
        TPropType GetAttribute<TPropType>(EntityBase entity, string key, int storeId = 0, TPropType defaultValue = default);

        /// <summary>
        /// 保存属性值
        /// </summary>
        /// <typeparam name="TPropType">属性类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="storeId">加载特定于某个站点的值；传递0以加载为所有站点共享的值</param>
        void SaveAttribute<TPropType>(EntityBase entity, string key, TPropType value, int storeId = 0);
    }
}
