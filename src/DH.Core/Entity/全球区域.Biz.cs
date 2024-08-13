using DH.Caching;
using DH.Core;
using DH.Core.Domain.Localization;
using DH.Core.Infrastructure;
using DH.Models;

using NewLife;
using NewLife.Caching;
using NewLife.Data;

using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

using XCode;

namespace DH.Entity;

/// <summary>全球区域</summary>
public partial class Regions : DHEntityBase<Regions>
{
    #region 对象操作
    static Regions()
    {
        // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
        //var df = Meta.Factory.AdditionalFields;
        //df.Add(nameof(CId));

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
        Lng = Math.Round(Lng, 6);
        Lat = Math.Round(Lat, 6);
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

        // 检查唯一索引
        // CheckExist(isNew, nameof(AreaCode));
    }

    ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //protected override void InitData()
    //{
    //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
    //    if (Meta.Session.Count > 0) return;

    //    if (XTrace.Debug) XTrace.WriteLine("开始初始化Regions[全球区域]数据……");

    //    var entity = new Regions();
    //    entity.Id = 0;
    //    entity.CId = 0;
    //    entity.Level = 0;
    //    entity.ParentCode = 0;
    //    entity.AreaCode = 0;
    //    entity.ZipCode = "abc";
    //    entity.CityCode = "abc";
    //    entity.Name = "abc";
    //    entity.ShortName = "abc";
    //    entity.MergerName = "abc";
    //    entity.PinYin = "abc";
    //    entity.Lng = 0.0;
    //    entity.Lat = 0.0;
    //    entity.CreateUser = "abc";
    //    entity.CreateUserID = 0;
    //    entity.CreateTime = DateTime.Now;
    //    entity.CreateIP = "abc";
    //    entity.UpdateUser = "abc";
    //    entity.UpdateUserID = 0;
    //    entity.UpdateTime = DateTime.Now;
    //    entity.UpdateIP = "abc";
    //    entity.Insert();

    //    if (XTrace.Debug) XTrace.WriteLine("完成初始化Regions[全球区域]数据！");
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

    /// <summary>
    /// 获取父区域
    /// </summary>
    [XmlIgnore, ScriptIgnore, IgnoreDataMember]
    public Regions ParentRegions => Extends.Get(nameof(ParentRegions), k => Regions.FindByAreaCode(ParentCode));

    /// <summary>
    /// 获取区域短名称
    /// </summary>
    [XmlIgnore, ScriptIgnore, IgnoreDataMember]
    public String OtherShortName => Extends.Get(nameof(OtherShortName), k => OtherName.IsNullOrEmpty() ? ShortName : OtherName);

    /// <summary>
    /// 获取区域名称
    /// </summary>
    [XmlIgnore, ScriptIgnore, IgnoreDataMember]
    public String OtherAliasName => Extends.Get(nameof(OtherAliasName), k => AliasName.IsNullOrEmpty() ? Name : AliasName);

    /// <summary>
    ///父级名称集
    /// </summary>
    [XmlIgnore, ScriptIgnore]
    public String MergerNames { get; set; }
    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static Regions FindById(Int32? id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 10000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }


    /// <summary>根据国家编号查找</summary>
    /// <param name="Cid">编号</param>
    /// <returns>实体对象</returns>
    public static IList<Regions> FindByCId(Int32 Cid)
    {
        if (Cid <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 10000) return Meta.Cache.FindAll(e => e.CId == Cid);

        return FindAll(_.CId == Cid);
    }

    /// <summary>根据国家编号、层级查找</summary>
    /// <param name="cId">国家编号</param>
    /// <param name="level">层级</param>
    /// <returns>实体列表</returns>
    public static IEnumerable<Regions> FindAllByCIdAndLevel(Int32 cId, Int32 level)
    {
        var key = CacheKeyFiled.Instance.Get($"{nameof(Regions)}FindAllByCIdAndLevel{cId}_{level}");
        var _cache = Cache.Default;

        var result = _cache.GetOrAdd<IEnumerable<Regions>>(key, e =>
        {
            // 实体缓存
            if (Meta.Session.Count < 10000) return Meta.Cache.FindAll(e => e.CId == cId && e.Level == level).OrderBy(e => e.Id);

            return FindAll(_.CId == cId & _.Level == level, new PageParameter { Sort = _.Id, Desc = false });
        }, 3600);

        return result;
    }

