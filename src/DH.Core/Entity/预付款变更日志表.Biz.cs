using DH.Extension;

using NewLife;
using NewLife.Data;

using Pek;

using XCode;
namespace DH.Entity;

/// <summary>预付款变更日志表</summary>
public partial class PdLog : DHEntityBase<PdLog> {
    #region 对象操作
    static PdLog()
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
        // 货币保留6位小数
        Amount = Math.Round(Amount, 6);
        FreezeAmount = Math.Round(FreezeAmount, 6);
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

    //    if (XTrace.Debug) XTrace.WriteLine("开始初始化PdLog[预付款变更日志表]数据……");

    //    var entity = new PdLog();
    //    entity.Id = 0;
    //    entity.UId = 0;
    //    entity.UName = "abc";
    //    entity.PdType = "abc";
    //    entity.Amount = 0.0;
    //    entity.FreezeAmount = 0.0;
    //    entity.Desc = "abc";
    //    entity.CreateUser = "abc";
    //    entity.CreateUserID = 0;
    //    entity.CreateTime = DateTime.Now;
    //    entity.CreateIP = "abc";
    //    entity.Insert();

    //    if (XTrace.Debug) XTrace.WriteLine("完成初始化PdLog[预付款变更日志表]数据！");
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
    public static PdLog FindById(Int32 id)
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
    public static IList<PdLog> FindAllByUId(Int32 uId)
    {
        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.UId == uId);

