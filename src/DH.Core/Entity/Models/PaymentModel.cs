using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>支付方式</summary>
public partial class PaymentModel
{
    #region 属性
    /// <summary>支付代码</summary>
    public String Id { get; set; }

    /// <summary>支付名称</summary>
    public String Name { get; set; }

    /// <summary>支付接口配置信息</summary>
    public String Config { get; set; }

    /// <summary>支付方式所适应平台 pc h5 app wm</summary>
    public String Platform { get; set; }

    /// <summary>支持的语言Id集合</summary>
    public String LanguageIds { get; set; }

    /// <summary>接口状态,是否启用</summary>
    public Boolean State { get; set; }

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

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IPayment model)
    {
        Id = model.Id;
        Name = model.Name;
        Config = model.Config;
        Platform = model.Platform;
        LanguageIds = model.LanguageIds;
        State = model.State;
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
