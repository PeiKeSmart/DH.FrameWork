namespace DH.Serialization;

internal class JsonParserException : Exception
{

    private String msg;

    public JsonParserException()
    {
    }

    public JsonParserException(String msg)
    {
        this.msg = msg;
    }

    public JsonParserException(String msg, Exception inner)
        : base(msg, inner)
    {
    }

    public override String Message
    {
        get
        {
            return base.Message + Environment.NewLine + this.msg;
        }
    }


    public override String ToString()
    {
        return base.ToString() + Environment.NewLine + this.msg;
    }

}
