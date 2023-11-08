using System;
using System.ComponentModel;
#if DISPLAY_ATTRIBUTE
using System.ComponentModel.DataAnnotations;
#endif
using System.Runtime.Serialization;

namespace EnumsNET;
/// <summary>
/// Specifies the enum string representation formats.
/// </summary>
[EnumFormatValidator]
public enum EnumFormat {
    /// <summary>
    /// Enum is represented by its decimal value.
    /// </summary>
    DecimalValue = 0,
    /// <summary>
    /// Enum is represented by its hexadecimal value.
    /// </summary>
    HexadecimalValue = 1,
    /// <summary>
    /// Enum is represented by its underlying value.
    /// </summary>
    UnderlyingValue = 2,
    /// <summary>
    /// Enum is represented by its name.
    /// </summary>
    Name = 3,
    /// <summary>
    /// Enum is represented by its <see cref="DescriptionAttribute.Description"/>.
    /// </summary>
    Description = 4,
    /// <summary>
    /// Enum is represented by its <see cref="EnumMemberAttribute.Value"/>.
    /// </summary>
    EnumMemberValue = 5,
#if DISPLAY_ATTRIBUTE
    /// <summary>
    /// Enum is represented by its <see cref="DisplayAttribute.Name"/>.
    /// </summary>
    DisplayName = 6
#endif
}

internal sealed class EnumFormatValidatorAttribute : Attribute, IEnumValidatorAttribute<EnumFormat> {
    public bool IsValid(EnumFormat value) => Enums.EnumFormatIsValid(value);
}