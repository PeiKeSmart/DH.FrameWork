using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>语言类别</summary>
public partial interface ILanguage
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>语言名称</summary>
    String Name { get; set; }

    /// <summary>显示名称</summary>
    String DisplayName { get; set; }

    /// <summary>英文名称</summary>
    String EnglishName { get; set; }

    /// <summary>旗帜文件名</summary>
    String FlagImageFileName { get; set; }

    /// <summary>本地化语言标识</summary>
    String LanguageCulture { get; set; }

    /// <summary>Url缩写</summary>
    String UniqueSeoCode { get; set; }

    /// <summary>语言简写</summary>
    String LangAbbreviation { get; set; }

    /// <summary>旗帜</summary>
    String Flag { get; set; }

    /// <summary>域名</summary>
    String Domain { get; set; }

    /// <summary>LCID</summary>
    Int32 Lcid { get; set; }

    /// <summary>状态。是否启用</summary>
    Boolean Status { get; set; }

    /// <summary>该语言是否支持从右到左</summary>
    Boolean Rtl { get; set; }

    /// <summary>排序</summary>
    Int32 DisplayOrder { get; set; }

    /// <summary>是否网站打开默认语言</summary>
    Byte IsDefault { get; set; }

    /// <summary>描述</summary>
    String Remark { get; set; }

    /// <summary>创建者</summary>
    String CreateUser { get; set; }

    /// <summary>创建者</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>更新者</summary>
    String UpdateUser { get; set; }

    /// <summary>更新者</summary>
    Int32 UpdateUserID { get; set; }

    /// <summary>更新时间</summary>
    DateTime UpdateTime { get; set; }

    /// <summary>更新地址</summary>
    String UpdateIP { get; set; }
    #endregion
}
