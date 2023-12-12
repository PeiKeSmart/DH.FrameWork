using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>会员经验日志表</summary>
public partial class ExpPointsLogModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>会员ID</summary>
    public Int32 UId { get; set; }

    /// <summary>会员名称</summary>
    public String UName { get; set; }

    /// <summary>经验值负数为扣除</summary>
    public Int32 Points { get; set; }

    /// <summary>经验值操作描述</summary>
    public String Desc { get; set; }

    /// <summary>经验值操作阶段</summary>
    public String Stage { get; set; }

    /// <summary>创建者</summary>
    public String CreateUser { get; set; }

    /// <summary>创建者</summary>
    public Int32 CreateUserID { get; set; }

    /// <summary>经验添加时间</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    public String CreateIP { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IExpPointsLog model)
    {
        Id = model.Id;
        UId = model.UId;
        UName = model.UName;
        Points = model.Points;
        Desc = model.Desc;
        Stage = model.Stage;
        CreateUser = model.CreateUser;
        CreateUserID = model.CreateUserID;
        CreateTime = model.CreateTime;
        CreateIP = model.CreateIP;
    }
    #endregion
}
