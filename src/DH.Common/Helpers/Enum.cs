using System.Reflection;

using Pek.Helpers;

namespace DH.Helpers;

/// <summary>
/// 枚举 操作
/// </summary>
public static partial class Enum
{
    #region GetItems(获取描述项集合)

    /// <summary>
    /// 获取描述项集合，文本设置为Description，值为Value
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    public static List<Item> GetItems<TEnum>() => GetItems(typeof(TEnum));

    /// <summary>
    /// 获取描述项集合，文本设置为Description，值为Value
    /// </summary>
    /// <param name="type">枚举类型</param>
    public static List<Item> GetItems(Type type)
    {
        type = Common.GetType(type);
        ValidateEnum(type);
        var result = new List<Item>();
        foreach (var field in type.GetFields())
            AddItem(type, result, field);
        return result.OrderBy(t => t.SortId).ToList();
    }

    /// <summary>
    /// 验证是否枚举类型
    /// </summary>
    /// <param name="enumType">类型</param>
    /// <exception cref="InvalidOperationException"></exception>
    private static void ValidateEnum(Type enumType)
    {
        if (enumType.IsEnum == false)
            throw new InvalidOperationException($"类型 {enumType} 不是枚举");
    }

    /// <summary>
    /// 添加描述项
    /// </summary>
    /// <param name="type">枚举类型</param>
    /// <param name="result">集合</param>
    /// <param name="field">字段</param>
    private static void AddItem(Type type, ICollection<Item> result, FieldInfo field)
    {
        if (!field.FieldType.IsEnum)
            return;
        var value = Pek.Helpers.Enum.GetValue(type, field.Name);
        var description = Pek.Helpers.Reflection.GetDescription(field);
        result.Add(new Item(description, value, value));
    }

    #endregion
}