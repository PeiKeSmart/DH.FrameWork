using System.Dynamic;
using System.Text.Json;

namespace DH;

/// <summary>
/// 通过Dynamic访问System.Text.Json对象
/// 参考：https://blog.51cto.com/shanyou/3048736
/// </summary>
public class JTextAccessor : System.Dynamic.DynamicObject {
    private readonly JsonElement _content;

    public JTextAccessor(JsonElement content)
    {
        _content = content;
    }

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        result = null;
        if (_content.TryGetProperty(binder.Name, out JsonElement value))
        {
            result = Obtain(value);
        }
        else return false;
        return true;
    }

    private object Obtain(in JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.String: return element.GetString();
            case JsonValueKind.Null: return null;
            case JsonValueKind.False: return false;
            case JsonValueKind.True: return true;
            case JsonValueKind.Number: return element.GetDouble();
            default: break;
        }

        if (element.ValueKind == JsonValueKind.Array)
        {
            var list = new List<object>();
            foreach (var item in element.EnumerateArray())
            {
                list.Add(Obtain(item));
            }

            return list;
        }
        // Undefine、Object
        else return new JTextAccessor(element);
    }
}