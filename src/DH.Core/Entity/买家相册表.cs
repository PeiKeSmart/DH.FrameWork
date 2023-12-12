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

/// <summary>买家相册表</summary>
[Serializable]
[DataObject]
[Description("买家相册表")]
[BindIndex("IX_DG_SnsAlbumClass_UId", false, "UId")]
[BindTable("DG_SnsAlbumClass", Description = "买家相册表", ConnName = "DG", DbType = DatabaseType.None)]
public partial class SnsAlbumClass : ISnsAlbumClass, IEntity<ISnsAlbumClass>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private Int32 _UId;
    /// <summary>会员ID</summary>
    [DisplayName("会员ID")]
    [Description("会员ID")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("UId", "会员ID", "")]
    public Int32 UId { get => _UId; set { if (OnPropertyChanging("UId", value)) { _UId = value; OnPropertyChanged("UId"); } } }

    private String _Name;
    /// <summary>相册名称</summary>
    [DisplayName("相册名称")]
    [Description("相册名称")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Name", "相册名称", "", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private String _Des;
    /// <summary>相册描述</summary>
    [DisplayName("相册描述")]
    [Description("相册描述")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("Des", "相册描述", "")]
    public String Des { get => _Des; set { if (OnPropertyChanging("Des", value)) { _Des = value; OnPropertyChanged("Des"); } } }

    private Int32 _Sort;
    /// <summary>相册排序</summary>
    [DisplayName("相册排序")]
    [Description("相册排序")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Sort", "相册排序", "")]
    public Int32 Sort { get => _Sort; set { if (OnPropertyChanging("Sort", value)) { _Sort = value; OnPropertyChanged("Sort"); } } }

    private String _Cover;
    /// <summary>相册封面</summary>
    [DisplayName("相册封面")]
    [Description("相册封面")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("Cover", "相册封面", "")]
    public String Cover { get => _Cover; set { if (OnPropertyChanging("Cover", value)) { _Cover = value; OnPropertyChanged("Cover"); } } }

    private Boolean _IsDefault;
    /// <summary>是否为买家秀相册</summary>
    [DisplayName("是否为买家秀相册")]
    [Description("是否为买家秀相册")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsDefault", "是否为买家秀相册", "")]
    public Boolean IsDefault { get => _IsDefault; set { if (OnPropertyChanging("IsDefault", value)) { _IsDefault = value; OnPropertyChanged("IsDefault"); } } }

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
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISnsAlbumClass model)
    {
        Id = model.Id;
        UId = model.UId;
        Name = model.Name;
        Des = model.Des;
        Sort = model.Sort;
        Cover = model.Cover;
        IsDefault = model.IsDefault;
        CreateUser = model.CreateUser;
        CreateUserID = model.CreateUserID;
        CreateTime = model.CreateTime;
        CreateIP = model.CreateIP;
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
            "UId" => _UId,
            "Name" => _Name,
            "Des" => _Des,
            "Sort" => _Sort,
            "Cover" => _Cover,
            "IsDefault" => _IsDefault,
            "CreateUser" => _CreateUser,
            "CreateUserID" => _CreateUserID,
            "CreateTime" => _CreateTime,
            "CreateIP" => _CreateIP,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "UId": _UId = value.ToInt(); break;
                case "Name": _Name = Convert.ToString(value); break;
                case "Des": _Des = Convert.ToString(value); break;
                case "Sort": _Sort = value.ToInt(); break;
                case "Cover": _Cover = Convert.ToString(value); break;
                case "IsDefault": _IsDefault = value.ToBoolean(); break;
                case "CreateUser": _CreateUser = Convert.ToString(value); break;
                case "CreateUserID": _CreateUserID = value.ToInt(); break;
                case "CreateTime": _CreateTime = value.ToDateTime(); break;
                case "CreateIP": _CreateIP = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得买家相册表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>会员ID</summary>
        public static readonly Field UId = FindByName("UId");

        /// <summary>相册名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>相册描述</summary>
        public static readonly Field Des = FindByName("Des");

        /// <summary>相册排序</summary>
        public static readonly Field Sort = FindByName("Sort");

        /// <summary>相册封面</summary>
        public static readonly Field Cover = FindByName("Cover");

        /// <summary>是否为买家秀相册</summary>
        public static readonly Field IsDefault = FindByName("IsDefault");

        /// <summary>创建者</summary>
        public static readonly Field CreateUser = FindByName("CreateUser");

        /// <summary>创建者</summary>
        public static readonly Field CreateUserID = FindByName("CreateUserID");

        /// <summary>创建时间</summary>
        public static readonly Field CreateTime = FindByName("CreateTime");

        /// <summary>创建地址</summary>
        public static readonly Field CreateIP = FindByName("CreateIP");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得买家相册表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>会员ID</summary>
        public const String UId = "UId";

        /// <summary>相册名称</summary>
        public const String Name = "Name";

        /// <summary>相册描述</summary>
        public const String Des = "Des";

        /// <summary>相册排序</summary>
        public const String Sort = "Sort";

        /// <summary>相册封面</summary>
        public const String Cover = "Cover";

        /// <summary>是否为买家秀相册</summary>
        public const String IsDefault = "IsDefault";

        /// <summary>创建者</summary>
        public const String CreateUser = "CreateUser";

        /// <summary>创建者</summary>
        public const String CreateUserID = "CreateUserID";

        /// <summary>创建时间</summary>
        public const String CreateTime = "CreateTime";

        /// <summary>创建地址</summary>
        public const String CreateIP = "CreateIP";
    }
    #endregion
}
