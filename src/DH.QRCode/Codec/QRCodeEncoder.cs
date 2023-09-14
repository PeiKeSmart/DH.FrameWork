using DH.QRCode.Codec.Util;

using System.Drawing;
using System.Text;

namespace DH.QRCode.Codec;

public class QRCodeEncoder {
    public enum ENCODE_MODE {
        ALPHA_NUMERIC,
        NUMERIC,
        BYTE
    }

    public enum ERROR_CORRECTION {
        L,
        M,
        Q,
        H
    }

    internal ERROR_CORRECTION qrcodeErrorCorrect;

    internal ENCODE_MODE qrcodeEncodeMode;

    internal int qrcodeVersion;

    internal int qrcodeStructureappendN;

    internal int qrcodeStructureappendM;

    internal int qrcodeStructureappendParity;

    internal System.Drawing.Color qrCodeBackgroundColor;

    internal System.Drawing.Color qrCodeForegroundColor;

    internal float qrCodeScale;

    internal string qrcodeStructureappendOriginaldata;

    public virtual ERROR_CORRECTION QRCodeErrorCorrect
    {
        get
        {
            return qrcodeErrorCorrect;
        }
        set
        {
            qrcodeErrorCorrect = value;
        }
    }

    public virtual int QRCodeVersion
    {
        get
        {
            return qrcodeVersion;
        }
        set
        {
            if (value >= 0 && value <= 40)
            {
                qrcodeVersion = value;
            }
        }
    }

    public virtual ENCODE_MODE QRCodeEncodeMode
    {
        get
        {
            return qrcodeEncodeMode;
        }
        set
        {
            qrcodeEncodeMode = value;
        }
    }

    public virtual float QRCodeScale
    {
        get
        {
            return qrCodeScale;
        }
        set
        {
            qrCodeScale = value;
        }
    }

    public virtual System.Drawing.Color QRCodeBackgroundColor
    {
        get
        {
            return qrCodeBackgroundColor;
        }
        set
        {
            qrCodeBackgroundColor = value;
        }
    }

    public virtual System.Drawing.Color QRCodeForegroundColor
    {
        get
        {
            return qrCodeForegroundColor;
        }
        set
        {
            qrCodeForegroundColor = value;
        }
    }

    public QRCodeEncoder()
    {
        qrcodeErrorCorrect = ERROR_CORRECTION.M;
        qrcodeEncodeMode = ENCODE_MODE.BYTE;
        qrcodeVersion = 7;
        qrcodeStructureappendN = 0;
        qrcodeStructureappendM = 0;
        qrcodeStructureappendParity = 0;
        qrcodeStructureappendOriginaldata = "";
        qrCodeScale = 4;
        qrCodeBackgroundColor = System.Drawing.Color.White;
        qrCodeForegroundColor = System.Drawing.Color.Black;
    }

    public virtual void setStructureappend(int m, int n, int p)
    {
        if (n > 1 && n <= 16 && m > 0 && m <= 16 && p >= 0 && p <= 255)
        {
            qrcodeStructureappendM = m;
            qrcodeStructureappendN = n;
            qrcodeStructureappendParity = p;
        }
    }

    public virtual int calStructureappendParity(sbyte[] originaldata)
    {
        int i = 0;
        int num = 0;
        int num2 = originaldata.Length;
        if (num2 > 1)
        {
            num = 0;
            for (; i < num2; i++)
            {
                num ^= originaldata[i] & 0xFF;
            }
        }
        else
        {
            num = -1;
        }
        return num;
    }

