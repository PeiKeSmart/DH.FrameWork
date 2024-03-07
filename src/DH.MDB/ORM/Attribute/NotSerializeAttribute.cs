namespace DH.ORM;

/// <summary>
/// 在 json 序列化的时候，打上 NotSerialize 批注的属性会被忽略
/// </summary>
[Serializable, AttributeUsage(AttributeTargets.Property)]
public class NotSerializeAttribute : Attribute
{
}
