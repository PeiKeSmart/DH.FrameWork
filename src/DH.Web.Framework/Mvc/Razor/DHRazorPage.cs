using DH.Core.Infrastructure;
using DH.Services.Localization;
using DH.Web.Framework.Localization;

namespace DH.Web.Framework.Mvc.Razor
{
    /// <summary>
    /// Web视图页
    /// </summary>
    /// <typeparam name="TModel">模型</typeparam>
    public abstract partial class DHRazorPage<TModel> : Microsoft.AspNetCore.Mvc.Razor.RazorPage<TModel>
    {
        private ILocalizationService _localizationService;
        private Localizer _localizer;

        /// <summary>
        /// 获取本地化资源
        /// </summary>
        public Localizer T
        {
            get
            {
                if (_localizationService == null)
                    _localizationService = EngineContext.Current.Resolve<ILocalizationService>();

                if (_localizer == null)
                {
                    _localizer = (format, args) =>
                    {
                        var resFormat = _localizationService.GetResource(format);
                        if (string.IsNullOrEmpty(resFormat))
                        {
                            return new LocalizedString(format);
                        }
                        return new LocalizedString((args == null || args.Length == 0)
                            ? resFormat
                            : string.Format(resFormat, args));
                    };
                }
                return _localizer;
            }
        }
    }

    /// <summary>
    /// Web视图页
    /// </summary>
    public abstract partial class DHRazorPage : DHRazorPage<dynamic>
    {
    }
}