    public virtual bool[][] calQrcode(byte[] qrcodeData)
    {
        int num = 0;
        int num2 = qrcodeData.Length;
        int[] array = new int[num2 + 32];
        sbyte[] array2 = new sbyte[num2 + 32];
        if (num2 <= 0)
        {
            bool[][] array3 = new bool[1][];
            bool[] array4 = (array3[0] = new bool[1]);
            return array3;
        }
        if (qrcodeStructureappendN > 1)
        {
            array[0] = 3;
            array2[0] = 4;
            array[1] = qrcodeStructureappendM - 1;
            array2[1] = 4;
            array[2] = qrcodeStructureappendN - 1;
            array2[2] = 4;
            array[3] = qrcodeStructureappendParity;
            array2[3] = 8;
            num = 4;
        }
        array2[num] = 4;
        int[] array5;
        int num3;
        switch (qrcodeEncodeMode)
        {
            case ENCODE_MODE.ALPHA_NUMERIC:
                {
                    array5 = new int[41]
                    {
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4
                    };
                    array[num] = 2;
                    num++;
                    array[num] = num2;
                    array2[num] = 9;
                    num3 = num;
                    num++;
                    for (int i = 0; i < num2; i++)
                    {
                        char c = (char)qrcodeData[i];
                        sbyte b = 0;
                        if (c >= '0' && c < ':')
                        {
                            b = (sbyte)(c - 48);
                        }
                        else if (c >= 'A' && c < '[')
                        {
                            b = (sbyte)(c - 55);
                        }
                        else
                        {
                            if (c == ' ')
                            {
                                b = 36;
                            }
                            if (c == '$')
                            {
                                b = 37;
                            }
                            if (c == '%')
                            {
                                b = 38;
                            }
                            if (c == '*')
                            {
                                b = 39;
                            }
                            if (c == '+')
                            {
                                b = 40;
                            }
                            if (c == '-')
                            {
                                b = 41;
                            }
                            if (c == '.')
                            {
                                b = 42;
                            }
                            if (c == '/')
                            {
                                b = 43;
                            }
                            if (c == ':')
                            {
                                b = 44;
                            }
                        }
                        if (i % 2 == 0)
                        {
                            array[num] = b;
                            array2[num] = 6;
                            continue;
                        }
                        array[num] = array[num] * 45 + b;
                        array2[num] = 11;
                        if (i < num2 - 1)
                        {
                            num++;
                        }
                    }
                    num++;
                    break;
                }
            case ENCODE_MODE.NUMERIC:
                {
                    array5 = new int[41]
                    {
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4,
                    4
                    };
                    array[num] = 1;
                    num++;
                    array[num] = num2;
                    array2[num] = 10;
                    num3 = num;
                    num++;
                    for (int i = 0; i < num2; i++)
                    {
                        if (i % 3 == 0)
                        {
                            array[num] = qrcodeData[i] - 48;
                            array2[num] = 4;
                            continue;
                        }
                        array[num] = array[num] * 10 + (qrcodeData[i] - 48);
                        if (i % 3 == 1)
                        {
                            array2[num] = 7;
                            continue;
                        }
                        array2[num] = 10;
                        if (i < num2 - 1)
                        {
                            num++;
                        }
                    }
                    num++;
                    break;
                }
            default:
                {
                    array5 = new int[41]
                    {
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8,
                    8
                    };
                    array[num] = 4;
                    num++;
                    array[num] = num2;
                    array2[num] = 8;
                    num3 = num;
                    num++;
                    for (int i = 0; i < num2; i++)
                    {
                        array[i + num] = qrcodeData[i] & 0xFF;
                        array2[i + num] = 8;
                    }
                    num += num2;
                    break;
                }
        }
        int num4 = 0;
        for (int i = 0; i < num; i++)
        {
            num4 += array2[i];
        }
        int num5 = qrcodeErrorCorrect switch
        {
            ERROR_CORRECTION.L => 1,
            ERROR_CORRECTION.Q => 3,
            ERROR_CORRECTION.H => 2,
            _ => 0,
        };
        int[][] array6 = new int[4][]
        {
                new int[41]
                {
                    0,
                    128,
                    224,
                    352,
                    512,
                    688,
                    864,
                    992,
                    1232,
                    1456,
                    1728,
                    2032,
                    2320,
                    2672,
                    2920,
                    3320,
                    3624,
                    4056,
                    4504,
                    5016,
                    5352,
                    5712,
                    6256,
                    6880,
                    7312,
                    8000,
                    8496,
                    9024,
                    9544,
                    10136,
                    10984,
                    11640,
                    12328,
                    13048,
                    13800,
                    14496,
                    15312,
                    15936,
                    16816,
                    17728,
                    18672
                },
                new int[41]
                {
                    0,
                    152,
                    272,
                    440,
                    640,
                    864,
                    1088,
                    1248,
                    1552,
                    1856,
                    2192,
                    2592,
                    2960,
                    3424,
                    3688,
                    4184,
                    4712,
                    5176,
                    5768,
                    6360,
                    6888,
                    7456,
                    8048,
                    8752,
                    9392,
                    10208,
                    10960,
                    11744,
                    12248,
                    13048,
                    13880,
                    14744,
                    15640,
                    16568,
                    17528,
                    18448,
                    19472,
                    20528,
                    21616,
                    22496,
                    23648
                },
                new int[41]
                {
                    0,
                    72,
                    128,
                    208,
                    288,
                    368,
                    480,
                    528,
                    688,
                    800,
                    976,
                    1120,
                    1264,
                    1440,
                    1576,
                    1784,
                    2024,
                    2264,
                    2504,
                    2728,
                    3080,
                    3248,
                    3536,
                    3712,
                    4112,
                    4304,
                    4768,
                    5024,
                    5288,
                    5608,
                    5960,
                    6344,
                    6760,
                    7208,
                    7688,
                    7888,
                    8432,
                    8768,
                    9136,
                    9776,
                    10208
                },
                new int[41]
                {
                    0,
                    104,
                    176,
                    272,
                    384,
                    496,
                    608,
                    704,
                    880,
                    1056,
                    1232,
                    1440,
                    1648,
                    1952,
                    2088,
                    2360,
                    2600,
                    2936,
                    3176,
                    3560,
                    3880,
                    4096,
                    4544,
                    4912,
                    5312,
                    5744,
                    6032,
                    6464,
                    6968,
                    7288,
                    7880,
                    8264,
                    8920,
                    9368,
                    9848,
                    10288,
                    10832,
                    11408,
                    12016,
                    12656,
                    13328
                }
        };
        int num6 = 0;
        if (qrcodeVersion == 0)
        {
            qrcodeVersion = 1;
            for (int i = 1; i <= 40; i++)
            {
                if (array6[num5][i] >= num4 + array5[qrcodeVersion])
                {
                    num6 = array6[num5][i];
                    break;
                }
                qrcodeVersion++;
            }
        }
        else
        {
            num6 = array6[num5][qrcodeVersion];
        }
        num4 += array5[qrcodeVersion];
        array2[num3] = (sbyte)(array2[num3] + array5[qrcodeVersion]);
        int[] array7 = new int[41]
        {
                0,
                26,
                44,
                70,
                100,
                134,
                172,
                196,
                242,
                292,
                346,
                404,
                466,
                532,
                581,
                655,
                733,
                815,
                901,
                991,
                1085,
                1156,
                1258,
                1364,
                1474,
                1588,
                1706,
                1828,
                1921,
                2051,
                2185,
                2323,
                2465,
                2611,
                2761,
                2876,
                3034,
                3196,
                3362,
                3532,
                3706
        };
        int num7 = array7[qrcodeVersion];
        int num8 = 17 + (qrcodeVersion << 2);
        int[] array8 = new int[41]
        {
                0,
                0,
                7,
                7,
                7,
                7,
                7,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                3,
                3,
                3,
                3,
                3,
                3,
                3,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                3,
                3,
                3,
                3,
                3,
                3,
                3,
                0,
                0,
                0,
                0,
                0,
                0
        };
        int num9 = array8[qrcodeVersion] + (num7 << 3);
        sbyte[] array9 = new sbyte[num9];
        sbyte[] array10 = new sbyte[num9];
        sbyte[] array11 = new sbyte[num9];
        sbyte[] array12 = new sbyte[15];
        sbyte[] array13 = new sbyte[15];
        sbyte[] array14 = new sbyte[1];
        sbyte[] array15 = new sbyte[128];
        try
        {
            string name = "qrv" + Convert.ToString(qrcodeVersion) + "_" + Convert.ToString(num5);
            MemoryStream memoryStream = new MemoryStream((byte[])Resources.ResourceManager.GetObject(name));
            BufferedStream bufferedStream = new BufferedStream(memoryStream);
            SystemUtils.ReadInput(bufferedStream, array9, 0, array9.Length);
            SystemUtils.ReadInput(bufferedStream, array10, 0, array10.Length);
            SystemUtils.ReadInput(bufferedStream, array11, 0, array11.Length);
            SystemUtils.ReadInput(bufferedStream, array12, 0, array12.Length);
            SystemUtils.ReadInput(bufferedStream, array13, 0, array13.Length);
            SystemUtils.ReadInput(bufferedStream, array14, 0, array14.Length);
            SystemUtils.ReadInput(bufferedStream, array15, 0, array15.Length);
            bufferedStream.Close();
            memoryStream.Close();
        }
        catch (Exception throwable)
        {
            SystemUtils.WriteStackTrace(throwable, Console.Error);
        }
        sbyte b2 = 1;
        for (sbyte b3 = 1; b3 < 128; b3 = (sbyte)(b3 + 1))
        {
            if (array15[b3] == 0)
            {
                b2 = b3;
                break;
            }
        }
        sbyte[] array16 = new sbyte[b2];
        Array.Copy(array15, 0, array16, 0, (byte)b2);
        sbyte[] array17 = new sbyte[15]
        {
                0,
                1,
                2,
                3,
                4,
                5,
                7,
                8,
                8,
                8,
                8,
                8,
                8,
                8,
                8
        };
        sbyte[] array18 = new sbyte[15]
        {
                8,
                8,
                8,
                8,
                8,
                8,
                8,
                8,
                7,
                5,
                4,
                3,
                2,
                1,
                0
        };
        int maxDataCodewords = num6 >> 3;
        int num10 = 4 * qrcodeVersion + 17;
        int num11 = num10 * num10;
        sbyte[] array19 = new sbyte[num11 + num10];
        try
        {
            string name = "qrvfr" + Convert.ToString(qrcodeVersion);
            MemoryStream memoryStream = new MemoryStream((byte[])Resources.ResourceManager.GetObject(name));
            BufferedStream bufferedStream = new BufferedStream(memoryStream);
            SystemUtils.ReadInput(bufferedStream, array19, 0, array19.Length);
            bufferedStream.Close();
            memoryStream.Close();
        }
        catch (Exception throwable)
        {
            SystemUtils.WriteStackTrace(throwable, Console.Error);
        }
        if (num4 <= num6 - 4)
        {
            array[num] = 0;
            array2[num] = 4;
        }
        else if (num4 < num6)
        {
            array[num] = 0;
            array2[num] = (sbyte)(num6 - num4);
        }
        else if (num4 > num6)
        {
            Console.Out.WriteLine("overflow");
        }
        sbyte[] codewords = divideDataBy8Bits(array, array2, maxDataCodewords);
        sbyte[] array20 = calculateRSECC(codewords, array14[0], array16, maxDataCodewords, num7);
        sbyte[][] array21 = new sbyte[num10][];
        for (int j = 0; j < num10; j++)
        {
            array21[j] = new sbyte[num10];
        }
        for (int i = 0; i < num10; i++)
        {
            for (int k = 0; k < num10; k++)
            {
                array21[k][i] = 0;
            }
        }
        for (int i = 0; i < num7; i++)
        {
            sbyte b4 = array20[i];
            for (int k = 7; k >= 0; k--)
            {
                int num12 = i * 8 + k;
                array21[array9[num12] & 0xFF][array10[num12] & 0xFF] = (sbyte)((255 * (b4 & 1)) ^ array11[num12]);
                b4 = (sbyte)SystemUtils.URShift(b4 & 0xFF, 1);
            }
        }
        for (int num13 = array8[qrcodeVersion]; num13 > 0; num13--)
        {
            int num14 = num13 + num7 * 8 - 1;
            array21[array9[num14] & 0xFF][array10[num14] & 0xFF] = (sbyte)(0xFF ^ array11[num14]);
        }
        sbyte b5 = selectMask(array21, array8[qrcodeVersion] + num7 * 8);
        sbyte b6 = (sbyte)(1 << (int)b5);
        sbyte b7 = (sbyte)((num5 << 3) | b5);
        string[] array22 = new string[32]
        {
                "101010000010010",
                "101000100100101",
                "101111001111100",
                "101101101001011",
                "100010111111001",
                "100000011001110",
                "100111110010111",
                "100101010100000",
                "111011111000100",
                "111001011110011",
                "111110110101010",
                "111100010011101",
                "110011000101111",
                "110001100011000",
                "110110001000001",
                "110100101110110",
                "001011010001001",
                "001001110111110",
                "001110011100111",
                "001100111010000",
                "000011101100010",
                "000001001010101",
                "000110100001100",
                "000100000111011",
                "011010101011111",
                "011000001101000",
                "011111100110001",
                "011101000000110",
                "010010010110100",
                "010000110000011",
                "010111011011010",
                "010101111101101"
        };
        for (int i = 0; i < 15; i++)
        {
            sbyte b8 = sbyte.Parse(array22[b7].Substring(i, i + 1 - i));
            array21[array17[i] & 0xFF][array18[i] & 0xFF] = (sbyte)(b8 * 255);
            array21[array12[i] & 0xFF][array13[i] & 0xFF] = (sbyte)(b8 * 255);
        }
        bool[][] array23 = new bool[num10][];
        for (int l = 0; l < num10; l++)
        {
            array23[l] = new bool[num10];
        }
        int num15 = 0;
        for (int i = 0; i < num10; i++)
        {
            for (int k = 0; k < num10; k++)
            {
                if ((array21[k][i] & b6) != 0 || array19[num15] == 49)
                {
                    array23[k][i] = true;
                }
                else
                {
                    array23[k][i] = false;
                }
                num15++;
            }
            num15++;
        }
        return array23;
    }

