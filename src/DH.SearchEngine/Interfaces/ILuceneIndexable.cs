using Lucene.Net.Documents;

using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.SearchEngine.Interfaces;

/// <summary>
/// 需要被索引的实体基类
/// </summary>
public interface ILuceneIndexable
{
    /// <summary>
    /// 索引id
    /// </summary>
    [LuceneIndex(Name = "IndexId", Store = Field.Store.YES)]
    [XmlIgnore, ScriptIgnore, IgnoreDataMember]
    public string IndexId { get; set; }

    /// <summary>
    /// 转换成Lucene文档
    /// </summary>
    /// <returns></returns>
    Document ToDocument();
}