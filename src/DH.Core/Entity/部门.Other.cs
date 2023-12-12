using NewLife;
using NewLife.Data;

using XCode;
using XCode.Membership;

namespace DH.Entity;

/// <summary>部门扩展</summary>
public class DepartmentE : Department {
    /// <summary>高级搜索</summary>
    /// <param name="parentId"></param>
    /// <param name="enable"></param>
    /// <param name="visible"></param>
    /// <param name="key"></param>
    /// <param name="page"></param>
    /// <param name="isInclude"></param>
    /// <returns></returns>
    public static IList<Department> Searchs(Int32 parentId, Boolean? enable, Boolean? visible, String key, PageParameter page, Boolean isInclude = false)
    {
        var exp = new WhereExpression();
        if (parentId >= 0)
        {
            if (isInclude)
            {
                exp &= _.Ex4.Contains($",{parentId},");
            }
            else
            {
                exp &= _.ParentID == parentId;
            }
        }

        if (enable != null) exp &= _.Enable == enable.Value;
        if (visible != null) exp &= _.Visible == visible.Value;
        if (!key.IsNullOrEmpty()) exp &= _.Code.Contains(key) | _.Name.Contains(key) | _.FullName.Contains(key);

        return FindAll(exp, page);
    }

    /// <summary>根据父级查找</summary>
    /// <param name="parentid">父级</param>
    /// <returns>实体集合</returns>
    public static IEnumerable<Department> FindAllByParentID(Int32 parentid)
    {
        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.ParentID == parentid);

        return FindAll(_.ParentID == parentid);
    }

    /// <summary>获取全部</summary>
    /// <returns>实体集合</returns>
    public static IEnumerable<Department> GetAll()
    {
        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Entities;

        return FindAll();
    }

    /// <summary>
    /// 获取全部部门集合并排序
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Department> GetList()
    {
        var list = DepartmentE.GetAll().Where(e => e.ParentID == 0).OrderBy(e => e.ID);
        IList<Department> listDepartment = new List<Department>();
        GetChildList(list, listDepartment);

        return listDepartment;
    }

    /// <summary>
    /// 获取子部门集合
    /// </summary>
    /// <param name="levelList"></param>
    /// <param name="list"></param>
    private static void GetChildList(IEnumerable<Department> levelList, IList<Department> list)
    {
        if (levelList.Any())
        {
            foreach (var item in levelList)
            {
                list.Add(item);

                var level = DepartmentE.FindAllByParentID(item.ID).OrderBy(e => e.ID);
                GetChildList(level, list);
            }
        }
    }

}