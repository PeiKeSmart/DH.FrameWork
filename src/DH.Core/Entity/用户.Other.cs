using NewLife;
using NewLife.Data;

using System.Text.Json;

using XCode;
using XCode.Membership;

namespace DH.Entity;

/// <summary>用户扩展</summary>
public partial class UserE : User
{
    #region 高级查询
    /// <summary>查询管理员</summary>
    /// <param name="IsManager">是否后台管理</param>
    /// <param name="p">分页数据</param>
    /// <returns></returns>
    public static IList<User> Search(Boolean? IsManager, PageParameter p)
    {
        if (Meta.Session.Count < 1000)
        {
            IList<User> list;
            if (IsManager != null)
            {
                if (IsManager.Value)
                {
                    list = Meta.Cache.FindAll(e => e.Ex1 == 1);
                }
                else
                {
                    list = Meta.Cache.FindAll(e => e.Ex1 == 0);
                }
            }
            else
            {
                list = FindAllWithCache();
            }

            p.TotalCount = list.Count;
            list = list.Skip((p.PageIndex - 1) * p.PageSize).Take(p.PageSize).ToList();


            return list;
        }

        var exp = new WhereExpression();
        if (IsManager != null)
        {
            if (IsManager.Value)
            {
                exp &= _.Ex1 == 1;
            }
            else
            {
                exp &= _.Ex1 == 0;
            }
        }

        return FindAll(exp, p);
    }

    /// <summary>
    /// 根据关键字查询
    /// </summary>
    /// <param name="IsManager">是否后台管理</param>
    /// <param name="p">分页数据</param>
    /// <param name="key">关键字，搜索代码、名称、昵称、手机、邮箱</param>
    /// <returns></returns>
    public static IEnumerable<User> Searchs(Boolean? IsManager, PageParameter p, string key)
    {
        if (Meta.Session.Count < 1000)
        {
            IEnumerable<User> list;
            if (IsManager != null)
            {
                if (IsManager.Value)
                {
                    list = Meta.Cache.FindAll(e => e.Ex1 == 1);
                }
                else
                {
                    list = Meta.Cache.FindAll(e => e.Ex1 == 0);
                }
            }
            else
            {
                list = FindAllWithCache();
            }
            if (!key.IsNullOrWhiteSpace())
            {
                list = list.Where(e => e.Name?.Contains(key.Trim(), StringComparison.OrdinalIgnoreCase) == true || e.Code?.Contains(key.Trim(), StringComparison.OrdinalIgnoreCase) == true || e.DisplayName?.Contains(key.Trim(), StringComparison.OrdinalIgnoreCase) == true || e.Mobile?.Contains(key.Trim(), StringComparison.OrdinalIgnoreCase) == true || e.Mail?.Contains(key.Trim(), StringComparison.OrdinalIgnoreCase) == true);
            }

            p.TotalCount = list.Count();

            list = list.OrderByDescending(e => e.ID).Skip((p.PageIndex - 1) * p.PageSize).Take(p.PageSize);
            return list;
        }
        var exp = new WhereExpression();
        if (IsManager != null)
        {
            if (IsManager.Value)
            {
                exp &= _.Ex1 == 1;
            }
            else
            {
                exp &= _.Ex1 == 0;
            }

        }
        if (!key.IsNullOrWhiteSpace())
        {
            exp &= _.Code.StartsWith(key) | _.Name.StartsWith(key) | _.DisplayName.StartsWith(key) | _.Mobile.StartsWith(key) | _.Mail.StartsWith(key);
        }

        p.Sort = "Id";
        p.Desc = true;

        return FindAll(exp, p);
    }

