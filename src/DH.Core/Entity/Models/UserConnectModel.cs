﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;

namespace DH.Entity;

/// <summary>用户链接。第三方绑定</summary>
public partial class UserConnectModel : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 ID { get; set; }

    /// <summary>提供商</summary>
    public String Provider { get; set; }

    /// <summary>用户。本地用户</summary>
    public Int32 UserID { get; set; }

    /// <summary>身份标识。用户名、OpenID</summary>
    public String OpenID { get; set; }

    /// <summary>全局标识。跨应用统一</summary>
    public String UnionID { get; set; }

    /// <summary>用户编号。第三方用户编号</summary>
    public Int64 LinkID { get; set; }

    /// <summary>昵称</summary>
    public String NickName { get; set; }

    /// <summary>设备标识。企业微信用于唯一标识设备，重装后改变</summary>
    public String DeviceId { get; set; }

    /// <summary>头像</summary>
    public String Avatar { get; set; }

    /// <summary>访问令牌</summary>
    public String AccessToken { get; set; }

    /// <summary>刷新令牌</summary>
    public String RefreshToken { get; set; }

    /// <summary>访问令牌过期时间</summary>
    public DateTime Expire { get; set; }

    /// <summary>刷新令牌过期时间</summary>
    public DateTime RefreshExpire { get; set; }

    /// <summary>启用</summary>
    public Boolean Enable { get; set; }

    /// <summary>创建用户</summary>
    public Int32 CreateUserID { get; set; }

    /// <summary>创建地址</summary>
    public String CreateIP { get; set; }

    /// <summary>创建时间</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>更新用户</summary>
    public Int32 UpdateUserID { get; set; }

    /// <summary>更新地址</summary>
    public String UpdateIP { get; set; }

    /// <summary>更新时间</summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>备注</summary>
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
                "Provider" => Provider,
                "UserID" => UserID,
                "OpenID" => OpenID,
                "UnionID" => UnionID,
                "LinkID" => LinkID,
                "NickName" => NickName,
                "DeviceId" => DeviceId,
                "Avatar" => Avatar,
                "AccessToken" => AccessToken,
                "RefreshToken" => RefreshToken,
                "Expire" => Expire,
                "RefreshExpire" => RefreshExpire,
                "Enable" => Enable,
                "CreateUserID" => CreateUserID,
                "CreateIP" => CreateIP,
                "CreateTime" => CreateTime,
                "UpdateUserID" => UpdateUserID,
                "UpdateIP" => UpdateIP,
                "UpdateTime" => UpdateTime,
                "Remark" => Remark,
                _ => null
            };
        }
        set
        {
            switch (name)
            {
                case "ID": ID = value.ToInt(); break;
                case "Provider": Provider = Convert.ToString(value); break;
                case "UserID": UserID = value.ToInt(); break;
                case "OpenID": OpenID = Convert.ToString(value); break;
                case "UnionID": UnionID = Convert.ToString(value); break;
                case "LinkID": LinkID = value.ToLong(); break;
                case "NickName": NickName = Convert.ToString(value); break;
                case "DeviceId": DeviceId = Convert.ToString(value); break;
                case "Avatar": Avatar = Convert.ToString(value); break;
                case "AccessToken": AccessToken = Convert.ToString(value); break;
                case "RefreshToken": RefreshToken = Convert.ToString(value); break;
                case "Expire": Expire = value.ToDateTime(); break;
                case "RefreshExpire": RefreshExpire = value.ToDateTime(); break;
                case "Enable": Enable = value.ToBoolean(); break;
                case "CreateUserID": CreateUserID = value.ToInt(); break;
                case "CreateIP": CreateIP = Convert.ToString(value); break;
                case "CreateTime": CreateTime = value.ToDateTime(); break;
                case "UpdateUserID": UpdateUserID = value.ToInt(); break;
                case "UpdateIP": UpdateIP = Convert.ToString(value); break;
                case "UpdateTime": UpdateTime = value.ToDateTime(); break;
                case "Remark": Remark = Convert.ToString(value); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IUserConnect model)
    {
        ID = model.ID;
        Provider = model.Provider;
        UserID = model.UserID;
        OpenID = model.OpenID;
        UnionID = model.UnionID;
        LinkID = model.LinkID;
        NickName = model.NickName;
        DeviceId = model.DeviceId;
        Avatar = model.Avatar;
        AccessToken = model.AccessToken;
        RefreshToken = model.RefreshToken;
        Expire = model.Expire;
        RefreshExpire = model.RefreshExpire;
        Enable = model.Enable;
        CreateUserID = model.CreateUserID;
        CreateIP = model.CreateIP;
        CreateTime = model.CreateTime;
        UpdateUserID = model.UpdateUserID;
        UpdateIP = model.UpdateIP;
        UpdateTime = model.UpdateTime;
        Remark = model.Remark;
    }
    #endregion
}
