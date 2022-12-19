using JiebaNet.Segmenter;
using Lucene.Net.Analysis.TokenAttributes;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lucene.Net.Analysis.JieBa
{
    public class JieBaTokenizer : Tokenizer
    {
        private List<JiebaNet.Segmenter.Token> _WordList = new List<JiebaNet.Segmenter.Token>();
        private String _InputText;
        private ICharTermAttribute termAtt;
        private IOffsetAttribute offsetAtt;
        private IPositionIncrementAttribute posIncrAtt;
        private ITypeAttribute typeAtt;
        private JiebaSegmenter segmenter;
        private IEnumerator<JiebaNet.Segmenter.Token> iter;
        private Int32 start;
        private TokenizerMode mode;

        public JieBaTokenizer(TextReader input, TokenizerMode Mode) : base(AttributeFactory.DEFAULT_ATTRIBUTE_FACTORY, input)
        {
            segmenter = new JiebaSegmenter();
            mode = Mode;
            Init();
        }

        private void Init()
        {
            termAtt = AddAttribute<ICharTermAttribute>();
            offsetAtt = AddAttribute<IOffsetAttribute>();
            posIncrAtt = AddAttribute<IPositionIncrementAttribute>();
            typeAtt = AddAttribute<ITypeAttribute>();
        }

        private string ReadToEnd(TextReader input)
        {
            return input.ReadToEnd();
        }

        public sealed override bool IncrementToken()
        {
            ClearAttributes();
            Token token = Next();
            if (token != null)
            {
                string s = token.ToString();
                termAtt.SetEmpty().Append(s);
                offsetAtt.SetOffset(CorrectOffset(token.StartOffset), CorrectOffset(token.EndOffset));
                typeAtt.Type = token.Type;
                return true;
            }
            End();
            Dispose();
            return false;
        }

        public Token Next()
        {
            int num = 0;
            if (iter.MoveNext())
            {
                JiebaNet.Segmenter.Token token = iter.Current;
                Token result = new Token(token.Word, token.StartIndex, token.EndIndex);
                start += num;
                return result;
            }
            return null;
        }

        public override void Reset()
        {
            base.Reset();
            _InputText = ReadToEnd(m_input);
            IEnumerable<JiebaNet.Segmenter.Token> enumerable = segmenter.Tokenize(_InputText, mode, true);
            _WordList.Clear();
            foreach (JiebaNet.Segmenter.Token item in enumerable)
            {
                _WordList.Add(item);
            }
            start = 0;
            iter = _WordList.GetEnumerator();
        }

    }
}
