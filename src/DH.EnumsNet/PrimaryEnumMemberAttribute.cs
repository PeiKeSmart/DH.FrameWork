namespace EnumsNET;
/// <summary>
/// Indicates if the enum member should be the primary enum member when there are duplicate values.
/// In the case of duplicate values, extension methods will use the enum member marked with this attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public sealed class PrimaryEnumMemberAttribute : Attribute {
}