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

/// <summary>角色翻译表</summary>
[Serializable]
[DataObject]
[Description("角色翻译表")]
[BindIndex("IU_DG_RoleLan_RId_LId", true, "RId,LId")]
[BindTable("DG_RoleLan", Description = "角色翻译表", ConnName = "DG", DbType = DatabaseType.None)]
public partial class RoleLan : IRoleLan, IEntity<IRoleLan>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private Int32 _RId;
    /// <summary>关联角色表Id</summary>
    [DisplayName("关联角色表Id")]
    [Description("关联角色表Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("RId", "关联角色表Id", "")]
    public Int32 RId { get => _RId; set { if (OnPropertyChanging("RId", value)) { _RId = value; OnPropertyChanged("RId"); } } }

    private Int32 _LId;
    /// <summary>关联所属语言Id</summary>
    [DisplayName("关联所属语言Id")]
    [Description("关联所属语言Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("LId", "关联所属语言Id", "")]
    public Int32 LId { get => _LId; set { if (OnPropertyChanging("LId", value)) { _LId = value; OnPropertyChanged("LId"); } } }

    private String _Name;
    /// <summary>角色分组名称</summary>
    [DisplayName("角色分组名称")]
    [Description("角色分组名称")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Name", "角色分组名称", "", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private String _Remark;
    /// <summary>备注</summary>
    [DisplayName("备注")]
    [Description("备注")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("Remark", "备注", "")]
    public String Remark { get => _Remark; set { if (OnPropertyChanging("Remark", value)) { _Remark = value; OnPropertyChanged("Remark"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IRoleLan model)
    {
        Id = model.Id;
        RId = model.RId;
        LId = model.LId;
        Name = model.Name;
        Remark = model.Remark;
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
            "RId" => _RId,
            "LId" => _LId,
            "Name" => _Name,
            "Remark" => _Remark,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "RId": _RId = value.ToInt(); break;
                case "LId": _LId = value.ToInt(); break;
                case "Name": _Name = Convert.ToString(value); break;
                case "Remark": _Remark = Convert.ToString(value); break;
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
    /// <summary>取得角色翻译表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>关联角色表Id</summary>
        public static readonly Field RId = FindByName("RId");

        /// <summary>关联所属语言Id</summary>
        public static readonly Field LId = FindByName("LId");

        /// <summary>角色分组名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>备注</summary>
        public static readonly Field Remark = FindByName("Remark");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得角色翻译表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>关联角色表Id</summary>
        public const String RId = "RId";

        /// <summary>关联所属语言Id</summary>
        public const String LId = "LId";

        /// <summary>角色分组名称</summary>
        public const String Name = "Name";

        /// <summary>备注</summary>
        public const String Remark = "Remark";
    }
    #endregion
}
