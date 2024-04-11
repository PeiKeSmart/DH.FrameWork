﻿using XCode.DataAccessLayer;

namespace DH.Models;

/// <summary>数据项</summary>
public class DbItem : ICubeModel {
    /// <summary>连接名</summary>
    public String Name { get; set; }

    /// <summary>数据库类型</summary>
    public DatabaseType Type { get; set; }

    /// <summary>连接字符串</summary>
    public String ConnStr { get; set; }

    /// <summary>数据驱动版本</summary>
    public String Version { get; set; }

    /// <summary>是否动态</summary>
    public Boolean Dynamic { get; set; }

    /// <summary>备份数</summary>
    public Int32 Backups { get; set; }

    /// <summary>表集合</summary>
    public IList<IDataTable> Tables { get; set; }

    /// <summary>库大小</summary>
    public Double Size { get; set; }

    /// <summary>
    /// 数据库名
    /// </summary>
    public String DataBase { get; set; }
}