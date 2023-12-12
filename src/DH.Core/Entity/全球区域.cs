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

/// <summary>全球区域</summary>
[Serializable]
[DataObject]
[Description("全球区域")]
[BindIndex("IX_DG_Regions_CId_Level", false, "CId,Level")]
[BindIndex("IX_DG_Regions_ParentCode", false, "ParentCode")]
[BindIndex("IU_DG_Regions_AreaCode", true, "AreaCode")]
[BindIndex("IX_DG_Regions_CityId", false, "CityId")]
[BindTable("DG_Regions", Description = "全球区域", ConnName = "Regions", DbType = DatabaseType.None)]
public partial class Regions : IRegions, IEntity<IRegions>
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
    /// <summary>国家编号</summary>
    [DisplayName("国家编号")]
    [Description("国家编号")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CId", "国家编号", "")]
    public Int32 CId { get => _CId; set { if (OnPropertyChanging("CId", value)) { _CId = value; OnPropertyChanged("CId"); } } }

    private Int32 _Level;
    /// <summary>层级</summary>
    [DisplayName("层级")]
    [Description("层级")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Level", "层级", "tinyint(1)")]
    public Int32 Level { get => _Level; set { if (OnPropertyChanging("Level", value)) { _Level = value; OnPropertyChanged("Level"); } } }

    private Int64 _ParentCode;
    /// <summary>父级行政代码</summary>
    [DisplayName("父级行政代码")]
    [Description("父级行政代码")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("ParentCode", "父级行政代码", "")]
    public Int64 ParentCode { get => _ParentCode; set { if (OnPropertyChanging("ParentCode", value)) { _ParentCode = value; OnPropertyChanged("ParentCode"); } } }

    private Int64 _AreaCode;
    /// <summary>行政代码</summary>
    [DisplayName("行政代码")]
    [Description("行政代码")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("AreaCode", "行政代码", "")]
    public Int64 AreaCode { get => _AreaCode; set { if (OnPropertyChanging("AreaCode", value)) { _AreaCode = value; OnPropertyChanged("AreaCode"); } } }

    private String _ZipCode;
    /// <summary>邮政编码</summary>
    [DisplayName("邮政编码")]
    [Description("邮政编码")]
    [DataObjectField(false, false, true, 6)]
    [BindColumn("ZipCode", "邮政编码", "")]
    public String ZipCode { get => _ZipCode; set { if (OnPropertyChanging("ZipCode", value)) { _ZipCode = value; OnPropertyChanged("ZipCode"); } } }

    private String _CityCode;
    /// <summary>区号</summary>
    [DisplayName("区号")]
    [Description("区号")]
    [DataObjectField(false, false, true, 6)]
    [BindColumn("CityCode", "区号", "")]
    public String CityCode { get => _CityCode; set { if (OnPropertyChanging("CityCode", value)) { _CityCode = value; OnPropertyChanged("CityCode"); } } }

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
    [BindColumn("ShortName", "简称", "")]
    public String ShortName { get => _ShortName; set { if (OnPropertyChanging("ShortName", value)) { _ShortName = value; OnPropertyChanged("ShortName"); } } }

    private String _OtherName;
    /// <summary>自定义简称</summary>
    [DisplayName("自定义简称")]
    [Description("自定义简称")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("OtherName", "自定义简称", "")]
    public String OtherName { get => _OtherName; set { if (OnPropertyChanging("OtherName", value)) { _OtherName = value; OnPropertyChanged("OtherName"); } } }

    private String _Regional;
    /// <summary>大区名称</summary>
    [DisplayName("大区名称")]
    [Description("大区名称")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Regional", "大区名称", "")]
    public String Regional { get => _Regional; set { if (OnPropertyChanging("Regional", value)) { _Regional = value; OnPropertyChanged("Regional"); } } }

    private String _MergerName;
    /// <summary>组合名</summary>
    [DisplayName("组合名")]
    [Description("组合名")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("MergerName", "组合名", "")]
    public String MergerName { get => _MergerName; set { if (OnPropertyChanging("MergerName", value)) { _MergerName = value; OnPropertyChanged("MergerName"); } } }

    private String _PinYin;
    /// <summary>拼音</summary>
    [DisplayName("拼音")]
    [Description("拼音")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("PinYin", "拼音", "")]
    public String PinYin { get => _PinYin; set { if (OnPropertyChanging("PinYin", value)) { _PinYin = value; OnPropertyChanged("PinYin"); } } }

    private Decimal _Lng;
    /// <summary>经度</summary>
    [DisplayName("经度")]
    [Description("经度")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Lng", "经度", "")]
    public Decimal Lng { get => _Lng; set { if (OnPropertyChanging("Lng", value)) { _Lng = value; OnPropertyChanged("Lng"); } } }

    private Decimal _Lat;
    /// <summary>纬度</summary>
    [DisplayName("纬度")]
    [Description("纬度")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Lat", "纬度", "")]
    public Decimal Lat { get => _Lat; set { if (OnPropertyChanging("Lat", value)) { _Lat = value; OnPropertyChanged("Lat"); } } }

    private Int32 _CityId;
    /// <summary>城市Id</summary>
    [DisplayName("城市Id")]
    [Description("城市Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CityId", "城市Id", "")]
    public Int32 CityId { get => _CityId; set { if (OnPropertyChanging("CityId", value)) { _CityId = value; OnPropertyChanged("CityId"); } } }

    private Int32 _Sort;
    /// <summary>排序</summary>
    [DisplayName("排序")]
    [Description("排序")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Sort", "排序", "")]
    public Int32 Sort { get => _Sort; set { if (OnPropertyChanging("Sort", value)) { _Sort = value; OnPropertyChanged("Sort"); } } }

    private String _CreateUser;
    /// <summary>创建者</summary>
    [DisplayName("创建者")]
    [Description("创建者")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CreateUser", "创建者", "")]
    public String CreateUser { get => _CreateUser; set { if (OnPropertyChanging("CreateUser", value)) { _CreateUser = value; OnPropertyChanged("CreateUser"); } } }

    private Int32 _CreateUserID;
    /// <summary>创建者</summary>
    [DisplayName("创建者")]
    [Description("创建者")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CreateUserID", "创建者", "")]
    public Int32 CreateUserID { get => _CreateUserID; set { if (OnPropertyChanging("CreateUserID", value)) { _CreateUserID = value; OnPropertyChanged("CreateUserID"); } } }

    private DateTime _CreateTime;
    /// <summary>创建时间</summary>
    [DisplayName("创建时间")]
    [Description("创建时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("CreateTime", "创建时间", "")]
    public DateTime CreateTime { get => _CreateTime; set { if (OnPropertyChanging("CreateTime", value)) { _CreateTime = value; OnPropertyChanged("CreateTime"); } } }

    private String _CreateIP;
    /// <summary>创建地址</summary>
    [DisplayName("创建地址")]
    [Description("创建地址")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CreateIP", "创建地址", "")]
    public String CreateIP { get => _CreateIP; set { if (OnPropertyChanging("CreateIP", value)) { _CreateIP = value; OnPropertyChanged("CreateIP"); } } }

    private String _UpdateUser;
    /// <summary>更新者</summary>
    [DisplayName("更新者")]
    [Description("更新者")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("UpdateUser", "更新者", "")]
    public String UpdateUser { get => _UpdateUser; set { if (OnPropertyChanging("UpdateUser", value)) { _UpdateUser = value; OnPropertyChanged("UpdateUser"); } } }

    private Int32 _UpdateUserID;
    /// <summary>更新者</summary>
    [DisplayName("更新者")]
    [Description("更新者")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("UpdateUserID", "更新者", "")]
    public Int32 UpdateUserID { get => _UpdateUserID; set { if (OnPropertyChanging("UpdateUserID", value)) { _UpdateUserID = value; OnPropertyChanged("UpdateUserID"); } } }

    private DateTime _UpdateTime;
    /// <summary>更新时间</summary>
    [DisplayName("更新时间")]
    [Description("更新时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("UpdateTime", "更新时间", "")]
    public DateTime UpdateTime { get => _UpdateTime; set { if (OnPropertyChanging("UpdateTime", value)) { _UpdateTime = value; OnPropertyChanged("UpdateTime"); } } }

    private String _UpdateIP;
    /// <summary>更新地址</summary>
    [DisplayName("更新地址")]
    [Description("更新地址")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("UpdateIP", "更新地址", "")]
    public String UpdateIP { get => _UpdateIP; set { if (OnPropertyChanging("UpdateIP", value)) { _UpdateIP = value; OnPropertyChanged("UpdateIP"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IRegions model)
    {
        Id = model.Id;
        CId = model.CId;
        Level = model.Level;
        ParentCode = model.ParentCode;
        AreaCode = model.AreaCode;
        ZipCode = model.ZipCode;
        CityCode = model.CityCode;
        Name = model.Name;
        AliasName = model.AliasName;
        ShortName = model.ShortName;
        OtherName = model.OtherName;
        Regional = model.Regional;
        MergerName = model.MergerName;
        PinYin = model.PinYin;
        Lng = model.Lng;
        Lat = model.Lat;
        CityId = model.CityId;
        Sort = model.Sort;
        CreateUser = model.CreateUser;
        CreateUserID = model.CreateUserID;
        CreateTime = model.CreateTime;
        CreateIP = model.CreateIP;
        UpdateUser = model.UpdateUser;
        UpdateUserID = model.UpdateUserID;
        UpdateTime = model.UpdateTime;
        UpdateIP = model.UpdateIP;
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
            "Level" => _Level,
            "ParentCode" => _ParentCode,
            "AreaCode" => _AreaCode,
            "ZipCode" => _ZipCode,
            "CityCode" => _CityCode,
            "Name" => _Name,
            "AliasName" => _AliasName,
            "ShortName" => _ShortName,
            "OtherName" => _OtherName,
            "Regional" => _Regional,
            "MergerName" => _MergerName,
            "PinYin" => _PinYin,
            "Lng" => _Lng,
            "Lat" => _Lat,
            "CityId" => _CityId,
            "Sort" => _Sort,
            "CreateUser" => _CreateUser,
            "CreateUserID" => _CreateUserID,
            "CreateTime" => _CreateTime,
            "CreateIP" => _CreateIP,
            "UpdateUser" => _UpdateUser,
            "UpdateUserID" => _UpdateUserID,
            "UpdateTime" => _UpdateTime,
            "UpdateIP" => _UpdateIP,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "CId": _CId = value.ToInt(); break;
                case "Level": _Level = value.ToInt(); break;
                case "ParentCode": _ParentCode = value.ToLong(); break;
                case "AreaCode": _AreaCode = value.ToLong(); break;
                case "ZipCode": _ZipCode = Convert.ToString(value); break;
                case "CityCode": _CityCode = Convert.ToString(value); break;
                case "Name": _Name = Convert.ToString(value); break;
                case "AliasName": _AliasName = Convert.ToString(value); break;
                case "ShortName": _ShortName = Convert.ToString(value); break;
                case "OtherName": _OtherName = Convert.ToString(value); break;
                case "Regional": _Regional = Convert.ToString(value); break;
                case "MergerName": _MergerName = Convert.ToString(value); break;
                case "PinYin": _PinYin = Convert.ToString(value); break;
                case "Lng": _Lng = Convert.ToDecimal(value); break;
                case "Lat": _Lat = Convert.ToDecimal(value); break;
                case "CityId": _CityId = value.ToInt(); break;
                case "Sort": _Sort = value.ToInt(); break;
                case "CreateUser": _CreateUser = Convert.ToString(value); break;
                case "CreateUserID": _CreateUserID = value.ToInt(); break;
                case "CreateTime": _CreateTime = value.ToDateTime(); break;
                case "CreateIP": _CreateIP = Convert.ToString(value); break;
                case "UpdateUser": _UpdateUser = Convert.ToString(value); break;
                case "UpdateUserID": _UpdateUserID = value.ToInt(); break;
                case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
                case "UpdateIP": _UpdateIP = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得全球区域字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>国家编号</summary>
        public static readonly Field CId = FindByName("CId");

        /// <summary>层级</summary>
        public static readonly Field Level = FindByName("Level");

        /// <summary>父级行政代码</summary>
        public static readonly Field ParentCode = FindByName("ParentCode");

        /// <summary>行政代码</summary>
        public static readonly Field AreaCode = FindByName("AreaCode");

        /// <summary>邮政编码</summary>
        public static readonly Field ZipCode = FindByName("ZipCode");

        /// <summary>区号</summary>
        public static readonly Field CityCode = FindByName("CityCode");

        /// <summary>名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>别名</summary>
        public static readonly Field AliasName = FindByName("AliasName");

        /// <summary>简称</summary>
        public static readonly Field ShortName = FindByName("ShortName");

        /// <summary>自定义简称</summary>
        public static readonly Field OtherName = FindByName("OtherName");

        /// <summary>大区名称</summary>
        public static readonly Field Regional = FindByName("Regional");

        /// <summary>组合名</summary>
        public static readonly Field MergerName = FindByName("MergerName");

        /// <summary>拼音</summary>
        public static readonly Field PinYin = FindByName("PinYin");

        /// <summary>经度</summary>
        public static readonly Field Lng = FindByName("Lng");

        /// <summary>纬度</summary>
        public static readonly Field Lat = FindByName("Lat");

        /// <summary>城市Id</summary>
        public static readonly Field CityId = FindByName("CityId");

        /// <summary>排序</summary>
        public static readonly Field Sort = FindByName("Sort");

        /// <summary>创建者</summary>
        public static readonly Field CreateUser = FindByName("CreateUser");

        /// <summary>创建者</summary>
        public static readonly Field CreateUserID = FindByName("CreateUserID");

        /// <summary>创建时间</summary>
        public static readonly Field CreateTime = FindByName("CreateTime");

        /// <summary>创建地址</summary>
        public static readonly Field CreateIP = FindByName("CreateIP");

        /// <summary>更新者</summary>
        public static readonly Field UpdateUser = FindByName("UpdateUser");

        /// <summary>更新者</summary>
        public static readonly Field UpdateUserID = FindByName("UpdateUserID");

        /// <summary>更新时间</summary>
        public static readonly Field UpdateTime = FindByName("UpdateTime");

        /// <summary>更新地址</summary>
        public static readonly Field UpdateIP = FindByName("UpdateIP");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得全球区域字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>国家编号</summary>
        public const String CId = "CId";

        /// <summary>层级</summary>
        public const String Level = "Level";

        /// <summary>父级行政代码</summary>
        public const String ParentCode = "ParentCode";

        /// <summary>行政代码</summary>
        public const String AreaCode = "AreaCode";

        /// <summary>邮政编码</summary>
        public const String ZipCode = "ZipCode";

        /// <summary>区号</summary>
        public const String CityCode = "CityCode";

        /// <summary>名称</summary>
        public const String Name = "Name";

        /// <summary>别名</summary>
        public const String AliasName = "AliasName";

        /// <summary>简称</summary>
        public const String ShortName = "ShortName";

        /// <summary>自定义简称</summary>
        public const String OtherName = "OtherName";

        /// <summary>大区名称</summary>
        public const String Regional = "Regional";

        /// <summary>组合名</summary>
        public const String MergerName = "MergerName";

        /// <summary>拼音</summary>
        public const String PinYin = "PinYin";

        /// <summary>经度</summary>
        public const String Lng = "Lng";

        /// <summary>纬度</summary>
        public const String Lat = "Lat";

        /// <summary>城市Id</summary>
        public const String CityId = "CityId";

        /// <summary>排序</summary>
        public const String Sort = "Sort";

        /// <summary>创建者</summary>
        public const String CreateUser = "CreateUser";

        /// <summary>创建者</summary>
        public const String CreateUserID = "CreateUserID";

        /// <summary>创建时间</summary>
        public const String CreateTime = "CreateTime";

        /// <summary>创建地址</summary>
        public const String CreateIP = "CreateIP";

        /// <summary>更新者</summary>
        public const String UpdateUser = "UpdateUser";

        /// <summary>更新者</summary>
        public const String UpdateUserID = "UpdateUserID";

        /// <summary>更新时间</summary>
        public const String UpdateTime = "UpdateTime";

        /// <summary>更新地址</summary>
        public const String UpdateIP = "UpdateIP";
    }
    #endregion
}
