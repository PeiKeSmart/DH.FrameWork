namespace DH.Serialization;

internal abstract class JsonParserBase
{

    protected CharSource charSrc;

    protected abstract void parse();
    public abstract Object getResult();

    public JsonParserBase()
    {
    }

    public JsonParserBase(CharSource charSrc)
    {
        this.charSrc = charSrc;
        parse();
    }

    protected JsonParserBase moveAndGetParser()
    {

        charSrc.moveToText();

        char c = charSrc.getCurrent();

        if (c == '"' || c == '\'')
        {
            return new StringJsonParser(this.charSrc, c);
        }

        if (c == '{')
        {
            charSrc.back();
            return new ObjectJsonParser(this.charSrc);
        }

        if (c == '[')
        {
            charSrc.back();
            return new ArrayJsonParser(this.charSrc);
        }

        return new ValueJsonParser(this.charSrc);

    }

    protected JsonParserException ex(String msg)
    {
        return new JsonParserException(msg + "(index:" + this.charSrc.getIndex().ToString() + ")\n" + this.charSrc.strSrc);
    }



}
