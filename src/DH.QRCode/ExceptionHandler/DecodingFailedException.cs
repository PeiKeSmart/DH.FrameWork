namespace DH.QRCode.ExceptionHandler;

[Serializable]
public class DecodingFailedException : ArgumentException {
    internal string message = null;

    public override string Message => message;

    public DecodingFailedException(string message)
    {
        this.message = message;
    }
}