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

/// <summary>国家翻译</summary>
[Serializable]
[DataObject]
[Description("国家翻译")]
[BindIndex("IU_DG_CountryLan_CId_LId", true, "CId,LId")]
[BindTable("DG_CountryLan", Description = "国家翻译", ConnName = "Regions", DbType = DatabaseType.None)]
public partial class CountryLan : ICountryLan, IEntity<ICountryLan>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private Int32 _CId;
    /// <summary>关联国家Id</summary>
    [DisplayName("关联国家Id")]
    [Description("关联国家Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CId", "关联国家Id", "")]
    public Int32 CId { get => _CId; set { if (OnPropertyChanging("CId", value)) { _CId = value; OnPropertyChanged("CId"); } } }

    private Int32 _LId;
    /// <summary>关联所属语言Id</summary>
    [DisplayName("关联所属语言Id")]
    [Description("关联所属语言Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("LId", "关联所属语言Id", "")]
    public Int32 LId { get => _LId; set { if (OnPropertyChanging("LId", value)) { _LId = value; OnPropertyChanged("LId"); } } }

    private String _Name;
    /// <summary>国家名称</summary>
    [DisplayName("国家名称")]
    [Description("国家名称")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Name", "国家名称", "varchar(100)", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ICountryLan model)
    {
        Id = model.Id;
        CId = model.CId;
        LId = model.LId;
        Name = model.Name;
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
            "CId" => _CId,
            "LId" => _LId,
            "Name" => _Name,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "CId": _CId = value.ToInt(); break;
                case "LId": _LId = value.ToInt(); break;
                case "Name": _Name = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得国家翻译字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>关联国家Id</summary>
        public static readonly Field CId = FindByName("CId");

        /// <summary>关联所属语言Id</summary>
        public static readonly Field LId = FindByName("LId");

        /// <summary>国家名称</summary>
        public static readonly Field Name = FindByName("Name");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得国家翻译字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>关联国家Id</summary>
        public const String CId = "CId";

        /// <summary>关联所属语言Id</summary>
        public const String LId = "LId";

        /// <summary>国家名称</summary>
        public const String Name = "Name";
    }
    #endregion
}
