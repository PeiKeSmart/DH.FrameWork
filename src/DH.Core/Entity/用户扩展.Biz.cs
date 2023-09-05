using NewLife;
using NewLife.Data;

using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

using XCode;
using XCode.Membership;

namespace DH.Entity {
    public partial class UserDetail : DHEntityBase<UserDetail>
    {
        #region 对象操作
        static UserDetail()
        {
            // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
            var df = Meta.Factory.AdditionalFields;
            df.Add(nameof(Points));
            df.Add(nameof(ExpPoints));
            df.Add(nameof(AvailablePredeposit));
            df.Add(nameof(FreezePredeposit));
            df.Add(nameof(AvailableRcBalance));
            df.Add(nameof(FreezeRcBalance));

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
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Session.Count > 0) return;

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化UserDetail[用户扩展]数据……");

        //    var entity = new UserDetail();
        //    entity.Id = 0;
        //    entity.LanguageId = 0;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化UserDetail[用户扩展]数据！");
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
        /// <summary>语言</summary>
        [XmlIgnore, IgnoreDataMember]
        //[ScriptIgnore]
        public Language Language => Extends.Get(nameof(Language), k => Language.FindById(LanguageId));

        /// <summary>语言名称</summary>
        [Map(nameof(LanguageId), typeof(Language), "Id")]
        public String LanguageName => Language?.Name;

        /// <summary>用户</summary>
        [XmlIgnore, ScriptIgnore, IgnoreDataMember]
        //[ScriptIgnore]
        public User User => Extends.Get(nameof(User), k => User.FindByID(Id));

        /// <summary>
        /// 获取租户信息
        /// </summary>
        [XmlIgnore, ScriptIgnore, IgnoreDataMember]
        public Tenant Tenant => Extends.Get(nameof(Tenant), k => Tenant.FindById(TenantId));
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static UserDetail FindById(Int32 id)
        {
            if (id <= 0) return null;

            UserDetail model;

            // 实体缓存
            if (Meta.Session.Count < 1000)
            {
                model = Meta.Cache.Find(e => e.Id == id);
            }
            else
            {
                // 单对象缓存
                model = Meta.SingleCache[id];
            }

            if (model == null)
            {
                var model1 = User.FindByID(id);
                if (model1 != null)
                {
                    model = new UserDetail
                    {
                        Id = id
                    };
                    model.Insert();
                }
            }

            return model;
        }

        /// <summary>根据用户类型查找</summary>
        /// <param name="uType">用户类型</param>
        /// <returns>实体集合</returns>
        public static IEnumerable<UserDetail> FindAllByUType(Int16 uType)
        {
            if (uType <= 0) return new List<UserDetail>();

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.UType == uType);

            return FindAll(_.UType == uType);
        }

        /// <summary>根据用户类型查找</summary>
        /// <param name="uType">用户类型</param>
        /// <returns>实体集合数量</returns>
        public static Int64 FindCountByUType(Int16 uType)
        {
            if (uType <= 0) return 0;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.UType == uType).Count;

            return FindCount(_.UType == uType);
        }

        /// <summary>根据编号查找</summary>
        /// <returns>实体对象</returns>
        public static Boolean IsSuperAdmin()
        {
            var model = ManageProvider.User;

            if (model == null) return false;
            return FindById(model.ID)?.IsSuper == true;
        }

        /// <summary>根据部门Id查找</summary>
        /// <param name="dId">部门Id</param>
        /// <returns>实体集合数量</returns>
        public static Int64 FindCountByDepartmentId(Int32 dId)
        {
            if (dId <= 0) return 0;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => !e.DepartmentIds.IsNullOrWhiteSpace() && e.DepartmentIds.Contains($",{dId},")).Count;

            return FindCount(_.DepartmentIds.Contains($",{dId},"));
        }

        /// <summary>根据上级会员Id查找</summary>
        /// <param name="uId">部门Id</param>
        /// <returns>实体集合数量</returns>
        public static Int64 FindCountByParentUId(Int32 uId)
        {
            if (uId <= 0) return 0;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.ParentUId == uId).Count;

            return FindCount(_.ParentUId == uId);
        }

        /// <summary>根据用户Id集合查找</summary>
        /// <param name="ids">用户Id集合</param>
        /// <returns>实体集合</returns>
        public static IEnumerable<UserDetail> FindByIds(String ids)
        {
            if (ids.IsNullOrWhiteSpace()) return new List<UserDetail>();

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => ids.SplitAsInt(",").Contains(e.Id));

            return FindAll(_.Id.In(ids));
        }

        /// <summary>根据是否销售查找</summary>
        /// <param name="isSales">是否销售</param>
        /// <returns>实体集合</returns>
        public static IEnumerable<UserDetail> FindByIsSales(Boolean isSales)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.IsSales == isSales);

            return FindAll(_.IsSales == isSales);
        }

        /// <summary>获取全部用户数据</summary>
        /// <returns>实体集合</returns>
        public static IEnumerable<UserDetail> GetAll()
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Entities;

            return FindAll();
        }


    /// <summary>根据推荐人ID查找</summary>
    /// <param name="referrerId">推荐人ID</param>
    /// <returns>实体列表</returns>
    public static IList<UserDetail> FindAllByReferrerId(Int32 referrerId)
    {
        if (referrerId <= 0) return new List<UserDetail>();

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.ReferrerId == referrerId);

        return FindAll(_.ReferrerId == referrerId);
    }

    /// <summary>根据用户所属租户Id查找</summary>
    /// <param name="tenantId">用户所属租户Id</param>
    /// <returns>实体列表</returns>
    public static IList<UserDetail> FindAllByTenantId(Int32 tenantId)
    {
        if (tenantId <= 0) return new List<UserDetail>();

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.TenantId == tenantId);

        return FindAll(_.TenantId == tenantId);
    }

    /// <summary>根据所属销售ID查找</summary>
    /// <param name="keFuId">所属销售ID</param>
    /// <returns>实体列表</returns>
    public static IList<UserDetail> FindAllByKeFuId(Int32 keFuId)
    {
        if (keFuId <= 0) return new List<UserDetail>();

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.KeFuId == keFuId);

        return FindAll(_.KeFuId == keFuId);
    }

    /// <summary>根据所属上级会员ID查找</summary>
    /// <param name="parentUId">所属上级会员ID</param>
    /// <returns>实体列表</returns>
    public static IList<UserDetail> FindAllByParentUId(Int32 parentUId)
    {
        if (parentUId <= 0) return new List<UserDetail>();

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.ParentUId == parentUId);

        return FindAll(_.ParentUId == parentUId);
    }
        #endregion

        #region 高级查询

        /// <summary>高级查询</summary>
        /// <param name="uId">用户Id</param>
        /// <param name="key">关键字</param>
        /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
        /// <returns>实体列表</returns>
        public static IList<UserDetail> Search(Int32 uId, String key, PageParameter page)
        {
            var exp = new WhereExpression();

            if (uId >= 0) exp &= _.ParentUId == uId;

            if (!key.IsNullOrWhiteSpace())
            {
                exp &= _.Id.In(UserE.FindSQLWithKey(UserE._.Mobile.Contains(key) | UserE._.Mail.Contains(key) | UserE._.Name.Contains(key)));
            }

            return FindAll(exp, page);
        }

        // Select Count(Id) as Id,Category From DH_UserDetail Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
        //static readonly FieldCache<UserDetail> _CategoryCache = new FieldCache<UserDetail>(nameof(Category))
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
}