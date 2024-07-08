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

/// <summary>其他消息模板</summary>
[Serializable]
[DataObject]
[Description("其他消息模板")]
[BindIndex("IU_DG_OtherMsgTpl_MCode", true, "MCode")]
[BindTable("DG_OtherMsgTpl", Description = "其他消息模板", ConnName = "DG", DbType = DatabaseType.None)]
public partial class OtherMsgTpl : IOtherMsgTpl, IEntity<IOtherMsgTpl>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _MName;
    /// <summary>模板名称</summary>
    [DisplayName("模板名称")]
    [Description("模板名称")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("MName", "模板名称", "")]
    public String MName { get => _MName; set { if (OnPropertyChanging("MName", value)) { _MName = value; OnPropertyChanged("MName"); } } }

    private String _MTitle;
    /// <summary>模板标题</summary>
    [DisplayName("模板标题")]
    [Description("模板标题")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("MTitle", "模板标题", "")]
    public String MTitle { get => _MTitle; set { if (OnPropertyChanging("MTitle", value)) { _MTitle = value; OnPropertyChanged("MTitle"); } } }

    private String _MCode;
    /// <summary>模板调用代码</summary>
    [DisplayName("模板调用代码")]
    [Description("模板调用代码")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("MCode", "模板调用代码", "", Master = true)]
    public String MCode { get => _MCode; set { if (OnPropertyChanging("MCode", value)) { _MCode = value; OnPropertyChanged("MCode"); } } }

    private String _MContent;
    /// <summary>模板内容</summary>
    [DisplayName("模板内容")]
    [Description("模板内容")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("MContent", "模板内容", "")]
    public String MContent { get => _MContent; set { if (OnPropertyChanging("MContent", value)) { _MContent = value; OnPropertyChanged("MContent"); } } }

    private String _SmsTplId;
    /// <summary>短信模板Id</summary>
    [DisplayName("短信模板Id")]
    [Description("短信模板Id")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("SmsTplId", "短信模板Id", "")]
    public String SmsTplId { get => _SmsTplId; set { if (OnPropertyChanging("SmsTplId", value)) { _SmsTplId = value; OnPropertyChanged("SmsTplId"); } } }

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
    public void Copy(IOtherMsgTpl model)
    {
        Id = model.Id;
        MName = model.MName;
        MTitle = model.MTitle;
        MCode = model.MCode;
        MContent = model.MContent;
        SmsTplId = model.SmsTplId;
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
            "MName" => _MName,
            "MTitle" => _MTitle,
            "MCode" => _MCode,
            "MContent" => _MContent,
            "SmsTplId" => _SmsTplId,
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
                case "MName": _MName = Convert.ToString(value); break;
                case "MTitle": _MTitle = Convert.ToString(value); break;
                case "MCode": _MCode = Convert.ToString(value); break;
                case "MContent": _MContent = Convert.ToString(value); break;
                case "SmsTplId": _SmsTplId = Convert.ToString(value); break;
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

    #region 扩展查询
    #endregion

    #region 字段名
    /// <summary>取得其他消息模板字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>模板名称</summary>
        public static readonly Field MName = FindByName("MName");

        /// <summary>模板标题</summary>
        public static readonly Field MTitle = FindByName("MTitle");

        /// <summary>模板调用代码</summary>
        public static readonly Field MCode = FindByName("MCode");

        /// <summary>模板内容</summary>
        public static readonly Field MContent = FindByName("MContent");

        /// <summary>短信模板Id</summary>
        public static readonly Field SmsTplId = FindByName("SmsTplId");

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

    /// <summary>取得其他消息模板字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>模板名称</summary>
        public const String MName = "MName";

        /// <summary>模板标题</summary>
        public const String MTitle = "MTitle";

        /// <summary>模板调用代码</summary>
        public const String MCode = "MCode";

        /// <summary>模板内容</summary>
        public const String MContent = "MContent";

        /// <summary>短信模板Id</summary>
        public const String SmsTplId = "SmsTplId";

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
