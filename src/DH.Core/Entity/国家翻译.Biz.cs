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

/// <summary>国家翻译</summary>
public partial class CountryLan : DHEntityBase<CountryLan> {
    #region 对象操作
    static CountryLan()
    {
        // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
        //var df = Meta.Factory.AdditionalFields;
        //df.Add(nameof(CId));

        // 过滤器 UserModule、TimeModule、IPModule
    }

    /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
    /// <param name="isNew">是否插入</param>
    public override void Valid(Boolean isNew)
    {
        // 如果没有脏数据，则不需要进行任何处理
        if (!HasDirty) return;

        // 在新插入数据或者修改了指定字段时进行修正
    }

    /// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected override void InitData()
    {
        // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        if (Meta.Session.Count > 0) return;

        if (XTrace.Debug) XTrace.WriteLine("开始初始化CountryLan[国家翻译]数据……");

        var list1 = new List<CountryLan>();
        list1.Add(new CountryLan
        {
            Name = "中国",
            CId = 1,
            LId = 1
        });
        list1.Insert();

        if (XTrace.Debug) XTrace.WriteLine("完成初始化CountryLan[国家翻译]数据！");
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
    public static CountryLan FindById(Int32 id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>
    /// 根据国家Id获取所属语言数据
    /// </summary>
    /// <param name="cId"></param>
    /// <returns></returns>
    public static IList<CountryLan> FindAllByCId(Int32 cId)
    {
        if (cId <= 0) return new List<CountryLan>();

        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.CId == cId);

        return FindAll(_.CId == cId);
    }

    /// <summary>
    /// 通过国家Id和语言Id获取翻译数据
    /// </summary>
    /// <param name="cId">国家Id</param>
    /// <param name="lId">语言Id</param>
    /// <param name="IsGetDefault">是否获取默认数据</param>
    /// <returns></returns>
    public static String FindNameByCIdAndLId(Int32 cId, Int32 lId, Boolean IsGetDefault = true)
    {
        if (cId <= 0 || lId <= 0) return "";

        if (Meta.Session.Count < 1000)
        {
            var model = Meta.Cache.Find(e => e.CId == cId && e.LId == lId);

            if (!IsGetDefault)
            {
                if (model == null)
                {
                    return "";
                }
                else
                {
                    return model.Name;
                }
            }

            return FindName(cId, model);
        }

        var exp = new WhereExpression();
        exp = _.CId == cId & _.LId == lId;

        var m = Find(exp);

        if (!IsGetDefault)
        {
            if (m == null)
            {
                return "";
            }
            else
            {
                return m.Name;
            }
        }

        return FindName(cId, m);
    }

    /// <summary>
    /// 获取翻译数据
    /// </summary>
    /// <param name="cId">国家Id</param>
    /// <param name="model"></param>
    /// <returns></returns>
    private static String FindName(Int32 cId, CountryLan model)
    {
        var r = Country.FindById(cId);

        if (model == null)
        {
            return r.Name;
        }
        else
        {
            var Name = model.Name.IsNullOrWhiteSpace() ? r.Name : model.Name;
            return Name;
        }
    }
    #endregion

    #region 高级查询

    // Select Count(Id) as Id,Category From CountryLan Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
    //static readonly FieldCache<CountryLan> _CategoryCache = new FieldCache<CountryLan>(nameof(Category))
    //{
    //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
    //};

    ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
    ///// <returns></returns>
    //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();

    /// <summary>
    /// 根据区域集合删除数据
    /// </summary>
    /// <param name="CIds">ID集合</param>
    public static void DelByCIds(String CIds)
    {
        if (Delete(_.CId.In(CIds)) > 0)
            Meta.Cache.Clear("");
    }

    #endregion

    #region 业务操作
    #endregion
}