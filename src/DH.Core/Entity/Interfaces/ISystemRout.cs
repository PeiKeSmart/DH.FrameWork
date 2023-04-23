using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>路由管理</summary>
public partial interface ISystemRout
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>类型，1为控制器，2为Razor Page</summary>
    Byte RType { get; set; }

    /// <summary>路由名称</summary>
    String Name { get; set; }

    /// <summary>Url路由</summary>
    String Url { get; set; }

    /// <summary>Url路由参数</summary>
    String Parms { get; set; }

    /// <summary>Razor Page实际路径</summary>
    String Pages { get; set; }

    /// <summary>区域名称</summary>
    String AreaName { get; set; }

    /// <summary>控制器</summary>
    String ControllerName { get; set; }

    /// <summary>控制器动作</summary>
    String ActionName { get; set; }

    /// <summary>映射路由</summary>
    String FromUrl { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>更新时间</summary>
    DateTime UpdateTime { get; set; }
    #endregion
}