    /// <summary>
    /// 根据部门Id、关键字查询
    /// </summary>
    /// <param name="IsManager">是否后台管理</param>
    /// <param name="p">分页数据</param>
    /// <param name="dId">部门Id</param>
    /// <param name="key">关键字，搜索代码、名称、昵称、手机、邮箱</param>
    /// <returns></returns>
    public static IEnumerable<User> Searchs(Boolean? IsManager, PageParameter p, Int32 dId, string key)
    {
        var exp = new WhereExpression();
        if (dId > 0)
        {
            exp &= UserDetail._.DepartmentIds.Contains($",{dId},");
        }

        var exp1 = new WhereExpression();

        if (IsManager != null)
        {
            if (IsManager.Value)
            {
                exp1 &= UserE._.Ex1 == 1;
            }
            else
            {
                exp1 &= UserE._.Ex1 == 0;
            }
        }

        exp1 &= UserE._.ID.In(UserDetail.FindSQLWithKey(exp));

        if (!key.IsNullOrWhiteSpace())
        {
            exp1 &= UserE._.Code.StartsWith(key) | UserE._.Name.StartsWith(key) | UserE._.DisplayName.StartsWith(key) | UserE._.Mobile.StartsWith(key) | UserE._.Mail.StartsWith(key);
        }

        p.Sort = "Id";
        p.Desc = true;

        return UserE.FindAll(exp1, p);
    }

