namespace DH.QRCode.Codec.Util;

public class ContentConverter {
    internal static char n = '\n';

    public static string convert(string targetString)
    {
        if (targetString == null)
        {
            return targetString;
        }
        if (targetString.IndexOf("MEBKM:") > -1)
        {
            targetString = convertDocomoBookmark(targetString);
        }
        if (targetString.IndexOf("MECARD:") > -1)
        {
            targetString = convertDocomoAddressBook(targetString);
        }
        if (targetString.IndexOf("MATMSG:") > -1)
        {
            targetString = convertDocomoMailto(targetString);
        }
        if (targetString.IndexOf("http\\://") > -1)
        {
            targetString = replaceString(targetString, "http\\://", "\nhttp://");
        }
        return targetString;
    }

    private static string convertDocomoBookmark(string targetString)
    {
        targetString = removeString(targetString, "MEBKM:");
        targetString = removeString(targetString, "TITLE:");
        targetString = removeString(targetString, ";");
        targetString = removeString(targetString, "URL:");
        return targetString;
    }

    private static string convertDocomoAddressBook(string targetString)
    {
        targetString = removeString(targetString, "MECARD:");
        targetString = removeString(targetString, ";");
        targetString = replaceString(targetString, "N:", "NAME1:");
        targetString = replaceString(targetString, "SOUND:", n + "NAME2:");
        targetString = replaceString(targetString, "TEL:", n + "TEL1:");
        targetString = replaceString(targetString, "EMAIL:", n + "MAIL1:");
        targetString += n;
        return targetString;
    }

    private static string convertDocomoMailto(string s)
    {
        string s2 = s;
        char c = '\n';
        s2 = removeString(s2, "MATMSG:");
        s2 = removeString(s2, ";");
        s2 = replaceString(s2, "TO:", "MAILTO:");
        s2 = replaceString(s2, "SUB:", c + "SUBJECT:");
        s2 = replaceString(s2, "BODY:", c + "BODY:");
        return s2 + c;
    }

    private static string replaceString(string s, string s1, string s2)
    {
        string text = s;
        for (int num = text.IndexOf(s1, 0); num > -1; num = text.IndexOf(s1, num + s2.Length))
        {
            text = text.Substring(0, num) + s2 + text.Substring(num + s1.Length);
        }
        return text;
    }

    private static string removeString(string s, string s1)
    {
        return replaceString(s, s1, "");
    }
}