using DH.Core.Infrastructure;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using Pek.Infrastructure;

namespace DH.Core.Configuration
{
    /// <summary>
    /// 表示应用程序设置帮助程序
    /// </summary>
    public partial class AppSettingsHelper
    {
        #region 字段

        private static Dictionary<string, int> _configurationOrder;

        #endregion

        #region 方法

        /// <summary>
        /// 使用传递的配置创建应用程序设置并将其保存到文件
        /// </summary>
        /// <param name="configurations">要保存的配置</param>
        /// <param name="fileProvider">文件提供程序</param>
        /// <param name="overwrite">是否覆盖appsettings文件</param>
        /// <returns>应用程序设置</returns>
        public static AppSettings SaveAppSettings(IList<IConfig> configurations, IDHFileProvider fileProvider, bool overwrite = true)
        {
            if (configurations is null)
                throw new ArgumentNullException(nameof(configurations));

            if (_configurationOrder is null)
                _configurationOrder = configurations.ToDictionary(config => config.Name, config => config.GetOrder());

            // 创建应用设置
            var appSettings = Singleton<AppSettings>.Instance ?? new AppSettings();
            appSettings.Update(configurations);
            Singleton<AppSettings>.Instance = appSettings;

            // 创建文件（如果不存在）
            var filePath = fileProvider.MapPath(DHConfigurationDefaults.AppSettingsFilePath);
            var fileExists = fileProvider.FileExists(filePath);
            fileProvider.CreateFile(filePath);

            // 获取原始配置参数
            var configuration = JsonConvert.DeserializeObject<AppSettings>(fileProvider.ReadAllText(filePath, Encoding.UTF8))
                ?.Configuration
                ?? new();
            foreach (var config in configurations)
            {
                configuration[config.Name] = JToken.FromObject(config);
            }

            // 按顺序对显示配置进行排序（例如，带0的数据配置将是第一个）
            appSettings.Configuration = configuration
                .SelectMany(outConfig => _configurationOrder.Where(inConfig => inConfig.Key == outConfig.Key).DefaultIfEmpty(),
                    (outConfig, inConfig) => new { OutConfig = outConfig, InConfig = inConfig })
                .OrderBy(config => config.InConfig.Value)
                .Select(config => config.OutConfig)
                .ToDictionary(config => config.Key, config => config.Value);

            // 将应用程序设置保存到文件
            if (!fileExists || overwrite)
            {
                var text = JsonConvert.SerializeObject(appSettings, Formatting.Indented);
                fileProvider.WriteAllText(filePath, text, Encoding.UTF8);
            }

            return appSettings;
        }

        #endregion
    }
}
