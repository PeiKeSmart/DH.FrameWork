using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>路由管理</summary>
public partial class SystemRoutModel : ISystemRout
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>类型，1为控制器，2为Razor Page</summary>
    public Byte RType { get; set; }

    /// <summary>路由名称</summary>
    public String Name { get; set; }

    /// <summary>Url路由</summary>
    public String Url { get; set; }

    /// <summary>Url路由参数</summary>
    public String Parms { get; set; }

    /// <summary>Razor Page实际路径</summary>
    public String Pages { get; set; }

    /// <summary>区域名称</summary>
    public String AreaName { get; set; }

    /// <summary>控制器</summary>
    public String ControllerName { get; set; }

    /// <summary>控制器动作</summary>
    public String ActionName { get; set; }

    /// <summary>映射路由</summary>
    public String FromUrl { get; set; }

    /// <summary>创建时间</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>更新时间</summary>
    public DateTime UpdateTime { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISystemRout model)
    {
        Id = model.Id;
        RType = model.RType;
        Name = model.Name;
        Url = model.Url;
        Parms = model.Parms;
        Pages = model.Pages;
        AreaName = model.AreaName;
        ControllerName = model.ControllerName;
        ActionName = model.ActionName;
        FromUrl = model.FromUrl;
        CreateTime = model.CreateTime;
        UpdateTime = model.UpdateTime;
    }
    #endregion
}
