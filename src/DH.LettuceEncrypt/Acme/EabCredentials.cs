namespace LettuceEncrypt.Acme;

/// <summary>
/// External Account Binding (EAB) account credentials
/// </summary>
public class EabCredentials
{
    /// <summary>
    /// Optional key identifier for external account binding
    /// </summary>
    public string? EabKeyId { get; set; }

    /// <summary>
    /// Optional key for use with external account binding
    /// </summary>
    public string? EabKey { get; set; }

    /// <summary>
    /// Optional key algorithm e.g HS256, for external account binding
    /// </summary>
    public string? EabKeyAlg { get; set; }
}
