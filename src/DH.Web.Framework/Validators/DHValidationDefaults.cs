namespace DH.Web.Framework.Validators
{
    /// <summary>
    /// 表示与验证相关的默认值
    /// </summary>
    public static partial class DHValidationDefaults
    {
        /// <summary>
        /// 获取用于验证模型的规则集的名称
        /// </summary>
        public static string ValidationRuleSet => "Validate";

        /// <summary>
        /// 获取非空验证中使用的区域设置的名称
        /// </summary>
        public static string NotNullValidationLocaleName => "Admin.Common.Validation.NotEmpty";
    }
}
