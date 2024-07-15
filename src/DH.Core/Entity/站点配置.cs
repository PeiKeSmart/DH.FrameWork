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

/// <summary>站点配置</summary>
[Serializable]
[DataObject]
[Description("站点配置")]
[BindIndex("IU_DH_Setting_Name_StoreId", true, "Name,StoreId")]
[BindTable("DH_Setting", Description = "站点配置", ConnName = "DG", DbType = DatabaseType.None)]
public partial class Setting : ISetting, IEntity<ISetting>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _Name;
    /// <summary>键名称</summary>
    [DisplayName("键名称")]
    [Description("键名称")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("Name", "键名称", "", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private String _Value;
    /// <summary>值</summary>
    [DisplayName("值")]
    [Description("值")]
    [DataObjectField(false, false, true, 6000)]
    [BindColumn("Value", "值", "")]
    public String Value { get => _Value; set { if (OnPropertyChanging("Value", value)) { _Value = value; OnPropertyChanged("Value"); } } }

    private Int32 _StoreId;
    /// <summary>站点Id</summary>
    [DisplayName("站点Id")]
    [Description("站点Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("StoreId", "站点Id", "")]
    public Int32 StoreId { get => _StoreId; set { if (OnPropertyChanging("StoreId", value)) { _StoreId = value; OnPropertyChanged("StoreId"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISetting model)
    {
        Id = model.Id;
        Name = model.Name;
        Value = model.Value;
        StoreId = model.StoreId;
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
            "Name" => _Name,
            "Value" => _Value,
            "StoreId" => _StoreId,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "Name": _Name = Convert.ToString(value); break;
                case "Value": _Value = Convert.ToString(value); break;
                case "StoreId": _StoreId = value.ToInt(); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 扩展查询
    /// <summary>根据键名称查找</summary>
    /// <param name="name">键名称</param>
    /// <returns>实体列表</returns>
    public static IList<Setting> FindAllByName(String name)
    {
        if (name.IsNullOrEmpty()) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.Name.EqualIgnoreCase(name));

        return FindAll(_.Name == name);
    }
    #endregion

    #region 字段名
    /// <summary>取得站点配置字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>键名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>值</summary>
        public static readonly Field Value = FindByName("Value");

        /// <summary>站点Id</summary>
        public static readonly Field StoreId = FindByName("StoreId");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得站点配置字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>键名称</summary>
        public const String Name = "Name";

        /// <summary>值</summary>
        public const String Value = "Value";

        /// <summary>站点Id</summary>
        public const String StoreId = "StoreId";
    }
    #endregion
}
