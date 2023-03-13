﻿using DH.MockData.Abstractions.Randomizers;
using DH.MockData.Core.Options;
using DH.MockData.Internals.Generators;

namespace DH.MockData.Core.Randomizers
{
    /// <summary>
    /// 数值随机生成器
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class NumberRandomizer<T> : RandomizerBase<NumberFieldOptions<T>>, INumberRandomizer<T> where T : struct
    {
        /// <summary>
        /// 生成器
        /// </summary>
        private readonly RandomThingsGenerator<T> _generator;

        /// <summary>
        /// 初始化一个<see cref="NumberRandomizer{T}"/>类型的实例
        /// </summary>
        /// <param name="options">数值配置</param>
        public NumberRandomizer(NumberFieldOptions<T> options) : base(options)
        {
            _generator = new RandomThingsGenerator<T>(options.Min, options.Max);
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        public T? Generate()
        {
            return IsNull() ? null : (T?)_generator.Generate();
        }
    }
}
