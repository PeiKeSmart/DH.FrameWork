using NewLife;
using NewLife.Data;

using XCode;
using XCode.Cache;

namespace DH.Entity
{
    public partial class UrlRecord : DHEntityBase<UrlRecord>
    {
        #region 对象操作
        static UrlRecord()
        {
            // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(nameof(EntityId));

            // 过滤器 UserModule、TimeModule、IPModule
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

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化UrlRecord[SlugURL记录]数据……");

        //    var entity = new UrlRecord();
        //    entity.EntityId = 0;
        //    entity.EntityName = "abc";
        //    entity.Slug = "abc";
        //    entity.IsActive = true;
        //    entity.LanguageId = true;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化UrlRecord[SlugURL记录]数据！");
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
        public static UrlRecord FindById(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.Id == id);
        }

        /// <summary>根据分段名称查找</summary>
        /// <param name="slug">分段名称</param>
        /// <returns>实体列表</returns>
        public static IEnumerable<UrlRecord> FindAllBySlug(String slug)
        {
            // 实体缓存
            if (Meta.Session.Count < 10000) return Meta.Cache.FindAll(e => e.Slug.EqualIgnoreCase(slug)).OrderByDescending(l => l.IsActive).ThenBy(l => l.Id);

            return FindAll(_.Slug == slug, new PageParameter { PageSize = 0, OrderBy = "IsActive desc, Id asc" });
        }

        /// <summary>获取所有分段路由</summary>
        /// <returns>分段路由集合</returns>
        public static IList<UrlRecord> GetAll()
        {
            // 实体缓存
            if (Meta.Session.Count < 10000) return Meta.Cache.Entities;

            return FindAll();
        }

        /// <summary>获取所有语言</summary>
        /// <returns>语言集合</returns>
        public static IEnumerable<UrlRecord> GetAllUrlRecords(Int32 entityId, String entityName, Int32 languageId)
        {
            // 实体缓存
            if (Meta.Session.Count < 10000)
            {
                return Meta.Cache.FindAll(e => e.EntityId == entityId && e.EntityName == entityName && e.LanguageId == languageId && e.IsActive).OrderByDescending(l => l.Id);
            }

            return FindAll(_.EntityId == entityId & _.EntityName == entityName & _.LanguageId == languageId & _.IsActive, new PageParameter { PageSize = 0, OrderBy = "Id desc" });
        }
        #endregion

        #region 高级查询
        /// <summary>高级查询</summary>
        /// <param name="entityId">对应实体标识符</param>
        /// <param name="entityName">对应实体名称</param>
        /// <param name="slug">分段名称</param>
        /// <param name="languageId">语言标识符</param>
        /// <param name="key">关键字</param>
        /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
        /// <returns>实体列表</returns>
        public static IList<UrlRecord> Search(Int32 entityId, String entityName, String slug, Boolean? languageId, String key, PageParameter page)
        {
            var exp = new WhereExpression();

            if (entityId >= 0) exp &= _.EntityId == entityId;
            if (!entityName.IsNullOrEmpty()) exp &= _.EntityName == entityName;
            if (!slug.IsNullOrEmpty()) exp &= _.Slug == slug;
            if (languageId != null) exp &= _.LanguageId == languageId;
            if (!key.IsNullOrEmpty()) exp &= _.EntityName.Contains(key) | _.Slug.Contains(key);

            return FindAll(exp, page);
        }

        // Select Count(Id) as Id,Slug From DH_UrlRecord Where CreateTime>'2020-01-24 00:00:00' Group By Slug Order By Id Desc limit 20
        static readonly FieldCache<UrlRecord> _SlugCache = new FieldCache<UrlRecord>(nameof(Slug))
        {
            //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
        };

        /// <summary>获取分段名称列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
        /// <returns></returns>
        public static IDictionary<String, String> GetSlugList() => _SlugCache.FindAllName();
        #endregion

        #region 业务操作
        #endregion
    }
}