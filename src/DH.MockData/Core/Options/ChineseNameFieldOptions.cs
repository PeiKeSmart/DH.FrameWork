using DH.MockData.Abstractions.Options;

namespace DH.MockData.Core.Options;

/// <summary>
/// 中文名配置
/// </summary>
public class ChineseNameFieldOptions : FieldOptionsBase, IStringFieldOptions
{
    /// <summary>
    /// 名字长度
    /// </summary>
    public int Length { get; set; } = 3;
}