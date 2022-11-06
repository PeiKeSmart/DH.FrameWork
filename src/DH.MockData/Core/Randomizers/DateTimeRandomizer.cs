using DH.MockData.Abstractions.Randomizers;
using DH.MockData.Core.Options;
using DH.MockData.Internals.Generators;

using System.Globalization;

namespace DH.MockData.Core.Randomizers;

/// <summary>
/// 日期时间随机生成器
/// </summary>
public class DateTimeRandomizer : RandomizerBase<DateTimeFieldOptions>, IDateTimeRandomizer
{
    /// <summary>
    /// 生成器
    /// </summary>
    private readonly RandomThingsGenerator<DateTime> _generator;

    /// <summary>
    /// 初始化一个<see cref="DateTimeRandomizer"/>类型的实例
    /// </summary>
    /// <param name="options">日期时间配置</param>
    public DateTimeRandomizer(DateTimeFieldOptions options) : base(options)
    {
        _generator = new RandomThingsGenerator<DateTime>(options.From, options.To);
    }

    /// <summary>
    /// 生成
    /// </summary>
    /// <returns></returns>
    public DateTime? Generate()
    {
        if (IsNull())
        {
            return null;
        }

        DateTime value = _generator.Generate();
        return Options.IncludeTime ? value : value.Date;
    }

    /// <summary>
    /// 生成并转换成字符串
    /// </summary>
    /// <returns></returns>
    public string GenerateAsString()
    {
        DateTime? date = Generate();

        string format = !string.IsNullOrEmpty(Options.Format) ? Options.Format : DateTimeFieldOptions.DefaultFormat;
        return date?.ToString(format, CultureInfo.InvariantCulture);
    }
}