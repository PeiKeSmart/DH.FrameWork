namespace DH.QRCode.ExceptionHandler;

[Serializable]
public class AlignmentPatternNotFoundException : ArgumentException {
    internal string message = null;

    public override string Message => message;

    public AlignmentPatternNotFoundException(string message)
    {
        this.message = message;
    }
}