    /// <summary>
    /// 根据用户名和手机号查询
    /// </summary>
    /// <param name="IsSystem">是否系统角色</param>
    /// <param name="p">分页数据</param>
    /// <param name="mobile">手机号</param>
    /// <param name="name">用户名</param>
    /// <param name="IsAdmin">是否管理员</param>
    /// <returns></returns>
    public static IEnumerable<User> Searchs(Boolean? IsSystem, Boolean? IsAdmin, PageParameter p, string mobile, string name)
    {
        if (Meta.Session.Count < 1000)
        {
            IEnumerable<User> list;
            if (IsSystem != null)
            {
                if (IsSystem.Value)
                {
                    list = Meta.Cache.FindAll(e => e.Ex1 == 1);
                }
                else
                {
                    list = Meta.Cache.FindAll(e => e.Ex1 == 0);
                }
            }
            else
            {
                list = FindAllWithCache();
            }
            if (!name.IsNullOrWhiteSpace())
            {
                list = list.Where(e => e.Name.Contains(name.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            if (mobile.IsNotNullOrWhiteSpace())
            {
                list = list.Where(e => e.Mobile.IsNotNullOrWhiteSpace() && e.Mobile.Contains(mobile.Trim()));
            }

            if (IsAdmin != null)
            {
                var list1 = RoleEx.Meta.Cache.Entities.FindAll(e => e.IsAdmin == IsAdmin.Value).Select(e => e.Id);

                list = list.Where(e => list1.Contains(e.RoleID));
            }

            p.TotalCount = list.Count();

            list = list.OrderByDescending(e => e.ID).Skip((p.PageIndex - 1) * p.PageSize).Take(p.PageSize);
            return list;
        }
        var exp = new WhereExpression();
        if (IsSystem != null)
        {
            if (IsSystem.Value)
            {
                exp &= _.Ex1 == 1;
            }
            else
            {
                exp &= _.Ex1 == 0;
            }

        }
        if (!name.IsNullOrWhiteSpace())
        {
            exp &= _.Name.Contains(name);
        }
        if (!mobile.IsNullOrWhiteSpace())
        {
            exp &= _.Mobile.Contains(mobile);
        }

        if (IsAdmin != null)
        {
            if (IsAdmin.Value)
            {
                exp &= _.RoleID.In(RoleEx.FindSQLWithKey(RoleEx._.IsAdmin == true));
            }
            else
            {
                exp &= _.RoleID.In(RoleEx.FindSQLWithKey(RoleEx._.IsAdmin == false));
            }
        }

        p.Sort = "Id";
        p.Desc = true;

        return FindAll(exp, p);
    }

    /// <summary>
    /// 根据用户名和手机号查询
    /// </summary>
    /// <param name="p">分页数据</param>
    /// <param name="field_name">搜索类型</param>
    /// <param name="field_value">搜索值</param>
    /// <param name="search_sort">排序</param>
    /// <param name="search_state">会员状态</param>
    /// <param name="search_grade">会员级别</param>
    /// <returns></returns>
    public static IEnumerable<User> Searchs(PageParameter p, String field_name, String field_value, String search_sort, String search_state, Int32 search_grade)
    {
        var exp = new WhereExpression();
        var exp1 = new WhereExpression();

        if (!field_name.IsNullOrWhiteSpace() && !field_value.IsNullOrWhiteSpace())
        {
            switch (field_name)
            {
                case "member_id":
                    if (field_value.ToInt() > 0)
                        exp &= _.ID == field_value;
                    break;

                case "member_name":
                    exp &= _.Name.Contains(field_value);
                    break;

                case "member_email":
                    exp &= _.Mail.Contains(field_value);
                    break;

                case "member_mobile":
                    exp &= _.Mobile.Contains(field_value);
                    break;

                case "member_truename":
                    exp1 &= UserDetail._.TrueName.Contains(field_value);
                    break;
                case "SSoID":
                    exp1 &= _.ID == UserConnect.FindByProviderAndLinkID("ChuangChu", field_value)?.UserID;
                    break;
            }
        }

        if (search_sort.IsNullOrWhiteSpace())
        {
            p.Sort = "Id";
            p.Desc = true;
        }
        else
        {
            p.OrderBy = search_sort;
        }

        if (!search_state.IsNullOrWhiteSpace())
        {
            switch (search_state)
            {
                case "no_memberstate":
                    exp &= _.Enable == false;
                    break;

                case "no_informallow":
                    exp1 &= UserDetail._.InformAllow == true;
                    break;

                case "no_isbuy":
                    exp1 &= UserDetail._.IsBuy == true;
                    break;

                case "no_isallowtalk":
                    exp1 &= UserDetail._.IsAllowTalk == true;
                    break;
            }
        }

        var SiteSettings = SiteSettingInfo.SiteSettings;
        if (search_grade > 0 && !SiteSettings.MemberGrade.IsNullOrWhiteSpace())
        {
            var list = JsonSerializer.Deserialize<List<GradeModel>>(SiteSettings.MemberGrade);

            if (search_grade <= list.Count)
            {
                var grade = list.FirstOrDefault(e => e.level == search_grade);
                exp1 &= UserDetail._.ExpPoints >= grade.exppoints;

                if (search_grade < list.Count)
                {
                    var grade1 = list.FirstOrDefault(e => e.level == search_grade + 1);

                    exp1 &= UserDetail._.ExpPoints < grade.exppoints;
                }
            }
        }

        if (!exp1.IsEmpty)
        {
            exp &= _.ID.In(UserDetail.FindSQLWithKey(exp1));
        }

        return FindAll(exp, p);
    }

    /// <summary>根据编号列表查找</summary>
    /// <param name="ids">编号列表</param>
    /// <returns>实体对象</returns>
    public static IList<User> FindByIds(String ids)
    {
        if (ids.IsNullOrWhiteSpace()) return new List<User>();

        ids = ids.Trim(',');

        if (Meta.Session.Count < 1000)
        {
            return Meta.Cache.FindAll(x => ids.SplitAsInt(",").Contains(x.ID));
        }

        return FindAll(_.ID.In(ids.Split(',')));
    }

    /// <summary>模糊查询名称</summary>
    /// <param name="key">关键字</param>
    /// <returns>实体对象</returns>
    public static IList<User> FindByLikeNames(String key)
    {
        if (key.IsNullOrWhiteSpace()) return new List<User>();

        if (Meta.Session.Count < 1000)
        {
            return Meta.Cache.FindAll(x => x.Name.Contains(key, StringComparison.OrdinalIgnoreCase));
        }
        var exp = new WhereExpression();
        exp &= _.Name.Contains(key.Trim());
        return FindAll(exp);
    }

    /// <summary>
    /// 根据用户名和手机号、角色查询
    /// </summary>
    /// <param name="p">分页数据</param>
    /// <param name="mobile">手机号</param>
    /// <param name="name">用户名</param>
    /// <param name="RoleID">角色ID</param>
    /// <param name="SortAsc">是否升序排序</param>
    /// <returns></returns>
    public static IEnumerable<User> Searchs(PageParameter p, string mobile, string name, int RoleID, Boolean SortAsc = false)
    {
        if (Meta.Session.Count < 1000)
        {
            IEnumerable<User> list = FindAllWithCache();

            if (!name.IsNullOrWhiteSpace())
            {
                list = list.Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }
            if (!mobile.IsNullOrWhiteSpace())
            {
                list = list.Where(e => e.Mobile.Contains(mobile.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            if (RoleID > 0)
            {
                list = list.Where(e => e.RoleID == RoleID);
            }
            p.TotalCount = list.Count();

            if (SortAsc)
            {
                list = list.OrderBy(e => e.ID).Skip((p.PageIndex - 1) * p.PageSize).Take(p.PageSize);
            }
            else
            {
                list = list.OrderByDescending(e => e.ID).Skip((p.PageIndex - 1) * p.PageSize).Take(p.PageSize);
            }

            return list;
        }
        var exp = new WhereExpression();
        if (!name.IsNullOrWhiteSpace())
        {
            exp &= _.Name.Contains(name);
        }
        if (!mobile.IsNullOrWhiteSpace())
        {
            exp &= _.Mobile.Contains(mobile.Trim());
        }
        if (RoleID > 0)
        {
            exp &= _.RoleID == RoleID;
        }
        p.Sort = "Id";

        if (SortAsc)
            p.Desc = false;
        else
            p.Desc = true;

        return FindAll(exp, p);
    }

    /// <summary>
    /// 根据用户名和手机号、部门查询。适用于非系统角色
    /// </summary>
    /// <param name="p">分页数据</param>
    /// <param name="mobile"></param>
    /// <param name="name"></param>
    /// <param name="RoleID"></param>
    /// <returns></returns>
    public static IEnumerable<User> Searchs1(PageParameter p, string mobile, string name, int RoleID)
    {
        if (Meta.Session.Count < 1000)
        {
            IEnumerable<User> list = FindAllWithCache();

            if (!name.IsNullOrWhiteSpace())
            {
                list = list.Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }
            if (!mobile.IsNullOrWhiteSpace())
            {
                list = list.Where(e => e.Mobile.Contains(mobile.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            if (RoleID > 0)
            {
                list = list.Where(e => e.RoleID == RoleID);
            }
            list = list.Where(x => x.Role?.IsSystem == false);
            p.TotalCount = list.Count();

            list = list.OrderByDescending(e => e.ID).Skip((p.PageIndex - 1) * p.PageSize).Take(p.PageSize);
            return list;
        }
        var exp = new WhereExpression();
        if (!name.IsNullOrWhiteSpace())
        {
            exp &= _.Name.Contains(name);
        }
        if (!mobile.IsNullOrWhiteSpace())
        {
            exp &= _.Mobile.Contains(mobile.Trim());
        }
        if (RoleID > 0)
        {
            exp &= _.RoleID == RoleID;
        }
        exp &= _.RoleID.NotIn(XCode.Membership.Role.FindSQLWithKey(XCode.Membership.Role._.IsSystem == true));
        p.Sort = "Id";
        p.Desc = true;

        return FindAll(exp, p);
    }

    /// <summary>
    /// 根据昵称查询是否存在用户
    /// </summary>
    /// <param name="Id">用户Id</param>
    /// <param name="DisplayName">用户昵称</param>
    /// <returns></returns>
    public static Boolean FindByDisplayName(Int32 Id, String DisplayName)
    {
        if (DisplayName.IsNullOrWhiteSpace()) return true;

        if (Meta.Session.Count < 1000)
        {
            if (Id == 0)
            {
                return Meta.Cache.Find(e => e.DisplayName == DisplayName) == null;
            }
            else
            {
                return Meta.Cache.Find(e => e.ID != Id && e.DisplayName == DisplayName) == null;
            }
        }
        else
        {
            var exp = new WhereExpression();
            exp &= _.DisplayName == DisplayName;

            if (Id > 0)
            {
                exp &= _.ID != Id;
            }

            return Find(exp) == null;
        }
    }

    /// <summary>
    /// 根据用户名查询是否存在用户
    /// </summary>
    /// <param name="Id">用户Id</param>
    /// <param name="Name">用户名</param>
    /// <returns></returns>
    public static Boolean FindByUserName(Int32 Id, String Name)
    {
        if (Name.IsNullOrWhiteSpace()) return true;

        if (Meta.Session.Count < 1000)
        {
            if (Id == 0)
            {
                return Meta.Cache.Find(e => e.Name == Name) == null;
            }
            else
            {
                return Meta.Cache.Find(e => e.ID != Id && e.Name == Name) == null;
            }
        }
        else
        {
            var exp = new WhereExpression();
            exp &= _.Name == Name;

            if (Id > 0)
            {
                exp &= _.ID != Id;
            }

            return Find(exp) == null;
        }
    }

    /// <summary>
    /// 根据手机号查询是否存在用户
    /// </summary>
    /// <param name="Id">用户Id</param>
    /// <param name="Mobile">手机号</param>
    /// <returns>是否指定Id用户有使用手机号</returns>
    public static Boolean FindByMobile(Int32 Id, String Mobile)
    {
        if (Mobile.IsNullOrWhiteSpace()) return true;

        if (Meta.Session.Count < 1000)
        {
            if (Id == 0)
            {
                return Meta.Cache.Find(e => e.Mobile == Mobile) == null;
            }
            else
            {
                return Meta.Cache.Find(e => e.ID != Id && e.Mobile == Mobile) == null;
            }
        }
        else
        {
            var exp = new WhereExpression();
            exp &= _.Mobile == Mobile;

            if (Id > 0)
            {
                exp &= _.ID != Id;
            }

            return Find(exp) == null;
        }
    }

    /// <summary>
    /// 根据邮箱查询是否存在用户
    /// </summary>
    /// <param name="Id">用户Id</param>
    /// <param name="Mail">邮箱</param>
    /// <returns></returns>
    public static Boolean FindByMail(Int32 Id, String Mail)
    {
        if (Mail.IsNullOrWhiteSpace()) return true;

        if (Meta.Session.Count < 1000)
        {
            if (Id == 0)
            {
                return Meta.Cache.Find(e => e.Mail == Mail) == null;
            }
            else
            {
                return Meta.Cache.Find(e => e.ID != Id && e.Mail == Mail) == null;
            }
        }
        else
        {
            var exp = new WhereExpression();
            exp &= _.Mail == Mail;

            if (Id > 0)
            {
                exp &= _.ID != Id;
            }

            return Find(exp) == null;
        }
    }

    /// <summary>
    /// 根据名字分页查询
    /// </summary>
    /// <param name="name"></param>
    /// <param name="p"></param>
    /// <param name="RoleId"></param>
    /// <param name="IsSystem"></param>
    /// <returns></returns>
    public static IEnumerable<User> PageByRoleId(string name, PageParameter p, Int32 RoleId, Boolean IsSystem = false)
    {
        if (Meta.Session.Count < 1000)
        {
            IList<User> list = Meta.Cache.FindAll(x => x.Role?.IsSystem == IsSystem);

            if (!name.IsNullOrWhiteSpace())
            {
                list = list.FindAll(x => x.Name.Contains(name));
            }

            if (RoleId > 0)
            {
                list = list.FindAll(x => x.RoleID == RoleId);
            }

            if (p != null)
            {
                p.TotalCount = list.Count();
                return list.OrderByDescending(e => e.ID).Skip(--p.PageIndex * p.PageSize).Take(p.PageSize);
            }

            return list.OrderByDescending(e => e.ID);
        }
        else
        {
            var exp = new WhereExpression();

            if (!name.IsNullOrWhiteSpace()) exp &= _.Name.Contains(name);
            if (RoleId > 0) exp &= _.RoleID == RoleId;
            exp &= _.RoleID.NotIn(XCode.Membership.Role.FindSQLWithKey(XCode.Membership.Role._.IsSystem == true));
            return FindAll(exp, p);
        }
    }

    /// <summary>
    /// 根据名字分页查询
    /// </summary>
    /// <param name="p"></param>
    /// <param name="RoleId"></param>
    /// <returns></returns>
    public static IEnumerable<User> PageByRoleId(Int32 RoleId, PageParameter p)
    {
        if (Meta.Session.Count < 1000)
        {
            var list = Meta.Cache.FindAll(x => RoleId <= 0 || x.RoleID == RoleId).OrderByDescending(e => e.ID);
            p.TotalCount = list.Count();
            return list.Skip(--p.PageIndex * p.PageSize).Take(p.PageSize);
        }
        else
        {
            var exp = new WhereExpression();

            if (RoleId > 0) exp &= _.RoleID == RoleId;
            return FindAll(exp, p);
        }
    }

    #endregion

    /// <summary>
    /// 获取最新用户
    /// </summary>
    /// <returns></returns>
    public static User GetLast()
    {
        if (Meta.Session.Count < 1000)
        {
            Meta.Cache.Entities.FirstOrDefault();
        }

        return Find(_.ID == FindMax("ID"));
    }

    /// <summary>
    /// 获取部门下的用户数量
    /// </summary>
    /// <param name="DepartmentId">部门编号</param>
    /// <returns></returns>
    public static Int64 GetCountByDId(Int32 DepartmentId)
    {
        if (Meta.Session.Count < 1000) return UserDetail.Meta.Cache.Entities.Count(e => !e.DepartmentIds.IsNullOrWhiteSpace() && e.DepartmentIds.Trim(',').SplitAsInt(",").Contains(DepartmentId));

        return UserDetail.FindCount(UserDetail._.DepartmentIds.Contains($",{DepartmentId},"));
    }

    /// <summary>
    /// 获取指定时间的用户数
    /// </summary>
    /// <param name="StarTime"></param>
    /// <param name="UId"></param>
    /// <returns></returns>
    public static Int64 FindCountByCreateTime(DateTime StarTime, Int32 UId)
    {
        if (StarTime < DateTime.MinValue)
        {
            return 0;
        }

        var StarTimeAddMonth = StarTime.AddMonths(1);
        var EndTime = $"{StarTimeAddMonth.Year}-{StarTimeAddMonth.Month}-01".ToDate();
        StarTime = StarTime.Date;

        if (Meta.Session.Count < 1000)
        {
            if (UId == 0)
            {
                return User.Meta.Cache.FindAll(e => e.RegisterTime >= StarTime && e.RegisterTime < EndTime).Count;
            }
            else
            {
                return UserDetail.Meta.Cache.FindAll(e => e.CreateTime >= StarTime && e.CreateTime < EndTime && e.ParentUId == UId).Count;
            }
        }

        if (UId == 0)
        {
            return FindCount(_.RegisterTime >= StarTime & _.RegisterTime < EndTime);
        }
        else
        {
            return UserDetail.FindCount(UserDetail._.CreateTime >= StarTime & UserDetail._.CreateTime < EndTime & UserDetail._.ParentUId == UId);
        }
    }
}
