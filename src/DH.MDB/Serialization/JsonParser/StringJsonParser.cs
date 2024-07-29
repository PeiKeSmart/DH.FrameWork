namespace DH.Serialization;

internal class StringJsonParser : ValueJsonParser
{

    private String _result;

    public override Object getResult()
    {
        return _result;
    }


    public StringJsonParser(CharSource charSrc, char c)
    {
        this.quote = c;
        this.charSrc = charSrc;
        parse();
    }

    private char quote = '"';


    protected override void parse()
    {

        charSrc.move();

        char c = charSrc.getCurrent();

        if (c == '\n' || c == '\r') throw ex("String ends at a new line while not end");

        if (c == quote)
        {
            _result = sb.ToString();
            return;
        }

        if (c == '\\')
            processEscape();
        else
            sb.Append(c);

        parse();

    }

}
