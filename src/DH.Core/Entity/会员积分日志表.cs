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

/// <summary>会员积分日志表</summary>
[Serializable]
[DataObject]
[Description("会员积分日志表")]
[BindIndex("IX_DG_PointsLog_UId_Stage", false, "UId,Stage")]
[BindTable("DG_PointsLog", Description = "会员积分日志表", ConnName = "DG", DbType = DatabaseType.None)]
public partial class PointsLog : IPointsLog, IEntity<IPointsLog>
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

    private String _UName;
    /// <summary>会员名称</summary>
    [DisplayName("会员名称")]
    [Description("会员名称")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("UName", "会员名称", "")]
    public String UName { get => _UName; set { if (OnPropertyChanging("UName", value)) { _UName = value; OnPropertyChanged("UName"); } } }

    private Int32 _AdminId;
    /// <summary>管理员ID</summary>
    [DisplayName("管理员ID")]
    [Description("管理员ID")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("AdminId", "管理员ID", "")]
    public Int32 AdminId { get => _AdminId; set { if (OnPropertyChanging("AdminId", value)) { _AdminId = value; OnPropertyChanged("AdminId"); } } }

    private String _AdminName;
    /// <summary>管理员名称</summary>
    [DisplayName("管理员名称")]
    [Description("管理员名称")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("AdminName", "管理员名称", "")]
    public String AdminName { get => _AdminName; set { if (OnPropertyChanging("AdminName", value)) { _AdminName = value; OnPropertyChanged("AdminName"); } } }

    private Int32 _Points;
    /// <summary>积分数,负数为扣除</summary>
    [DisplayName("积分数")]
    [Description("积分数,负数为扣除")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Points", "积分数,负数为扣除", "")]
    public Int32 Points { get => _Points; set { if (OnPropertyChanging("Points", value)) { _Points = value; OnPropertyChanged("Points"); } } }

    private String _Desc;
    /// <summary>积分操作描述</summary>
    [DisplayName("积分操作描述")]
    [Description("积分操作描述")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Desc", "积分操作描述", "")]
    public String Desc { get => _Desc; set { if (OnPropertyChanging("Desc", value)) { _Desc = value; OnPropertyChanged("Desc"); } } }

    private String _Stage;
    /// <summary>积分操作阶段。regist注册,login登录,comments商品评论,order订单消费,system系统调整,pointorder礼品兑换,exchange积分兑换,signin签到,inviter推荐注册,rebate推荐返利</summary>
    [DisplayName("积分操作阶段")]
    [Description("积分操作阶段。regist注册,login登录,comments商品评论,order订单消费,system系统调整,pointorder礼品兑换,exchange积分兑换,signin签到,inviter推荐注册,rebate推荐返利")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Stage", "积分操作阶段。regist注册,login登录,comments商品评论,order订单消费,system系统调整,pointorder礼品兑换,exchange积分兑换,signin签到,inviter推荐注册,rebate推荐返利", "")]
    public String Stage { get => _Stage; set { if (OnPropertyChanging("Stage", value)) { _Stage = value; OnPropertyChanged("Stage"); } } }

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
    /// <summary>积分添加时间</summary>
    [DisplayName("积分添加时间")]
    [Description("积分添加时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("CreateTime", "积分添加时间", "")]
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
    public void Copy(IPointsLog model)
    {
        Id = model.Id;
        UId = model.UId;
        UName = model.UName;
        AdminId = model.AdminId;
        AdminName = model.AdminName;
        Points = model.Points;
        Desc = model.Desc;
        Stage = model.Stage;
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
            "UName" => _UName,
            "AdminId" => _AdminId,
            "AdminName" => _AdminName,
            "Points" => _Points,
            "Desc" => _Desc,
            "Stage" => _Stage,
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
                case "UName": _UName = Convert.ToString(value); break;
                case "AdminId": _AdminId = value.ToInt(); break;
                case "AdminName": _AdminName = Convert.ToString(value); break;
                case "Points": _Points = value.ToInt(); break;
                case "Desc": _Desc = Convert.ToString(value); break;
                case "Stage": _Stage = Convert.ToString(value); break;
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
    /// <summary>取得会员积分日志表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>会员ID</summary>
        public static readonly Field UId = FindByName("UId");

        /// <summary>会员名称</summary>
        public static readonly Field UName = FindByName("UName");

        /// <summary>管理员ID</summary>
        public static readonly Field AdminId = FindByName("AdminId");

        /// <summary>管理员名称</summary>
        public static readonly Field AdminName = FindByName("AdminName");

        /// <summary>积分数,负数为扣除</summary>
        public static readonly Field Points = FindByName("Points");

        /// <summary>积分操作描述</summary>
        public static readonly Field Desc = FindByName("Desc");

        /// <summary>积分操作阶段。regist注册,login登录,comments商品评论,order订单消费,system系统调整,pointorder礼品兑换,exchange积分兑换,signin签到,inviter推荐注册,rebate推荐返利</summary>
        public static readonly Field Stage = FindByName("Stage");

        /// <summary>创建者</summary>
        public static readonly Field CreateUser = FindByName("CreateUser");

        /// <summary>创建者</summary>
        public static readonly Field CreateUserID = FindByName("CreateUserID");

        /// <summary>积分添加时间</summary>
        public static readonly Field CreateTime = FindByName("CreateTime");

        /// <summary>创建地址</summary>
        public static readonly Field CreateIP = FindByName("CreateIP");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得会员积分日志表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>会员ID</summary>
        public const String UId = "UId";

        /// <summary>会员名称</summary>
        public const String UName = "UName";

        /// <summary>管理员ID</summary>
        public const String AdminId = "AdminId";

        /// <summary>管理员名称</summary>
        public const String AdminName = "AdminName";

        /// <summary>积分数,负数为扣除</summary>
        public const String Points = "Points";

        /// <summary>积分操作描述</summary>
        public const String Desc = "Desc";

        /// <summary>积分操作阶段。regist注册,login登录,comments商品评论,order订单消费,system系统调整,pointorder礼品兑换,exchange积分兑换,signin签到,inviter推荐注册,rebate推荐返利</summary>
        public const String Stage = "Stage";

        /// <summary>创建者</summary>
        public const String CreateUser = "CreateUser";

        /// <summary>创建者</summary>
        public const String CreateUserID = "CreateUserID";

        /// <summary>积分添加时间</summary>
        public const String CreateTime = "CreateTime";

        /// <summary>创建地址</summary>
        public const String CreateIP = "CreateIP";
    }
    #endregion
}
