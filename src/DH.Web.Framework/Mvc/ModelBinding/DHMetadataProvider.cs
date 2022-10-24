using DH.Core;

using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace DH.Web.Framework.Mvc.ModelBinding
{
    /// <summary>
    /// 表示将自定义属性添加到模型元数据的元数据提供程序，以便以后可以检索它
    /// </summary>
    public class DHMetadataProvider : IDisplayMetadataProvider
    {
        /// <summary>
        /// 设置显示元数据属性的值
        /// </summary>
        /// <param name="context">显示元数据提供程序上下文</param>
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            // 获取所有自定义属性
            var additionalValues = context.Attributes.OfType<IModelAttribute>().ToList();

            // 并尝试将它们作为元数据的附加值添加
            foreach (var additionalValue in additionalValues)
            {
                if (context.DisplayMetadata.AdditionalValues.ContainsKey(additionalValue.Name))
                    throw new DHException("There is already an attribute with the name '{0}' on this model", additionalValue.Name);

                context.DisplayMetadata.AdditionalValues.Add(additionalValue.Name, additionalValue);
            }
        }
    }
}