    private static sbyte[] divideDataBy8Bits(int[] data, sbyte[] bits, int maxDataCodewords)
    {
        int num = bits.Length;
        int num2 = 0;
        int num3 = 8;
        int num4 = 0;
        if (num != data.Length)
        {
        }
        for (int i = 0; i < num; i++)
        {
            num4 += bits[i];
        }
        int num5 = (num4 - 1) / 8 + 1;
        sbyte[] array = new sbyte[maxDataCodewords];
        for (int i = 0; i < num5; i++)
        {
            array[i] = 0;
        }
        for (int i = 0; i < num; i++)
        {
            int num6 = data[i];
            int num7 = bits[i];
            bool flag = true;
            if (num7 == 0)
            {
                break;
            }
            while (flag)
            {
                if (num3 > num7)
                {
                    array[num2] = (sbyte)((array[num2] << num7) | num6);
                    num3 -= num7;
                    flag = false;
                    continue;
                }
                num7 -= num3;
                array[num2] = (sbyte)((array[num2] << num3) | (num6 >> num7));
                if (num7 == 0)
                {
                    flag = false;
                }
                else
                {
                    num6 &= (1 << num7) - 1;
                    flag = true;
                }
                num2++;
                num3 = 8;
            }
        }
        if (num3 != 8)
        {
            array[num2] = (sbyte)(array[num2] << num3);
        }
        else
        {
            num2--;
        }
        if (num2 < maxDataCodewords - 1)
        {
            bool flag = true;
            while (num2 < maxDataCodewords - 1)
            {
                num2++;
                if (flag)
                {
                    array[num2] = -20;
                }
                else
                {
                    array[num2] = 17;
                }
                flag = !flag;
            }
        }
        return array;
    }

