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

/// <summary>操作日志</summary>
public partial class UserLog : DHEntityBase<UserLog> {
    #region 对象操作
    static UserLog()
    {
        // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
        //var df = Meta.Factory.AdditionalFields;
        //df.Add(nameof(CreateUserID));

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

    //    if (XTrace.Debug) XTrace.WriteLine("开始初始化UserLog[操作日志]数据……");

    //    var entity = new UserLog();
    //    entity.Id = 0;
    //    entity.Content = "abc";
    //    entity.Url = "abc";
    //    entity.CreateUser = "abc";
    //    entity.CreateUserID = 0;
    //    entity.CreateTime = DateTime.Now;
    //    entity.CreateIP = "abc";
    //    entity.Insert();

    //    if (XTrace.Debug) XTrace.WriteLine("完成初始化UserLog[操作日志]数据！");
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
    public static UserLog FindById(Int32 id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }
    #endregion

    #region 高级查询


    //exp &= _.CreateTime.Between(start, end);
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="name"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    public static IEnumerable<UserLog> Searchs(string name, string start, string end, PageParameter page)
    {
        // 实体缓存
        if (Meta.Session.Count < 1000)
        {
            IEnumerable<UserLog> list;

            list = FindAllWithCache();
            if (name.IsNotNullAndWhiteSpace())
            {
                list = list.Where(e => e.CreateUser.Contains(name));
            }
            if (start.IsNotNullAndWhiteSpace())
            {
                list = list.Where(x => x.CreateTime >= start.ToDateTime());
            }
            if (end.IsNotNullAndWhiteSpace())
            {
                list = list.Where(x => x.CreateTime <= end.ToDateTime());
            }
            page.TotalCount = list.Count();

            list = list.OrderByDescending(e => e.Id).Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize);
            return list;
        }
        var exp = new WhereExpression();
        if (name.IsNotNullAndWhiteSpace())
        {
            exp &= _.CreateUser.Contains(name);
        }
        exp &= _.CreateTime.Between(start.ToDateTime(), end.ToDateTime());
        return FindAll(exp, page);
    }

    //exp &= _.CreateTime.Between(start, end);
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="name"></param>
    /// <param name="page"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="Ids"></param>
    /// <returns></returns>
    public static IEnumerable<UserLog> SearchsIds(string Ids, string name, string start, string end, PageParameter page)
    {
        // 实体缓存
        if (Meta.Session.Count < 1000)
        {
            IEnumerable<UserLog> list;

            list = FindAllWithCache();
            list = list.Where(x => Ids.SplitAsInt(",").Contains(x.CreateUserID));
            if (name.IsNotNullAndWhiteSpace())
            {
                list = list.Where(e => e.CreateUser.Contains(name));
            }
            if (start.IsNotNullAndWhiteSpace())
            {
                list = list.Where(x => x.CreateTime >= start.ToDateTime());
            }
            if (end.IsNotNullAndWhiteSpace())
            {
                list = list.Where(x => x.CreateTime <= end.ToDateTime());
            }
            page.TotalCount = list.Count();

            list = list.OrderByDescending(e => e.Id).Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize);
            return list;
        }
        var exp = new WhereExpression();
        exp &= _.CreateUserID.In(Ids.Split(','));
        if (name.IsNotNullAndWhiteSpace())
        {
            exp &= _.CreateUser.Contains(name);
        }
        if (start.IsNotNullAndWhiteSpace() && end.IsNotNullAndWhiteSpace())
        {
            exp &= _.CreateTime.Between(start.ToDateTime(), end.ToDateTime());
        }

        return FindAll(exp, page);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="name"></param>
    /// <param name="page"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="Ids"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IEnumerable<UserLog> SearchsIds(string Ids, string name, string key, string start, string end, PageParameter page)
    {
        // 实体缓存
        if (Meta.Session.Count < 1000)
        {
            IEnumerable<UserLog> list;

            list = FindAllWithCache();
            list = list.Where(x => Ids.SplitAsInt(",").Contains(x.CreateUserID));
            if (name.IsNotNullAndWhiteSpace())
            {
                list = list.Where(e => e.CreateUser.Contains(name));
            }
            if (start.IsNotNullAndWhiteSpace())
            {
                list = list.Where(x => x.CreateTime >= start.ToDateTime());
            }
            if (end.IsNotNullAndWhiteSpace())
            {
                list = list.Where(x => x.CreateTime <= end.ToDateTime());
            }
            if (key.IsNotNullAndWhiteSpace())
            {
                list = list.Where(x => x.Content.Contains(key));
            }
            page.TotalCount = list.Count();

            list = list.OrderByDescending(e => e.Id).Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize);
            return list;
        }
        var exp = new WhereExpression();
        exp &= _.CreateUserID.In(Ids.Split(','));
        if (name.IsNotNullAndWhiteSpace())
        {
            exp &= _.CreateUser.Contains(name);
        }
        if (key.IsNotNullAndWhiteSpace())
        {
            exp &= _.Content.Contains(key);
        }
        if (start.IsNotNullAndWhiteSpace() && end.IsNotNullAndWhiteSpace())
        {
            exp &= _.CreateTime.Between(start.ToDateTime(), end.ToDateTime());
        }

        return FindAll(exp, page);
    }

    /// <summary>
    /// 根据ID集合删除数据
    /// </summary>
    /// <param name="Ids">ID集合</param>
    public static void DelByNIds(String Ids)
    {
        if (Delete(_.Id.In(Ids.Trim(','))) > 0)
            Meta.Cache.Clear("");
    }


    ///// <summary>
    ///// 获取所有数据
    ///// </summary>
    ///// <returns></returns>
    //public static IList<UserLog> GetAll()
    //{
    //    if (Meta.Session.Count < 1000) return FindAllWithCache();

    //    return FindAll();
    //}

    // Select Count(Id) as Id,Category From UserLog Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
    //static readonly FieldCache<UserLog> _CategoryCache = new FieldCache<UserLog>(nameof(Category))
    //{
    //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
    //};

    ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
    ///// <returns></returns>
    //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();
    #endregion

    #region 业务操作
    /// <summary>
    /// 添加日志
    /// </summary>
    /// <param name="Content">操作内容</param>
    /// <param name="Url">操作Url</param>
    /// <returns></returns>
    public static Int32 AddLog(String Content, String Url)
    {
        var model = new UserLog();
        model.Content = Content;
        model.Url = Url;
        return model.Insert();
    }
    #endregion
}