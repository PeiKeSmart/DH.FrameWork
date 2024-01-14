using DH.DynamicObject;
using DH.Json;
using DH.Web;

namespace DH.Helpers;

public class Xmtool {
    public static DynamicObjectExt DynamicObject()
    {
        return new DynamicObjectExt();
    }

    public static JsonTool Json
    {
        get
        {
            return JsonTool.New();
        }
    }

    public static WebTool Web
    {
        get
        {
            return WebTool.New();
        }
    }

    public static DateTimeTool DateTime()
    {
        return DateTimeTool.New();
    }

    public static CryptoTool Crypto()
    {
        return CryptoTool.New();
    }

    public static HashTool Hash()
    {
        return HashTool.New();
    }

    public static RegexTool Regex()
    {
        return RegexTool.New();
    }

    public static TypeTool Type()
    {
        return TypeTool.New();
    }

    public static XmlTool Xml()
    {
        return XmlTool.New();
    }

    public static MailTool Mail(string host, int port, string account, string password, bool enableSsl = false)
    {
        return MailTool.New(host, port, enableSsl, account, password);
    }

    public static RandomTool Random()
    {
        return RandomTool.New();
    }
}