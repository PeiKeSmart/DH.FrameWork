namespace DH.QRCode.ExceptionHandler;

[Serializable]
public class InvalidDataBlockException : ArgumentException {
    internal string message = null;

    public override string Message => message;

    public InvalidDataBlockException(string message)
    {
        this.message = message;
    }
}