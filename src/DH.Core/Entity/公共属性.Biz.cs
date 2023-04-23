using NewLife;

using System.Runtime.Serialization;
using System.Xml.Serialization;

using XCode.Membership;

namespace DH.Entity;

public partial class GenericAttribute : DHEntityBase<GenericAttribute>
{
    #region 对象操作
    static GenericAttribute()
    {
        // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
        //var df = Meta.Factory.AdditionalFields;
        //df.Add(nameof(EntityId));

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

    //    if (XTrace.Debug) XTrace.WriteLine("开始初始化GenericAttribute[公共属性]数据……");

    //    var entity = new GenericAttribute();
    //    entity.Key = "abc";
    //    entity.EntityId = 0;
    //    entity.KeyGroup = "abc";
    //    entity.Value = "abc";
    //    entity.StoreId = 0;
    //    entity.CreateUser = "abc";
    //    entity.CreateUserID = 0;
    //    entity.CreateTime = DateTime.Now;
    //    entity.CreateIP = "abc";
    //    entity.UpdateUser = "abc";
    //    entity.UpdateUserID = 0;
    //    entity.UpdateTime = DateTime.Now;
    //    entity.UpdateIP = "abc";
    //    entity.Insert();

    //    if (XTrace.Debug) XTrace.WriteLine("完成初始化GenericAttribute[公共属性]数据！");
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
    /// <summary>站点标识符</summary>
    [XmlIgnore, IgnoreDataMember]
    //[ScriptIgnore]
    public SiteInfo Store => Extends.Get(nameof(Store), k => SiteInfo.FindById(StoreId));

    /// <summary>站点标识符</summary>
    [XmlIgnore, IgnoreDataMember]
    public String StoreName => Store?.SiteName;

    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static GenericAttribute FindById(Int32 id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 10000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static IEnumerable<GenericAttribute> FindByEntityIdAndKeyGroup(Int32 entityId, String keyGroup)
    {
        if (entityId <= 0 || keyGroup.IsNullOrWhiteSpace()) return new List<GenericAttribute>();

        // 实体缓存
        if (Meta.Session.Count < 10000) return Meta.Cache.FindAll(e => e.EntityId == entityId && e.KeyGroup.EqualIgnoreCase(keyGroup));

        return FindAll(_.EntityId == entityId & _.KeyGroup == keyGroup);
    }
    #endregion

    #region 高级查询

    // Select Count(Id) as Id,Category From DH_GenericAttribute Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
    //static readonly FieldCache<GenericAttribute> _CategoryCache = new FieldCache<GenericAttribute>(nameof(Category))
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