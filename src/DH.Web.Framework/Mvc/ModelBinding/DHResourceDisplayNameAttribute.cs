using DH.Core;
using DH.Core.Infrastructure;
using DH.Services.Localization;

using System.ComponentModel;

namespace DH.Web.Framework.Mvc.ModelBinding
{
    /// <summary>
    /// 表示通过区域设置资源的传递键指定显示名称的模型属性
    /// </summary>
    public sealed class DHResourceDisplayNameAttribute : DisplayNameAttribute, IModelAttribute
    {
        #region Fields

        private string _resourceValue = string.Empty;

        #endregion

        #region Ctor

        /// <summary>
        /// 创建属性的实例
        /// </summary>
        /// <param name="resourceKey">区域设置资源的键</param>
        public DHResourceDisplayNameAttribute(string resourceKey) : base(resourceKey)
        {
            ResourceKey = resourceKey;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取或设置区域设置资源的键
        /// </summary>
        public string ResourceKey { get; set; }

        /// <summary>
        /// 获取显示名称
        /// </summary>
        public override string DisplayName
        {
            get
            {
                // 获取工作语言标识符
                var workingLanguageId = EngineContext.Current.Resolve<IWorkContext>().GetWorkingLanguage().Id;

                // 获取区域设置资源值
                _resourceValue = EngineContext.Current.Resolve<ILocalizationService>().GetResource(ResourceKey, workingLanguageId, true, ResourceKey);

                return _resourceValue;
            }
        }

        /// <summary>
        /// 获取属性的名称
        /// </summary>
        public string Name => nameof(DHResourceDisplayNameAttribute);

        #endregion
    }
}
