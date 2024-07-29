﻿// ======================================================================
// 
//           filename : ExporterHeaderAttribute.cs
//           description :
// 
//           created by 雪雁 at  2019-09-11 13:51
//           文档官网：https://docs.xin-lai.com
//           公众号教程：麦扣聊技术
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
// 
// ======================================================================

using System;
using Magicodes.IE.Core;
using SixLabors.ImageSharp;

namespace Magicodes.ExporterAndImporter.Core
{

    /// <summary>
    ///     导出属性特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExporterHeaderAttribute : Attribute
    {
        /// <inheritdoc />
        public ExporterHeaderAttribute(string displayName = null, float fontSize = 11, string format = null,
            bool isBold = true, bool isAutoFit = true, bool autoCenterColumn = false, int width = 0, KnownColor fontColor = KnownColor.Empty)
        {
            DisplayName = displayName;
            FontSize = fontSize;
            Format = format;
            IsBold = isBold;
            IsAutoFit = isAutoFit;
            AutoCenterColumn = autoCenterColumn;
            Width = width;
            if (fontColor != KnownColor.Empty)
            {
                FontColor = Color.Parse(fontColor.ToString("G"));
            }
        }

        /// <summary>
        ///     显示名称
        /// </summary>
        public string DisplayName { set; get; }

        /// <summary>
        ///     字体大小
        /// </summary>
        public float FontSize { set; get; }

        /// <summary>
        ///     是否加粗
        /// </summary>
        public bool IsBold { set; get; }

        /// <summary>
        ///     格式化（身份证'@'，日期'yyyy-MM-dd'、'yyyy-MM-dd HH:mm:ss'，数字'#,##0'）
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        ///     是否自适应
        /// </summary>
        public bool IsAutoFit { set; get; }

        /// <summary>
        ///     自动居中
        /// </summary>
        public bool AutoCenterColumn { get; set; }

        /// <summary>
        ///     是否忽略
        /// </summary>
        public bool IsIgnore { get; set; }

        /// <summary>
        ///     宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int ColumnIndex { get; set; } = 10000;

        /// <summary>
        /// 自动换行
        /// </summary>
        public bool WrapText { get; set; }

        /// <summary>
        /// Hidden
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        public Color? FontColor { get; set; }
    }
}