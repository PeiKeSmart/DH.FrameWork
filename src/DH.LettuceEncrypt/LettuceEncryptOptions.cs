using System.Security.Cryptography.X509Certificates;
using LettuceEncrypt.Acme;

namespace LettuceEncrypt;

/// <summary>
/// ���ڽ�ACME����������Ϊ�Զ�����HTTPS֤���ѡ�
/// </summary>
public class LettuceEncryptOptions
{
    private string[] _domainNames = Array.Empty<string>();
    private bool? _useStagingServer;

    /// <summary>
    /// ҪΪ������֤���������
    /// </summary>
    public string[] DomainNames
    {
        get => _domainNames;
        set => _domainNames = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// ��ʾ��ͬ��ACME�������ķ������
    /// </summary>
    public bool AcceptTermsOfService { get; set; }

    /// <summary>
    /// ������֤��䷢����ע��ĵ����ʼ���ַ��
    /// </summary>
    public string EmailAddress { get; set; } = string.Empty;

    /// <summary>
    /// ʹ��Let's Encrypt��ʱ��������
    /// <para>
    /// ������Ӧ�ó��򿪷��ڼ佨��ģ����һ��Զ�����
    /// ����йܻ�������Ϊ Development 
    /// </para>
    /// </summary>
    public bool UseStagingServer
    {
        get => _useStagingServer ?? false;
        set => _useStagingServer = value;
    }

    internal bool UseStagingServerExplicitlySet => _useStagingServer.HasValue;

    /// <summary>
    /// �ڹ����ɹ����ص�֤��֮ǰ���������䷢�ߴ��ݸ�֤�飬
    /// ֤���ڲ�������֤�䷢�ߵ���ʵ�ԡ�
    /// <para>
    /// This is useful especially when using a staging server (e.g. for integration tests) with a root certificate
    /// that is not part of certes' embedded resources.
    /// See https://github.com/fszlin/certes/tree/v3.0.0/src/Certes/Resources/Certificates for context.
    /// </para>
    /// </summary>
    /// <remarks>
    /// Lettuce encrypt uses certes internally, while certes depends on BouncyCastle.Cryptography to parse
    /// certificates. See https://github.com/bcgit/bc-csharp/blob/830d9b8c7bdfcec511bff0a6cf4a0e8ed568e7c1/crypto/src/x509/X509CertificateParser.cs#L20
    /// if you're wondering what certificate formats are supported.
    /// </remarks>
    public string[] AdditionalIssuers { get; set; } = Array.Empty<string>();

    /// <summary>
    /// A certificate to use if a certificates cannot be created automatically.
    /// <para>
    /// This can be null if there is not fallback certificate.
    /// </para>
    /// </summary>
    public X509Certificate2? FallbackCertificate { get; set; }

    /// <summary>
    /// ��������֤������ޡ�
    /// ����Ϊ<c>null</c>�ɽ����Զ�������
    /// </summary>
    public TimeSpan? RenewDaysInAdvance { get; set; } = TimeSpan.FromDays(30);

    /// <summary>
    /// ֤����¼��Ƶ��
    /// </summary>
    public TimeSpan? RenewalCheckPeriod { get; set; } = TimeSpan.FromDays(1);

    /// <summary>
    /// ����Ϊ֤������˽Կ�ķǶԳ��㷨��RS256��ES256��ES384��ES512
    /// </summary>
    public KeyAlgorithm KeyAlgorithm { get; set; } = KeyAlgorithm.ES256;

    /// <summary>
    /// ָ��LettuceEncrypt����ʹ������ACME��ѯ����֤������Ȩ��
    /// Ĭ��ֵΪ<see cref="ChallengeType.Any"/>��
    /// </summary>
    public ChallengeType AllowedChallengeTypes { get; set; } = ChallengeType.Any;

    /// <summary>
    /// ���ڴ������ʻ��Ŀ�ѡEAB���ⲿ�ʻ��󶨣��ʻ�ƾ�ݡ�
    /// </summary>
    public EabCredentials EabCredentials { get; set; } = new();

    /// <summary>
    /// Ftp����
    /// </summary>
    public String? FtpDomain { get; set; }

    /// <summary>
    /// Ftp�û���
    /// </summary>
    public String? FtpUser { get; set; }

    /// <summary>
    /// Ftp����
    /// </summary>
    public String? FtpPassWord { get; set; }
}
