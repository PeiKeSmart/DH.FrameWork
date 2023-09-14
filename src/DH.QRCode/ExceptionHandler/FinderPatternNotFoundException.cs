namespace DH.QRCode.ExceptionHandler;

[Serializable]
public class FinderPatternNotFoundException : Exception {
    internal string message = null;

    public override string Message => message;

    public FinderPatternNotFoundException(string message)
    {
        this.message = message;
    }
}