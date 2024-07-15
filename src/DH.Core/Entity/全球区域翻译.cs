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

/// <summary>全球区域翻译</summary>
[Serializable]
[DataObject]
[Description("全球区域翻译")]
[BindIndex("IU_DG_RegionsLan_RId_LId", true, "RId,LId")]
[BindTable("DG_RegionsLan", Description = "全球区域翻译", ConnName = "Regions", DbType = DatabaseType.None)]
public partial class RegionsLan : IRegionsLan, IEntity<IRegionsLan>
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
    /// <summary>关联区域Id</summary>
    [DisplayName("关联区域Id")]
    [Description("关联区域Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("RId", "关联区域Id", "")]
    public Int32 RId { get => _RId; set { if (OnPropertyChanging("RId", value)) { _RId = value; OnPropertyChanged("RId"); } } }

    private Int32 _LId;
    /// <summary>关联所属语言Id</summary>
    [DisplayName("关联所属语言Id")]
    [Description("关联所属语言Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("LId", "关联所属语言Id", "")]
    public Int32 LId { get => _LId; set { if (OnPropertyChanging("LId", value)) { _LId = value; OnPropertyChanged("LId"); } } }

    private String _Name;
    /// <summary>名称</summary>
    [DisplayName("名称")]
    [Description("名称")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Name", "名称", "", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private String _AliasName;
    /// <summary>别名</summary>
    [DisplayName("别名")]
    [Description("别名")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("AliasName", "别名", "", Master = true)]
    public String AliasName { get => _AliasName; set { if (OnPropertyChanging("AliasName", value)) { _AliasName = value; OnPropertyChanged("AliasName"); } } }

    private String _ShortName;
    /// <summary>简称</summary>
    [DisplayName("简称")]
    [Description("简称")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("ShortName", "简称", "", Master = true)]
    public String ShortName { get => _ShortName; set { if (OnPropertyChanging("ShortName", value)) { _ShortName = value; OnPropertyChanged("ShortName"); } } }

    private String _MergerName;
    /// <summary>组合名</summary>
    [DisplayName("组合名")]
    [Description("组合名")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("MergerName", "组合名", "", Master = true)]
    public String MergerName { get => _MergerName; set { if (OnPropertyChanging("MergerName", value)) { _MergerName = value; OnPropertyChanged("MergerName"); } } }

    private String _OtherName;
    /// <summary>自定义简称</summary>
    [DisplayName("自定义简称")]
    [Description("自定义简称")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("OtherName", "自定义简称", "")]
    public String OtherName { get => _OtherName; set { if (OnPropertyChanging("OtherName", value)) { _OtherName = value; OnPropertyChanged("OtherName"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IRegionsLan model)
    {
        Id = model.Id;
        RId = model.RId;
        LId = model.LId;
        Name = model.Name;
        AliasName = model.AliasName;
        ShortName = model.ShortName;
        MergerName = model.MergerName;
        OtherName = model.OtherName;
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
            "AliasName" => _AliasName,
            "ShortName" => _ShortName,
            "MergerName" => _MergerName,
            "OtherName" => _OtherName,
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
                case "AliasName": _AliasName = Convert.ToString(value); break;
                case "ShortName": _ShortName = Convert.ToString(value); break;
                case "MergerName": _MergerName = Convert.ToString(value); break;
                case "OtherName": _OtherName = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 扩展查询
    /// <summary>根据关联区域Id查找</summary>
    /// <param name="rId">关联区域Id</param>
    /// <returns>实体列表</returns>
    public static IList<RegionsLan> FindAllByRId(Int32 rId)
    {
        if (rId < 0) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.RId == rId);

        return FindAll(_.RId == rId);
    }
    #endregion

    #region 字段名
    /// <summary>取得全球区域翻译字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>关联区域Id</summary>
        public static readonly Field RId = FindByName("RId");

        /// <summary>关联所属语言Id</summary>
        public static readonly Field LId = FindByName("LId");

        /// <summary>名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>别名</summary>
        public static readonly Field AliasName = FindByName("AliasName");

        /// <summary>简称</summary>
        public static readonly Field ShortName = FindByName("ShortName");

        /// <summary>组合名</summary>
        public static readonly Field MergerName = FindByName("MergerName");

        /// <summary>自定义简称</summary>
        public static readonly Field OtherName = FindByName("OtherName");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得全球区域翻译字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>关联区域Id</summary>
        public const String RId = "RId";

        /// <summary>关联所属语言Id</summary>
        public const String LId = "LId";

        /// <summary>名称</summary>
        public const String Name = "Name";

        /// <summary>别名</summary>
        public const String AliasName = "AliasName";

        /// <summary>简称</summary>
        public const String ShortName = "ShortName";

        /// <summary>组合名</summary>
        public const String MergerName = "MergerName";

        /// <summary>自定义简称</summary>
        public const String OtherName = "OtherName";
    }
    #endregion
}
