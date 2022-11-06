using DH.MockData.Abstractions.Randomizers;
using DH.MockData.Core.Options;
using DH.MockData.Extensions;

using Fare;

namespace DH.MockData.Core.Randomizers;

/// <summary>
/// 正则表达式随机生成器
/// </summary>
public class TextRegexRandomizer : RandomizerBase<TextRegexFieldOptions>, IStringRandomizer
{
    /// <summary>
    /// 生成器
    /// </summary>
    private readonly Xeger _generator;

    /// <summary>
    /// 初始化一个<see cref="TextRegexRandomizer"/>类型的实例
    /// </summary>
    /// <param name="options">正则表达式配置</param>
    public TextRegexRandomizer(TextRegexFieldOptions options) : base(options)
    {
        _generator = new Xeger(options.Pattern);
    }

    /// <summary>
    /// 生成
    /// </summary>
    /// <returns></returns>
    public string Generate()
    {
        return IsNull() || string.IsNullOrEmpty(Options.Pattern) ? null : _generator.Generate();
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