    private static sbyte[] calculateRSECC(sbyte[] codewords, sbyte rsEccCodewords, sbyte[] rsBlockOrder, int maxDataCodewords, int maxCodewords)
    {
        sbyte[][] array = new sbyte[256][];
        for (int i = 0; i < 256; i++)
        {
            array[i] = new sbyte[rsEccCodewords];
        }
        try
        {
            string name = "rsc" + rsEccCodewords;
            MemoryStream memoryStream = new MemoryStream((byte[])Resources.ResourceManager.GetObject(name));
            BufferedStream bufferedStream = new BufferedStream(memoryStream);
            for (int i = 0; i < 256; i++)
            {
                SystemUtils.ReadInput(bufferedStream, array[i], 0, array[i].Length);
            }
            bufferedStream.Close();
            memoryStream.Close();
        }
        catch (Exception throwable)
        {
            SystemUtils.WriteStackTrace(throwable, Console.Error);
        }
        int num = 0;
        int num2 = 0;
        int num3 = 0;
        sbyte[][] array2 = new sbyte[rsBlockOrder.Length][];
        sbyte[] array3 = new sbyte[maxCodewords];
        Array.Copy(codewords, 0, array3, 0, codewords.Length);
        for (num = 0; num < rsBlockOrder.Length; num++)
        {
            array2[num] = new sbyte[(rsBlockOrder[num] & 0xFF) - rsEccCodewords];
        }
        for (num = 0; num < maxDataCodewords; num++)
        {
            array2[num3][num2] = codewords[num];
            num2++;
            if (num2 >= (rsBlockOrder[num3] & 0xFF) - rsEccCodewords)
            {
                num2 = 0;
                num3++;
            }
        }
        for (num3 = 0; num3 < rsBlockOrder.Length; num3++)
        {
            sbyte[] array4 = new sbyte[array2[num3].Length];
            array2[num3].CopyTo(array4, 0);
            int num4 = rsBlockOrder[num3] & 0xFF;
            int num5 = num4 - rsEccCodewords;
            for (num2 = num5; num2 > 0; num2--)
            {
                sbyte b = array4[0];
                if (b != 0)
                {
                    sbyte[] array5 = new sbyte[array4.Length - 1];
                    Array.Copy(array4, 1, array5, 0, array4.Length - 1);
                    sbyte[] xb = array[b & 0xFF];
                    array4 = calculateByteArrayBits(array5, xb, "xor");
                }
                else if (rsEccCodewords < array4.Length)
                {
                    sbyte[] array6 = new sbyte[array4.Length - 1];
                    Array.Copy(array4, 1, array6, 0, array4.Length - 1);
                    array4 = new sbyte[array6.Length];
                    array6.CopyTo(array4, 0);
                }
                else
                {
                    sbyte[] array6 = new sbyte[rsEccCodewords];
                    Array.Copy(array4, 1, array6, 0, array4.Length - 1);
                    array6[rsEccCodewords - 1] = 0;
                    array4 = new sbyte[array6.Length];
                    array6.CopyTo(array4, 0);
                }
            }
            Array.Copy(array4, 0, array3, codewords.Length + num3 * rsEccCodewords, (byte)rsEccCodewords);
        }
        return array3;
    }

