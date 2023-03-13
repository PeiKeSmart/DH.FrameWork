using DH.MockData.Abstractions.Options;

namespace DH.MockData.Core.Options
{
    /// <summary>
    /// 正则表达式配置
    /// </summary>
    public class TextRegexFieldOptions : FieldOptionsBase, IStringFieldOptions
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Pattern { get; set; }
    }
}
