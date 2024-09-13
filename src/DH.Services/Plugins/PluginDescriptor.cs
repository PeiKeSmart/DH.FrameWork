using System.Reflection;
using System.Text;

using DH.Core.Infrastructure;

using Newtonsoft.Json;

using Pek;

namespace DH.Services.Plugins
{
    /// <summary>
    /// 表示插件描述符
    /// </summary>
    public partial class PluginDescriptor : PluginDescriptorBaseInfo, IDescriptor, IComparable<PluginDescriptor>
    {
        #region 初始化

        public PluginDescriptor()
        {
            SupportedVersions = new List<string>();
            LimitedToStores = new List<int>();
            LimitedToCustomerRoles = new List<int>();
            DependsOn = new List<string>();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 从描述文本获取插件描述符
        /// </summary>
        /// <param name="text">说明文本</param>
        /// <returns>插件描述符</returns>
        public static PluginDescriptor GetPluginDescriptorFromText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new PluginDescriptor();

            // 从JSON文件获取插件描述符
            var descriptor = JsonConvert.DeserializeObject<PluginDescriptor>(text);

            if (!descriptor.SupportedVersions.Any())
                descriptor.SupportedVersions.Add("2.00");

            return descriptor;
        }

        /// <summary>
        /// 获取插件的实例
        /// </summary>
        /// <typeparam name="TPlugin">插件类型</typeparam>
        /// <returns>插件实例</returns>
        public virtual TPlugin Instance<TPlugin>() where TPlugin : class, IPlugin
        {
            // 尝试将插件解析为未注册的服务
            var instance = EngineContext.Current.ResolveUnregistered(PluginType);

            // 尝试获取类型化实例
            var typedInstance = instance as TPlugin;
            if (typedInstance != null)
                typedInstance.PluginDescriptor = this;

            return typedInstance;
        }

        /// <summary>
        /// 将此实例与指定的PluginDescriptor对象进行比较
        /// </summary>
        /// <param name="other">要与此实例比较的PluginDescriptor</param>
        /// <returns>一个整数，指示此实例是在指定参数的排序顺序中的前面、后面还是出现在同一位置</returns>
        public int CompareTo(PluginDescriptor other)
        {
            if (DisplayOrder != other.DisplayOrder)
                return DisplayOrder.CompareTo(other.DisplayOrder);

            return string.Compare(SystemName, other.SystemName, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// 以字符串形式返回插件
        /// </summary>
        /// <returns>FriendlyName的值</returns>
        public override string ToString()
        {
            return FriendlyName;
        }

        /// <summary>
        /// 将插件描述符保存到插件描述文件
        /// </summary>
        public virtual void Save()
        {
            //由于插件是在使用默认提供程序进行IoC初始化之前加载的，为了避免可能出现的问题，我们使用CommonHelper.DefaultFileProvider而不是主文件提供程序
            var fileProvider = CommonHelper.DefaultFileProvider;

            // 获取描述文件路径
            if (OriginalAssemblyFile == null)
                throw new Exception($"Cannot load original assembly path for {SystemName} plugin.");

            var filePath = fileProvider.Combine(fileProvider.GetDirectoryName(OriginalAssemblyFile), DHPluginDefaults.DescriptionFileName);
            if (!fileProvider.FileExists(filePath))
                throw new Exception($"Description file for {SystemName} plugin does not exist. {filePath}");

            // 保存文件
            var text = JsonConvert.SerializeObject(this, Formatting.Indented);
            fileProvider.WriteAllText(filePath, text, Encoding.UTF8);
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取或设置插件组
        /// </summary>
        [JsonProperty(PropertyName = "Group")]
        public virtual string Group { get; set; }

        /// <summary>
        /// 获取或设置插件友好名称
        /// </summary>
        [JsonProperty(PropertyName = "FriendlyName")]
        public virtual string FriendlyName { get; set; }

        /// <summary>
        /// 获取或设置支持的版本
        /// </summary>
        [JsonProperty(PropertyName = "SupportedVersions")]
        public virtual IList<string> SupportedVersions { get; set; }

        /// <summary>
        /// 获取或设置作者
        /// </summary>
        [JsonProperty(PropertyName = "Author")]
        public virtual string Author { get; set; }

        /// <summary>
        /// 获取或设置显示顺序
        /// </summary>
        [JsonProperty(PropertyName = "DisplayOrder")]
        public virtual int DisplayOrder { get; set; }

        /// <summary>
        /// 获取或设置程序集文件的名称
        /// </summary>
        [JsonProperty(PropertyName = "FileName")]
        public virtual string AssemblyFileName { get; set; }

        /// <summary>
        /// 获取或设置描述
        /// </summary>
        [JsonProperty(PropertyName = "Description")]
        public virtual string Description { get; set; }

        /// <summary>
        /// 获取或设置此插件可用的存储标识符列表。如果为空，则此插件在所有商店中都可用
        /// </summary>
        [JsonProperty(PropertyName = "LimitedToStores")]
        public virtual IList<int> LimitedToStores { get; set; }

        /// <summary>
        /// 获取或设置此插件可用的客户角色标识符列表。如果为空，则此插件可用于所有插件。
        /// </summary>
        [JsonProperty(PropertyName = "LimitedToCustomerRoles")]
        public virtual IList<int> LimitedToCustomerRoles { get; set; }

        /// <summary>
        /// 获取或设置此插件所依赖的插件系统名称列表
        /// </summary>
        [JsonProperty(PropertyName = "DependsOnSystemNames")]
        public virtual IList<string> DependsOn { get; set; }

        /// <summary>
        /// 获取或设置指示是否安装插件的值
        /// </summary>
        [JsonIgnore]
        public virtual bool Installed { get; set; }

        /// <summary>
        /// 获取或设置插件类型
        /// </summary>
        [JsonIgnore]
        public virtual Type PluginType { get; set; }

        /// <summary>
        /// 获取或设置原始程序集文件
        /// </summary>
        [JsonIgnore]
        public virtual string OriginalAssemblyFile { get; set; }

        /// <summary>
        /// 获取或设置插件目录中所有库文件的列表
        /// </summary>
        [JsonIgnore]
        public virtual IList<string> PluginFiles { get; set; }

        /// <summary>
        /// 获取或设置应用程序中处于活动状态的程序集
        /// </summary>
        [JsonIgnore]
        public virtual Assembly ReferencedAssembly { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否需要在插件页上显示插件
        /// </summary>
        [JsonIgnore]
        public virtual bool ShowInPluginsList { get; set; } = true;

        #endregion
    }
}
