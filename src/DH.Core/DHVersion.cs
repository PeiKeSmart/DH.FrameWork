namespace DH.Core
{
    /// <summary>
    /// 表示版本
    /// </summary>
    public static class DHVersion
    {
        /// <summary>
        /// 获取主存储版本
        /// </summary>
        public const string CURRENT_VERSION = "1.00";

        /// <summary>
        /// 获取次要存储版本
        /// </summary>
        public const string MINOR_VERSION = "0";

        /// <summary>
        /// 获取完整存储版本
        /// </summary>
        public const string FULL_VERSION = CURRENT_VERSION + "." + MINOR_VERSION;
    }
}
