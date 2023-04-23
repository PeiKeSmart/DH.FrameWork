﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;

namespace DH.Entity;

/// <summary>用户在线</summary>
public partial class UserOnlineModel : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 ID { get; set; }

    /// <summary>用户。当前登录人</summary>
    public Int32 UserID { get; set; }

    /// <summary>名称。当前登录人，或根据设备标识推算出来的使用人</summary>
    public String Name { get; set; }

    /// <summary>会话。Web的SessionID或Server的会话编号</summary>
    public String SessionID { get; set; }

    /// <summary>登录方。OAuth提供商，从哪个渠道登录</summary>
    public String OAuthProvider { get; set; }

    /// <summary>次数</summary>
    public Int32 Times { get; set; }

    /// <summary>页面</summary>
    public String Page { get; set; }

    /// <summary>平台。操作系统平台，Windows/Linux/Android等</summary>
    public String Platform { get; set; }

    /// <summary>系统。操作系统，带版本</summary>
    public String OS { get; set; }

    /// <summary>设备。手机品牌型号</summary>
    public String Device { get; set; }

    /// <summary>浏览器。浏览器名称，带版本</summary>
    public String Brower { get; set; }

    /// <summary>网络。微信访问时，感知到WIFI或4G网络</summary>
    public String NetType { get; set; }

    /// <summary>设备标识。唯一标识设备，位于浏览器Cookie，重装后改变</summary>
    public String DeviceId { get; set; }

    /// <summary>状态</summary>
    public String Status { get; set; }

    /// <summary>在线时间。本次在线总时间，秒</summary>
    public Int32 OnlineTime { get; set; }

    /// <summary>最后错误</summary>
    public DateTime LastError { get; set; }

    /// <summary>追踪。链路追踪，用于APM性能追踪定位，还原该事件的调用链</summary>
    public String TraceId { get; set; }

    /// <summary>地址。根据IP计算</summary>
    public String Address { get; set; }

    /// <summary>创建地址</summary>
    public String CreateIP { get; set; }

    /// <summary>创建时间</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>更新地址</summary>
    public String UpdateIP { get; set; }

    /// <summary>修改时间</summary>
    public DateTime UpdateTime { get; set; }
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
                "UserID" => UserID,
                "Name" => Name,
                "SessionID" => SessionID,
                "OAuthProvider" => OAuthProvider,
                "Times" => Times,
                "Page" => Page,
                "Platform" => Platform,
                "OS" => OS,
                "Device" => Device,
                "Brower" => Brower,
                "NetType" => NetType,
                "DeviceId" => DeviceId,
                "Status" => Status,
                "OnlineTime" => OnlineTime,
                "LastError" => LastError,
                "TraceId" => TraceId,
                "Address" => Address,
                "CreateIP" => CreateIP,
                "CreateTime" => CreateTime,
                "UpdateIP" => UpdateIP,
                "UpdateTime" => UpdateTime,
                _ => null
            };
        }
        set
        {
            switch (name)
            {
                case "ID": ID = value.ToInt(); break;
                case "UserID": UserID = value.ToInt(); break;
                case "Name": Name = Convert.ToString(value); break;
                case "SessionID": SessionID = Convert.ToString(value); break;
                case "OAuthProvider": OAuthProvider = Convert.ToString(value); break;
                case "Times": Times = value.ToInt(); break;
                case "Page": Page = Convert.ToString(value); break;
                case "Platform": Platform = Convert.ToString(value); break;
                case "OS": OS = Convert.ToString(value); break;
                case "Device": Device = Convert.ToString(value); break;
                case "Brower": Brower = Convert.ToString(value); break;
                case "NetType": NetType = Convert.ToString(value); break;
                case "DeviceId": DeviceId = Convert.ToString(value); break;
                case "Status": Status = Convert.ToString(value); break;
                case "OnlineTime": OnlineTime = value.ToInt(); break;
                case "LastError": LastError = value.ToDateTime(); break;
                case "TraceId": TraceId = Convert.ToString(value); break;
                case "Address": Address = Convert.ToString(value); break;
                case "CreateIP": CreateIP = Convert.ToString(value); break;
                case "CreateTime": CreateTime = value.ToDateTime(); break;
                case "UpdateIP": UpdateIP = Convert.ToString(value); break;
                case "UpdateTime": UpdateTime = value.ToDateTime(); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IUserOnline model)
    {
        ID = model.ID;
        UserID = model.UserID;
        Name = model.Name;
        SessionID = model.SessionID;
        OAuthProvider = model.OAuthProvider;
        Times = model.Times;
        Page = model.Page;
        Platform = model.Platform;
        OS = model.OS;
        Device = model.Device;
        Brower = model.Brower;
        NetType = model.NetType;
        DeviceId = model.DeviceId;
        Status = model.Status;
        OnlineTime = model.OnlineTime;
        LastError = model.LastError;
        TraceId = model.TraceId;
        Address = model.Address;
        CreateIP = model.CreateIP;
        CreateTime = model.CreateTime;
        UpdateIP = model.UpdateIP;
        UpdateTime = model.UpdateTime;
    }
    #endregion
}
