using DH.MockData.Abstractions.Options;

namespace DH.MockData.Core.Options;

/// <summary>
/// 邮箱地址配置
/// </summary>
public class EmailAddressFieldOptions : FieldOptionsBase, IStringFieldOptions
{
    /// <summary>
    /// 是否男性
    /// </summary>
    public bool Male { get; set; } = true;

    /// <summary>
    /// 是否女性
    /// </summary>
    public bool Female { get; set; } = true;

    /// <summary>
    /// 是否左右
    /// </summary>
    public bool Left2Right { get; set; } = true;
}