    private static sbyte[] calculateByteArrayBits(sbyte[] xa, sbyte[] xb, string ind)
    {
        sbyte[] array;
        sbyte[] array2;
        if (xa.Length > xb.Length)
        {
            array = new sbyte[xa.Length];
            xa.CopyTo(array, 0);
            array2 = new sbyte[xb.Length];
            xb.CopyTo(array2, 0);
        }
        else
        {
            array = new sbyte[xb.Length];
            xb.CopyTo(array, 0);
            array2 = new sbyte[xa.Length];
            xa.CopyTo(array2, 0);
        }
        int num = array.Length;
        int num2 = array2.Length;
        sbyte[] array3 = new sbyte[num];
        for (int i = 0; i < num; i++)
        {
            if (i < num2)
            {
                if ((object)ind == "xor")
                {
                    array3[i] = (sbyte)(array[i] ^ array2[i]);
                }
                else
                {
                    array3[i] = (sbyte)(array[i] | array2[i]);
                }
            }
            else
            {
                array3[i] = array[i];
            }
        }
        return array3;
    }

    private static sbyte selectMask(sbyte[][] matrixContent, int maxCodewordsBitWithRemain)
    {
        int num = matrixContent.Length;
        int[] array = new int[8];
        int[] array2 = array;
        array = new int[8];
        int[] array3 = array;
        array = new int[8];
        int[] array4 = array;
        array = new int[8];
        int[] array5 = array;
        int num2 = 0;
        int num3 = 0;
        array = new int[8];
        int[] array6 = array;
        for (int i = 0; i < num; i++)
        {
            array = new int[8];
            int[] array7 = array;
            array = new int[8];
            int[] array8 = array;
            bool[] array9 = new bool[8];
            bool[] array10 = array9;
            array9 = new bool[8];
            bool[] array11 = array9;
            for (int j = 0; j < num; j++)
            {
                if (j > 0 && i > 0)
                {
                    num2 = matrixContent[j][i] & matrixContent[j - 1][i] & matrixContent[j][i - 1] & matrixContent[j - 1][i - 1] & 0xFF;
                    num3 = (matrixContent[j][i] & 0xFF) | (matrixContent[j - 1][i] & 0xFF) | (matrixContent[j][i - 1] & 0xFF) | (matrixContent[j - 1][i - 1] & 0xFF);
                }
                for (int k = 0; k < 8; k++)
                {
                    array7[k] = ((array7[k] & 0x3F) << 1) | (SystemUtils.URShift(matrixContent[j][i] & 0xFF, k) & 1);
                    array8[k] = ((array8[k] & 0x3F) << 1) | (SystemUtils.URShift(matrixContent[i][j] & 0xFF, k) & 1);
                    if ((matrixContent[j][i] & (1 << k)) != 0)
                    {
                        array6[k]++;
                    }
                    if (array7[k] == 93)
                    {
                        array4[k] += 40;
                    }
                    if (array8[k] == 93)
                    {
                        array4[k] += 40;
                    }
                    if (j > 0 && i > 0)
                    {
                        if (((uint)num2 & (true ? 1u : 0u)) != 0 || (num3 & 1) == 0)
                        {
                            array3[k] += 3;
                        }
                        num2 >>= 1;
                        num3 >>= 1;
                    }
                    if ((array7[k] & 0x1F) == 0 || (array7[k] & 0x1F) == 31)
                    {
                        if (j > 3)
                        {
                            if (array10[k])
                            {
                                array2[k]++;
                            }
                            else
                            {
                                array2[k] += 3;
                                array10[k] = true;
                            }
                        }
                    }
                    else
                    {
                        array10[k] = false;
                    }
                    if ((array8[k] & 0x1F) == 0 || (array8[k] & 0x1F) == 31)
                    {
                        if (j > 3)
                        {
                            if (array11[k])
                            {
                                array2[k]++;
                                continue;
                            }
                            array2[k] += 3;
                            array11[k] = true;
                        }
                    }
                    else
                    {
                        array11[k] = false;
                    }
                }
            }
        }
        int num4 = 0;
        sbyte result = 0;
        int[] array12 = new int[21]
        {
                90,
                80,
                70,
                60,
                50,
                40,
                30,
                20,
                10,
                0,
                0,
                10,
                20,
                30,
                40,
                50,
                60,
                70,
                80,
                90,
                90
        };
        for (int k = 0; k < 8; k++)
        {
            array5[k] = array12[20 * array6[k] / maxCodewordsBitWithRemain];
            int num5 = array2[k] + array3[k] + array4[k] + array5[k];
            if (num5 < num4 || k == 0)
            {
                result = (sbyte)k;
                num4 = num5;
            }
        }
        return result;
    }

    public virtual Bitmap Encode(string content, Encoding encoding)
    {
        bool[][] array = calQrcode(encoding.GetBytes(content));
        SolidBrush solidBrush = new SolidBrush(qrCodeBackgroundColor);
        Bitmap bitmap = new Bitmap((Int32)(array.Length * qrCodeScale) + 1, (Int32)(array.Length * qrCodeScale) + 1);
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.FillRectangle(solidBrush, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
        solidBrush.Color = qrCodeForegroundColor;
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array.Length; j++)
            {
                if (array[j][i])
                {
                    graphics.FillRectangle(solidBrush, j * qrCodeScale, i * qrCodeScale, qrCodeScale, qrCodeScale);
                }
            }
        }
        return bitmap;
    }

    public virtual Bitmap Encode(string content)
    {
        if (QRCodeUtility.IsUniCode(content))
        {
            return Encode(content, Encoding.GetEncoding("gb2312"));
        }
        return Encode(content, Encoding.ASCII);
    }
}