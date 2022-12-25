using DH.SearchEngine.Interfaces;

using JiebaNet.Segmenter;

using Lucene.Net.Analysis;
using Lucene.Net.Store;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Directory = Lucene.Net.Store.Directory;

namespace DH.SearchEngine.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="option"></param>
    public static IServiceCollection AddSearchEngine(this IServiceCollection services, LuceneIndexerOptions option) 
    {
        services.AddSingleton(option);
        services.AddMemoryCache();
        services.TryAddSingleton<Directory>(s => FSDirectory.Open(option.Path));
        services.TryAddSingleton<Analyzer>(s => new JieBaAnalyzer(TokenizerMode.Search));
        services.TryAddScoped<ILuceneIndexer, LuceneIndexer>();
        services.TryAddScoped<ILuceneIndexSearcher, LuceneIndexSearcher>();
        services.TryAddScoped<ISearchEngine, SearchEngine>();
        return services;
    }
}