using DH.Core.Configuration;

using System.Text.Json.Serialization;

using WebOptimizer;

namespace DH.Web.Framework.WebOptimizer
{
    public partial class WebOptimizerConfig : WebOptimizerOptions, IConfig
    {
        #region Ctor

        public WebOptimizerConfig()
        {
            EnableDiskCache = true;
            EnableTagHelperBundling = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 指示是否启用JS文件绑定和缩小的值
        /// </summary>
        public bool EnableJavaScriptBundling { get; private set; } = true;

        /// <summary>
        /// 指示是否启用CSS文件绑定和缩小的值
        /// </summary>
        public bool EnableCssBundling { get; private set; } = true;

        /// <summary>
        /// 获取或设置生成的包的js文件名的后缀
        /// </summary>
        public string JavaScriptBundleSuffix { get; private set; } = ".scripts";

        /// <summary>
        /// 获取或设置生成的包的css文件名的后缀
        /// </summary>
        public string CssBundleSuffix { get; private set; } = ".styles";

        /// <summary>
        /// 获取要加载配置的节名称
        /// </summary>
        [JsonIgnore]
        public string Name => "WebOptimizer";

        /// <summary>
        /// 获取配置顺序
        /// </summary>
        /// <returns>Order</returns>
        public int GetOrder() => 2;

        #endregion
    }
}
