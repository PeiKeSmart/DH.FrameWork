using DH.Core.Infrastructure;
using DH.Services.Plugins;

using Newtonsoft.Json;

using System.Text;

namespace DH.Services.Themes
{
    /// <summary>
    /// 表示默认主题提供程序实现
    /// </summary>
    public partial class ThemeProvider : IThemeProvider
    {
        #region Fields

        private static readonly object _locker = new();

        private readonly IDHFileProvider _fileProvider;

        protected Dictionary<string, ThemeDescriptor> _themeDescriptors;

        #endregion

        #region Ctor

        public ThemeProvider(IDHFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
            Initialize();
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Initializes theme provider
        /// </summary>
        protected virtual void Initialize()
        {
            if (_themeDescriptors != null)
                return;

            //prevent multi loading data
            lock (_locker)
            {
                //data can be loaded while we waited
                if (_themeDescriptors != null)
                    return;

                //load all theme descriptors
                _themeDescriptors =
                    new Dictionary<string, ThemeDescriptor>(StringComparer.InvariantCultureIgnoreCase);

                var themeDirectoryPath = _fileProvider.MapPath(DHPluginDefaults.ThemesPath);
                foreach (var descriptionFile in _fileProvider.GetFiles(themeDirectoryPath,
                    DHPluginDefaults.ThemeDescriptionFileName, false))
                {
                    var text = _fileProvider.ReadAllText(descriptionFile, Encoding.UTF8);
                    if (string.IsNullOrEmpty(text))
                        continue;

                    //get theme descriptor
                    var themeDescriptor = GetThemeDescriptorFromText(text);

                    //some validation
                    if (string.IsNullOrEmpty(themeDescriptor?.SystemName))
                        throw new Exception($"A theme descriptor '{descriptionFile}' has no system name");

                    _themeDescriptors.TryAdd(themeDescriptor.SystemName, themeDescriptor);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get theme descriptor from the description text
        /// </summary>
        /// <param name="text">Description text</param>
        /// <returns>Theme descriptor</returns>
        public ThemeDescriptor GetThemeDescriptorFromText(string text)
        {
            //get theme description from the JSON file
            var themeDescriptor = JsonConvert.DeserializeObject<ThemeDescriptor>(text);

            //some validation
            if (_themeDescriptors.ContainsKey(themeDescriptor.SystemName))
                throw new Exception($"A theme with '{themeDescriptor.SystemName}' system name is already defined");

            return themeDescriptor;
        }

        /// <summary>
        /// 获取所有主题
        /// </summary>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含主题描述符的列表
        /// </returns>
        public Task<IList<ThemeDescriptor>> GetThemesAsync()
        {
            return Task.FromResult<IList<ThemeDescriptor>>(_themeDescriptors.Values.ToList());
        }

        /// <summary>
        /// Get a theme by the system name
        /// </summary>
        /// <param name="systemName">Theme system name</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the me descriptor
        /// </returns>
        public Task<ThemeDescriptor> GetThemeBySystemNameAsync(string systemName)
        {
            if (string.IsNullOrEmpty(systemName))
                return Task.FromResult<ThemeDescriptor>(null);

            _themeDescriptors.TryGetValue(systemName, out var descriptor);

            return Task.FromResult(descriptor);
        }

        /// <summary>
        /// 检查具有指定系统名称的主题是否存在
        /// </summary>
        /// <param name="systemName">Theme system name</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the rue if the theme exists; otherwise false
        /// </returns>
        public Task<bool> ThemeExistsAsync(string systemName)
        {
            if (string.IsNullOrEmpty(systemName))
                return Task.FromResult(false);

            return Task.FromResult(_themeDescriptors.ContainsKey(systemName));
        }

        #endregion
    }
}
