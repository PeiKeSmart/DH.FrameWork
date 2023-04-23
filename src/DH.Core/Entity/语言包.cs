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

/// <summary>语言包</summary>
[Serializable]
[DataObject]
[Description("语言包")]
[BindIndex("IU_DG_LocaleStringResource_LanKey_CultureId", true, "LanKey,CultureId")]
[BindTable("DG_LocaleStringResource", Description = "语言包", ConnName = "DG", DbType = DatabaseType.None)]
public partial class LocaleStringResource : ILocaleStringResource, IEntity<LocaleStringResourceModel>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _LanKey;
    /// <summary>资源名称</summary>
    [DisplayName("资源名称")]
    [Description("资源名称")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("LanKey", "资源名称", "", Master = true)]
    public String LanKey { get => _LanKey; set { if (OnPropertyChanging("LanKey", value)) { _LanKey = value; OnPropertyChanged("LanKey"); } } }

    private String _LanValue;
    /// <summary>资源值</summary>
    [DisplayName("资源值")]
    [Description("资源值")]
    [DataObjectField(false, false, true, 2048)]
    [BindColumn("LanValue", "资源值", "")]
    public String LanValue { get => _LanValue; set { if (OnPropertyChanging("LanValue", value)) { _LanValue = value; OnPropertyChanged("LanValue"); } } }

    private Int32 _CultureId;
    /// <summary>语言标识符</summary>
    [DisplayName("语言标识符")]
    [Description("语言标识符")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CultureId", "语言标识符", "")]
    public Int32 CultureId { get => _CultureId; set { if (OnPropertyChanging("CultureId", value)) { _CultureId = value; OnPropertyChanged("CultureId"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(LocaleStringResourceModel model)
    {
        Id = model.Id;
        LanKey = model.LanKey;
        LanValue = model.LanValue;
        CultureId = model.CultureId;
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
            "LanKey" => _LanKey,
            "LanValue" => _LanValue,
            "CultureId" => _CultureId,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "LanKey": _LanKey = Convert.ToString(value); break;
                case "LanValue": _LanValue = Convert.ToString(value); break;
                case "CultureId": _CultureId = value.ToInt(); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得语言包字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>资源名称</summary>
        public static readonly Field LanKey = FindByName("LanKey");

        /// <summary>资源值</summary>
        public static readonly Field LanValue = FindByName("LanValue");

        /// <summary>语言标识符</summary>
        public static readonly Field CultureId = FindByName("CultureId");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得语言包字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>资源名称</summary>
        public const String LanKey = "LanKey";

        /// <summary>资源值</summary>
        public const String LanValue = "LanValue";

        /// <summary>语言标识符</summary>
        public const String CultureId = "CultureId";
    }
    #endregion
}
