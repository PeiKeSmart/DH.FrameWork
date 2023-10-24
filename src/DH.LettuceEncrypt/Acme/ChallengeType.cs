namespace LettuceEncrypt.Acme;

/// <summary>
/// An enumeration that represents the various kinds of challenges in the ACME protocol.
/// See https://letsencrypt.org/docs/challenge-types/.
/// </summary>
[Flags]
public enum ChallengeType
{
    /// <summary>
    /// The HTTP-01 challenge, which uses a well-known URL on the server and a HTTP request/response.
    /// See https://letsencrypt.org/docs/challenge-types/#http-01-challenge
    /// </summary>
    Http01 = 1 << 0,

    /// <summary>
    /// The TLS-ALPN-01 challenge, which uses an auto-generated, ephemeral certificate in the TLS handshake.
    /// See https://letsencrypt.org/docs/challenge-types/#tls-alpn-01
    /// </summary>
    TlsAlpn01 = 1 << 1,

    /// <summary>
    /// The DNS-01 challenge, which uses TXT record under that domain name.
    /// See https://letsencrypt.org/docs/challenge-types/#dns-01-challenge
    /// </summary>
    Dns01 = 1 << 2,

    /// <summary>
    /// A special flag which represents all known challenge types.
    /// </summary>
    Any = 0xFFFF,
}
