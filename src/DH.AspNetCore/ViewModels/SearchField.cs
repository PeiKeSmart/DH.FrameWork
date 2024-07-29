﻿using DH.ViewModels;

namespace DH.AspNetCore.ViewModels;

/// <summary>搜索字段</summary>
public class SearchField : DataField {
    /// <summary>是否多选框</summary>
    /// <remarks>
    /// 多选框返回的数据有多条，需要逗号分隔；
    /// 如果没有选中任何项，则没有返回，此时会强制覆盖Url参数中的同名字段，避免取消选中无效的问题。
    /// </remarks>
    public Boolean Multiple { get; set; }
}