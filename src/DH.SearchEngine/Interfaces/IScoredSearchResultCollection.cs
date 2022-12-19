﻿using System.Collections.Generic;

namespace DH.SearchEngine.Interfaces
{
    /// <summary>
    /// 搜索结果集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IScoredSearchResultCollection<T>
    {
        /// <summary>
        /// 总条数
        /// </summary>
        int TotalHits { get; set; }

        /// <summary>
        /// 耗时
        /// </summary>
        long Elapsed { get; set; }

        /// <summary>
        /// 结果集
        /// </summary>
        IList<IScoredSearchResult<T>> Results { get; set; }
    }
}