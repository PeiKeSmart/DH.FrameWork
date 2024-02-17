using LettuceEncrypt.Acme;

using System.Security.Cryptography.X509Certificates;

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
    /// ����ʹ�ô��и�֤����ݴ���������������ڼ��ɲ��ԣ�ʱ��������
    /// �ⲻ�� Certes Ƕ��ʽ��Դ��һ���֡�
    /// �й������ģ������ https://github.com/fszlin/certes/tree/v3.0.0/src/Certes/Resources/Certificates
    /// </para>
    /// </summary>
    /// <remarks>
    /// ���ڲ�ʹ��certes����certes������BouncyCastle.Cryptography������
    /// ֤�� �� https://github.com/bcgit/bc-csharp/blob/830d9b8c7bdfcec511bff0a6cf4a0e8ed568e7c1/crypto/src/x509/X509CertificateParser.cs#L20
    /// �������֪��֧����Щ֤���ʽ��
    /// </remarks>
    public string[] AdditionalIssuers { get; set; } = Array.Empty<string>();

    /// <summary>
    /// �޷��Զ�����֤��ʱҪʹ�õ�֤�顣
    /// <para>
    /// ���û�л���֤�飬���ֵ����Ϊ null��
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
    /// Http-01����ʱ������Ftp����
    /// </summary>
    public String? FtpDomain { get; set; }

    /// <summary>
    /// Http-01����ʱ������Ftp�û���
    /// </summary>
    public String? FtpUser { get; set; }

    /// <summary>
    /// Http-01����ʱ������Ftp����
    /// </summary>
    public String? FtpPassWord { get; set; }

    /// <summary>
    /// Linux�����±���/www/server/panel/vhost/cert Pem֤�鲿��·��
    /// </summary>
    public String? PemCertDeployPath { get; set; }

    /// <summary>
    /// Linux�����±���/www/server/panel/vhost/ssl/CertPem֤�鲿��·��
    /// </summary>
    public String? PemSslDeployPath { get; set; }

    /// <summary>
    /// �Ƿ��ڲ�LocalHost���ʶ����������ʡ��練���������ڲ���Localhost����
    /// </summary>
    public Boolean IsLocalHost { get; set; } = true;
}
