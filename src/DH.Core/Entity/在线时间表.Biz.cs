using DH.Helpers;
using DH.Timing;

using NewLife;
using NewLife.Data;
using NewLife.Log;
using NewLife.Serialization;

using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

using XCode;
using XCode.Membership;

namespace DH.Entity;

public partial class SysOnlineTime : DHEntityBase<SysOnlineTime> {
    #region 对象操作
    static SysOnlineTime()
    {
        // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
        var df = Meta.Factory.AdditionalFields;
        df.Add(nameof(MonthTimes));
        df.Add(nameof(DayTimes));

        // 过滤器 UserModule、TimeModule、IPModule
        Meta.Modules.Add<TimeModule>();
    }

    /// <summary>验证并修补数据，通过抛出异常的方式提示验证失败。</summary>
    /// <param name="isNew">是否插入</param>
    public override void Valid(Boolean isNew)
    {
        // 如果没有脏数据，则不需要进行任何处理
        if (!HasDirty) return;

        // 建议先调用基类方法，基类方法会做一些统一处理
        base.Valid(isNew);

        // 在新插入数据或者修改了指定字段时进行修正
        //if (!Dirtys[nameof(UpdateTime)]) UpdateTime = DateTime.Now;

        // 检查唯一索引
        // CheckExist(isNew, nameof(Id));
    }

    ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //protected override void InitData()
    //{
    //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
    //    if (Meta.Session.Count > 0) return;

    //    if (XTrace.Debug) XTrace.WriteLine("开始初始化SysOnlineTime[在线时间表]数据……");

    //    var entity = new SysOnlineTime();
    //    entity.Id = 0;
    //    entity.Total = 0;
    //    entity.Year = 0;
    //    entity.Month = 0;
    //    entity.Week = 0;
    //    entity.Day = 0;
    //    entity.UpdateTime = DateTime.Now;
    //    entity.Insert();

    //    if (XTrace.Debug) XTrace.WriteLine("完成初始化SysOnlineTime[在线时间表]数据！");
    //}

    ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
    ///// <summary>已重载CESHI。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
    ///// <returns></returns>
    //public override Int32 Insert()
    //{
    //    return base.Insert();
    //}

    ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
    ///// <returns></returns>
    //protected override Int32 OnDelete()
    //{
    //    return base.OnDelete();
    //}
    #endregion

    #region  添加历史记录
    #endregion

    #region 扩展属性
    /// <summary>用户</summary>
    [XmlIgnore, ScriptIgnore, IgnoreDataMember]
    //[ScriptIgnore]
    public User User => Extends.Get(nameof(User), k => User.FindByID(UId));

    /// <summary>角色</summary>
    [XmlIgnore, ScriptIgnore, IgnoreDataMember]
    //[ScriptIgnore]
    public Role Role => Extends.Get(nameof(Role), k => Role.FindByID(RoleId));
    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static SysOnlineTime FindById(Int32 id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static SysOnlineTime FindByUId(Int32 id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.UId == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据编号列表查找</summary>
    /// <param name="ids">编号列表</param>
    /// <returns>实体对象</returns>
    public static IList<SysOnlineTime> FindByIds(string ids)
    {
        if (ids.IsNullOrWhiteSpace()) return new List<SysOnlineTime>();

        ids = ids.Trim(',');

        if (Meta.Session.Count < 1000)
        {
            return Meta.Cache.FindAll(x => ids.SplitAsInt(",").Contains(x.UId));
        }

        return FindAll(_.UId.In(ids.Split(',')));
    }

    /// <summary>根据用户编号、年、月查找</summary>
    /// <param name="id">用户编号</param>
    /// <param name="year">年</param>
    /// <param name="month">月</param>
    /// <returns>实体对象</returns>
    public static SysOnlineTime FindByIdAndYearAndMonth(Int32 id, Int32 year, Int32 month)
    {
        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.UId == id && e.Year == year && e.Month == month);

        return Find(_.UId == id & _.Year == year & _.Month == month);
    }

    /// <summary>根据角色查找</summary>
    /// <param name="roleId">角色</param>
    /// <returns>实体列表</returns>
    public static IList<SysOnlineTime> FindAllByRoleId(Int32 roleId)
    {
        if (roleId <= 0) return new List<SysOnlineTime>();

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.RoleId == roleId);

        return FindAll(_.RoleId == roleId);
    }

