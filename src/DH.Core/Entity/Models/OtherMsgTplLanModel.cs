using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>其他消息模板翻译表</summary>
public partial class OtherMsgTplLanModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>关联其他消息模板Id</summary>
    public Int32 OId { get; set; }

    /// <summary>关联所属语言Id</summary>
    public Int32 LId { get; set; }

    /// <summary>模板名称</summary>
    public String MName { get; set; }

    /// <summary>模板标题</summary>
    public String MTitle { get; set; }

    /// <summary>模板内容</summary>
    public String MContent { get; set; }

    /// <summary>短信模板Id</summary>
    public String SmsTplId { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IOtherMsgTplLan model)
    {
        Id = model.Id;
        OId = model.OId;
        LId = model.LId;
        MName = model.MName;
        MTitle = model.MTitle;
        MContent = model.MContent;
        SmsTplId = model.SmsTplId;
    }
    #endregion
}
