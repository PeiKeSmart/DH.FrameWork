using System;

#if DISPLAY_ATTRIBUTE
using System.ComponentModel.DataAnnotations;
#endif

namespace EnumsNET;
/// <summary>
/// Specifies what enum members to include.
/// </summary>
[Flags]
public enum EnumMemberSelection {
    /// <summary>
    /// Include all enum members.
    /// </summary>
    All = 0,
    /// <summary>
    /// Include only distinct valued enum members.
    /// </summary>
    Distinct = 1,
    /// <summary>
    /// Include each flag enum member.
    /// </summary>
    Flags = 2,
#if DISPLAY_ATTRIBUTE
    /// <summary>
    /// Include enum members in display order using <see cref="DisplayAttribute.Order"/>.
    /// </summary>
    DisplayOrder = 4
#endif
}