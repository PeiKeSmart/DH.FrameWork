namespace DH.ORM;

/// <summary>
/// ORM在保存数据的时候，会忽略打上 NotSave 批注的属性
/// </summary>
[Serializable, AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
public class NotSaveAttribute : Attribute
{
}
