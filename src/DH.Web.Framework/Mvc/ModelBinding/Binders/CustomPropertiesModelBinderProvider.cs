using DH.Web.Framework.Models;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DH.Web.Framework.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// 表示CustomProperties的模型绑定提供程序
    /// </summary>
    [Obsolete]
    public class CustomPropertiesModelBinderProvider : IModelBinderProvider
    {
        IModelBinder IModelBinderProvider.GetBinder(ModelBinderProviderContext context)
        {
            var propertyBinders = context.Metadata.Properties
                    .ToDictionary(modelProperty => modelProperty, modelProperty => context.CreateBinder(modelProperty));

            if (context.Metadata.ModelType == typeof(Dictionary<string, object>) && context.Metadata.PropertyName == nameof(BaseDHModel.CustomProperties))
                return new CustomPropertiesModelBinder();
            else
                return null;
        }
    }
}