    /// <summary>根据父级行政代码查找</summary>
    /// <param name="parentCode">父级行政代码</param>
    /// <returns>实体列表</returns>
    public static IList<Regions> FindAllByParentCode(Int64 parentCode)
    {
        // 实体缓存
        if (Meta.Session.Count < 10000) return Meta.Cache.FindAll(e => e.ParentCode == parentCode);

        return FindAll(_.ParentCode == parentCode);
    }

    /// <summary>根据所属父级Id查找</summary>
    /// <param name="Id">所属父级Id</param>
    /// <returns>实体列表</returns>
    public static IEnumerable<Regions> FindAllByParentId(Int32 Id)
    {
        if (Id < 0) return new List<Regions>();

        var model = FindById(Id);
        if (model == null) return new List<Regions>();

        // 实体缓存
        if (Meta.Session.Count < 10000) return Meta.Cache.FindAll(e => e.ParentCode == model.AreaCode).OrderBy(e => e.Id);

        return FindAll(_.ParentCode == model.ParentCode, new PageParameter { PageSize = 0, Sort = _.Id, Desc = false });
    }

    /// <summary>根据行政代码查找</summary>
    /// <param name="areaCode">行政代码</param>
    /// <returns>实体对象</returns>
    public static Regions FindByAreaCode(Int64 areaCode)
    {
        if (areaCode <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 10000) return Meta.Cache.Find(e => e.AreaCode == areaCode);

        return Find(_.AreaCode == areaCode);
    }

    /// <summary>
    /// 根据国家和层级和父级Code获取树状图
    /// </summary>
    /// <param name="parentCode">父编码</param>
    /// <param name="Level">树型层级</param>
    /// <returns></returns>
    public static IList<RegionsTree> FindProvinceTrees(Int64 parentCode = 0, Int32 Level = -1)
    {
        var localizationSettings = LocalizationSettings.Current;

        if (localizationSettings.IsEnable)
        {
            if (Level == -1)
            {
                var _workContext = EngineContext.Current.Resolve<IWorkContext>();

                var list1 = Country.FindAllWithCache().Where(e => e.IsEnabled).OrderBy(e => e.DisplayOrder).ThenBy(e => e.Id);
                var listTree1 = new List<RegionsTree>();
                foreach (var item in list1)
                {
                    var model = new RegionsTree();
                    model.id = item.Id;
                    model.name = CountryLan.FindNameByCIdAndLId(item.Id, _workContext.WorkingLanguage.Id);
                    model.pId = parentCode;

                    var listarea = FindAllByCIdAndLevel(item.Id, 0);
                    if (!listarea.Any())
                    {
                        model.isParent = false;
                    }
                    else
                    {
                        model.isParent = true;
                    }
                    listTree1.Add(model);
                }
                return listTree1;
            }
            else if (Level == 0)
            {
                var list1 = FindAllByCIdAndLevel(parentCode.ToInt(), 0).OrderBy(e => e.Id);
                return GetRegionsTree(list1);
            }
        }

        var list = FindAllByParentCode(parentCode).OrderBy(e => e.Id);
        return GetRegionsTree(list);
    }

    private static IList<RegionsTree> GetRegionsTree(IEnumerable<Regions> list, Int64 parentCode = 0)
    {
        var listTree = new List<RegionsTree>();
        foreach (var item in list)
        {
            var model = new RegionsTree();
            model.id = item.AreaCode;
            model.name = item.Name;
            model.pId = parentCode;
            if (item.Level + 1 == 2)
            {
                model.isParent = false;
            }
            else
            {
                var lIS = FindAllByParentCode(item.AreaCode);
                if (lIS.Count == 0)
                {
                    model.isParent = false;
                }
            }

            listTree.Add(model);
        }
        return listTree;
    }

