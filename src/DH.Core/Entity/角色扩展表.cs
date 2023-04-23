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

/// <summary>角色扩展表</summary>
[Serializable]
[DataObject]
[Description("角色扩展表")]
[BindTable("DH_RoleEx", Description = "角色扩展表", ConnName = "DG", DbType = DatabaseType.None)]
public partial class RoleEx : IRoleEx, IEntity<RoleExModel>
{
    #region 属性
    private Int32 _Id;
    /// <summary>角色编号</summary>
    [DisplayName("角色编号")]
    [Description("角色编号")]
    [DataObjectField(true, false, false, 0)]
    [BindColumn("Id", "角色编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private Boolean _IsAdmin;
    /// <summary>是否管理员</summary>
    [DisplayName("是否管理员")]
    [Description("是否管理员")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsAdmin", "是否管理员", "")]
    public Boolean IsAdmin { get => _IsAdmin; set { if (OnPropertyChanging("IsAdmin", value)) { _IsAdmin = value; OnPropertyChanged("IsAdmin"); } } }

    private String _Roles;
    /// <summary>角色权限</summary>
    [DisplayName("角色权限")]
    [Description("角色权限")]
    [DataObjectField(false, false, true, 512)]
    [BindColumn("Roles", "角色权限", "")]
    public String Roles { get => _Roles; set { if (OnPropertyChanging("Roles", value)) { _Roles = value; OnPropertyChanged("Roles"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(RoleExModel model)
    {
        Id = model.Id;
        IsAdmin = model.IsAdmin;
        Roles = model.Roles;
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
            "IsAdmin" => _IsAdmin,
            "Roles" => _Roles,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "IsAdmin": _IsAdmin = value.ToBoolean(); break;
                case "Roles": _Roles = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得角色扩展表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>角色编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>是否管理员</summary>
        public static readonly Field IsAdmin = FindByName("IsAdmin");

        /// <summary>角色权限</summary>
        public static readonly Field Roles = FindByName("Roles");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得角色扩展表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>角色编号</summary>
        public const String Id = "Id";

        /// <summary>是否管理员</summary>
        public const String IsAdmin = "IsAdmin";

        /// <summary>角色权限</summary>
        public const String Roles = "Roles";
    }
    #endregion
}
