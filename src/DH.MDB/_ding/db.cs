using System.Collections;

namespace DH;

/// <summary>
/// wojilu ORM 最主要的工具，集中了对象的常用 CRUD (读取/插入/更新/删除) 操作。主要方法都是泛型方法。
/// </summary>
public class db
{

    internal static List<T> getResults<T>(IList list)
    {
        return cvt.ToList<T>(list);
    }

}
