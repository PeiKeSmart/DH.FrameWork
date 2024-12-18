﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>在线用户表</summary>
public partial class SysOnlineUsersModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>用户id</summary>
    public Int32 Uid { get; set; }

    /// <summary>用户sessionid</summary>
    public Int64 Sid { get; set; }

    /// <summary>用户昵称</summary>
    public String NickName { get; set; }

    /// <summary>用户名</summary>
    public String Name { get; set; }

    /// <summary>用户ip</summary>
    public String Ip { get; set; }

    /// <summary>用户所在区域</summary>
    public String Region { get; set; }

    /// <summary>运营商</summary>
    public String Network { get; set; }

    /// <summary>代号</summary>
    public String Numbers { get; set; }

    /// <summary>请求次数</summary>
    public Int32 Clicks { get; set; }

    /// <summary>特征字符串</summary>
    public String UserAgent { get; set; }

    /// <summary>最后更新时间</summary>
    public DateTime Updatetime { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISysOnlineUsers model)
    {
        Id = model.Id;
        Uid = model.Uid;
        Sid = model.Sid;
        NickName = model.NickName;
        Name = model.Name;
        Ip = model.Ip;
        Region = model.Region;
        Network = model.Network;
        Numbers = model.Numbers;
        Clicks = model.Clicks;
        UserAgent = model.UserAgent;
        Updatetime = model.Updatetime;
    }
    #endregion
}
