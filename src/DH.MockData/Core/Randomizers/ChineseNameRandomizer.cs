﻿using DH.MockData.Abstractions.Randomizers;
using DH.MockData.Core.Options;
using DH.MockData.Datas;
using DH.MockData.Extensions;
using DH.MockData.Internals.Generators;

namespace DH.MockData.Core.Randomizers
{
    /// <summary>
    /// 姓名随机生成器
    /// </summary>
    public class ChineseNameRandomizer : RandomizerBase<ChineseNameFieldOptions>, IStringRandomizer
    {
        /// <summary>
        /// 生成器
        /// </summary>
        private readonly RandomGenerator _generator;

        /// <summary>
        /// 初始化一个<see cref="ChineseNameRandomizer"/>类型的实例
        /// </summary>
        /// <param name="options">配置</param>
        public ChineseNameRandomizer(ChineseNameFieldOptions options) : base(options)
        {
            _generator = new RandomGenerator();
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        public string Generate()
        {
            return
                $"{CommonData.Instance.ChineseFirstNames[_generator.GenerateInt(0, CommonData.Instance.ChineseFirstNames.Count)]}{_generator.GenerateRandomLengthChinese(Options.Length)}";
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
