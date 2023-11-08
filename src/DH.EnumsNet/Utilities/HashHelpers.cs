namespace EnumsNET.Utilities;
internal static partial class HashHelpers {
    public static int PowerOf2(int v)
    {
        if ((v & (v - 1)) == 0 && v >= 1)
        {
            return v;
        }

        var i = 4;
        while (i < v)
        {
            i <<= 1;
        }

        return i;
    }
}