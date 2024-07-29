namespace DH.Serialization;

internal class InitJsonParser : JsonParserBase
{

    private Object _result;

    public InitJsonParser(CharSource charSrc)
        : base(charSrc)
    {
    }

    protected override void parse()
    {
        _result = moveAndGetParser().getResult();
    }

    public override Object getResult()
    {
        return _result;
    }

}
