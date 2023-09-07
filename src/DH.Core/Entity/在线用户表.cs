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

/// <summary>在线用户表</summary>
[Serializable]
[DataObject]
[Description("在线用户表")]
[BindTable("DH_OnlineUsers", Description = "在线用户表", ConnName = "DG", DbType = DatabaseType.None)]
public partial class SysOnlineUsers : ISysOnlineUsers, IEntity<ISysOnlineUsers>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "int(11)")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private Int32 _Uid;
    /// <summary>用户id</summary>
    [DisplayName("用户id")]
    [Description("用户id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Uid", "用户id", "int(11)")]
    public Int32 Uid { get => _Uid; set { if (OnPropertyChanging("Uid", value)) { _Uid = value; OnPropertyChanged("Uid"); } } }

    private String _Sid;
    /// <summary>用户sessionid</summary>
    [DisplayName("用户sessionid")]
    [Description("用户sessionid")]
    [DataObjectField(false, false, false, 16)]
    [BindColumn("Sid", "用户sessionid", "varchar(16)")]
    public String Sid { get => _Sid; set { if (OnPropertyChanging("Sid", value)) { _Sid = value; OnPropertyChanged("Sid"); } } }

    private String _NickName;
    /// <summary>用户昵称</summary>
    [DisplayName("用户昵称")]
    [Description("用户昵称")]
    [DataObjectField(false, false, false, 100)]
    [BindColumn("NickName", "用户昵称", "varchar(100)")]
    public String NickName { get => _NickName; set { if (OnPropertyChanging("NickName", value)) { _NickName = value; OnPropertyChanged("NickName"); } } }

    private String _Ip;
    /// <summary>用户ip</summary>
    [DisplayName("用户ip")]
    [Description("用户ip")]
    [DataObjectField(false, false, false, 20)]
    [BindColumn("Ip", "用户ip", "varchar(20)")]
    public String Ip { get => _Ip; set { if (OnPropertyChanging("Ip", value)) { _Ip = value; OnPropertyChanged("Ip"); } } }

    private Int16 _Regionid;
    /// <summary>用户所在区域id</summary>
    [DisplayName("用户所在区域id")]
    [Description("用户所在区域id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Regionid", "用户所在区域id", "smallint(6)")]
    public Int16 Regionid { get => _Regionid; set { if (OnPropertyChanging("Regionid", value)) { _Regionid = value; OnPropertyChanged("Regionid"); } } }

    private DateTime _Updatetime;
    /// <summary>最后更新时间</summary>
    [DisplayName("最后更新时间")]
    [Description("最后更新时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Updatetime", "最后更新时间", "")]
    public DateTime Updatetime { get => _Updatetime; set { if (OnPropertyChanging("Updatetime", value)) { _Updatetime = value; OnPropertyChanged("Updatetime"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISysOnlineUsers model)
    {
        Id = model.Id;
        Uid = model.Uid;
        Sid = model.Sid;
        NickName = model.NickName;
        Ip = model.Ip;
        Regionid = model.Regionid;
        Updatetime = model.Updatetime;
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
            "Uid" => _Uid,
            "Sid" => _Sid,
            "NickName" => _NickName,
            "Ip" => _Ip,
            "Regionid" => _Regionid,
            "Updatetime" => _Updatetime,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "Uid": _Uid = value.ToInt(); break;
                case "Sid": _Sid = Convert.ToString(value); break;
                case "NickName": _NickName = Convert.ToString(value); break;
                case "Ip": _Ip = Convert.ToString(value); break;
                case "Regionid": _Regionid = Convert.ToInt16(value); break;
                case "Updatetime": _Updatetime = value.ToDateTime(); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得在线用户表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>用户id</summary>
        public static readonly Field Uid = FindByName("Uid");

        /// <summary>用户sessionid</summary>
        public static readonly Field Sid = FindByName("Sid");

        /// <summary>用户昵称</summary>
        public static readonly Field NickName = FindByName("NickName");

        /// <summary>用户ip</summary>
        public static readonly Field Ip = FindByName("Ip");

        /// <summary>用户所在区域id</summary>
        public static readonly Field Regionid = FindByName("Regionid");

        /// <summary>最后更新时间</summary>
        public static readonly Field Updatetime = FindByName("Updatetime");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得在线用户表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>用户id</summary>
        public const String Uid = "Uid";

        /// <summary>用户sessionid</summary>
        public const String Sid = "Sid";

        /// <summary>用户昵称</summary>
        public const String NickName = "NickName";

        /// <summary>用户ip</summary>
        public const String Ip = "Ip";

        /// <summary>用户所在区域id</summary>
        public const String Regionid = "Regionid";

        /// <summary>最后更新时间</summary>
        public const String Updatetime = "Updatetime";
    }
    #endregion
}
