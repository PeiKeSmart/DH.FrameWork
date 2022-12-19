using JiebaNet.Segmenter;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.TokenAttributes;
using Lucene.Net.Util;
using System.IO;

namespace Lucene.Net.Analysis.JieBa
{
    public class JieBaAnalyzer : Analyzer
    {
        public TokenizerMode mode;

        public JieBaAnalyzer(TokenizerMode Mode)
        {
            mode = Mode;
        }

        protected override TokenStreamComponents CreateComponents(string filedName, TextReader reader)
        {
            JieBaTokenizer jieBaTokenizer = new JieBaTokenizer(reader, this.mode);
            TokenStream tokenStream = new LowerCaseFilter(LuceneVersion.LUCENE_48, jieBaTokenizer);
            tokenStream.AddAttribute<ICharTermAttribute>();
            tokenStream.AddAttribute<IOffsetAttribute>();
            return new TokenStreamComponents(jieBaTokenizer, tokenStream);
        }
    }
}
