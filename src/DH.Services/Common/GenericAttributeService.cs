using DH.Entity;

using Pek;

using XCode;

namespace DH.Services.Common
{
    /// <summary>
    /// 获取实体的属性
    /// </summary>
    public partial class GenericAttributeService : IGenericAttributeService
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
        public virtual TPropType GetAttribute<TPropType>(EntityBase entity, string key, int storeId = 0, TPropType defaultValue = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var keyGroup = entity.GetType().Name;

            var props = GenericAttribute.FindByEntityIdAndKeyGroup(entity["Id"].ToInt(), keyGroup);

            // 这里的小黑客（仅用于单元测试）。对于这种情况，我们应该在单元测试中编写expect返回规则
            if (props == null)
                return defaultValue;

            props = props.Where(x => x.StoreId == storeId).ToList();
            if (!props.Any())
                return defaultValue;

            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); //should be culture invariant

            if (prop == null || string.IsNullOrEmpty(prop.Value))
                return defaultValue;

            return CommonHelper.To<TPropType>(prop.Value);
        }

        /// <summary>
        /// 保存属性值
        /// </summary>
        /// <typeparam name="TPropType">属性类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="storeId">加载特定于某个站点的值；传递0以加载为所有站点共享的值</param>
        public virtual void SaveAttribute<TPropType>(EntityBase entity, string key, TPropType value, int storeId = 0)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var keyGroup = entity.GetType().Name;

            var props = (GenericAttribute.FindByEntityIdAndKeyGroup(entity["Id"].ToInt(), keyGroup))
                .Where(x => x.StoreId == storeId)
                .ToList();
            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); // 应该是区域性不变的

            var valueStr = CommonHelper.To<string>(value);

            if (prop != null)
            {
                if (string.IsNullOrWhiteSpace(valueStr))
                    // 删除
                    prop.Delete();
                else
                {
                    // 更新
                    prop.Value = valueStr;
                    prop.Update();
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(valueStr))
                    return;

                // 插入
                prop = new GenericAttribute
                {
                    EntityId = entity["Id"].ToInt(),
                    Key = key,
                    KeyGroup = keyGroup,
                    Value = valueStr,
                    StoreId = storeId
                };

                prop.Insert();
            }
        }

    }
}
