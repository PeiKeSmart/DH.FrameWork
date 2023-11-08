namespace EnumsNET.Numerics;

internal static class Number {
    public static int BitCount(int v)
    {
        v -= (v >> 1) & 0x55555555;
        v = (v & 0x33333333) + ((v >> 2) & 0x33333333);
        return (((v + (v >> 4)) & 0xF0F0F0F) * 0x1010101) >> 24;
    }

    public static int BitCount(long v)
    {
        v -= (v >> 1) & 0x5555555555555555;
        v = (v & 0x3333333333333333) + ((v >> 2) & 0x3333333333333333);
        return (int)((((v + (v >> 4)) & 0xF0F0F0F0F0F0F0F) * 0x101010101010101) >> 56);
    }
}