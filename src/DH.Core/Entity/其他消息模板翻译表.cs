using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace DH.Entity;

/// <summary>其他消息模板翻译表</summary>
[Serializable]
[DataObject]
[Description("其他消息模板翻译表")]
[BindIndex("IU_DG_OtherMsgTplLan_OId_LId", true, "OId,LId")]
[BindTable("DG_OtherMsgTplLan", Description = "其他消息模板翻译表", ConnName = "DG", DbType = DatabaseType.None)]
public partial class OtherMsgTplLan : IOtherMsgTplLan, IEntity<IOtherMsgTplLan>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private Int32 _OId;
    /// <summary>关联其他消息模板Id</summary>
    [DisplayName("关联其他消息模板Id")]
    [Description("关联其他消息模板Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("OId", "关联其他消息模板Id", "")]
    public Int32 OId { get => _OId; set { if (OnPropertyChanging("OId", value)) { _OId = value; OnPropertyChanged("OId"); } } }

    private Int32 _LId;
    /// <summary>关联所属语言Id</summary>
    [DisplayName("关联所属语言Id")]
    [Description("关联所属语言Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("LId", "关联所属语言Id", "")]
    public Int32 LId { get => _LId; set { if (OnPropertyChanging("LId", value)) { _LId = value; OnPropertyChanged("LId"); } } }

    private String _MName;
    /// <summary>模板名称</summary>
    [DisplayName("模板名称")]
    [Description("模板名称")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("MName", "模板名称", "", Master = true)]
    public String MName { get => _MName; set { if (OnPropertyChanging("MName", value)) { _MName = value; OnPropertyChanged("MName"); } } }

    private String _MTitle;
    /// <summary>模板标题</summary>
    [DisplayName("模板标题")]
    [Description("模板标题")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("MTitle", "模板标题", "")]
    public String MTitle { get => _MTitle; set { if (OnPropertyChanging("MTitle", value)) { _MTitle = value; OnPropertyChanged("MTitle"); } } }

    private String _MContent;
    /// <summary>模板内容</summary>
    [DisplayName("模板内容")]
    [Description("模板内容")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("MContent", "模板内容", "")]
    public String MContent { get => _MContent; set { if (OnPropertyChanging("MContent", value)) { _MContent = value; OnPropertyChanged("MContent"); } } }

    private String _SmsTplId;
    /// <summary>短信模板Id</summary>
    [DisplayName("短信模板Id")]
    [Description("短信模板Id")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("SmsTplId", "短信模板Id", "")]
    public String SmsTplId { get => _SmsTplId; set { if (OnPropertyChanging("SmsTplId", value)) { _SmsTplId = value; OnPropertyChanged("SmsTplId"); } } }
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

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public override Object this[String name]
    {
        get => name switch
        {
            "Id" => _Id,
            "OId" => _OId,
            "LId" => _LId,
            "MName" => _MName,
            "MTitle" => _MTitle,
            "MContent" => _MContent,
            "SmsTplId" => _SmsTplId,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "OId": _OId = value.ToInt(); break;
                case "LId": _LId = value.ToInt(); break;
                case "MName": _MName = Convert.ToString(value); break;
                case "MTitle": _MTitle = Convert.ToString(value); break;
                case "MContent": _MContent = Convert.ToString(value); break;
                case "SmsTplId": _SmsTplId = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 扩展查询
    #endregion

    #region 字段名
    /// <summary>取得其他消息模板翻译表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>关联其他消息模板Id</summary>
        public static readonly Field OId = FindByName("OId");

        /// <summary>关联所属语言Id</summary>
        public static readonly Field LId = FindByName("LId");

        /// <summary>模板名称</summary>
        public static readonly Field MName = FindByName("MName");

        /// <summary>模板标题</summary>
        public static readonly Field MTitle = FindByName("MTitle");

        /// <summary>模板内容</summary>
        public static readonly Field MContent = FindByName("MContent");

        /// <summary>短信模板Id</summary>
        public static readonly Field SmsTplId = FindByName("SmsTplId");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得其他消息模板翻译表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>关联其他消息模板Id</summary>
        public const String OId = "OId";

        /// <summary>关联所属语言Id</summary>
        public const String LId = "LId";

        /// <summary>模板名称</summary>
        public const String MName = "MName";

        /// <summary>模板标题</summary>
        public const String MTitle = "MTitle";

        /// <summary>模板内容</summary>
        public const String MContent = "MContent";

        /// <summary>短信模板Id</summary>
        public const String SmsTplId = "SmsTplId";
    }
    #endregion
}
