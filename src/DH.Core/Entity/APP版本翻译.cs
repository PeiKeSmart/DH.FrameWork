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

/// <summary>APP版本翻译</summary>
[Serializable]
[DataObject]
[Description("APP版本翻译")]
[BindIndex("IU_DG_AppVersionLan_AId_LId", true, "AId,LId")]
[BindTable("DG_AppVersionLan", Description = "APP版本翻译", ConnName = "DG", DbType = DatabaseType.None)]
public partial class AppVersionLan : IAppVersionLan, IEntity<IAppVersionLan>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private Int32 _AId;
    /// <summary>APP版本Id</summary>
    [DisplayName("APP版本Id")]
    [Description("APP版本Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("AId", "APP版本Id", "")]
    public Int32 AId { get => _AId; set { if (OnPropertyChanging("AId", value)) { _AId = value; OnPropertyChanged("AId"); } } }

    private Int32 _LId;
    /// <summary>关联所属语言Id</summary>
    [DisplayName("关联所属语言Id")]
    [Description("关联所属语言Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("LId", "关联所属语言Id", "")]
    public Int32 LId { get => _LId; set { if (OnPropertyChanging("LId", value)) { _LId = value; OnPropertyChanged("LId"); } } }

    private String _Content;
    /// <summary>升级内容</summary>
    [DisplayName("升级内容")]
    [Description("升级内容")]
    [DataObjectField(false, false, true, 1024)]
    [BindColumn("Content", "升级内容", "")]
    public String Content { get => _Content; set { if (OnPropertyChanging("Content", value)) { _Content = value; OnPropertyChanged("Content"); } } }
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

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public override Object this[String name]
    {
        get => name switch
        {
            "Id" => _Id,
            "AId" => _AId,
            "LId" => _LId,
            "Content" => _Content,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "AId": _AId = value.ToInt(); break;
                case "LId": _LId = value.ToInt(); break;
                case "Content": _Content = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得APP版本翻译字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>APP版本Id</summary>
        public static readonly Field AId = FindByName("AId");

        /// <summary>关联所属语言Id</summary>
        public static readonly Field LId = FindByName("LId");

        /// <summary>升级内容</summary>
        public static readonly Field Content = FindByName("Content");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得APP版本翻译字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>APP版本Id</summary>
        public const String AId = "AId";

        /// <summary>关联所属语言Id</summary>
        public const String LId = "LId";

        /// <summary>升级内容</summary>
        public const String Content = "Content";
    }
    #endregion
}
