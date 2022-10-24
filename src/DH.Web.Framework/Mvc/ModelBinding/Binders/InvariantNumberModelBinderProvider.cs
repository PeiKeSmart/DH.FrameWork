using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

using System.Globalization;
using System.Numerics;

namespace DH.Web.Framework.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// 表示用于绑定数字类型的模型绑定提供程序
    /// </summary>
    public class InvariantNumberModelBinderProvider : IModelBinderProvider
    {
        #region Fields

        private static readonly HashSet<Type> _integerTypes = new()
        {
            typeof(int), typeof(long), typeof(short), typeof(sbyte),
            typeof(byte), typeof(ulong), typeof(ushort), typeof(uint), typeof(BigInteger)
        };

        private static readonly HashSet<Type> _floatingPointTypes = new()
        {
            typeof(double), typeof(decimal), typeof(float)
        };

        #endregion

        /// <summary>
        /// 创建一个模型绑定器
        /// </summary>
        /// <param name="context">上下文对象</param>
        /// <returns>浮点类型的模型绑定器实例</returns>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            var modelType = context.Metadata.UnderlyingOrModelType;

            if (modelType is null)
                return null;

            if (_floatingPointTypes.Contains(modelType))
                return new InvariantNumberModelBinder(NumberStyles.Float, new FloatingPointTypeModelBinderProvider().GetBinder(context));

            if (_integerTypes.Contains(modelType))
                return new InvariantNumberModelBinder(NumberStyles.Integer, new SimpleTypeModelBinderProvider().GetBinder(context));

            return null;
        }
    }
}
