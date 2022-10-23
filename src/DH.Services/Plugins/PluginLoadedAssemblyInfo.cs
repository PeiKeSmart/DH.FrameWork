namespace DH.Services.Plugins
{
    /// <summary>
    /// 表示有关插件加载的程序集的信息
    /// </summary>
    public partial class PluginLoadedAssemblyInfo
    {
        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="shortName">程序集简称</param>
        /// <param name="assemblyInMemory">程序集全名</param>
        public PluginLoadedAssemblyInfo(string shortName, string assemblyInMemory)
        {
            ShortName = shortName;
            References = new List<(string PluginName, string AssemblyName)>();
            AssemblyFullNameInMemory = assemblyInMemory;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取短程序集名称
        /// </summary>
        public string ShortName { get; }

        /// <summary>
        /// 获取加载到内存中的完整程序集名称
        /// </summary>
        public string AssemblyFullNameInMemory { get; }

        /// <summary>
        /// 获取所有提到的插件程序集对的列表
        /// </summary>
        public List<(string PluginName, string AssemblyName)> References { get; }

        /// <summary>
        /// 获取与加载的程序集版本冲突的插件列表
        /// </summary>
        public IList<(string PluginName, string AssemblyName)> Collisions =>
            References.Where(reference => !reference.AssemblyName.Equals(AssemblyFullNameInMemory, StringComparison.CurrentCultureIgnoreCase)).ToList();

        #endregion
    }
}
