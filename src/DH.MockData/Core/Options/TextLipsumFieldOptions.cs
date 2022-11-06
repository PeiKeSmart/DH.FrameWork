using DH.MockData.Abstractions.Options;

namespace DH.MockData.Core.Options;

/// <summary>
/// 段落配置
/// </summary>
public class TextLipsumFieldOptions : FieldOptionsBase, IStringFieldOptions
{
    /// <summary>
    /// 段落
    /// </summary>
    public int Paragraphs { get; set; } = 1;
}