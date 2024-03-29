﻿using System;
using System.Collections.Generic;

namespace Baidu.Yingyan.Track
{
    /// <summary>
    /// 纠偏选项
    /// </summary>
    public class TrackHistoryProcessOption
    {
        /// <summary>
        /// 去噪，默认为1 取值范围[0,5]
        /// </summary>
        public Int32? denoise_grade { get; set; }

        /// <summary>
        /// 抽稀 取值范围[0,5] 0（不抽稀）
        /// </summary>
        public Int32? vacuate_grade { get; set; }

        /// <summary>
        /// 绑路，之前未开通绑路的service，默认值为0；之前已开通绑路的service，默认值为1
        /// </summary>
        public bool? need_mapmatch { get; set; }

        /// <summary>
        ///   定位精度过滤，用于过滤掉定位精度较差的轨迹点，默认为0
        /// </summary>
        public int? radius_threshold { get; set; }

        /// <summary>
        /// 交通方式
        /// </summary>
        public TrackHistoryTransportModeEnums? transport_mode { get; set; }

        /// <summary>
        /// 获取选项值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetOption(string name, bool value)
        {
            var t = value == true ? 1 : 0;
            return $"{name}={t}";
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var options = new List<string>();
            if (denoise_grade != null)
                options.Add($"{nameof(denoise_grade)}={(int)denoise_grade}");
            if (radius_threshold > 0)
                options.Add($"{nameof(radius_threshold)}={radius_threshold}");
            if (need_mapmatch != null)
                options.Add(GetOption(nameof(need_mapmatch), need_mapmatch.Value));
            if (transport_mode != null)
                options.Add($"{nameof(transport_mode)}={transport_mode}");
            if (vacuate_grade != null)
                options.Add($"{nameof(vacuate_grade)}={vacuate_grade}");
            return string.Join(",", options);
        }
    }

    /// <summary>
    /// 交通方式
    /// </summary>
    public enum TrackHistoryTransportModeEnums
    {
        /// <summary>
        /// 自动(默认)
        /// </summary>
        auto = 0,

        /// <summary>
        /// 驾车
        /// </summary>
        driving = 1,

        /// <summary>
        ///  骑行
        /// </summary>
        riding = 2,

        /// <summary>
        /// 步行
        /// </summary>
        walking = 3,
    }
}
