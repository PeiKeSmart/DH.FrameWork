namespace DH.Serialization;

internal class ObjectJsonParser : JsonParserBase
{

    private JsonObject map = new JsonObject();

    public override Object getResult()
    {
        return this.map;
    }

    public ObjectJsonParser(CharSource charSrc)
        : base(charSrc)
    {
    }

    protected override void parse()
    {

        charSrc.moveToText();

        if (charSrc.getCurrent() != '{') throw ex("json Object must start with { ");


        charSrc.moveToText();
        if (charSrc.getCurrent() == '}') return;
        charSrc.back();

        parseOne();

    }

    private void parseOne()
    {


        charSrc.moveToText();

        if (charSrc.getCurrent() == '}') return;

        // 解析key
        charSrc.back();
        String key = moveAndGetParser().getResult().ToString();

        // 解析冒号
        charSrc.moveToText();
        if (charSrc.getCurrent() != ':') throw ex("json object's property pair must seperated with :");

        // 解析value
        Object val = moveAndGetParser().getResult();

        // 获取值
        map.Add(key, val);

        // 处理剩下的字符
        charSrc.moveToText();
        char c = charSrc.getCurrent();
        if (c == '}') return;
        if (c != ',') throw ex("json object's property must seperated with ,");

        charSrc.moveToText();
        if (charSrc.getCurrent() == '}')
            return;
        else
        {
            charSrc.back();
            parseOne();
        }


    }


}
