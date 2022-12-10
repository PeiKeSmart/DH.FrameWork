namespace DH.AspNetCore.Attributes;

/// <summary>
/// 隐藏swagger接口特性标识
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class HiddenApiAttribute : System.Attribute
{
}