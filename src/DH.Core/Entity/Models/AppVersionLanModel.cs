using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>APP版本翻译</summary>
public partial class AppVersionLanModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>APP版本Id</summary>
    public Int32 AId { get; set; }

    /// <summary>关联所属语言Id</summary>
    public Int32 LId { get; set; }

    /// <summary>升级内容</summary>
    public String Content { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IAppVersionLan model)
    {
        Id = model.Id;
        AId = model.AId;
        LId = model.LId;
        Content = model.Content;
    }
    #endregion
}
