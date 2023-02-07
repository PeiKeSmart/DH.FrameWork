using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.X509;

namespace DH.Payment.UnionPay.Utility
{
    /// <summary>
    /// UnionPay 证书信息
    /// </summary>
    public class UnionPayCertificate
    {
        public X509Certificate cert;
        public string certId;
        public ICipherParameters key;
    }
}
