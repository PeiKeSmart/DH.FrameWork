using DH.Exceptions;

using Newtonsoft.Json.Linq;

using System.Text.Json.Serialization;

namespace DH.Core.Configuration
{
    /// <summary>
    /// 表示应用程序设置
    /// </summary>
    public partial class AppSettings
    {
        #region 字段

        private readonly Dictionary<Type, IConfig> _configurations = new();

        #endregion

        #region 初始化

        public AppSettings(IList<IConfig> configurations = null)
        {
            _configurations = configurations
                ?.OrderBy(config => config.GetOrder())
                ?.ToDictionary(config => config.GetType(), config => config)
                ?? new Dictionary<Type, IConfig>();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置原始配置参数
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, JToken> Configuration { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 按类型获取配置参数
        /// </summary>
        /// <typeparam name="TConfig">配置类型</typeparam>
        /// <returns>配置参数</returns>
        public TConfig Get<TConfig>() where TConfig : class, IConfig
        {
            if (_configurations[typeof(TConfig)] is not TConfig config)
                throw new DHException($"未找到类型为'{typeof(TConfig)}'的配置");

            return config;
        }

        /// <summary>
        /// 更新应用设置
        /// </summary>
        /// <param name="configurations">要更新的配置</param>
        public void Update(IList<IConfig> configurations)
        {
            foreach (var config in configurations)
            {
                _configurations[config.GetType()] = config;
            }
        }

        #endregion
    }
}
