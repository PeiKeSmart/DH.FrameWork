using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>认证日志</summary>
public partial interface IAuthCheckLog
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>会员ID</summary>
    Int32 UId { get; set; }

    /// <summary>身份证号</summary>
    String IdCard { get; set; }

    /// <summary>手机号</summary>
    String Mobile { get; set; }

    /// <summary>真实姓名</summary>
    String TrueName { get; set; }

    /// <summary>认证类型。1为身份证认证，2为手机号认证，3为银行卡认证</summary>
    Int16 CheckType { get; set; }

    /// <summary>认证是否成功</summary>
    Boolean State { get; set; }

    /// <summary>返回的数据值</summary>
    String Remark { get; set; }

    /// <summary>创建者</summary>
    String CreateUser { get; set; }

    /// <summary>创建用户</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }
    #endregion
}
