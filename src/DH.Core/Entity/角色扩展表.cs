using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace DH.Entity
{
    /// <summary>角色扩展表</summary>
    [Serializable]
    [DataObject]
    [Description("角色扩展表")]
    [BindTable("DH_RoleEx", Description = "角色扩展表", ConnName = "DH", DbType = DatabaseType.None)]
    public partial class RoleEx
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, false, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private Boolean _IsAdmin;
        /// <summary>是否管理员</summary>
        [DisplayName("是否管理员")]
        [Description("是否管理员")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsAdmin", "是否管理员", "")]
        public Boolean IsAdmin { get => _IsAdmin; set { if (OnPropertyChanging("IsAdmin", value)) { _IsAdmin = value; OnPropertyChanged("IsAdmin"); } } }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case "Id": return _Id;
                    case "IsAdmin": return _IsAdmin;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "IsAdmin": _IsAdmin = value.ToBoolean(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得角色扩展表字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>是否管理员</summary>
            public static readonly Field IsAdmin = FindByName("IsAdmin");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得角色扩展表字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>是否管理员</summary>
            public const String IsAdmin = "IsAdmin";
        }
        #endregion
    }
}