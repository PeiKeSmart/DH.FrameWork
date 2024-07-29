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

public partial class AppVersionLan : DHEntityBase<AppVersionLan>
{
    #region 对象操作
    static AppVersionLan()
    {
        // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
        //var df = Meta.Factory.AdditionalFields;
        //df.Add(nameof(AId));

        // 过滤器 UserModule、TimeModule、IPModule

        // 实体缓存
        // var ec = Meta.Cache;
        // ec.Expire = 60;
    }

    /// <summary>验证并修补数据，返回验证结果，或者通过抛出异常的方式提示验证失败。</summary>
    /// <param name="method">添删改方法</param>
    public override Boolean Valid(DataMethod method)
    {
        //if (method == DataMethod.Delete) return true;
        // 如果没有脏数据，则不需要进行任何处理
        if (!HasDirty) return true;

        // 建议先调用基类方法，基类方法会做一些统一处理
        if (!base.Valid(method)) return false;

        // 在新插入数据或者修改了指定字段时进行修正

        return true;
    }

    ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //protected override void InitData()
    //{
    //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
    //    if (Meta.Session.Count > 0) return;

    //    if (XTrace.Debug) XTrace.WriteLine("开始初始化AppVersionLan[APP版本翻译]数据……");

    //    var entity = new AppVersionLan();
    //    entity.AId = 0;
    //    entity.LId = 0;
    //    entity.Content = "abc";
    //    entity.Insert();

    //    if (XTrace.Debug) XTrace.WriteLine("完成初始化AppVersionLan[APP版本翻译]数据！");
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
    public static AppVersionLan FindById(Int32 id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>
    /// 根据版本Id获取所属语言数据
    /// </summary>
    /// <param name="aId">版本Id</param>
    /// <returns></returns>
    public static IList<AppVersionLan> FindAllByAId(Int32 aId)
    {
        if (aId <= 0) return new List<AppVersionLan>();

        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.AId == aId);

        return FindAll(_.AId == aId);
    }

    /// <summary>
    /// 通过版本Id和语言Id获取翻译数据
    /// </summary>
    /// <param name="aId">版本Id</param>
    /// <param name="lId">语言Id</param>
    /// <param name="IsGetDefault">是否获取默认数据</param>
    /// <returns></returns>
    public static String FindByAIdAndLId(Int32 aId, Int32 lId, Boolean IsGetDefault = true)
    {
        if (aId <= 0 || lId <= 0) return "";

        if (Meta.Session.Count < 1000)
        {
            var model = Meta.Cache.Find(e => e.AId == aId && e.LId == lId);
            if (IsGetDefault)
            {
                return FindNameAndRemark(aId, model);
            }
            else
            {
                if (model == null)
                    return "";
                return model.Content;
            }
        }

        var exp = new WhereExpression();
        exp = _.AId == aId & _.LId == lId;

        var m = Find(exp);

        if (IsGetDefault)
        {
            return FindNameAndRemark(aId, m);
        }
        else
        {
            if (m == null)
                return "";
            return m.Content;
        }
    }

    /// <summary>
    /// 获取翻译数据
    /// </summary>
    /// <param name="aId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    private static String FindNameAndRemark(Int32 aId, AppVersionLan model)
    {
        var r = AppVersion.FindById(aId);

        if (model == null)
        {
            return r.Content;
        }
        else
        {
            var Remark = model.Content.IsNullOrWhiteSpace() ? r.Content : model.Content;
            return Remark;
        }
    }

    /// <summary>根据APP版本Id、关联所属语言Id查找</summary>
    /// <param name="aId">APP版本Id</param>
    /// <param name="lId">关联所属语言Id</param>
    /// <returns>实体对象</returns>
    public static AppVersionLan FindByAIdAndLId(Int32 aId, Int32 lId)
    {
        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.AId == aId && e.LId == lId);

        return Find(_.AId == aId & _.LId == lId);
    }
    #endregion

    #region 高级查询

    // Select Count(Id) as Id,Category From DG_AppVersionLan Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
    //static readonly FieldCache<AppVersionLan> _CategoryCache = new FieldCache<AppVersionLan>(nameof(Category))
    //{
    //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
    //};

    ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
    ///// <returns></returns>
    //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();
    #endregion

    #region 业务操作
    public IAppVersionLan ToModel()
    {
        var model = new AppVersionLan();
        model.Copy(this);

        return model;
    }

    #endregion
}