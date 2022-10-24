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
    /// <summary>用户扩展</summary>
    [Serializable]
    [DataObject]
    [Description("用户扩展")]
    [BindTable("DH_UserDetail", Description = "用户扩展", ConnName = "DH", DbType = DatabaseType.None)]
    public partial class UserDetail
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, false, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private Int32 _LanguageId;
        /// <summary>语言Id</summary>
        [DisplayName("语言Id")]
        [Description("语言Id")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LanguageId", "语言Id", "")]
        public Int32 LanguageId { get => _LanguageId; set { if (OnPropertyChanging("LanguageId", value)) { _LanguageId = value; OnPropertyChanged("LanguageId"); } } }

        private Boolean _IsSuper;
        /// <summary>是否超级管理员</summary>
        [DisplayName("是否超级管理员")]
        [Description("是否超级管理员")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsSuper", "是否超级管理员", "")]
        public Boolean IsSuper { get => _IsSuper; set { if (OnPropertyChanging("IsSuper", value)) { _IsSuper = value; OnPropertyChanged("IsSuper"); } } }
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
                    case "LanguageId": return _LanguageId;
                    case "IsSuper": return _IsSuper;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "LanguageId": _LanguageId = value.ToInt(); break;
                    case "IsSuper": _IsSuper = value.ToBoolean(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得用户扩展字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>语言Id</summary>
            public static readonly Field LanguageId = FindByName("LanguageId");

            /// <summary>是否超级管理员</summary>
            public static readonly Field IsSuper = FindByName("IsSuper");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得用户扩展字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>语言Id</summary>
            public const String LanguageId = "LanguageId";

            /// <summary>是否超级管理员</summary>
            public const String IsSuper = "IsSuper";
        }
        #endregion
    }
}