using DH.MockData.Abstractions.Options;

namespace DH.MockData.Core.Options
{
    /// <summary>
    /// 单词配置
    /// </summary>
    public class TextWordsFieldOptions : FieldOptionsBase, IStringFieldOptions
    {
        /// <summary>
        /// 最小值
        /// </summary>
        public int Min { get; set; } = 1;

        /// <summary>
        /// 最大值
        /// </summary>
        public int Max { get; set; } = 10;
    }
}
