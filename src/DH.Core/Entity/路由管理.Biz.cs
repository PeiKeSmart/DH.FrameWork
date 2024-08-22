using NewLife;
using NewLife.Data;

using Pek;

using XCode;
using XCode.Cache;

namespace DH.Entity;

/// <summary>路由管理</summary>
public partial class SystemRout : DHEntityBase<SystemRout>
{
    #region 对象操作
    static SystemRout()
    {

        // 过滤器 UserModule、TimeModule、IPModule
        Meta.Modules.Add<TimeModule>();
    }

    /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
    /// <param name="isNew">是否插入</param>
    public override void Valid(Boolean isNew)
    {
        // 如果没有脏数据，则不需要进行任何处理
        if (!HasDirty) return;

        // 在新插入数据或者修改了指定字段时进行修正
        //if (isNew && !Dirtys[nameof(CreateTime)]) CreateTime = DateTime.Now;
        //if (!Dirtys[nameof(UpdateTime)]) UpdateTime = DateTime.Now;

        // 检查唯一索引
        // CheckExist(isNew, nameof(Url));
    }

    ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //protected override void InitData()
    //{
    //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
    //    if (Meta.Session.Count > 0) return;

    //    if (XTrace.Debug) XTrace.WriteLine("开始初始化SystemRout[路由管理]数据……");

    //    var entity = new SystemRout();
    //    entity.Id = 0;
    //    entity.RType = 0;
    //    entity.Name = "abc";
    //    entity.Url = "abc";
    //    entity.Parms = "abc";
    //    entity.Pages = "abc";
    //    entity.AreaName = "abc";
    //    entity.ControllerName = "abc";
    //    entity.ActionName = "abc";
    //    entity.FromUrl = "abc";
    //    entity.CreateTime = DateTime.Now;
    //    entity.UpdateTime = DateTime.Now;
    //    entity.Insert();

    //    if (XTrace.Debug) XTrace.WriteLine("完成初始化SystemRout[路由管理]数据！");
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
    public static SystemRout FindById(Int32 id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据映射路由查找</summary>
    /// <param name="fromUrl">映射路由</param>
    /// <returns>实体列表</returns>
    public static IList<SystemRout> FindAllByFromUrl(String fromUrl)
    {
        if (fromUrl.IsNullOrWhiteSpace()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.FromUrl == fromUrl);

        return FindAll(_.FromUrl == fromUrl);
    }

    /// <summary>根据映射路由、类型查找</summary>
    /// <param name="rtype">类型</param>
    /// <param name="fromurl">映射路由</param>
    /// <returns>实体对象</returns>
    public static SystemRout FindByRTypeAndFromUrl(Byte rtype, String fromurl)
    {
        if (fromurl.IsNullOrWhiteSpace()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.RType == rtype && e.FromUrl == fromurl);

        return Find(_.RType == rtype & _.FromUrl == fromurl);
    }

    /// <summary>根据Url路由查找</summary>
    /// <param name="url">Url路由</param>
    /// <returns>实体对象</returns>
    public static SystemRout FindByUrl(String url)
    {
        if (url.IsNullOrWhiteSpace()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Url == url);

        return Find(_.Url == url);
    }

    /// <summary>根据类型、Url路由查找</summary>
    /// <param name="rtype">类型</param>
    /// <param name="url">Url路由</param>
    /// <returns>实体对象</returns>
    public static SystemRout FindByRTypeAndUrl(Byte rtype, String url)
    {
        if (url.IsNullOrWhiteSpace()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.RType == rtype && e.Url == url);

        return Find(_.RType == rtype & _.Url == url);
    }

    /// <summary>根据类型、Url路由匹配查找</summary>
    /// <param name="rtype">类型</param>
    /// <param name="url">Url路由</param>
    /// <returns>实体对象</returns>
    public static (SystemRout route, IDictionary<String, String> dic) FindByRTypeAndRegexUrl(Byte rtype, String url)
    {
        if (url.IsNullOrWhiteSpace()) return (null, null);

        var urlArray = url.TrimStart('/').Split('/');
        var dic = new Dictionary<String, String>();

        // 实体缓存
        if (Meta.Session.Count < 1000)
        {
            var model = Meta.Cache.Find(e => e.RType == rtype && e.FromUrl.SafeString().Length > 0 && url.Contains(e.Url, StringComparison.OrdinalIgnoreCase) && urlArray.Length == e.FromUrl.Split('/').Length);

            if (model == null) return (null, null);

            var fromurlArray = model.FromUrl.Split('/');
            for (var i = 0; i < fromurlArray.Length; i++)
            {
                if (fromurlArray[i].Contains("{"))
                {
                    var name = fromurlArray[i].Replace("{", "").Replace("}", "");
                    dic[name] = urlArray[i];
                }
            }

            return (model, dic);
        }
        else
        {
            var list = FindAll(_.FromUrl.NotIsNullOrEmpty());
            foreach (var item in list)
            {
                var fromurlArray = item.FromUrl.Split('/');
                if (url.Contains(item.Url, StringComparison.OrdinalIgnoreCase) && urlArray.Length == fromurlArray.Length)
                {
                    for (var i = 0; i < fromurlArray.Length; i++)
                    {
                        if (fromurlArray[i].Contains("{"))
                        {
                            var name = fromurlArray[i].Replace("{", "").Replace("}", "");
                            dic[name] = urlArray[i];
                        }
                    }

                    return (item, dic);
                }
            }
        }

        return (null, null);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="url">Url路由</param>
    /// <param name="fromUrl">映射路由</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<SystemRout> Search(String url, String fromUrl, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (!url.IsNullOrEmpty()) exp &= _.Url == url;
        if (!fromUrl.IsNullOrEmpty()) exp &= _.FromUrl == fromUrl;
        if (!key.IsNullOrEmpty()) exp &= _.Name.Contains(key) | _.Parms.Contains(key) | _.Pages.Contains(key) | _.AreaName.Contains(key) | _.ControllerName.Contains(key) | _.ActionName.Contains(key);

        return FindAll(exp, page);
    }

    // Select Count(Id) as Id,FromUrl From SystemRout Where CreateTime>'2020-01-24 00:00:00' Group By FromUrl Order By Id Desc limit 20
    static readonly FieldCache<SystemRout> _FromUrlCache = new FieldCache<SystemRout>(nameof(FromUrl))
    {
        //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
    };

    /// <summary>获取映射路由列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
    /// <returns></returns>
    public static IDictionary<String, String> GetFromUrlList() => _FromUrlCache.FindAllName();
    #endregion

    #region 业务操作
    #endregion
}