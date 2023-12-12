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

/// <summary>应用日志。用于OAuthServer的子系统</summary>
public partial class AppLog : DHEntityBase<AppLog> {
    #region 对象操作
    static AppLog()
    {
        // 累加字段
        //Meta.Factory.AdditionalFields.Add(__.Logins);

        // 过滤器 UserModule、TimeModule、IPModule
        Meta.Modules.Add<UserModule>();
        Meta.Modules.Add<TimeModule>();
        Meta.Modules.Add<IPModule>();
    }
    #endregion

    #region 扩展属性
    /// <summary>应用</summary>
    [XmlIgnore, ScriptIgnore, IgnoreDataMember]
    public App App => Extends.Get(nameof(App), k => App.FindById(AppId));

    /// <summary>应用</summary>
    [Map(__.AppId, typeof(App), "Id")]
    public String AppName => App + "";
    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static AppLog FindById(Int64 id)
    {
        if (id <= 0) return null;

        //// 实体缓存
        //if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.ID == id);
    }

    /// <summary>根据应用查找</summary>
    /// <param name="appId">应用</param>
    /// <returns>实体列表</returns>
    public static IList<AppLog> FindAllByAppId(Int32 appId)
    {
        if (appId <= 0) return new List<AppLog>();

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.AppId == appId);

        return FindAll(_.AppId == appId);
    }
    #endregion

    #region 高级查询
    /// <summary>高级搜索</summary>
    /// <param name="appId"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="key"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    public static IList<AppLog> Search(Int32 appId, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (appId >= 0) exp &= _.AppId == appId;
        exp &= _.Id.Between(start, end, Meta.Factory.Snow);

        return FindAll(exp, page);
    }
    #endregion

    #region 业务操作
    /// <summary>创建日志</summary>
    /// <param name="appid"></param>
    /// <param name="action"></param>
    /// <param name="success"></param>
    /// <param name="remark"></param>
    /// <returns></returns>
    public static AppLog Create(Int32 appid, String action, Boolean success, String remark)
    {
        var log = new AppLog
        {
            AppId = appid,
            Action = action,
            Success = success,
            TraceId = DefaultSpan.Current?.TraceId,
            Remark = remark,
            CreateTime = DateTime.Now,
        };
        log.SaveAsync();

        return log;
    }
    #endregion
}