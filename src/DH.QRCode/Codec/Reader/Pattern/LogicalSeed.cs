namespace DH.QRCode.Codec.Reader.Pattern;

public class LogicalSeed {
    private static int[][] seed;

    public static int[] getSeed(int version)
    {
        return seed[version - 1];
    }

    public static int getSeed(int version, int patternNumber)
    {
        return seed[version - 1][patternNumber];
    }

    static LogicalSeed()
    {
        seed = new int[40][];
        seed[0] = new int[2]
        {
                6,
                14
        };
        seed[1] = new int[2]
        {
                6,
                18
        };
        seed[2] = new int[2]
        {
                6,
                22
        };
        seed[3] = new int[2]
        {
                6,
                26
        };
        seed[4] = new int[2]
        {
                6,
                30
        };
        seed[5] = new int[2]
        {
                6,
                34
        };
        seed[6] = new int[3]
        {
                6,
                22,
                38
        };
        seed[7] = new int[3]
        {
                6,
                24,
                42
        };
        seed[8] = new int[3]
        {
                6,
                26,
                46
        };
        seed[9] = new int[3]
        {
                6,
                28,
                50
        };
        seed[10] = new int[3]
        {
                6,
                30,
                54
        };
        seed[11] = new int[3]
        {
                6,
                32,
                58
        };
        seed[12] = new int[3]
        {
                6,
                34,
                62
        };
        seed[13] = new int[4]
        {
                6,
                26,
                46,
                66
        };
        seed[14] = new int[4]
        {
                6,
                26,
                48,
                70
        };
        seed[15] = new int[4]
        {
                6,
                26,
                50,
                74
        };
        seed[16] = new int[4]
        {
                6,
                30,
                54,
                78
        };
        seed[17] = new int[4]
        {
                6,
                30,
                56,
                82
        };
        seed[18] = new int[4]
        {
                6,
                30,
                58,
                86
        };
        seed[19] = new int[4]
        {
                6,
                34,
                62,
                90
        };
        seed[20] = new int[5]
        {
                6,
                28,
                50,
                72,
                94
        };
        seed[21] = new int[5]
        {
                6,
                26,
                50,
                74,
                98
        };
        seed[22] = new int[5]
        {
                6,
                30,
                54,
                78,
                102
        };
        seed[23] = new int[5]
        {
                6,
                28,
                54,
                80,
                106
        };
        seed[24] = new int[5]
        {
                6,
                32,
                58,
                84,
                110
        };
        seed[25] = new int[5]
        {
                6,
                30,
                58,
                86,
                114
        };
        seed[26] = new int[5]
        {
                6,
                34,
                62,
                90,
                118
        };
        seed[27] = new int[6]
        {
                6,
                26,
                50,
                74,
                98,
                122
        };
        seed[28] = new int[6]
        {
                6,
                30,
                54,
                78,
                102,
                126
        };
        seed[29] = new int[6]
        {
                6,
                26,
                52,
                78,
                104,
                130
        };
        seed[30] = new int[6]
        {
                6,
                30,
                56,
                82,
                108,
                134
        };
        seed[31] = new int[6]
        {
                6,
                34,
                60,
                86,
                112,
                138
        };
        seed[32] = new int[6]
        {
                6,
                30,
                58,
                86,
                114,
                142
        };
        seed[33] = new int[6]
        {
                6,
                34,
                62,
                90,
                118,
                146
        };
        seed[34] = new int[7]
        {
                6,
                30,
                54,
                78,
                102,
                126,
                150
        };
        seed[35] = new int[7]
        {
                6,
                24,
                50,
                76,
                102,
                128,
                154
        };
        seed[36] = new int[7]
        {
                6,
                28,
                54,
                80,
                106,
                132,
                158
        };
        seed[37] = new int[7]
        {
                6,
                32,
                58,
                84,
                110,
                136,
                162
        };
        seed[38] = new int[7]
        {
                6,
                26,
                54,
                82,
                110,
                138,
                166
        };
        seed[39] = new int[7]
        {
                6,
                30,
                58,
                86,
                114,
                142,
                170
        };
    }
}