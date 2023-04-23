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

/// <summary>站点设置</summary>
[Serializable]
[DataObject]
[Description("站点设置")]
[BindIndex("IX_DG_SiteSetting_Key", false, "Key")]
[BindTable("DG_SiteSetting", Description = "站点设置", ConnName = "DG", DbType = DatabaseType.None)]
public partial class SiteSettingInfo : ISiteSettingInfo, IEntity<SiteSettingInfoModel>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _Key;
    /// <summary>键名称</summary>
    [DisplayName("键名称")]
    [Description("键名称")]
    [DataObjectField(false, false, false, 100)]
    [BindColumn("Key", "键名称", "")]
    public String Key { get => _Key; set { if (OnPropertyChanging("Key", value)) { _Key = value; OnPropertyChanged("Key"); } } }

    private String _Value;
    /// <summary>键值</summary>
    [DisplayName("键值")]
    [Description("键值")]
    [DataObjectField(false, false, true, 4000)]
    [BindColumn("Value", "键值", "")]
    public String Value { get => _Value; set { if (OnPropertyChanging("Value", value)) { _Value = value; OnPropertyChanged("Value"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(SiteSettingInfoModel model)
    {
        Id = model.Id;
        Key = model.Key;
        Value = model.Value;
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
            "Key" => _Key,
            "Value" => _Value,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "Key": _Key = Convert.ToString(value); break;
                case "Value": _Value = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得站点设置字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>键名称</summary>
        public static readonly Field Key = FindByName("Key");

        /// <summary>键值</summary>
        public static readonly Field Value = FindByName("Value");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得站点设置字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>键名称</summary>
        public const String Key = "Key";

        /// <summary>键值</summary>
        public const String Value = "Value";
    }
    #endregion
}
