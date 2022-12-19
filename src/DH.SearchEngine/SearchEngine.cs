using System.Diagnostics;
using System.Reflection;

using DH.SearchEngine.Extensions;
using DH.SearchEngine.Interfaces;

using JiebaNet.Segmenter;

using Lucene.Net.Analysis;
using Lucene.Net.Documents;

using Microsoft.Extensions.Caching.Memory;

using NewLife;
using NewLife.Reflection;

using Directory = Lucene.Net.Store.Directory;

namespace DH.SearchEngine
{
    /// <summary>
    /// 搜索引擎
    /// </summary>
    public class SearchEngine : ISearchEngine
    {
        /// <summary>
        /// 索引器
        /// </summary>
        public ILuceneIndexer LuceneIndexer { get; }

        /// <summary>
        /// 索引搜索器
        /// </summary>
        public ILuceneIndexSearcher LuceneIndexSearcher { get; }

        /// <summary>
        /// 索引条数
        /// </summary>
        public int IndexCount => LuceneIndexer.Count();

        /// <summary>
        /// 搜索引擎
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="analyzer"></param>
        /// <param name="memoryCache"></param>
        public SearchEngine(Directory directory, Analyzer analyzer, IMemoryCache memoryCache)
        {
            LuceneIndexer = new LuceneIndexer(directory, analyzer);
            LuceneIndexSearcher = new LuceneIndexSearcher(directory, analyzer, memoryCache);
        }

        /// <summary>
        ///获取文档的具体版本
        /// </summary>
        /// <param name ="doc">要转换的文档</param>
        /// <returns></returns>
        private ILuceneIndexable GetConcreteFromDocument(Document doc)
        {
            var t = Type.GetType(doc.Get("Type"));
            var obj = t.Assembly.CreateInstance(t.FullName, true) as ILuceneIndexable;
            foreach (var p in t.GetProperties().Where(p => p.GetCustomAttributes<LuceneIndexAttribute>().Any()))
            {
                p.SetValue(obj, doc.Get(p.Name, p.PropertyType));
            }
            return obj;
        }

        /// <summary>
        /// 扫描数据库上下文并对所有已实现ILuceneIndexable的对象，并创建索引
        /// </summary>
        public void CreateIndex()
        {
            if (LuceneIndexer == null)
            {
                return;
            }

            var index = new List<ILuceneIndexable>();

            var i = typeof(ILuceneIndexable).GetAllSubclasses();

            foreach (var item in i)
            {
                index.AddRange(item.Invoke("GetAll", "") as IEnumerable<ILuceneIndexable>);
            }

            if (index.Any())
            {
                LuceneIndexer.CreateIndex(index);
            }
        }

        /// <summary>
        /// 创建指定数据表的索引
        /// </summary>
        public void CreateIndex(List<string> tables)
        {
            if (LuceneIndexer == null)
            {
                return;
            }

            var index = new List<ILuceneIndexable>();

            var i = typeof(ILuceneIndexable).GetAllSubclasses();

            foreach (var item in i)
            {
                if (tables.Contains(item.Name))
                {
                    index.AddRange(item.Invoke("GetAll", "") as IEnumerable<ILuceneIndexable>);
                }
            }

            if (index.Any())
            {
                LuceneIndexer.CreateIndex(index);
            }
        }

        /// <summary>
        /// 创建指定集合的索引
        /// </summary>
        /// <param name="index"></param>
        public void CreateIndex(IEnumerable<ILuceneIndexable> index)
        {
            if (index.Any())
            {
                LuceneIndexer.CreateIndex(index);
            }
        }

        /// <summary>
        /// 删除索引
        /// </summary>
        public void DeleteIndex()
        {
            LuceneIndexer?.DeleteAll();
        }

