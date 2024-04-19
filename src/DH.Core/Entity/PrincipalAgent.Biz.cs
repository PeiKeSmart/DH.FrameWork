﻿using System;
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

public partial class PrincipalAgent : Entity<PrincipalAgent>
{
    #region 对象操作
    static PrincipalAgent()
    {
        // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
        //var df = Meta.Factory.AdditionalFields;
        //df.Add(nameof(PrincipalId));

        // 过滤器 UserModule、TimeModule、IPModule
        Meta.Modules.Add(new UserModule { AllowEmpty = false });
        Meta.Modules.Add<TimeModule>();
        Meta.Modules.Add(new IPModule { AllowEmpty = false });

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

        // 处理当前已登录用户信息，可以由UserModule过滤器代劳
        /*var user = ManageProvider.User;
        if (user != null)
        {
            if (method == DataMethod.Insert && !Dirtys[nameof(CreateUserId)]) CreateUserId = user.ID;
            if (!Dirtys[nameof(UpdateUserId)]) UpdateUserId = user.ID;
        }*/
        //if (method == DataMethod.Insert && !Dirtys[nameof(CreateTime)]) CreateTime = DateTime.Now;
        //if (!Dirtys[nameof(UpdateTime)]) UpdateTime = DateTime.Now;
        //if (method == DataMethod.Insert && !Dirtys[nameof(CreateIP)]) CreateIP = ManageProvider.UserHost;
        //if (!Dirtys[nameof(UpdateIP)]) UpdateIP = ManageProvider.UserHost;

        return true;
    }

    ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //protected override void InitData()
    //{
    //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
    //    if (Meta.Session.Count > 0) return;

    //    if (XTrace.Debug) XTrace.WriteLine("开始初始化PrincipalAgent[委托代理]数据……");

    //    var entity = new PrincipalAgent();
    //    entity.PrincipalId = 0;
    //    entity.AgentId = 0;
    //    entity.Enable = true;
    //    entity.Times = 0;
    //    entity.Expire = DateTime.Now;
    //    entity.CreateUserId = 0;
    //    entity.CreateTime = DateTime.Now;
    //    entity.CreateIP = "abc";
    //    entity.UpdateUserId = 0;
    //    entity.UpdateTime = DateTime.Now;
    //    entity.UpdateIP = "abc";
    //    entity.Remark = "abc";
    //    entity.Insert();

    //    if (XTrace.Debug) XTrace.WriteLine("完成初始化PrincipalAgent[委托代理]数据！");
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
    public static PrincipalAgent FindById(Int32 id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据委托人查找</summary>
    /// <param name="principalId">委托人</param>
    /// <returns>实体列表</returns>
    public static IList<PrincipalAgent> FindAllByPrincipalId(Int32 principalId)
    {
        if (principalId <= 0) return new List<PrincipalAgent>();

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.PrincipalId == principalId);

        return FindAll(_.PrincipalId == principalId);
    }

    /// <summary>根据代理人查找</summary>
    /// <param name="agentId">代理人</param>
    /// <returns>实体列表</returns>
    public static IList<PrincipalAgent> FindAllByAgentId(Int32 agentId)
    {
        if (agentId <= 0) return new List<PrincipalAgent>();

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.AgentId == agentId);

        return FindAll(_.AgentId == agentId);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="principalId">委托人。把自己的身份权限委托给别人</param>
    /// <param name="agentId">代理人。代理获得别人身份权限</param>
    /// <param name="start">更新时间开始</param>
    /// <param name="end">更新时间结束</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<PrincipalAgent> Search(Int32 principalId, Int32 agentId, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (principalId >= 0) exp &= _.PrincipalId == principalId;
        if (agentId >= 0) exp &= _.AgentId == agentId;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= _.CreateIP.Contains(key) | _.UpdateIP.Contains(key) | _.Remark.Contains(key);

        return FindAll(exp, page);
    }

    // Select Count(Id) as Id,Category From PrincipalAgent Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
    //static readonly FieldCache<PrincipalAgent> _CategoryCache = new FieldCache<PrincipalAgent>(nameof(Category))
    //{
    //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
    //};

    ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
    ///// <returns></returns>
    //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();
    #endregion

    #region 业务操作
    public IPrincipalAgent ToModel()
    {
        var model = new PrincipalAgent();
        model.Copy(this);

        return model;
    }

    #endregion
}
