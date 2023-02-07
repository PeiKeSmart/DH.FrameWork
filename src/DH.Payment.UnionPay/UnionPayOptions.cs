using DG.Payment.UnionPay.Utility;
using Microsoft.Extensions.Logging;
using NewLife;
using System.IO;

namespace DG.Payment.UnionPay
{
    /// <summary>
    /// UnionPay 选项。
    /// </summary>
    public class UnionPayOptions
    {
        internal UnionPayCertificate EncryptCertificate;
        internal UnionPayCertificate MiddleCertificate;
        internal UnionPayCertificate RootCertificate;
        internal UnionPayCertificate SignCertificate => UnionPaySignature.GetSignCertificate(SignCert, SignCertPassword);

        private string encryptCert;
        private string middleCert;
        private string rootCert;
        private string signCert;

        /// <summary>
        /// 商户代码
        /// </summary>
        public string MerId { get; set; }

        /// <summary>
        /// 接入类型
        /// 0：普通商户直连接入
        /// 2：平台类商户接入
        /// </summary>
        public string AccessType { get; set; } = "0";

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; } = "5.1.0";

        /// <summary>
        /// 编码方式
        /// </summary>
        public string Encoding { get; } = "UTF-8";

        /// <summary>
        /// 签名方法
        /// 01（表示采用RSA签名）
        /// HASH表示散列算法
        /// 11：支持散列方式验证SHA-256
        /// 12：支持散列方式验证SM3
        /// </summary>
        public string SignMethod { get; set; } = "01";

        /// <summary>
        /// 签名证书 
        /// </summary>
        public string SignCert 
        { 
            get => signCert; 
            set
            {
                if (!value.IsNullOrWhiteSpace())
                {
                    signCert = value.GetFullPath();
                }
            }
        }

        /// <summary>
        /// 签名证书密码
        /// </summary>
        public string SignCertPassword { get; set; }

        /// <summary>
        /// 加密证书 
        /// </summary>
        public string EncryptCert
        {
            get => encryptCert;
            set
            {
                if (!string.IsNullOrEmpty(encryptCert))
                {
                    encryptCert = value.GetFullPath();
                    EncryptCertificate = UnionPaySignature.GetCertificate(encryptCert);
                }
            }
        }

        /// <summary>
        /// 验签中级证书
        /// </summary>
        public string MiddleCert
        {
            get => middleCert;
            set
            {
                if (!string.IsNullOrEmpty(middleCert))
                {
                    middleCert = value.GetFullPath();
                    MiddleCertificate = UnionPaySignature.GetCertificate(middleCert);
                }
            }
        }

        /// <summary>
        /// 验签根证书 
        /// </summary>
        public string RootCert
        {
            get => rootCert;
            set
            {
                if (!string.IsNullOrEmpty(rootCert))
                {
                    rootCert = value.GetFullPath();
                    RootCertificate = UnionPaySignature.GetCertificate(rootCert);
                }
            }
        }

        /// <summary>
        /// 散列方式签名密钥
        /// </summary>
        public string SecureKey { get; set; }

        /// <summary>
        /// 测试模式
        /// </summary>
        public bool TestMode { get; set; }

        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
    }
}