    /// <summary>根据当前层级查找</summary>
    /// <param name="level">当前层级</param>
    /// <returns>实体列表</returns>
    public static IList<Regions> FindAllByLevel(Int32 level)
    {
        // 实体缓存
        if (Meta.Session.Count < 10000) return Meta.Cache.FindAll(e => e.Level == level);

        return FindAll(_.Level == level);
    }

    /// <summary>
    /// 根据列表Ids获取列表
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public static IList<Regions> FindByIds(String ids)
    {
        if (ids.IsNullOrWhiteSpace()) return new List<Regions>();

        ids = ids.Trim(',');

        if (Meta.Session.Count < 10000)
        {
            return Meta.Cache.FindAll(x => ids.SplitAsInt(",").Contains(x.Id));
        }

        return FindAll(_.Id.In(ids.Split(',')));
    }

    /// <summary>根据城市Id查找</summary>
    /// <param name="cityId">城市Id</param>
    /// <returns>实体列表</returns>
    public static IList<Regions> FindAllByCityId(Int32 cityId)
    {
        if (cityId <= 0) return new List<Regions>();

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.CityId == cityId);

        return FindAll(_.CityId == cityId);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="cId">国家编号</param>
    /// <param name="level">层级</param>
    /// <param name="parentCode">父级行政代码</param>
    /// <param name="areaCode">行政代码</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<Regions> Search(Int32 cId, Int32 level, Int64 parentCode, Int64 areaCode, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (cId >= 0) exp &= _.CId == cId;
        if (level >= 0) exp &= _.Level == level;
        if (parentCode >= 0) exp &= _.ParentCode == parentCode;
        if (areaCode >= 0) exp &= _.AreaCode == areaCode;
        if (!key.IsNullOrEmpty()) exp &= _.ZipCode.Contains(key) | _.CityCode.Contains(key) | _.Name.Contains(key) | _.ShortName.Contains(key) | _.MergerName.Contains(key) | _.PinYin.Contains(key) | _.CreateUser.Contains(key) | _.CreateIP.Contains(key) | _.UpdateUser.Contains(key) | _.UpdateIP.Contains(key);

        return FindAll(exp, page);
    }

    // Select Count(Id) as Id,Category From Regions Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
    //static readonly FieldCache<Regions> _CategoryCache = new FieldCache<Regions>(nameof(Category))
    //{
    //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
    //};

    ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
    ///// <returns></returns>
    //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();
    #endregion

    #region 业务操作
    /// <summary>
    /// 获取区域等级
    /// </summary>
    /// <param name="Level">层级</param>
    /// <returns></returns>
    public static String GetLevel(Int32 Level)
    {
        switch (Level)
        {
            case 0:
                return LocaleStringResource.GetResource("省");

            case 1:
                return LocaleStringResource.GetResource("市");

            case 2:
                return LocaleStringResource.GetResource("区县");

            default:
                return String.Empty;
        }
    }

    /// <summary>
    /// 根据行政编码查询所属子集数据
    /// </summary>
    /// <param name="Code"></param>
    /// <returns></returns>
    public static IList<Regions> FindByParentCode(Int64 Code)
    {
        if (Code <= 0) return new List<Regions>();

        var exp = new WhereExpression();
        exp &= _.ParentCode == Code;
        return FindAll(exp);
    }

    /// <summary>
    /// 根据ID集合删除数据
    /// </summary>
    /// <param name="Ids">ID集合</param>
    public static void DelByIds(String Ids)
    {
        if (Ids.IsNullOrWhiteSpace()) return;

        if (Delete(Ids) > 0)
            Meta.Cache.Clear("");
    }

    /// <summary>根据名称查找</summary>
    /// <param name="name">设备DeviceName</param>
    /// <returns>实体对象</returns>
    public static Regions FindByName(String name)
    {
        if (name.IsNullOrWhiteSpace()) return null;

        if (Meta.Session.Count < 10000) return Meta.Cache.Find(e => e.Name == name);

        return Find(_.Name == name);
    }
    #endregion
}