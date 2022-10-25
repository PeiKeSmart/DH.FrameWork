using DH.Core;
using DH.Core.Domain;
using DH.Core.Domain.Customers;
using DH.Services.Common;
using DH.Services.Themes;

using NewLife.Log;
using NewLife.Serialization;

namespace DH.Web.Framework.Themes
{
    /// <summary>
    /// 表示主题上下文实现
    /// </summary>
    public partial class ThemeContext : IThemeContext
    {
        #region Fields

        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IStoreContext _storeContext;
        private readonly IThemeProvider _themeProvider;
        private readonly IWorkContext _workContext;
        private readonly StoreInformationSettings _storeInformationSettings;

        private string _cachedThemeName;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="genericAttributeService">Generic attribute service</param>
        /// <param name="storeContext">Store context</param>
        /// <param name="themeProvider">Theme provider</param>
        /// <param name="workContext">Work context</param>
        /// <param name="storeInformationSettings">Store information settings</param>
        public ThemeContext(IGenericAttributeService genericAttributeService,
            IStoreContext storeContext,
            IWorkContext workContext,
            IThemeProvider themeProvider,
            StoreInformationSettings storeInformationSettings)
        {
            _genericAttributeService = genericAttributeService;
            _storeContext = storeContext;
            _themeProvider = themeProvider;
            _workContext = workContext;
            _themeProvider = themeProvider;
            _storeInformationSettings = storeInformationSettings;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取或设置当前主题系统名称
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        public virtual async Task<string> GetWorkingThemeNameAsync()
        {
            if (!string.IsNullOrEmpty(_cachedThemeName))
                return _cachedThemeName;

            var themeName = string.Empty;

            // 是否允许客户选择主题
            var customer = _workContext.GetCurrentCustomer();

            if (_storeInformationSettings.AllowCustomerToSelectTheme &&
                customer != null)
            {
                var store = _storeContext.GetCurrentStore();
                themeName = _genericAttributeService.GetAttribute<string>(customer,
                    DHCustomerDefaults.WorkingThemeNameAttribute, store.Id);
            }

            // 如果没有，尝试获取默认站点主题
            if (string.IsNullOrEmpty(themeName))
                themeName = _storeInformationSettings.DefaultStoreTheme;

            // 确保此主题存在
            if (!await _themeProvider.ThemeExistsAsync(themeName))
            {
                // 如果不存在，请尝试获取第一个
                themeName = (await _themeProvider.GetThemesAsync()).FirstOrDefault()?.SystemName
                            ?? throw new Exception("无法加载主题");
            }

            // 缓存主题系统名称
            _cachedThemeName = themeName;

            return themeName;
        }

        /// <summary>
        /// 设置当前主题系统名称
        /// </summary>
        public virtual void SetWorkingThemeNameAsync(string workingThemeName)
        {
            // 设置当前主题系统名称
            var customer = _workContext.GetCurrentCustomer();
            if (!_storeInformationSettings.AllowCustomerToSelectTheme ||
                customer == null)
                return;

            // 按客户主题系统名称保存所选内容
            var store = _storeContext.GetCurrentStore();
            _genericAttributeService.SaveAttribute(customer,
                DHCustomerDefaults.WorkingThemeNameAttribute, workingThemeName,
                store.Id);

            // 清除缓存
            _cachedThemeName = null;
        }

        #endregion
    }
}
