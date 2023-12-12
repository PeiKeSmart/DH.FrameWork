using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>模型列。实体表的数据列</summary>
public partial interface IModelColumn
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>模型表</summary>
    Int32 TableId { get; set; }

    /// <summary>名称</summary>
    String Name { get; set; }

    /// <summary>显示名</summary>
    String DisplayName { get; set; }

    /// <summary>启用</summary>
    Boolean Enable { get; set; }

    /// <summary>数据类型</summary>
    String DataType { get; set; }

    /// <summary>元素类型。image,file,html,singleSelect,multipleSelect</summary>
    String ItemType { get; set; }

    /// <summary>主键</summary>
    Boolean PrimaryKey { get; set; }

    /// <summary>主字段。主字段作为业务主要字段，代表当前数据行意义</summary>
    Boolean Master { get; set; }

    /// <summary>长度</summary>
    Int32 Length { get; set; }

    /// <summary>允许空</summary>
    Boolean Nullable { get; set; }

    /// <summary>数据字段</summary>
    Boolean IsDataObjectField { get; set; }

    /// <summary>说明</summary>
    String Description { get; set; }

    /// <summary>列表页显示</summary>
    Boolean ShowInList { get; set; }

    /// <summary>添加表单页显示</summary>
    Boolean ShowInAddForm { get; set; }

    /// <summary>编辑表单页显示</summary>
    Boolean ShowInEditForm { get; set; }

    /// <summary>详情表单页显示</summary>
    Boolean ShowInDetailForm { get; set; }

    /// <summary>搜索显示</summary>
    Boolean ShowInSearch { get; set; }

    /// <summary>排序</summary>
    Int32 Sort { get; set; }

    /// <summary>宽度</summary>
    String Width { get; set; }

    /// <summary>单元格文字</summary>
    String CellText { get; set; }

    /// <summary>单元格标题。数据单元格上的提示文字</summary>
    String CellTitle { get; set; }

    /// <summary>单元格链接。数据单元格的链接</summary>
    String CellUrl { get; set; }

    /// <summary>头部文字</summary>
    String HeaderText { get; set; }

    /// <summary>头部标题。数据移上去后显示的文字</summary>
    String HeaderTitle { get; set; }

    /// <summary>头部链接。一般是排序</summary>
    String HeaderUrl { get; set; }

    /// <summary>数据动作。设为action时走ajax请求</summary>
    String DataAction { get; set; }

    /// <summary>多选数据源</summary>
    String DataSource { get; set; }

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
    #endregion
}
