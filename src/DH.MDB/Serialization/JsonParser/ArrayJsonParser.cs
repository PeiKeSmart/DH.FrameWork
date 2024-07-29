namespace DH.Serialization;

internal class ArrayJsonParser : JsonParserBase
{

    private List<object> list = new List<object>();

    public override Object getResult()
    {
        return list;
    }

    public ArrayJsonParser(CharSource charSrc)
        : base(charSrc)
    {
    }

    protected override void parse()
    {

        charSrc.moveToText();
        if (charSrc.getCurrent() != '[') throw ex("json array must start with [");

        charSrc.moveToText();
        if (charSrc.getCurrent() == ']') return;

        // 回到[
        charSrc.back();

        parseOne();

    }

    private void parseOne()
    {

        charSrc.moveToText();

        if (charSrc.getCurrent() == ',')
        {
            charSrc.back();
            list.Add(null);
        }
        else
        {
            charSrc.back();

            // 将值加入列表
            Object val = moveAndGetParser().getResult();
            list.Add(val);
        }

        // 剩余字符处理
        charSrc.moveToText();

        char c = charSrc.getCurrent();
        if (c == ']')
        {
            return;
        }

        if (c != ',') throw ex("json array must seperated with , ");

        charSrc.moveToText();
        if (charSrc.getCurrent() == ']')
        {
            return;
        }
        else
        {
            charSrc.back();
            parseOne();
        }

    }


}
