using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>语言类别</summary>
public partial class LanguageModel : ILanguage
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>语言名称</summary>
    public String Name { get; set; }

    /// <summary>显示名称</summary>
    public String DisplayName { get; set; }

    /// <summary>英文名称</summary>
    public String EnglishName { get; set; }

    /// <summary>旗帜文件名</summary>
    public String FlagImageFileName { get; set; }

    /// <summary>本地化语言标识</summary>
    public String LanguageCulture { get; set; }

    /// <summary>Url缩写</summary>
    public String UniqueSeoCode { get; set; }

    /// <summary>语言简写</summary>
    public String LangAbbreviation { get; set; }

    /// <summary>旗帜</summary>
    public String Flag { get; set; }

    /// <summary>域名</summary>
    public String Domain { get; set; }

    /// <summary>LCID</summary>
    public Int32 Lcid { get; set; }

    /// <summary>状态。是否启用</summary>
    public Boolean Status { get; set; }

    /// <summary>该语言是否支持从右到左</summary>
    public Boolean Rtl { get; set; }

    /// <summary>排序</summary>
    public Int32 DisplayOrder { get; set; }

    /// <summary>是否网站打开默认语言</summary>
    public Byte IsDefault { get; set; }

    /// <summary>描述</summary>
    public String Remark { get; set; }

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
    public void Copy(ILanguage model)
    {
        Id = model.Id;
        Name = model.Name;
        DisplayName = model.DisplayName;
        EnglishName = model.EnglishName;
        FlagImageFileName = model.FlagImageFileName;
        LanguageCulture = model.LanguageCulture;
        UniqueSeoCode = model.UniqueSeoCode;
        LangAbbreviation = model.LangAbbreviation;
        Flag = model.Flag;
        Domain = model.Domain;
        Lcid = model.Lcid;
        Status = model.Status;
        Rtl = model.Rtl;
        DisplayOrder = model.DisplayOrder;
        IsDefault = model.IsDefault;
        Remark = model.Remark;
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
