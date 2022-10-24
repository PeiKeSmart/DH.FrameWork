using Newtonsoft.Json;

namespace DH.Core.Configuration
{
    /// <summary>
    /// 表示Azure Blob存储配置参数
    /// </summary>
    public partial class AzureBlobConfig : IConfig
    {
        /// <summary>
        /// 获取或设置Azure Blob存储的连接字符串
        /// </summary>
        public string ConnectionString { get; private set; } = string.Empty;

        /// <summary>
        /// 获取或设置Azure Blob存储的容器名称
        /// </summary>
        public string ContainerName { get; private set; } = string.Empty;

        /// <summary>
        /// 获取或设置Azure Blob存储的终结点
        /// </summary>
        public string EndPoint { get; private set; } = string.Empty;

        /// <summary>
        /// 获取或设置在构造url时是否将容器名称附加到AzureBlobStorageEndPoint
        /// </summary>
        public bool AppendContainerName { get; private set; } = true;

        /// <summary>
        /// 获取或设置是否在Azure Blob存储中存储数据保护密钥
        /// </summary>
        public bool StoreDataProtectionKeys { get; private set; } = false;

        /// <summary>
        /// 获取或设置用于存储数据保护密钥的Azure容器名称（此容器应与用于媒体的容器分开，并且应为私有）
        /// </summary>
        public string DataProtectionKeysContainerName { get; private set; } = string.Empty;

        /// <summary>
        /// 获取或设置用于加密数据保护密钥的Azure密钥保管库ID。（这是可选的）
        /// </summary>
        public string DataProtectionKeysVaultId { get; private set; } = string.Empty;

        /// <summary>
        /// 获取一个值，该值指示是否应使用Azure Blob存储
        /// </summary>
        [JsonIgnore]
        public bool Enabled => !string.IsNullOrEmpty(ConnectionString);

        /// <summary>
        /// 是否使用Azure密钥库加密数据保护密钥
        /// </summary>
        [JsonIgnore]
        public bool DataProtectionKeysEncryptWithVault => !string.IsNullOrEmpty(DataProtectionKeysVaultId);
    }
}
