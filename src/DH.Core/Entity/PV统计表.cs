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
using XCode.Common;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace DH.Entity;

/// <summary>PV统计表</summary>
[Serializable]
[DataObject]
[Description("PV统计表")]
[BindTable("DH_PvStats", Description = "PV统计表", ConnName = "DG", DbType = DatabaseType.None)]
public partial class SysPvstats : ISysPvstats, IEntity<ISysPvstats>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "int(11)")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _Category;
    /// <summary>分类</summary>
    [DisplayName("分类")]
    [Description("分类")]
    [DataObjectField(false, false, false, 10)]
    [BindColumn("Category", "分类", "varchar(10)")]
    public String Category { get => _Category; set { if (OnPropertyChanging("Category", value)) { _Category = value; OnPropertyChanged("Category"); } } }

    private String _Value;
    /// <summary>访问者系统</summary>
    [DisplayName("访问者系统")]
    [Description("访问者系统")]
    [DataObjectField(false, false, false, 30)]
    [BindColumn("Value", "访问者系统", "varchar(30)")]
    public String Value { get => _Value; set { if (OnPropertyChanging("Value", value)) { _Value = value; OnPropertyChanged("Value"); } } }

    private Int32 _Count;
    /// <summary>数量</summary>
    [DisplayName("数量")]
    [Description("数量")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Count", "数量", "int(11)")]
    public Int32 Count { get => _Count; set { if (OnPropertyChanging("Count", value)) { _Count = value; OnPropertyChanged("Count"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISysPvstats model)
    {
        Id = model.Id;
        Category = model.Category;
        Value = model.Value;
        Count = model.Count;
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
            "Category" => _Category,
            "Value" => _Value,
            "Count" => _Count,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "Category": _Category = Convert.ToString(value); break;
                case "Value": _Value = Convert.ToString(value); break;
                case "Count": _Count = value.ToInt(); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得PV统计表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>分类</summary>
        public static readonly Field Category = FindByName("Category");

        /// <summary>访问者系统</summary>
        public static readonly Field Value = FindByName("Value");

        /// <summary>数量</summary>
        public static readonly Field Count = FindByName("Count");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得PV统计表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>分类</summary>
        public const String Category = "Category";

        /// <summary>访问者系统</summary>
        public const String Value = "Value";

        /// <summary>数量</summary>
        public const String Count = "Count";
    }
    #endregion
}
