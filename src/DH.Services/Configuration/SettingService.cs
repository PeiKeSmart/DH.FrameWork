using DH.Core;
using DH.Core.Configuration;
using DH.Entity;

using NewLife.Caching;
using NewLife.Log;
using NewLife.Serialization;

using System.ComponentModel;

namespace DH.Services.Configuration
{
    /// <summary>
    /// 设置管理器
    /// </summary>
    public partial class SettingService : ISettingService
    {
        private readonly ICache _cache;

        public SettingService(ICache cache)
        {
            _cache = cache;
        }


        /// <summary>
        /// 获取所有设置
        /// </summary>
        /// <returns>
        /// 任务结果包含设置
        /// </returns>
        protected virtual IDictionary<string, IList<Setting>> GetAllSettingsDictionary()
        {
            return _cache.GetOrAdd<IDictionary<string, IList<Setting>>>(DHSettingsDefaults.SettingsAllAsDictionaryCacheKey.ToString(), e =>
            {
                var settings = GetAllSettings();

                var dictionary = new Dictionary<string, IList<Setting>>();
                foreach (var s in settings)
                {
                    var resourceName = s.Name.ToLowerInvariant();
                    var settingForCaching = new Setting
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Value = s.Value,
                        StoreId = s.StoreId
                    };
                    if (!dictionary.ContainsKey(resourceName))
                        // 第一次设置
                        dictionary.Add(resourceName, new List<Setting>
                        {
                            settingForCaching
                        });
                    else
                        // 已添加
                        // 很可能是相同名称的设置，但对于某些站点（storeId>0）
                        dictionary[resourceName].Add(settingForCaching);
                }

                return dictionary;
            });
        }

        /// <summary>
        /// 获取所有设置
        /// </summary>
        /// <returns>
        /// 任务结果包含设置
        /// </returns>
        public virtual IEnumerable<Setting> GetAllSettings()
        {
            var settings = Setting.GetAllSettings();

            return settings;
        }

        /// <summary>
        /// 按键获取设置值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="storeId">站点标识符</param>
        /// <param name="loadSharedValueIfNotFound">一个值，指示如果找不到特定于某个值的值，是否应加载共享（针对所有存储）值</param>
        /// <returns>
        /// 任务结果包含设置值
        /// </returns>
        public virtual T GetSettingByKey<T>(string key, T defaultValue = default,
            int storeId = 0, bool loadSharedValueIfNotFound = false)
        {
            if (string.IsNullOrEmpty(key))
                return defaultValue;

            var settings = GetAllSettingsDictionary();
            key = key.Trim().ToLowerInvariant();
            if (!settings.ContainsKey(key))
                return defaultValue;

            var settingsByKey = settings[key];
            var setting = settingsByKey.FirstOrDefault(x => x.StoreId == storeId);

            // 负载共享值？
            if (setting == null && storeId > 0 && loadSharedValueIfNotFound)
                setting = settingsByKey.FirstOrDefault(x => x.StoreId == 0);

            return setting != null ? CommonHelper.To<T>(setting.Value) : defaultValue;
        }

        /// <summary>
        /// 加载设置
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="storeId">应加载设置的站点标识符</param>
        public virtual T LoadSetting<T>(int storeId = 0) where T : ISettings, new()
        {
            return (T)LoadSetting(typeof(T), storeId);
        }

        /// <summary>
        /// 加载设置
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="storeId">应加载设置的站点标识符</param>
        /// <returns></returns>
        public virtual ISettings LoadSetting(Type type, int storeId = 0)
        {
            var settings = Activator.CreateInstance(type);

            if (!DHSetting.Current.IsInstalled)
                return settings as ISettings;

            foreach (var prop in type.GetProperties())
            {
                // 获取可以读写的属性
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = type.Name + "." + prop.Name;
                // 按站点加载
                var setting = GetSettingByKey<string>(key, storeId: storeId, loadSharedValueIfNotFound: true);
                if (setting == null)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).IsValid(setting))
                    continue;

                var value = TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromInvariantString(setting);

                // set属性
                prop.SetValue(settings, value, null);
            }

            return settings as ISettings;
        }

    }
}
