using DH.Services.Plugins;

using DH.Core;
using DH.Core.ComponentModel;
using DH.Core.Configuration;
using DH.Core.Infrastructure;
using DH.Data.Mapping;

using Microsoft.AspNetCore.Mvc.ApplicationParts;

using System.Reflection;

namespace DH.Web.Framework.Infrastructure.Extensions
{
    /// <summary>
    /// 表示应用程序部件管理器扩展
    /// </summary>
    public static partial class ApplicationPartManagerExtensions
    {
        #region 字段

        private static readonly IDHFileProvider _fileProvider;
        private static readonly List<string> _baseAppLibraries;
        private static readonly Dictionary<string, PluginLoadedAssemblyInfo> _loadedAssemblies = new();
        private static readonly ReaderWriterLockSlim _locker = new();

        #endregion

        #region 初始化

        static ApplicationPartManagerExtensions()
        {
            // 我们使用默认的文件提供程序，因为DI尚未初始化
            _fileProvider = CommonHelper.DefaultFileProvider;

            _baseAppLibraries = new List<string>();

            // 从 /bin/{version}/ 目录获取所有库
            _baseAppLibraries.AddRange(_fileProvider.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Select(fileName => _fileProvider.GetFileName(fileName)));

            // 从基本站点目录获取所有库
            if (!AppDomain.CurrentDomain.BaseDirectory.Equals(Environment.CurrentDirectory, StringComparison.InvariantCultureIgnoreCase))
            {
                _baseAppLibraries.AddRange(_fileProvider.GetFiles(Environment.CurrentDirectory, "*.dll")
                    .Select(fileName => _fileProvider.GetFileName(fileName)));
            }

            // 从refs目录获取所有库
            var refsPathName = _fileProvider.Combine(Environment.CurrentDirectory, DHPluginDefaults.RefsPathName);
            if (_fileProvider.DirectoryExists(refsPathName))
            {
                _baseAppLibraries.AddRange(_fileProvider.GetFiles(refsPathName, "*.dll")
                    .Select(fileName => _fileProvider.GetFileName(fileName)));
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取有关插件的信息的访问权限
        /// </summary>
        private static IPluginsInfo PluginsInfo
        {
            get => Singleton<IPluginsInfo>.Instance;
            set => Singleton<IPluginsInfo>.Instance = value;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 加载并注册程序集
        /// </summary>
        /// <param name="applicationPartManager">应用程序部件管理器</param>
        /// <param name="assemblyFile">程序集文件的路径</param>
        /// <param name="useUnsafeLoadAssembly">指示是否将程序集从上下文加载到加载中，绕过一些安全检查</param>
        /// <returns>程序集</returns>
        private static Assembly AddApplicationParts(ApplicationPartManager applicationPartManager, string assemblyFile, bool useUnsafeLoadAssembly)
        {
            // 尝试加载程序集
            Assembly assembly;

            try
            {
                assembly = Assembly.LoadFrom(assemblyFile);
            }
            catch (FileLoadException)
            {
                if (useUnsafeLoadAssembly)
                {
                    //如果应用程序已从web复制，则Windows会将其标记为web应用程序，即使它位于本地计算机上。您可以通过更改文件属性来更改该指定，也可以使用＜loadFromRemoteSources＞元素授予程序集完全信任。另外，您可以使用UnsafeLoadFrom方法加载操作系统标记为已从web加载的本地程序集。
                    //see http://go.microsoft.com/fwlink/?LinkId=155569 for more information.
                    assembly = Assembly.UnsafeLoadFrom(assemblyFile);
                }
                else
                    throw;
            }

            // 注册插件定义
            applicationPartManager.ApplicationParts.Add(new AssemblyPart(assembly));

            return assembly;
        }

        /// <summary>
        /// 执行文件部署并返回加载的程序集
        /// </summary>
        /// <param name="applicationPartManager">应用程序部件管理器</param>
        /// <param name="assemblyFile">插件程序集文件的路径</param>
        /// <param name="pluginConfig">插件配置</param>
        /// <param name="fileProvider">DH文件提供程序</param>
        /// <returns>程序集</returns>
        private static Assembly PerformFileDeploy(this ApplicationPartManager applicationPartManager,
            string assemblyFile, PluginConfig pluginConfig, IDHFileProvider fileProvider)
        {
            // 确保目录结构正确
            if (string.IsNullOrEmpty(assemblyFile) ||
                string.IsNullOrEmpty(fileProvider.GetParentDirectory(assemblyFile)))
                throw new InvalidOperationException(
                    $"The plugin directory for the {fileProvider.GetFileName(assemblyFile)} file exists in a directory outside of the allowed directory hierarchy");

            var assembly =
                AddApplicationParts(applicationPartManager, assemblyFile, pluginConfig.UseUnsafeLoadAssembly);

            // 删除.deps文件
            if (assemblyFile.EndsWith(".dll"))
                _fileProvider.DeleteFile(assemblyFile[0..^4] + ".deps.json");

            return assembly;
        }

        /// <summary>
        /// 检查程序集是否已加载
        /// </summary>
        /// <param name="filePath">程序集文件路径</param>
        /// <param name="pluginName">插件系统名称</param>
        /// <returns>检查结果</returns>
        private static bool IsAlreadyLoaded(string filePath, string pluginName)
        {
            // 忽略已加载的库
            //（我们这样做是因为并非所有库都在应用程序启动后立即加载）
            var fileName = _fileProvider.GetFileName(filePath);
            if (_baseAppLibraries.Any(library => library.Equals(fileName, StringComparison.InvariantCultureIgnoreCase)))
                return true;

            try
            {
                // 获取不带扩展名的文件名
                var fileNameWithoutExtension = _fileProvider.GetFileNameWithoutExtension(filePath);
                if (string.IsNullOrEmpty(fileNameWithoutExtension))
                    throw new Exception($"Cannot get file extension for {fileName}");

                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    // 按文件名比较程序集
                    var assemblyName = (assembly.FullName ?? string.Empty).Split(',').FirstOrDefault();
                    if (!fileNameWithoutExtension.Equals(assemblyName, StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    // 未找到加载的程序集
                    if (!_loadedAssemblies.ContainsKey(assemblyName))
                    {
                        // 将其添加到列表中，以便稍后查找冲突
                        _loadedAssemblies.Add(assemblyName, new PluginLoadedAssemblyInfo(assemblyName, assembly.FullName));
                    }

                    // 设置程序集名称和插件名称以供进一步使用
                    _loadedAssemblies[assemblyName].References.Add((pluginName, AssemblyName.GetAssemblyName(filePath).FullName));

                    return true;
                }
            }
            catch
            {
                // 忽略
            }

            // 未找到任何内容
            return false;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化插件系统
        /// </summary>
        /// <param name="applicationPartManager">应用程序部件管理器</param>
        /// <param name="pluginConfig">插件配置</param>
        public static void InitializePlugins(this ApplicationPartManager applicationPartManager, PluginConfig pluginConfig)
        {
            if (applicationPartManager == null)
                throw new ArgumentNullException(nameof(applicationPartManager));

            if (pluginConfig == null)
                throw new ArgumentNullException(nameof(pluginConfig));

            // 使用对资源的锁定访问执行
            using (new ReaderWriteLockDisposable(_locker))
            {
                try
                {
                    // 确保插件目录已创建
                    var pluginsDirectory = _fileProvider.MapPath(DHPluginDefaults.Path);
                    _fileProvider.CreateDirectory(pluginsDirectory);

                    // 确保已创建上传目录
                    var uploadedPath = _fileProvider.MapPath(DHPluginDefaults.UploadedPath);
                    _fileProvider.CreateDirectory(uploadedPath);

                    foreach (var directory in _fileProvider.GetDirectories(uploadedPath))
                    {
                        var moveTo = _fileProvider.Combine(pluginsDirectory, _fileProvider.GetDirectoryNameOnly(directory));

                        if (_fileProvider.DirectoryExists(moveTo))
                            _fileProvider.DeleteDirectory(moveTo);

                        _fileProvider.DirectoryMove(directory, moveTo);
                    }

                    PluginsInfo = new PluginsInfo(_fileProvider);
                    PluginsInfo.LoadPluginInfo();

                    foreach (var pluginDescriptor in PluginsInfo.PluginDescriptors.Where(p => p.needToDeploy)
                                 .Select(p => p.pluginDescriptor))
                    {
                        var mainPluginFile = pluginDescriptor.OriginalAssemblyFile;

                        // 尝试部署主插件程序集
                        pluginDescriptor.ReferencedAssembly =
                            applicationPartManager.PerformFileDeploy(mainPluginFile, pluginConfig, _fileProvider);

                        // 然后部署所有其他引用的程序集
                        var filesToDeploy = pluginDescriptor.PluginFiles.Where(file =>
                            !_fileProvider.GetFileName(file).Equals(_fileProvider.GetFileName(mainPluginFile)) &&
                            !IsAlreadyLoaded(file, pluginDescriptor.SystemName)).ToList();

                        foreach (var file in filesToDeploy)
                            applicationPartManager.PerformFileDeploy(file, pluginConfig, _fileProvider);

                        // 确定插件类型（每个程序集只允许一个插件）
                        var pluginType = pluginDescriptor.ReferencedAssembly.GetTypes().FirstOrDefault(type =>
                            typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface && type.IsClass &&
                            !type.IsAbstract);

                        if (pluginType != default)
                            pluginDescriptor.PluginType = pluginType;
                    }
                }
                catch (Exception exception)
                {
                    // 引发完全异常
                    var message = string.Empty;
                    for (var inner = exception; inner != null; inner = inner.InnerException)
                        message = $"{message}{inner.Message}{Environment.NewLine}";

                    throw new Exception(message, exception);
                }

                PluginsInfo.AssemblyLoadedCollision = _loadedAssemblies.Select(item => item.Value)
                    .Where(loadedAssemblyInfo => loadedAssemblyInfo.Collisions.Any()).ToList();

                // 从插件添加名称兼容性类型
                var nameCompatibilityList = PluginsInfo.PluginDescriptors.Where(pd => pd.pluginDescriptor.Installed).SelectMany(pd => pd
                    .pluginDescriptor.ReferencedAssembly.GetTypes().Where(type =>
                        typeof(INameCompatibility).IsAssignableFrom(type) && !type.IsInterface && type.IsClass &&
                        !type.IsAbstract));
                NameCompatibilityManager.AdditionalNameCompatibilities.AddRange(nameCompatibilityList);
            }
        }

        #endregion


    }
}
