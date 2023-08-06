using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>指令管理</summary>
public partial interface IOrderManager
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>指令名称</summary>
    String Name { get; set; }

    /// <summary>指令编号</summary>
    String Code { get; set; }

    /// <summary>操作类型</summary>
    String OptCategory { get; set; }

    /// <summary>启用</summary>
    Boolean Enable { get; set; }

    /// <summary>数据,进行后续操作依赖值</summary>
    String Data { get; set; }

    /// <summary>数据类型,String、Int、Double、Decimal等</summary>
    String DataType { get; set; }

    /// <summary>请求地址</summary>
    String Url { get; set; }

    /// <summary>请求方式,GET、POST、PUT、DELETE</summary>
    String Method { get; set; }

    /// <summary>值字段</summary>
    String ValueField { get; set; }

    /// <summary>文本字段</summary>
    String LabelField { get; set; }

    /// <summary>创建者</summary>
    Int32 CreateUserId { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>更新者</summary>
    Int32 UpdateUserId { get; set; }

    /// <summary>更新时间</summary>
    DateTime UpdateTime { get; set; }

    /// <summary>更新地址</summary>
    String UpdateIP { get; set; }

    /// <summary>内容</summary>
    String Remark { get; set; }
    #endregion
}
