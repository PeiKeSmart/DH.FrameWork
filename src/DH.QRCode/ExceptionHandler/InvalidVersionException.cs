namespace DH.QRCode.ExceptionHandler;

[Serializable]
public class InvalidVersionException : VersionInformationException {
    internal string message;

    public override string Message => message;

    public InvalidVersionException(string message)
    {
        this.message = message;
    }
}