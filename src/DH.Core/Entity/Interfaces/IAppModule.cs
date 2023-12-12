using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>应用插件。基于框架实现的应用功能插件</summary>
public partial interface IAppModule
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>名称</summary>
    String Name { get; set; }

    /// <summary>显示名</summary>
    String DisplayName { get; set; }

    /// <summary>类型。.NET/Javascript/Lua</summary>
    String Type { get; set; }

    /// <summary>类名。完整类名</summary>
    String ClassName { get; set; }

    /// <summary>文件。插件文件包，zip压缩</summary>
    String FilePath { get; set; }

    /// <summary>启用</summary>
    Boolean Enable { get; set; }

    /// <summary>描述</summary>
    String Remark { get; set; }
    #endregion
}
