﻿using System;

namespace XCode
{
    /// <summary>模型检查模式</summary>
    public enum ModelCheckModes
    {
        /// <summary>初始化时检查所有表。默认值。具有最好性能。</summary>
        CheckAllTablesWhenInit,

        /// <summary>第一次使用时检查表。常用于通用实体类等存在大量实体类但不会同时使用所有实体类的场合，避免反向工程生成没有使用到的实体类的数据表。</summary>
        CheckTableWhenFirstUse
    }

    /// <summary>模型检查模式</summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ModelCheckModeAttribute : Attribute
    {
        /// <summary>模式</summary>
        public ModelCheckModes Mode { get; set; }

        /// <summary>指定实体类的模型检查模式</summary>
        /// <param name="mode"></param>
        public ModelCheckModeAttribute(ModelCheckModes mode) { Mode = mode; }
    }
}