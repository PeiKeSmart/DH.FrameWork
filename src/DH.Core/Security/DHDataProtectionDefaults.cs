namespace DH.Core.Security
{
    /// <summary>
    /// 表示与数据保护相关的默认值
    /// </summary>
    public static partial class DHDataProtectionDefaults
    {
        /// <summary>
        /// 获取用于将保护密钥列表存储到Azure的密钥文件的名称（与启用的UseAzureBlobStorageToStoreDataProtectionKeys选项一起使用）
        /// </summary>
        public static string AzureDataProtectionKeyFile => "DataProtectionKeys.xml";

        /// <summary>
        /// 获取用于将保护密钥列表存储到本地文件系统的密钥路径的名称（在未启用UseAzureBlobStorageToStoreDataProtectionKeys选项时使用）
        /// </summary>
        public static string DataProtectionKeysPath => "~/App_Data/DataProtectionKeys";
    }
}
