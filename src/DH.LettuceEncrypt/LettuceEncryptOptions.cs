using LettuceEncrypt.Acme;

using System.Security.Cryptography.X509Certificates;

namespace LettuceEncrypt;

/// <summary>
/// 用于将ACME服务器配置为自动生成HTTPS证书的选项。
/// </summary>
public class LettuceEncryptOptions
{
    private string[] _domainNames = Array.Empty<string>();
    private bool? _useStagingServer;

    /// <summary>
    /// 要为其生成证书的域名。
    /// </summary>
    public string[] DomainNames
    {
        get => _domainNames;
        set => _domainNames = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// 表示您同意ACME服务器的服务条款。
    /// </summary>
    public bool AcceptTermsOfService { get; set; }

    /// <summary>
    /// 用于向证书颁发机构注册的电子邮件地址。
    /// </summary>
    public string EmailAddress { get; set; } = string.Empty;

    /// <summary>
    /// 使用Let's Encrypt临时服务器。
    /// <para>
    /// 这是在应用程序开发期间建议的，并且会自动启用
    /// 如果托管环境名称为 Development 
    /// </para>
    /// </summary>
    public bool UseStagingServer
    {
        get => _useStagingServer ?? false;
        set => _useStagingServer = value;
    }

    internal bool UseStagingServerExplicitlySet => _useStagingServer.HasValue;

    /// <summary>
    /// 在构建成功下载的证书之前，将其他颁发者传递给证书，
    /// 证书内部用来验证颁发者的真实性。
    /// <para>
    /// 这在使用带有根证书的暂存服务器（例如用于集成测试）时尤其有用
    /// 这不是 Certes 嵌入式资源的一部分。
    /// 有关上下文，请参阅 https://github.com/fszlin/certes/tree/v3.0.0/src/Certes/Resources/Certificates
    /// </para>
    /// </summary>
    /// <remarks>
    /// 在内部使用certes，而certes依赖于BouncyCastle.Cryptography来解析
    /// 证书 见 https://github.com/bcgit/bc-csharp/blob/830d9b8c7bdfcec511bff0a6cf4a0e8ed568e7c1/crypto/src/x509/X509CertificateParser.cs#L20
    /// 如果您想知道支持哪些证书格式。
    /// </remarks>
    public string[] AdditionalIssuers { get; set; } = Array.Empty<string>();

    /// <summary>
    /// 无法自动创建证书时要使用的证书。
    /// <para>
    /// 如果没有回退证书，则此值可能为 null。
    /// </para>
    /// </summary>
    public X509Certificate2? FallbackCertificate { get; set; }

    /// <summary>
    /// 尝试续订证书的期限。
    /// 设置为<c>null</c>可禁用自动续订。
    /// </summary>
    public TimeSpan? RenewDaysInAdvance { get; set; } = TimeSpan.FromDays(30);

    /// <summary>
    /// 证书更新检查频率
    /// </summary>
    public TimeSpan? RenewalCheckPeriod { get; set; } = TimeSpan.FromDays(1);

    /// <summary>
    /// 用于为证书生成私钥的非对称算法：RS256、ES256、ES384、ES512
    /// </summary>
    public KeyAlgorithm KeyAlgorithm { get; set; } = KeyAlgorithm.ES256;

    /// <summary>
    /// 指定LettuceEncrypt可以使用哪种ACME质询来验证域所有权。
    /// 默认值为<see cref="ChallengeType.Any"/>。
    /// </summary>
    public ChallengeType AllowedChallengeTypes { get; set; } = ChallengeType.Any;

    /// <summary>
    /// 用于创建新帐户的可选EAB（外部帐户绑定）帐户凭据。
    /// </summary>
    public EabCredentials EabCredentials { get; set; } = new();

    /// <summary>
    /// Http-01检验时域名的Ftp连接
    /// </summary>
    public String? FtpDomain { get; set; }

    /// <summary>
    /// Http-01检验时域名的Ftp用户名
    /// </summary>
    public String? FtpUser { get; set; }

    /// <summary>
    /// Http-01检验时域名的Ftp密码
    /// </summary>
    public String? FtpPassWord { get; set; }

    /// <summary>
    /// Linux环境下宝塔/www/server/panel/vhost/cert Pem证书部署路径
    /// </summary>
    public String? PemCertDeployPath { get; set; }

    /// <summary>
    /// Linux环境下宝塔/www/server/panel/vhost/ssl/CertPem证书部署路径
    /// </summary>
    public String? PemSslDeployPath { get; set; }

    /// <summary>
    /// 是否内部LocalHost访问而非域名访问。如反向代理或者内部以Localhost访问
    /// </summary>
    public Boolean IsLocalHost { get; set; } = true;
}
