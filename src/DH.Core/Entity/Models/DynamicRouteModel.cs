using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;
using NewLife.Reflection;

namespace DH.Entity;

/// <summary>动态路由表</summary>
public partial class DynamicRouteModel : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>正则表达式</summary>
    public String RegexInfo { get; set; }

    /// <summary>控制器</summary>
    public String Controller { get; set; }

    /// <summary>动作</summary>
    public String Action { get; set; }

    /// <summary>区域</summary>
    public String Area { get; set; }

    /// <summary>其他参数</summary>
    public String Other { get; set; }

    /// <summary>是否启用</summary>
    public Boolean Enable { get; set; }

    /// <summary>创建者</summary>
    public String CreateUser { get; set; }

    /// <summary>创建者</summary>
    public Int32 CreateUserID { get; set; }

    /// <summary>创建时间</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    public String CreateIP { get; set; }

    /// <summary>更新者</summary>
    public String UpdateUser { get; set; }

    /// <summary>更新者</summary>
    public Int32 UpdateUserID { get; set; }

    /// <summary>更新时间</summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>更新地址</summary>
    public String UpdateIP { get; set; }
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
                "Id" => Id,
                "RegexInfo" => RegexInfo,
                "Controller" => Controller,
                "Action" => Action,
                "Area" => Area,
                "Other" => Other,
                "Enable" => Enable,
                "CreateUser" => CreateUser,
                "CreateUserID" => CreateUserID,
                "CreateTime" => CreateTime,
                "CreateIP" => CreateIP,
                "UpdateUser" => UpdateUser,
                "UpdateUserID" => UpdateUserID,
                "UpdateTime" => UpdateTime,
                "UpdateIP" => UpdateIP,
                _ => this.GetValue(name),
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "RegexInfo": RegexInfo = Convert.ToString(value); break;
                case "Controller": Controller = Convert.ToString(value); break;
                case "Action": Action = Convert.ToString(value); break;
                case "Area": Area = Convert.ToString(value); break;
                case "Other": Other = Convert.ToString(value); break;
                case "Enable": Enable = value.ToBoolean(); break;
                case "CreateUser": CreateUser = Convert.ToString(value); break;
                case "CreateUserID": CreateUserID = value.ToInt(); break;
                case "CreateTime": CreateTime = value.ToDateTime(); break;
                case "CreateIP": CreateIP = Convert.ToString(value); break;
                case "UpdateUser": UpdateUser = Convert.ToString(value); break;
                case "UpdateUserID": UpdateUserID = value.ToInt(); break;
                case "UpdateTime": UpdateTime = value.ToDateTime(); break;
                case "UpdateIP": UpdateIP = Convert.ToString(value); break;
                default: this.SetValue(name, value); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IDynamicRoute model)
    {
        Id = model.Id;
        RegexInfo = model.RegexInfo;
        Controller = model.Controller;
        Action = model.Action;
        Area = model.Area;
        Other = model.Other;
        Enable = model.Enable;
        CreateUser = model.CreateUser;
        CreateUserID = model.CreateUserID;
        CreateTime = model.CreateTime;
        CreateIP = model.CreateIP;
        UpdateUser = model.UpdateUser;
        UpdateUserID = model.UpdateUserID;
        UpdateTime = model.UpdateTime;
        UpdateIP = model.UpdateIP;
    }
    #endregion
}
