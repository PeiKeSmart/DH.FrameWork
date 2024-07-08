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

/// <summary>预存款充值表</summary>
public partial class PdRecharge : DHEntityBase<PdRecharge> {
    #region 对象操作
    static PdRecharge()
    {
        // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
        //var df = Meta.Factory.AdditionalFields;
        //df.Add(nameof(UId));

        // 过滤器 UserModule、TimeModule、IPModule
        Meta.Modules.Add<UserModule>();
        Meta.Modules.Add<TimeModule>();
        Meta.Modules.Add<IPModule>();
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
        // 货币保留6位小数
        Amount = Math.Round(Amount, 6);
        // 处理当前已登录用户信息，可以由UserModule过滤器代劳
        /*var user = ManageProvider.User;
        if (user != null)
        {
            if (isNew && !Dirtys[nameof(CreateUserID)]) CreateUserID = user.ID;
        }*/
        //if (isNew && !Dirtys[nameof(CreateTime)]) CreateTime = DateTime.Now;
        //if (isNew && !Dirtys[nameof(CreateIP)]) CreateIP = ManageProvider.UserHost;
    }

    ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //protected override void InitData()
    //{
    //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
    //    if (Meta.Session.Count > 0) return;

    //    if (XTrace.Debug) XTrace.WriteLine("开始初始化PdRecharge[预存款充值表]数据……");

    //    var entity = new PdRecharge();
    //    entity.Id = 0;
    //    entity.Sn = "abc";
    //    entity.UId = 0;
    //    entity.UName = "abc";
    //    entity.Amount = 0.0;
    //    entity.PCode = "abc";
    //    entity.TradeSn = "abc";
    //    entity.State = true;
    //    entity.PayTime = DateTime.Now;
    //    entity.CreateUser = "abc";
    //    entity.CreateUserID = 0;
    //    entity.CreateIP = "abc";
    //    entity.CreateTime = DateTime.Now;
    //    entity.Insert();

    //    if (XTrace.Debug) XTrace.WriteLine("完成初始化PdRecharge[预存款充值表]数据！");
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
    public static PdRecharge FindById(Int32 id)
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
    public static IList<PdRecharge> FindAllByUId(Int32 uId)
    {
        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.UId == uId);

        return FindAll(_.UId == uId);
    }

    /// <summary>
    /// 根据条件查询充值记录
    /// </summary>
    /// <param name="key"></param>
    /// <param name="STime"></param>
    /// <param name="ETime"></param>
    /// <param name="State"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static IEnumerable<PdRecharge> FindByTime(string key, DateTime STime, DateTime ETime, int State, PageParameter p)
    {
        if (Meta.Session.Count < 1000)
        {
            var list = Meta.Cache.FindAll(x => (key.IsNullOrWhiteSpace() || x.UName.Contains(key)) && (STime == DateTime.MinValue || x.CreateTime > STime) && (ETime == DateTime.MinValue || x.CreateTime < ETime) && (State == 0 || x.State == (State == 1))).OrderByDescending(x => x.CreateTime);
            p.TotalCount = list.Count();
            return list.Skip(--p.PageIndex * p.PageSize).Take(p.PageSize);
        }
        var exp = new WhereExpression();
        if (key.IsNotNullAndWhiteSpace()) exp &= _.UName.Contains(key);
        if (STime != DateTime.MinValue) exp &= _.CreateTime > STime;
        if (ETime != DateTime.MinValue) exp &= _.CreateTime < ETime;
        if (State != 0) exp &= _.State == (State == 1);
        return FindAll(exp, p);
    }

    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="uId">会员ID</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<PdRecharge> Search(Int32 uId, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (uId >= 0) exp &= _.UId == uId;
        if (!key.IsNullOrEmpty()) exp &= _.Sn.Contains(key) | _.UName.Contains(key) | _.PCode.Contains(key) | _.TradeSn.Contains(key) | _.CreateUser.Contains(key) | _.CreateIP.Contains(key);

        return FindAll(exp, page);
    }

    /// <summary>
    /// 根据会员名称创建时间处理人ID
    /// </summary>
    /// <param name="mname"></param>
    /// <param name="UId"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static IEnumerable<PdRecharge> Searchs(string mname, int UId, PageParameter p)
    {
        if (Meta.Session.Count < 1000)
        {
            var list = Meta.Cache.FindAll(e => e.UId == UId);
            if (mname.IsNotNullAndWhiteSpace())
            {
                list = list.Where(x => x.UName.Contains(mname) || x.PCode.Contains(mname)).ToList();
            }

            p.TotalCount = list.Count();

            return list.Skip((p.PageIndex - 1) * p.PageSize).Take(p.PageSize);
        }

        var exp = new WhereExpression();
        if (mname.IsNotNullAndWhiteSpace())
        {
            exp &= _.UName.Contains(mname);
        }

        exp &= _.UId == UId;


        return FindAll(exp, p);
    }

    /// <summary>高级查询</summary>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <param name="eTime">截止时间</param>
    /// <param name="sTime">开始时间</param>
    /// <param name="uName">会员名称</param>
    /// <param name="PayState">支付状态</param>
    /// <returns>实体列表</returns>
    public static IList<PdRecharge> Search(String uName, DateTime? sTime, DateTime? eTime, String PayState, PageParameter page)
    {
        var exp = new WhereExpression();

        if (!uName.IsNullOrWhiteSpace()) exp &= _.UName.Contains(uName);
        if (sTime.HasValue) exp &= _.CreateTime >= sTime.Value;
        if (eTime.HasValue) exp &= _.CreateTime < eTime.Value;

        if (!PayState.IsNullOrWhiteSpace())
        {
            exp &= _.State == PayState.ToBoolean();
        }

        return FindAll(exp, page);
    }


    // Select Count(Id) as Id,Category From DG_PdRecharge Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
    //static readonly FieldCache<PdRecharge> _CategoryCache = new FieldCache<PdRecharge>(nameof(Category))
    //{
    //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
    //};

    ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
    ///// <returns></returns>
    //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();
    #endregion

    #region 业务操作
    #endregion
}