﻿using DH.MockData.Abstractions.Randomizers;
using DH.MockData.Core.Options;
using DH.MockData.Datas;
using DH.MockData.Extensions;
using DH.MockData.Internals.Generators;

namespace DH.MockData.Core.Randomizers
{
    /// <summary>
    /// 城市随机生成器
    /// </summary>
    public class CityRandomizer : RandomizerBase<CityFieldOptions>, IStringRandomizer
    {
        /// <summary>
        /// 生成器
        /// </summary>
        private readonly RandomStringFromListGenerator _generator;

        /// <summary>
        /// 初始化一个<see cref="CityRandomizer"/>类型的实例
        /// </summary>
        /// <param name="options">城市配置</param>
        public CityRandomizer(CityFieldOptions options) : base(options)
        {
            _generator = new RandomStringFromListGenerator(CommonData.Instance.CityNames);
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
