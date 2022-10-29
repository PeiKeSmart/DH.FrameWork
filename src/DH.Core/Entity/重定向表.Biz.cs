using NewLife;
using NewLife.Data;
using NewLife.Log;

using System.ComponentModel;

using XCode;
using XCode.Membership;

namespace DH.Entity
{
    /// <summary>重定向表</summary>
    public partial class RouteRewrite : DHEntityBase<RouteRewrite>
    {
        #region 对象操作
        static RouteRewrite()
        {
            // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(nameof(ParentId));

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

            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            if (Name.IsNullOrEmpty()) throw new ArgumentNullException(nameof(Name), "名称不能为空！");
            if (RegexInfo.IsNullOrEmpty()) throw new ArgumentNullException(nameof(RegexInfo), "正则表达式不能为空！");
            if (ReplacementInfo.IsNullOrEmpty()) throw new ArgumentNullException(nameof(ReplacementInfo), "uri匹配实际路径不能为空！");

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

        /// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void InitData()
        {
            // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
            if (Meta.Session.Count > 0) return;

            if (XTrace.Debug) XTrace.WriteLine("开始初始化RouteRewrite[重定向表]数据……");

            var entity = new RouteRewrite();
            entity.Name = "首页";
            entity.RegexInfo = @"^{language}/Index.html";
            entity.ReplacementInfo = "/{language}/";
            entity.ParentId = 0;
            entity.Insert();

            if (XTrace.Debug) XTrace.WriteLine("完成初始化RouteRewrite[重定向表]数据！");
        }

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
        public static RouteRewrite FindById(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.Id == id);
        }

        /// <summary>获取所有数据</summary>
        /// <returns>实体集合</returns>
        public static IList<RouteRewrite> GetAll()
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return FindAllWithCache();

            return FindAll();
        }
        #endregion

        #region 高级查询

        // Select Count(Id) as Id,Category From DG_RouteRewrite Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
        //static readonly FieldCache<RouteRewrite> _CategoryCache = new FieldCache<RouteRewrite>(nameof(Category))
        //{
        //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
        //};

        ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
        ///// <returns></returns>
        //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="page">分页</param>
        /// <returns></returns>
        public static IEnumerable<RouteRewrite> Searchs(string key, PageParameter page)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000)
            {
                IEnumerable<RouteRewrite> list;

                list = FindAllWithCache();
                if (!key.IsNullOrWhiteSpace())
                {
                    list = list.Where(e => e.Name.Contains(key) || e.ReplacementInfo.Contains(key));
                }
                page.TotalCount = list.Count();
                list = list.OrderByDescending(e => e.Id).Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize);
                return list;
            }

            var exp = new WhereExpression();
            if (!key.IsNullOrWhiteSpace())
            {
                exp &= _.Name.Contains(key) | _.ReplacementInfo.Contains(key);
            }

            return FindAll(exp, page);
        }


        /// <summary>
        /// 根据ID集合删除数据
        /// </summary>
        /// <param name="Ids">ID集合</param>
        public static void DelByIds(String Ids)
        {
            if (Delete(_.Id.In(Ids.Trim(','))) > 0)
                Meta.Cache.Clear("");
        }


        /// <summary></summary>
        /// <param name="ParentId">父级Id</param>
        /// <returns>实体列表</returns>
        public static IList<RouteRewrite> FindAllByParentId(Int32 ParentId)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.ParentId == ParentId);

            return FindAll(_.ParentId == ParentId);
        }

        #endregion

        #region 业务操作

        #endregion
    }
}