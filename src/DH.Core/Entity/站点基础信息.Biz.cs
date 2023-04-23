using DH.Core;
using DH.Core.Infrastructure;

using NewLife;
using NewLife.Log;

using System.ComponentModel;
using System.Reflection;

using XCode.Membership;

namespace DH.Entity {
    public partial class SiteInfo : DHEntityBase<SiteInfo>
    {
        #region 对象操作
        static SiteInfo()
        {
            // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(nameof(CreateUserID));

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
            if (SiteName.IsNullOrEmpty()) throw new ArgumentNullException(nameof(SiteName), "网站名称不能为空！");

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

            if (XTrace.Debug) XTrace.WriteLine("开始初始化SiteInfo[站点基础信息]数据……");

            var entity = new SiteInfo();
            entity.SiteName = "湖北登灏-猿人易";
            entity.SeoTitle = "湖北登灏-猿人易";
            entity.SeoKey = "湖北登灏-猿人易";
            entity.SeoDescribe = "湖北登灏-猿人易";
            entity.Status = 1;
            entity.Url = $"{DHSetting.Current.CurDomainUrl.TrimEnd('/')}/";
            entity.Registration = "粤ICP备10000000号";

            var asm = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            if (asm != null)
            {
                var att = asm.GetCustomAttribute<AssemblyCopyrightAttribute>();
                if (att != null)
                {
                    entity.SiteCopyright = att.Copyright;
                }
            }

            entity.Insert();

            if (XTrace.Debug) XTrace.WriteLine("完成初始化SiteInfo[站点基础信息]数据！");
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
        public static SiteInfo FindById(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.Id == id);
        }

        /// <summary>获取默认站点</summary>
        /// <returns>实体对象</returns>
        public static SiteInfo FindDefault()
        {
            if (DHSetting.Current.SiteId == 0)
            {
                DHSetting.Current.SiteId = 1;
                DHSetting.Current.Save();
            }

            var modelSite = FindById(DHSetting.Current.SiteId);
            if (modelSite == null)
            {
                modelSite = FindAllWithCache().OrderBy(e => e.Id).FirstOrDefault();
            }

            return modelSite;
        }

        /// <summary>获取默认站点SEO数据</summary>
        /// <returns>SEO元素数据</returns>
        public static (String SiteName, String SeoTitle, String SeoKey, String SeoDescribe, String Registration, String SiteCopyright) GetDefaultSeo()
        {
            if (DHSetting.Current.SiteId == 0)
            {
                DHSetting.Current.SiteId = 1;
                DHSetting.Current.Save();
            }

            var workContext = EngineContext.Current.Resolve<IWorkContext>();

            return SiteInfoLan.FindBySIdAndLId(DHSetting.Current.SiteId, workContext.WorkingLanguage.Id, true);
        }
        #endregion

        #region 高级查询

        // Select Count(Id) as Id,Category From DG_SiteInfo Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
        //static readonly FieldCache<SiteInfo> _CategoryCache = new FieldCache<SiteInfo>(nameof(Category))
        //{
        //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
        //};

        ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
        ///// <returns></returns>
        //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();
        #endregion

        #region 业务操作
        /// <summary>
        /// 指示站点是否包含指定的主机
        /// </summary>
        /// <param name="store">站点</param>
        /// <param name="host">主机</param>
        /// <returns>true - 包含, false - 否</returns>
        public static Boolean ContainsHostValue(SiteInfo store, String host)
        {
            if (store == null)
                throw new ArgumentNullException(nameof(store));

            if (string.IsNullOrEmpty(host))
                return false;

            var contains = ParseHostValues(store).Any(x => x.Equals(host, StringComparison.InvariantCultureIgnoreCase));

            return contains;
        }

        /// <summary>
        /// 解析以逗号分隔的主机
        /// </summary>
        /// <param name="store">站点</param>
        /// <returns>逗号分隔的主机</returns>
        public static string[] ParseHostValues(SiteInfo store)
        {
            if (store == null)
                throw new ArgumentNullException(nameof(store));

            var parsedValues = new List<string>();
            if (string.IsNullOrEmpty(store.Hosts))
                return parsedValues.ToArray();

            var hosts = store.Hosts.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var host in hosts)
            {
                var tmp = host.Trim();
                if (!string.IsNullOrEmpty(tmp))
                    parsedValues.Add(tmp);
            }

            return parsedValues.ToArray();
        }
        #endregion
    }
}