using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using NewLife.Log;
using NewLife.Model;
using NewLife.Reflection;
using NewLife.Threading;
using NewLife.Web;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;
using XCode.Membership;
using XCode.Shards;

namespace DH.Entity;

/// <summary>会员积分日志表</summary>
public partial class PointsLog : DHEntityBase<PointsLog> {
    #region 对象操作
    static PointsLog()
    {
        // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
        //var df = Meta.Factory.AdditionalFields;
        //df.Add(nameof(UId));

        // 过滤器 UserModule、TimeModule、IPModule
        Meta.Modules.Add<UserModule>();
        Meta.Modules.Add<TimeModule>();
        Meta.Modules.Add<IPModule>();
    }

    /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
    /// <param name="isNew">是否插入</param>
    public override void Valid(Boolean isNew)
    {
        // 如果没有脏数据，则不需要进行任何处理
        if (!HasDirty) return;

        // 在新插入数据或者修改了指定字段时进行修正
        // 处理当前已登录用户信息，可以由UserModule过滤器代劳
        /*var user = ManageProvider.User;
        if (user != null)
        {
            if (isNew && !Dirtys[nameof(CreateUserID)]) CreateUserID = user.ID;
            if (!Dirtys[nameof(UpdateUserID)]) UpdateUserID = user.ID;
        }*/
        //if (isNew && !Dirtys[nameof(CreateTime)]) CreateTime = DateTime.Now;
        //if (!Dirtys[nameof(UpdateTime)]) UpdateTime = DateTime.Now;
        //if (isNew && !Dirtys[nameof(CreateIP)]) CreateIP = ManageProvider.UserHost;
        //if (!Dirtys[nameof(UpdateIP)]) UpdateIP = ManageProvider.UserHost;
    }

    ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //protected override void InitData()
    //{
    //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
    //    if (Meta.Session.Count > 0) return;

    //    if (XTrace.Debug) XTrace.WriteLine("开始初始化PointsLog[会员积分日志表]数据……");

    //    var entity = new PointsLog();
    //    entity.Id = 0;
    //    entity.UId = 0;
    //    entity.UName = "abc";
    //    entity.Points = 0;
    //    entity.Desc = "abc";
    //    entity.Stage = "abc";
    //    entity.CreateUser = "abc";
    //    entity.CreateUserID = 0;
    //    entity.CreateTime = DateTime.Now;
    //    entity.CreateIP = "abc";
    //    entity.UpdateUser = "abc";
    //    entity.UpdateUserID = 0;
    //    entity.UpdateTime = DateTime.Now;
    //    entity.UpdateIP = "abc";
    //    entity.Insert();

    //    if (XTrace.Debug) XTrace.WriteLine("完成初始化PointsLog[会员积分日志表]数据！");
    //}

    ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
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

    #region 扩展属性
    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static PointsLog FindById(Int32 id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据会员ID查找</summary>
    /// <param name="uId">会员ID</param>
    /// <returns>实体列表</returns>
    public static IList<PointsLog> FindAllByUId(Int32 uId)
    {
        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.UId == uId);

        return FindAll(_.UId == uId);
    }

    /// <summary>根据会员ID和积分操作阶段查找</summary>
    /// <param name="uId">会员ID</param>
    /// <param name="stage">积分操作阶段</param>
    /// <returns>实体列表</returns>
    public static IList<PointsLog> FindAllByUIdAndStage(Int32 uId, String stage)
    {
        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.UId == uId && e.Stage == stage);

        return FindAll(_.UId == uId & _.Stage == stage);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="uId">会员ID</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<PointsLog> Search(Int32 uId, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (uId >= 0) exp &= _.UId == uId;
        if (!key.IsNullOrEmpty()) exp &= _.UName.Contains(key) | _.Desc.Contains(key) | _.Stage.Contains(key) | _.CreateUser.Contains(key) | _.CreateIP.Contains(key);

        return FindAll(exp, page);
    }

    /// <summary>高级查询</summary>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <param name="adminName">管理员名称</param>
    /// <param name="desc">描述</param>
    /// <param name="eTime">截止时间</param>
    /// <param name="stage">操作阶段</param>
    /// <param name="sTime">开始时间</param>
    /// <param name="uName">会员名称</param>
    /// <returns>实体列表</returns>
    public static IList<PointsLog> Search(String uName, DateTime? sTime, DateTime? eTime, String stage, String adminName, String desc, PageParameter page)
    {
        var exp = new WhereExpression();

        if (!uName.IsNullOrWhiteSpace()) exp &= _.UName.Contains(uName);
        if (sTime.HasValue) exp &= _.CreateTime >= sTime.Value;
        if (eTime.HasValue) exp &= _.CreateTime < eTime.Value;
        if (!stage.IsNullOrWhiteSpace()) exp &= _.Stage == stage;
        if (!adminName.IsNullOrWhiteSpace()) exp &= _.AdminName.Contains(adminName);
        if (!desc.IsNullOrWhiteSpace()) exp &= _.Desc.Contains(desc);

        return FindAll(exp, page);
    }

    // Select Count(Id) as Id,Category From PointsLog Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
    //static readonly FieldCache<PointsLog> _CategoryCache = new FieldCache<PointsLog>(nameof(Category))
    //{
    //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
    //};

    ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
    ///// <returns></returns>
    //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();
    #endregion

    #region 业务操作
    /// <summary>
    /// 添加积分日志
    /// </summary>
    /// <param name="uId">会员ID</param>
    /// <param name="userName">会员名称</param>
    /// <param name="points">积分数,负数为扣除</param>
    /// <param name="desc">积分操作描述</param>
    /// <param name="stage">积分操作阶段</param>
    /// <returns></returns>
    public static Int32 AddPointsLog(Int32 uId, String userName, Int32 points, String desc, String stage)
    {
        var model = new PointsLog();
        model.UId = uId;
        model.UName = userName;
        model.Points = points;
        model.Desc = desc;
        model.Stage = stage;

        return model.Insert();
    }
    #endregion
}