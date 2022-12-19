using Lucene.Net.Documents;
using DH.SearchEngine.Interfaces;

namespace DH.SearchEngine
{
    /// <summary>
    /// 搜索结果
    /// </summary>
    public class LuceneSearchResult : ILuceneSearchResult
    {
        /// <summary>
        /// 匹配度
        /// </summary>
        public float Score { get; set; }

        /// <summary>
        /// 文档
        /// </summary>
        public Document Document { get; set; }
    }
}