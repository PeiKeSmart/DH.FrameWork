using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>认证日志</summary>
public partial class AuthCheckLogModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>会员ID</summary>
    public Int32 UId { get; set; }

    /// <summary>身份证号</summary>
    public String IdCard { get; set; }

    /// <summary>手机号</summary>
    public String Mobile { get; set; }

    /// <summary>真实姓名</summary>
    public String TrueName { get; set; }

    /// <summary>认证类型。1为身份证认证，2为手机号认证，3为银行卡认证</summary>
    public Int16 CheckType { get; set; }

    /// <summary>认证是否成功</summary>
    public Boolean State { get; set; }

    /// <summary>返回的数据值</summary>
    public String Remark { get; set; }

    /// <summary>创建者</summary>
    public String CreateUser { get; set; }

    /// <summary>创建用户</summary>
    public Int32 CreateUserID { get; set; }

    /// <summary>创建地址</summary>
    public String CreateIP { get; set; }

    /// <summary>创建时间</summary>
    public DateTime CreateTime { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IAuthCheckLog model)
    {
        Id = model.Id;
        UId = model.UId;
        IdCard = model.IdCard;
        Mobile = model.Mobile;
        TrueName = model.TrueName;
        CheckType = model.CheckType;
        State = model.State;
        Remark = model.Remark;
        CreateUser = model.CreateUser;
        CreateUserID = model.CreateUserID;
        CreateIP = model.CreateIP;
        CreateTime = model.CreateTime;
    }
    #endregion
}
