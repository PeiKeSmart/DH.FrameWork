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

/// <summary>应用系统</summary>
public partial class App : DHEntityBase<App> {
    #region 对象操作
    static App()
    {
        // 累加字段
        var df = Meta.Factory.AdditionalFields;
        df.Add(__.Auths);

        // 过滤器 UserModule、TimeModule、IPModule
        Meta.Modules.Add<UserModule>();
        Meta.Modules.Add<TimeModule>();
        Meta.Modules.Add<IPModule>();

        // 单对象缓存
        var sc = Meta.SingleCache;
        sc.FindSlaveKeyMethod = k => Find(__.Name, k);
        sc.GetSlaveKeyMethod = e => e.Name;
    }

    /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
    /// <param name="isNew">是否插入</param>
    public override void Valid(Boolean isNew)
    {
        // 如果没有脏数据，则不需要进行任何处理
        if (!HasDirty) return;

        // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
        if (Name.IsNullOrEmpty()) throw new ArgumentNullException(nameof(Name), "名称不能为空！");

        //if (Secret.IsNullOrEmpty()) Secret = Rand.NextString(16).ToLower();

        //if (RoleIds == null || RoleIds.Length == 0)
        //    Roles = null;
        //else
        //    Roles = RoleIds.Distinct().Join(",");
    }

    ///// <summary>加载后</summary>
    //protected override void OnLoad()
    //{
    //    base.OnLoad();

    //    RoleIds = Roles.SplitAsInt();
    //}

    /// <summary>已重载。显示系统名称</summary>
    /// <returns></returns>
    public override String ToString() => DisplayName.IsNullOrEmpty() ? Name : DisplayName;
    #endregion

    #region 扩展属性
    ///// <summary>角色集合</summary>
    //[XmlIgnore, ScriptIgnore, IgnoreDataMember]
    //[Map(nameof(Roles))]
    //public Int32[] RoleIds { get; set; }
    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static App FindById(Int32 id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        //return Meta.SingleCache[id];

        return Find(_.Id == id);
    }

    /// <summary>根据名称查找</summary>
    /// <param name="name">名称</param>
    /// <returns>实体对象</returns>
    public static App FindByName(String name)
    {
        if (name.IsNullOrEmpty()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Name.EqualIgnoreCase(name));

        // 单对象缓存
        //return Meta.SingleCache.GetItemWithSlaveKey(name) as App;

        return Find(_.Name == name.Trim());
    }

    /// <summary>根据密钥查找</summary>
    /// <param name="appkey">密钥</param>
    /// <returns>实体对象</returns>
    public static App FindBySecret(String appkey)
    {
        if (appkey.IsNullOrEmpty()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Secret.EqualIgnoreCase(appkey));

        return Find(_.Secret == appkey.Trim());
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

    /// <summary>根据编号列表查找</summary>
    /// <param name="ids">编号列表</param>
    /// <returns>实体对象</returns>
    public static IList<App> FindByIds(String ids)
    {
        if (ids.IsNullOrWhiteSpace()) return new List<App>();

        ids = ids.Trim(',');

        if (Meta.Session.Count < 1000)
        {
            return Meta.Cache.FindAll(x => ids.SplitAsInt(",").Contains(x.Id));
        }

        return FindAll(_.Id.In(ids.Split(',')));
    }
    #endregion

    #region 高级查询
    #endregion

    #region 业务操作
    /// <summary>验证回调地址</summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public Boolean ValidCallback(String url)
    {
        if (Urls.IsNullOrEmpty()) return true;
        if (url.IsNullOrEmpty()) return false;

        var us = Urls.Split(",", ";");
        if (us == null || us.Length == 0) return true;

        // 截取协议 http/https
        var p = url.IndexOf("://");
        if (p <= 0) return false;

        // 特殊支持localhost
        if (url.StartsWithIgnoreCase("http://localhost/", "https://localhost/")) return true;
        if (url.StartsWithIgnoreCase("http://localhost:", "https://localhost:") && !url.Contains("@")) return true;

        var sch = url[..(p + 3)];

        // 给没有头部的地址加上
        for (var i = 0; i < us.Length; i++)
        {
            if (!us[i].Contains("://")) us[i] = sch + us[i];
        }

        // 判断地址是否以指定开头
        return url.StartsWithIgnoreCase(us);
    }

    /// <summary>验证来源地址</summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public Boolean ValidSource(String ip)
    {
        if (ip.IsNullOrEmpty()) return true;

        // 匹配黑名单
        var ps = Black.Split(",", ";");
        if (ps != null && ps.Length > 0)
        {
            if (ps.Any(e => ip.IsMatch(ip))) return false;
        }

        // 匹配白名单
        ps = White.Split(",", ";");
        if (ps != null && ps.Length > 0)
        {
            if (ps.Any(e => ip.IsMatch(ip))) return true;

            // 白名单存在，但匹配失败，则直接失败
            return false;
        }

        return true;
    }

    /// <summary>验证应用密钥是否有效</summary>
    /// <param name="appkey"></param>
    /// <returns></returns>
    public static App Valid(String appkey)
    {
        var app = FindBySecret(appkey);
        if (app == null || !app.Enable) throw new XException("非法授权！");

        return app;
    }
    #endregion
}