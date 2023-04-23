using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>消息记录</summary>
public partial interface ISendLog
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>0为短信，1为邮箱</summary>
    Int16 SType { get; set; }

    /// <summary>手机号/邮箱</summary>
    String Account { get; set; }

    /// <summary>消息内容</summary>
    String Msg { get; set; }

    /// <summary>类型：1为注册，2为登录，3为找回密码，4绑定手机/邮箱，5安全验证,6账号申诉，0测试</summary>
    Int16 MType { get; set; }

    /// <summary>发送回复数据</summary>
    String Remark { get; set; }

    /// <summary>短信平台返回的Id</summary>
    String SmsId { get; set; }

    /// <summary>消息会员名</summary>
    String CreateUser { get; set; }

    /// <summary>消息会员ID，注册为0</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>消息添加时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>消息请求IP</summary>
    String CreateIP { get; set; }
    #endregion
}