        return FindAll(_.UId == uId);
    }

    /// <summary>
    /// 查询所有的列表
    /// </summary>
    /// <returns></returns>
    public static IList<PdLog> GetAll()
    {
        // 实体缓存
        if (Meta.Session.Count < 1000) return FindAllWithCache();

        return FindAll();
    }

    /// <summary>
    /// 根据会员名称创建时间处理人ID
    /// </summary>
    /// <param name="mname"></param>
    /// <param name="stime"></param>
    /// <param name="etime"></param>
    /// <param name="aname"></param>
    /// <param name="p"></param>
    /// <returns></returns>

    public static IEnumerable<PdLog> Searchs(string mname, string stime, string etime, string aname, PageParameter p)
    {

        if (Meta.Session.Count < 1000)
        {
            var list = Meta.Cache.FindAll(e => (mname.IsNullOrWhiteSpace() || e.UName.Contains(mname))
            && ((stime.IsNullOrWhiteSpace() || etime.IsNullOrWhiteSpace()) || (e.CreateTime > stime.ToDateTime() && e.CreateTime < etime.ToDateTime()))
            && (aname.IsNullOrWhiteSpace() || e.CreateUser.Contains(aname))).OrderByDescending(e => e.CreateTime);
            p.TotalCount = list.Count();

            return list.Skip((p.PageIndex - 1) * p.PageSize).Take(p.PageSize);
        }

        var exp = new WhereExpression();
        if (mname.IsNotNullAndWhiteSpace())
        {
            exp &= _.UName.Contains(mname);
        }

        if (stime.IsNotNullAndWhiteSpace() && etime.IsNotNullAndWhiteSpace())
        {
            exp &= _.CreateTime > stime.ToDateTime() & _.CreateTime < etime.ToDateTime();
        }

        if (aname.IsNotNullAndWhiteSpace())
        {
            exp &= _.CreateUser.Contains(aname);
        }

        return FindAll(exp, p);

    }

    /// <summary>
    /// 根据用户ID查看收支明细
    /// </summary>
    /// <param name="key">关键字</param>
    /// <param name="Uid">用户Id</param>
    /// <param name="p">分页参数</param>
    /// <returns></returns>
    public static IEnumerable<PdLog> FindAmountByUid(String key, int Uid, PageParameter p)
    {
        if (Meta.Session.Count < 1000)
        {
            var list = Meta.Cache.FindAll(e => (key.IsNullOrWhiteSpace() || e.Id == key.ToInt() && e.UId == Uid) && e.Amount != 0).OrderByDescending(e => e.CreateTime);

            p.TotalCount = list.Count();

            return list.Skip((p.PageIndex - 1) * p.PageSize).Take(p.PageSize);
        }

        var exp = new WhereExpression();
        if (key.IsNotNullAndWhiteSpace())
        {
            exp &= _.Id == key;
        }

        exp &= _.UId == Uid;
        exp &= _.Amount != 0;

        return FindAll(exp, p);
    }

    /// <summary>
    /// 根据用户ID查看可以申请发票的收支明细
    /// </summary>
    /// <param name="key">关键字</param>
    /// <param name="Uid">用户Id</param>
    /// <param name="p">分页参数</param>
    /// <param name="Types">需要查询的类型(为空则查询所有的类型)</param>
    /// <param name="ApplyArr">已经申请过的Id</param>
    /// <param name="WhetherPage">是否分页</param>
    /// <returns></returns>
    public static IEnumerable<PdLog> FindAmountByTypeUid(String key, int Uid, PageParameter p, string Types, int[] ApplyArr, bool WhetherPage)
    {
        var arr = Types.SafeString().Trim(',').SplitString(",");
        if (Meta.Session.Count < 1000)
        {
            var list = Meta.Cache.FindAll(e => (key.IsNullOrWhiteSpace() || e.Id == key.ToInt() && e.UId == Uid) && e.Amount != 0 && !ApplyArr.Contains(e.Id) && (Types.IsNullOrWhiteSpace() || arr.Contains(e.PdType))).OrderByDescending(e => e.CreateTime);
            if (WhetherPage)
            {
                p.TotalCount = list.Count();
                return list.Skip((p.PageIndex - 1) * p.PageSize).Take(p.PageSize);
            }
            else
            {
                return list;
            }
        }

        var exp = new WhereExpression();
        if (key.IsNotNullAndWhiteSpace())
        {
            exp &= _.Id == key;
        }
        if (Types.IsNotNullAndWhiteSpace())
        {
            exp &= _.PdType.In(arr);
        }
        exp &= _.UId == Uid;
        exp &= _.Amount != 0;
        exp &= _.Id.NotIn(ApplyArr);
        if (WhetherPage)
            return FindAll(exp, p);
        else
            return FindAll(exp);
    }




    /// <summary>
    /// 根据用户Id查询消费日志
    /// </summary>
    /// <param name="Ids"></param>
    /// <returns></returns>
    public static IList<PdLog> FindByIds(string Ids)
    {
        var arr = Ids.Trim(',').SplitAsInt(",");

        if (Meta.Session.Count < 1000)
        {
            return Meta.Cache.FindAll(x => arr.Contains(x.Id));
        }

        return FindAll(_.Id.In(arr));
    }


    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="uId">会员ID</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<PdLog> Search(Int32 uId, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (uId >= 0) exp &= _.UId == uId;
        if (!key.IsNullOrEmpty()) exp &= _.UName.Contains(key) | _.PdType.Contains(key) | _.Desc.Contains(key) | _.CreateUser.Contains(key) | _.CreateIP.Contains(key);

        return FindAll(exp, page);
    }

    /// <summary>高级查询</summary>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <param name="eTime">截止时间</param>
    /// <param name="sTime">开始时间</param>
    /// <param name="uName">会员名称</param>
    /// <param name="aName">支付状态</param>
    /// <returns>实体列表</returns>
    public static IList<PdLog> Search(String uName, DateTime? sTime, DateTime? eTime, String aName, PageParameter page)
    {
        var exp = new WhereExpression();

        if (!uName.IsNullOrWhiteSpace()) exp &= _.UName.Contains(uName);
        if (sTime.HasValue) exp &= _.CreateTime >= sTime.Value;
        if (eTime.HasValue) exp &= _.CreateTime < eTime.Value;

        if (!aName.IsNullOrWhiteSpace())
        {
            exp &= _.AdminName.Contains(aName);
        }

        return FindAll(exp, page);
    }

    // Select Count(Id) as Id,Category From PdLog Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
    //static readonly FieldCache<PdLog> _CategoryCache = new FieldCache<PdLog>(nameof(Category))
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