        /// <summary>
        /// 执行搜索并将结果限制为特定类型，在返回之前，搜索结果将转换为相关类型，但不返回任何评分信息
        /// </summary>
        /// <typeparam name ="T">要搜索的实体类型 - 注意：必须实现ILuceneIndexable </typeparam>
        /// <param name ="options">搜索选项</param>
        /// <returns></returns>
        public ISearchResultCollection<T> Search<T>(SearchOptions options)
        {
            options.Type = typeof(T);
            var indexResults = LuceneIndexSearcher.ScoredSearch(options);
            ISearchResultCollection<T> resultSet = new SearchResultCollection<T>
            {
                TotalHits = indexResults.TotalHits
            };

            var sw = Stopwatch.StartNew();
            foreach (var indexResult in indexResults.Results)
            {
                var entity = (T)GetConcreteFromDocument(indexResult.Document);
                resultSet.Results.Add(entity);
            }

            sw.Stop();
            resultSet.Elapsed = indexResults.Elapsed + sw.ElapsedMilliseconds;
            return resultSet;
        }

        /// <summary>
        /// 执行搜索并将结果限制为特定类型，在返回之前，搜索结果将转换为相关类型，但不返回任何评分信息
        /// </summary>
        /// <typeparam name ="T">要搜索的实体类型 - 注意：必须实现ILuceneIndexable </typeparam>
        /// <param name ="options">搜索选项</param>
        /// <returns></returns>
        public IScoredSearchResultCollection<T> ScoredSearch<T>(SearchOptions options)
        {
            // 确保类型匹配
            if (typeof(T) != typeof(ILuceneIndexable))
            {
                options.Type = typeof(T);
            }

            var indexResults = LuceneIndexSearcher.ScoredSearch(options);
            IScoredSearchResultCollection<T> results = new ScoredSearchResultCollection<T>();
            results.TotalHits = indexResults.TotalHits;
            var sw = Stopwatch.StartNew();
            foreach (var indexResult in indexResults.Results)
            {
                IScoredSearchResult<T> result = new ScoredSearchResult<T>();
                result.Score = indexResult.Score;
                result.Entity = (T)GetConcreteFromDocument(indexResult.Document);
                results.Results.Add(result);
            }

            sw.Stop();
            results.Elapsed = indexResults.Elapsed + sw.ElapsedMilliseconds;
            return results;
        }

        /// <summary>
        /// 执行搜索并将结果限制为特定类型，在返回之前，搜索结果将转换为相关类型
        /// </summary>
        /// <param name ="options">搜索选项</param>
        /// <returns></returns>
        public IScoredSearchResultCollection<ILuceneIndexable> ScoredSearch(SearchOptions options)
        {
            return ScoredSearch<ILuceneIndexable>(options);
        }

        /// <summary>
        /// 执行搜索并将结果限制为特定类型，在返回之前，搜索结果将转换为相关类型
        /// </summary>
        /// <param name ="options">搜索选项</param>
        /// <returns></returns>
        public ISearchResultCollection<ILuceneIndexable> Search(SearchOptions options)
        {
            return Search<ILuceneIndexable>(options);
        }

        /// <summary>
        /// 搜索一条匹配度最高的记录
        /// </summary>
        /// <param name ="options">搜索选项</param>
        /// <returns></returns>
        public ILuceneIndexable SearchOne(SearchOptions options)
        {
            return GetConcreteFromDocument(LuceneIndexSearcher.ScoredSearchSingle(options));
        }

        /// <summary>
        /// 搜索一条匹配度最高的记录
        /// </summary>
        /// <param name ="options">搜索选项</param>
        /// <returns></returns>
        public T SearchOne<T>(SearchOptions options) where T : class
        {
            return GetConcreteFromDocument(LuceneIndexSearcher.ScoredSearchSingle(options)) as T;
        }

        /// <summary>
        /// 导入自定义词库
        /// </summary>
        /// <param name="words"></param>
        public void ImportCustomerKeywords(IEnumerable<string> words)
        {
            var segmenter = new JiebaSegmenter();
            foreach (var word in words)
            {
                segmenter.AddWord(word);
            }
        }
    }
}