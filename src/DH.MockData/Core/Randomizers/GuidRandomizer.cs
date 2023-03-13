using DH.MockData.Abstractions.Randomizers;
using DH.MockData.Core.Options;
using DH.MockData.Extensions;
using System;

namespace DH.MockData.Core.Randomizers
{
    /// <summary>
    /// Guid随机生成器
    /// </summary>
    public class GuidRandomizer : RandomizerBase<GuidFieldOptions>, IGuidRandomizer
    {
        /// <summary>
        /// 初始化一个<see cref="GuidRandomizer"/>类型的实例
        /// </summary>
        /// <param name="options">Guid配置</param>
        public GuidRandomizer(GuidFieldOptions options) : base(options)
        {
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        public Guid? Generate()
        {
            if (IsNull())
            {
                return null;
            }

            return Guid.NewGuid();
        }

        /// <summary>
        /// 生成并转换成字符串
        /// </summary>
        /// <returns></returns>
        public string GenerateAsString()
        {
            if (IsNull())
            {
                return null;
            }

            return Guid.NewGuid().ToString().ToCasedInvariant(Options.Uppercase);
        }
    }
}
