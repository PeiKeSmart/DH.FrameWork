using DH.MockData.Abstractions.Randomizers;
using DH.MockData.Core.Options;
using DH.MockData.Datas;
using DH.MockData.Extensions;
using DH.MockData.Internals.Generators;

namespace DH.MockData.Core.Randomizers
{
    /// <summary>
    /// 国家随机生成器
    /// </summary>
    public class CountryRandomizer : RandomizerBase<CountryFieldOptions>, IStringRandomizer
    {
        /// <summary>
        /// 生成器
        /// </summary>
        private readonly RandomStringFromListGenerator _generator;

        /// <summary>
        /// 初始化一个<see cref="CountryRandomizer"/>类型的实例
        /// </summary>
        /// <param name="options">国家配置</param>
        public CountryRandomizer(CountryFieldOptions options) : base(options)
        {
            _generator = new RandomStringFromListGenerator(CommonData.Instance.CountryNames);
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        public string Generate()
        {
            return IsNull() ? null : _generator.Generate();
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="upperCase">是否大写</param>
        /// <returns></returns>
        public string Generate(bool upperCase)
        {
            return Generate().ToCasedInvariant(upperCase);
        }
    }
}
