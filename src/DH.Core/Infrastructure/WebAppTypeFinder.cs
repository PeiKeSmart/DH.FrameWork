using System.Reflection;

namespace DH.Core.Infrastructure
{
    /// <summary>
    /// 提供有关当前web应用程序中类型的信息。（可选）此类可以查看bin文件夹中的所有程序集。
    /// </summary>
    public partial class WebAppTypeFinder : AppDomainTypeFinder
    {
        #region 字段

        private bool _binFolderAssembliesLoaded;

        #endregion

        #region 初始化

        public WebAppTypeFinder(IDHFileProvider fileProvider = null) : base(fileProvider)
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置是否应专门检查web应用程序的bin文件夹中的程序集是否在应用程序加载时加载。在应用程序重新加载后需要在AppDomain中加载插件的情况下，需要这样做。
        /// </summary>
        public bool EnsureBinFolderAssembliesLoaded { get; set; } = true;

        #endregion

        #region 方法

        /// <summary>
        /// 获取\Bin目录的物理磁盘路径
        /// </summary>
        /// <returns>物理路径。 例如： "c:\inetpub\wwwroot\bin"</returns>
        public virtual string GetBinDirectory()
        {
            return AppContext.BaseDirectory;
        }

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <returns>Result</returns>
        public override IList<Assembly> GetAssemblies()
        {
            if (!EnsureBinFolderAssembliesLoaded || _binFolderAssembliesLoaded)
                return base.GetAssemblies();

            _binFolderAssembliesLoaded = true;
            var binPath = GetBinDirectory();
            //binPath = _webHelper.MapPath("~/bin");
            LoadMatchingAssemblies(binPath);

            return base.GetAssemblies();
        }

        #endregion
    }
}