    /// <summary>根据用户名查找</summary>
    /// <param name="uName">用户名</param>
    /// <returns>实体列表</returns>
    public static IList<SysOnlineTime> FindAllByUName(String uName)
    {
        if (uName.IsNullOrEmpty()) return new List<SysOnlineTime>();

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.UName.EqualIgnoreCase(uName));

        return FindAll(_.UName == uName);
    }

    /// <summary>根据用户编号查找</summary>
    /// <param name="uId">用户编号</param>
    /// <returns>实体列表</returns>
    public static IList<SysOnlineTime> FindAllByUId(Int32 uId)
    {
        if (uId <= 0) return new List<SysOnlineTime>();

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.UId == uId);

        return FindAll(_.UId == uId);
    }

    /// <summary>根据用户编号、年、月查找</summary>
    /// <param name="uId">用户编号</param>
    /// <param name="year">年</param>
    /// <param name="month">月</param>
    /// <returns>实体对象</returns>
    public static SysOnlineTime FindByUIdAndYearAndMonth(Int32 uId, Int32 year, Int32 month)
    {
        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.UId == uId && e.Year == year && e.Month == month);

        return Find(_.UId == uId & _.Year == year & _.Month == month);
    }

    /// <summary>根据编号查找</summary>
    /// <param name="selects">字段</param>
    /// <returns>实体对象</returns>
    public static IList<SysOnlineTime> GetAll(String selects = null)
    {
        // 实体缓存
        if (Meta.Session.Count < 10000) return Meta.Cache.Entities;

        return FindAll(null, null, selects);
    }
    #endregion

    #region 高级查询

    /// <summary>高级查询</summary>
    /// <param name="Year">年</param>
    /// <param name="Month">月</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<SysOnlineTime> Searchs(Int32 Year, Int32 Month, PageParameter page)
    {
        var exp = new WhereExpression();

        if (Year > 0)
        {
            exp &= _.Year == Year;
        }

        if (Month > 0)
        {
            exp &= _.Month == Month;
        }

        return FindAll(exp, page);
    }

    /// <summary>高级查询</summary>
    /// <param name="StartTime">开始时间</param>
    /// <param name="EndTime">结束时间</param>
    /// <param name="RoleId">角色Id</param>
    /// <param name="UName">用户名</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<SysOnlineTime> Searchs(DateTime? StartTime, DateTime? EndTime, Int32 RoleId, String UName, PageParameter page)
    {
        var exp = new WhereExpression();

        if (StartTime.HasValue)
        {
            exp &= _.Year >= StartTime.Value.Year;
            exp &= _.Month >= StartTime.Value.Month;
        }

        if (EndTime.HasValue)
        {
            exp &= _.Year <= EndTime.Value.Year;
            exp &= _.Month <= EndTime.Value.Month;
        }

        if (RoleId > 0)
        {
            exp &= _.RoleId == RoleId;
        }

        if (!UName.IsNullOrWhiteSpace())
        {
            exp &= _.UName.Contains(UName);
        }

        return FindAll(exp, page);
    }

    // Select Count(Id) as Id,Category From DH_SysOnlineTime Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
    //static readonly FieldCache<SysOnlineTime> _CategoryCache = new FieldCache<SysOnlineTime>(nameof(Category))
    //{
    //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
    //};

    ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
    ///// <returns></returns>
    //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();
    #endregion

    #region 业务操作
    public ISysOnlineTime ToModel()
    {
        var model = new SysOnlineTime();
        model.Copy(this);

        return model;
    }

    /// <summary>
    /// 更新用户在线时间
    /// </summary>
    /// <param name="uid">用户id</param>
    public static void UpdateUserOnlineTime(Int32 uid, Int32 roleId, String uName)
    {
        if (uid <= 0) return;

        int updateOnlineTimeSpan = DHSetting.Current.UpdateOnlineTimeSpan;
        if (updateOnlineTimeSpan == 0) return;

        var modelUser = UserDetail.FindById(uid);
        if (modelUser == null) return;

        var lastUpdateTime = modelUser.LastUpdateTime;

        if (lastUpdateTime == 0)
        {
            modelUser.LastUpdateTime = DateTime.Now.ToTimeStamp();
            modelUser.Update();
        }
        else
        {
            if (lastUpdateTime <= DateTime.Now.AddMinutes(-updateOnlineTimeSpan).ToTimeStamp())
            {
                UpdateUserOnlineTime(uid, updateOnlineTimeSpan, DateTime.Now, roleId, uName);

                modelUser.LastUpdateTime = DateTime.Now.ToTimeStamp();
                modelUser.Update();
            }
        }
    }

    /// <summary>
    /// 更新用户在线时间
    /// </summary>
    /// <param name="uid">用户id</param>
    /// <param name="onlineTime">在线时间</param>
    /// <param name="updateTime">更新时间</param>
    /// <param name="roleId">角色Id</param>
    /// <param name="uName">用户名</param>
    private static void UpdateUserOnlineTime(Int32 uid, Int32 onlineTime, DateTime updateTime, Int32 roleId, String uName)
    {
        if (uid <= 0) return;

        onlineTime = Math.Abs(onlineTime);

        var modelUser = UserDetail.FindById(uid);
        if (modelUser != null)
        {
            modelUser.OnlineTime += onlineTime;
            modelUser.Update();
        }

        var model = FindByIdAndYearAndMonth(uid, updateTime.Year, updateTime.Month);
        if (model != null)
        {
            XTrace.WriteLine($"测试写入在线时间表的数据3：{onlineTime}_{model.ToJson()}");

            model.UId = uid;
            model.RoleId = roleId;
            model.UName = uName;
            model.Year = updateTime.Year;
            model.Month = updateTime.Month;
            model.MonthTimes += onlineTime;

            var dayTimes = model[$"Day{updateTime.Day}"].ToDGInt();
            if (dayTimes == 0)
            {
                model.DayTimes = onlineTime;
            }
            else
            {
                model.DayTimes += onlineTime;
            }

            model.UpdateTime = updateTime;

            if (updateTime.Day == 1)
            {
                model.Day1 = model.DayTimes;
            }
            else if (updateTime.Day == 2)
            {
                model.Day2 = model.DayTimes;
            }
            else if (updateTime.Day == 3)
            {
                model.Day3 = model.DayTimes;
            }
            else if (updateTime.Day == 4)
            {
                model.Day4 = model.DayTimes;
            }
            else if (updateTime.Day == 5)
            {
                model.Day5 = model.DayTimes;
            }
            else if (updateTime.Day == 6)
            {
                model.Day6 = model.DayTimes;
            }
            else if (updateTime.Day == 7)
            {
                model.Day7 = model.DayTimes;
            }
            else if (updateTime.Day == 8)
            {
                model.Day8 = model.DayTimes;
            }
            else if (updateTime.Day == 9)
            {
                model.Day9 = model.DayTimes;
            }
            else if (updateTime.Day == 10)
            {
                model.Day10 = model.DayTimes;
            }
            else if (updateTime.Day == 11)
            {
                model.Day11 = model.DayTimes;
            }
            else if (updateTime.Day == 12)
            {
                model.Day12 = model.DayTimes;
            }
            else if (updateTime.Day == 13)
            {
                model.Day13 = model.DayTimes;
            }
            else if (updateTime.Day == 14)
            {
                model.Day14 = model.DayTimes;
            }
            else if (updateTime.Day == 15)
            {
                model.Day15 = model.DayTimes;
            }
            else if (updateTime.Day == 16)
            {
                model.Day16 = model.DayTimes;
            }
            else if (updateTime.Day == 17)
            {
                model.Day17 = model.DayTimes;
            }
            else if (updateTime.Day == 18)
            {
                model.Day18 = model.DayTimes;
            }
            else if (updateTime.Day == 19)
            {
                model.Day19 = model.DayTimes;
            }
            else if (updateTime.Day == 20)
            {
                model.Day20 = model.DayTimes;
            }
            else if (updateTime.Day == 21)
            {
                model.Day21 = model.DayTimes;
            }
            else if (updateTime.Day == 22)
            {
                model.Day22 = model.DayTimes;
            }
            else if (updateTime.Day == 23)
            {
                model.Day23 = model.DayTimes;
            }
            else if (updateTime.Day == 24)
            {
                model.Day24 = model.DayTimes;
            }
            else if (updateTime.Day == 25)
            {
                model.Day25 = model.DayTimes;
            }
            else if (updateTime.Day == 26)
            {
                model.Day26 = model.DayTimes;
            }
            else if (updateTime.Day == 27)
            {
                model.Day27 = model.DayTimes;
            }
            else if (updateTime.Day == 28)
            {
                model.Day28 = model.DayTimes;
            }
            else if (updateTime.Day == 29)
            {
                model.Day29 = model.DayTimes;
            }
            else if (updateTime.Day == 30)
            {
                model.Day30 = model.DayTimes;
            }
            else if (updateTime.Day == 31)
            {
                model.Day31 = model.DayTimes;
            }

            //model[$"Day{updateTime.Day}"] = model.DayTimes;
            //model.SetItem($"Day{updateTime.Day}", model.DayTimes);

            XTrace.WriteLine($"测试写入在线时间表的数据2：{onlineTime}_{model.ToJson()}");

            model.Update();
        }
        else
        {
            model = new SysOnlineTime();
            model.UpdateTime = updateTime;
            model.UId = uid;
            model.UName = uName;
            model.Year = updateTime.Year;
            model.Month = updateTime.Month;
            model.MonthTimes = onlineTime;
            model.DayTimes = onlineTime;
            model.RoleId = roleId;

            for (var i = 1; i <= DateTimeUtil.GetMonthLen(updateTime.Year, updateTime.Month); i++)
            {
                if (i == updateTime.Day)
                {
                    //model.SetItem($"Day{i}", model.DayTimes);
                    //model[$"Day{i}"] = model.DayTimes;

                    if (updateTime.Day == 1)
                    {
                        model.Day1 = model.DayTimes;
                    }
                    else if (updateTime.Day == 2)
                    {
                        model.Day2 = model.DayTimes;
                    }
                    else if (updateTime.Day == 3)
                    {
                        model.Day3 = model.DayTimes;
                    }
                    else if (updateTime.Day == 4)
                    {
                        model.Day4 = model.DayTimes;
                    }
                    else if (updateTime.Day == 5)
                    {
                        model.Day5 = model.DayTimes;
                    }
                    else if (updateTime.Day == 6)
                    {
                        model.Day6 = model.DayTimes;
                    }
                    else if (updateTime.Day == 7)
                    {
                        model.Day7 = model.DayTimes;
                    }
                    else if (updateTime.Day == 8)
                    {
                        model.Day8 = model.DayTimes;
                    }
                    else if (updateTime.Day == 9)
                    {
                        model.Day9 = model.DayTimes;
                    }
                    else if (updateTime.Day == 10)
                    {
                        model.Day10 = model.DayTimes;
                    }
                    else if (updateTime.Day == 11)
                    {
                        model.Day11 = model.DayTimes;
                    }
                    else if (updateTime.Day == 12)
                    {
                        model.Day12 = model.DayTimes;
                    }
                    else if (updateTime.Day == 13)
                    {
                        model.Day13 = model.DayTimes;
                    }
                    else if (updateTime.Day == 14)
                    {
                        model.Day14 = model.DayTimes;
                    }
                    else if (updateTime.Day == 15)
                    {
                        model.Day15 = model.DayTimes;
                    }
                    else if (updateTime.Day == 16)
                    {
                        model.Day16 = model.DayTimes;
                    }
                    else if (updateTime.Day == 17)
                    {
                        model.Day17 = model.DayTimes;
                    }
                    else if (updateTime.Day == 18)
                    {
                        model.Day18 = model.DayTimes;
                    }
                    else if (updateTime.Day == 19)
                    {
                        model.Day19 = model.DayTimes;
                    }
                    else if (updateTime.Day == 20)
                    {
                        model.Day20 = model.DayTimes;
                    }
                    else if (updateTime.Day == 21)
                    {
                        model.Day21 = model.DayTimes;
                    }
                    else if (updateTime.Day == 22)
                    {
                        model.Day22 = model.DayTimes;
                    }
                    else if (updateTime.Day == 23)
                    {
                        model.Day23 = model.DayTimes;
                    }
                    else if (updateTime.Day == 24)
                    {
                        model.Day24 = model.DayTimes;
                    }
                    else if (updateTime.Day == 25)
                    {
                        model.Day25 = model.DayTimes;
                    }
                    else if (updateTime.Day == 26)
                    {
                        model.Day26 = model.DayTimes;
                    }
                    else if (updateTime.Day == 27)
                    {
                        model.Day27 = model.DayTimes;
                    }
                    else if (updateTime.Day == 28)
                    {
                        model.Day28 = model.DayTimes;
                    }
                    else if (updateTime.Day == 29)
                    {
                        model.Day29 = model.DayTimes;
                    }
                    else if (updateTime.Day == 30)
                    {
                        model.Day30 = model.DayTimes;
                    }
                    else if (updateTime.Day == 31)
                    {
                        model.Day31 = model.DayTimes;
                    }
                }
                else
                {
                    model.SetItem($"Day{i}", 0);
                    //model[$"Day{i}"] = 0;

                    if (updateTime.Day == 1)
                    {
                        model.Day1 = 0;
                    }
                    else if (updateTime.Day == 2)
                    {
                        model.Day2 = 0;
                    }
                    else if (updateTime.Day == 3)
                    {
                        model.Day3 = 0;
                    }
                    else if (updateTime.Day == 4)
                    {
                        model.Day4 = 0;
                    }
                    else if (updateTime.Day == 5)
                    {
                        model.Day5 = 0;
                    }
                    else if (updateTime.Day == 6)
                    {
                        model.Day6 = 0;
                    }
                    else if (updateTime.Day == 7)
                    {
                        model.Day7 = 0;
                    }
                    else if (updateTime.Day == 8)
                    {
                        model.Day8 = 0;
                    }
                    else if (updateTime.Day == 9)
                    {
                        model.Day9 = 0;
                    }
                    else if (updateTime.Day == 10)
                    {
                        model.Day10 = 0;
                    }
                    else if (updateTime.Day == 11)
                    {
                        model.Day11 = 0;
                    }
                    else if (updateTime.Day == 12)
                    {
                        model.Day12 = 0;
                    }
                    else if (updateTime.Day == 13)
                    {
                        model.Day13 = 0;
                    }
                    else if (updateTime.Day == 14)
                    {
                        model.Day14 = 0;
                    }
                    else if (updateTime.Day == 15)
                    {
                        model.Day15 = 0;
                    }
                    else if (updateTime.Day == 16)
                    {
                        model.Day16 = 0;
                    }
                    else if (updateTime.Day == 17)
                    {
                        model.Day17 = 0;
                    }
                    else if (updateTime.Day == 18)
                    {
                        model.Day18 = 0;
                    }
                    else if (updateTime.Day == 19)
                    {
                        model.Day19 = 0;
                    }
                    else if (updateTime.Day == 20)
                    {
                        model.Day20 = 0;
                    }
                    else if (updateTime.Day == 21)
                    {
                        model.Day21 = 0;
                    }
                    else if (updateTime.Day == 22)
                    {
                        model.Day22 = 0;
                    }
                    else if (updateTime.Day == 23)
                    {
                        model.Day23 = 0;
                    }
                    else if (updateTime.Day == 24)
                    {
                        model.Day24 = 0;
                    }
                    else if (updateTime.Day == 25)
                    {
                        model.Day25 = 0;
                    }
                    else if (updateTime.Day == 26)
                    {
                        model.Day26 = 0;
                    }
                    else if (updateTime.Day == 27)
                    {
                        model.Day27 = 0;
                    }
                    else if (updateTime.Day == 28)
                    {
                        model.Day28 = 0;
                    }
                    else if (updateTime.Day == 29)
                    {
                        model.Day29 = 0;
                    }
                    else if (updateTime.Day == 30)
                    {
                        model.Day30 = 0;
                    }
                    else if (updateTime.Day == 31)
                    {
                        model.Day31 = 0;
                    }
                }
            }

            XTrace.WriteLine($"测试写入在线时间表的数据1：{onlineTime}_{model.ToJson()}");

            model.Insert();
        }
    }

    /// <summary>
    /// 根据ID集合删除数据
    /// </summary>
    /// <param name="Ids">ID集合</param>
    public static void DelByIds(string Ids)
    {
        var list = FindByIds(Ids);

        if (list.Delete() > 0)
            Meta.Cache.Clear("");
    }

    #endregion
}