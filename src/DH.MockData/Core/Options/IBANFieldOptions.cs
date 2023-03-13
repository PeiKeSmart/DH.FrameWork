using DH.MockData.Abstractions.Options;

namespace DH.MockData.Core.Options
{
    /// <summary>
    /// 银行账号配置
    /// </summary>
    public class IBANFieldOptions : FieldOptionsBase, IStringFieldOptions
    {
        /// <summary>
        /// 国家代码
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// 类型。IBAN、BBAN、BOTH
        /// </summary>
        public string Type { get; set; } = "IBAN";
    }
}
