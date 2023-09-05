using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;
using NewLife.Reflection;

namespace DH.Entity;

/// <summary>用户统计</summary>
public partial class UserStatModel : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 ID { get; set; }

    /// <summary>统计日期</summary>
    public DateTime Date { get; set; }

    /// <summary>总数。总用户数</summary>
    public Int32 Total { get; set; }

    /// <summary>登录数。总登录数</summary>
    public Int32 Logins { get; set; }

    /// <summary>OAuth登录。OAuth总登录数</summary>
    public Int32 OAuths { get; set; }

    /// <summary>最大在线。最大在线用户数</summary>
    public Int32 MaxOnline { get; set; }

    /// <summary>活跃。今天活跃用户数</summary>
    public Int32 Actives { get; set; }

    /// <summary>7天活跃。7天活跃用户数</summary>
    public Int32 ActivesT7 { get; set; }

    /// <summary>30天活跃。30天活跃用户数</summary>
    public Int32 ActivesT30 { get; set; }

    /// <summary>新用户。今天注册新用户数</summary>
    public Int32 News { get; set; }

    /// <summary>7天注册。7天内注册新用户数</summary>
    public Int32 NewsT7 { get; set; }

    /// <summary>30天注册。30天注册新用户数</summary>
    public Int32 NewsT30 { get; set; }

    /// <summary>在线时间。累计在线总时间，秒</summary>
    public Int32 OnlineTime { get; set; }

    /// <summary>创建时间</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>更新时间</summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>详细信息</summary>
    public String Remark { get; set; }
    #endregion

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public virtual Object this[String name]
    {
        get
        {
            return name switch
            {
                "ID" => ID,
                "Date" => Date,
                "Total" => Total,
                "Logins" => Logins,
                "OAuths" => OAuths,
                "MaxOnline" => MaxOnline,
                "Actives" => Actives,
                "ActivesT7" => ActivesT7,
                "ActivesT30" => ActivesT30,
                "News" => News,
                "NewsT7" => NewsT7,
                "NewsT30" => NewsT30,
                "OnlineTime" => OnlineTime,
                "CreateTime" => CreateTime,
                "UpdateTime" => UpdateTime,
                "Remark" => Remark,
                _ => this.GetValue(name),
            };
        }
        set
        {
            switch (name)
            {
                case "ID": ID = value.ToInt(); break;
                case "Date": Date = value.ToDateTime(); break;
                case "Total": Total = value.ToInt(); break;
                case "Logins": Logins = value.ToInt(); break;
                case "OAuths": OAuths = value.ToInt(); break;
                case "MaxOnline": MaxOnline = value.ToInt(); break;
                case "Actives": Actives = value.ToInt(); break;
                case "ActivesT7": ActivesT7 = value.ToInt(); break;
                case "ActivesT30": ActivesT30 = value.ToInt(); break;
                case "News": News = value.ToInt(); break;
                case "NewsT7": NewsT7 = value.ToInt(); break;
                case "NewsT30": NewsT30 = value.ToInt(); break;
                case "OnlineTime": OnlineTime = value.ToInt(); break;
                case "CreateTime": CreateTime = value.ToDateTime(); break;
                case "UpdateTime": UpdateTime = value.ToDateTime(); break;
                case "Remark": Remark = Convert.ToString(value); break;
                default: this.SetValue(name, value); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IUserStat model)
    {
        ID = model.ID;
        Date = model.Date;
        Total = model.Total;
        Logins = model.Logins;
        OAuths = model.OAuths;
        MaxOnline = model.MaxOnline;
        Actives = model.Actives;
        ActivesT7 = model.ActivesT7;
        ActivesT30 = model.ActivesT30;
        News = model.News;
        NewsT7 = model.NewsT7;
        NewsT30 = model.NewsT30;
        OnlineTime = model.OnlineTime;
        CreateTime = model.CreateTime;
        UpdateTime = model.UpdateTime;
        Remark = model.Remark;
    }
    #endregion
}
