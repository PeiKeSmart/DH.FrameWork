using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>其他消息模板翻译表</summary>
public partial interface IOtherMsgTplLan
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>关联其他消息模板Id</summary>
    Int32 OId { get; set; }

    /// <summary>关联所属语言Id</summary>
    Int32 LId { get; set; }

    /// <summary>模板名称</summary>
    String MName { get; set; }

    /// <summary>模板标题</summary>
    String MTitle { get; set; }

    /// <summary>模板内容</summary>
    String MContent { get; set; }

    /// <summary>短信模板Id</summary>
    String SmsTplId { get; set; }
    #endregion
}
