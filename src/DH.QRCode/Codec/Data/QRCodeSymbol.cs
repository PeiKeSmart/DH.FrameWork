using DH.QRCode.Codec.Ecc;
using DH.QRCode.Codec.Reader.Pattern;
using DH.QRCode.Codec.Util;
using DH.QRCode.Geom;

using System.Collections;

namespace DH.QRCode.Codec.Data;

public class QRCodeSymbol {
    internal int version;

    internal int errorCollectionLevel;

    internal int maskPattern;

    internal int dataCapacity;

    internal bool[][] moduleMatrix;

    internal int width;

    internal int height;

    internal Point[][] alignmentPattern;

    internal int[][] numErrorCollectionCode = new int[40][]
    {
            new int[4]
            {
                7,
                10,
                13,
                17
            },
            new int[4]
            {
                10,
                16,
                22,
                28
            },
            new int[4]
            {
                15,
                26,
                36,
                44
            },
            new int[4]
            {
                20,
                36,
                52,
                64
            },
            new int[4]
            {
                26,
                48,
                72,
                88
            },
            new int[4]
            {
                36,
                64,
                96,
                112
            },
            new int[4]
            {
                40,
                72,
                108,
                130
            },
            new int[4]
            {
                48,
                88,
                132,
                156
            },
            new int[4]
            {
                60,
                110,
                160,
                192
            },
            new int[4]
            {
                72,
                130,
                192,
                224
            },
            new int[4]
            {
                80,
                150,
                224,
                264
            },
            new int[4]
            {
                96,
                176,
                260,
                308
            },
            new int[4]
            {
                104,
                198,
                288,
                352
            },
            new int[4]
            {
                120,
                216,
                320,
                384
            },
            new int[4]
            {
                132,
                240,
                360,
                432
            },
            new int[4]
            {
                144,
                280,
                408,
                480
            },
            new int[4]
            {
                168,
                308,
                448,
                532
            },
            new int[4]
            {
                180,
                338,
                504,
                588
            },
            new int[4]
            {
                196,
                364,
                546,
                650
            },
            new int[4]
            {
                224,
                416,
                600,
                700
            },
            new int[4]
            {
                224,
                442,
                644,
                750
            },
            new int[4]
            {
                252,
                476,
                690,
                816
            },
            new int[4]
            {
                270,
                504,
                750,
                900
            },
            new int[4]
            {
                300,
                560,
                810,
                960
            },
            new int[4]
            {
                312,
                588,
                870,
                1050
            },
            new int[4]
            {
                336,
                644,
                952,
                1110
            },
            new int[4]
            {
                360,
                700,
                1020,
                1200
            },
            new int[4]
            {
                390,
                728,
                1050,
                1260
            },
            new int[4]
            {
                420,
                784,
                1140,
                1350
            },
            new int[4]
            {
                450,
                812,
                1200,
                1440
            },
            new int[4]
            {
                480,
                868,
                1290,
                1530
            },
            new int[4]
            {
                510,
                924,
                1350,
                1620
            },
            new int[4]
            {
                540,
                980,
                1440,
                1710
            },
            new int[4]
            {
                570,
                1036,
                1530,
                1800
            },
            new int[4]
            {
                570,
                1064,
                1590,
                1890
            },
            new int[4]
            {
                600,
                1120,
                1680,
                1980
            },
            new int[4]
            {
                630,
                1204,
                1770,
                2100
            },
            new int[4]
            {
                660,
                1260,
                1860,
                2220
            },
            new int[4]
            {
                720,
                1316,
                1950,
                2310
            },
            new int[4]
            {
                750,
                1372,
                2040,
                2430
            }
    };

    internal int[][] numRSBlocks = new int[40][]
    {
            new int[4]
            {
                1,
                1,
                1,
                1
            },
            new int[4]
            {
                1,
                1,
                1,
                1
            },
            new int[4]
            {
                1,
                1,
                2,
                2
            },
            new int[4]
            {
                1,
                2,
                2,
                4
            },
            new int[4]
            {
                1,
                2,
                4,
                4
            },
            new int[4]
            {
                2,
                4,
                4,
                4
            },
            new int[4]
            {
                2,
                4,
                6,
                5
            },
            new int[4]
            {
                2,
                4,
                6,
                6
            },
            new int[4]
            {
                2,
                5,
                8,
                8
            },
            new int[4]
            {
                4,
                5,
                8,
                8
            },
            new int[4]
            {
                4,
                5,
                8,
                11
            },
            new int[4]
            {
                4,
                8,
                10,
                11
            },
            new int[4]
            {
                4,
                9,
                12,
                16
            },
            new int[4]
            {
                4,
                9,
                16,
                16
            },
            new int[4]
            {
                6,
                10,
                12,
                18
            },
            new int[4]
            {
                6,
                10,
                17,
                16
            },
            new int[4]
            {
                6,
                11,
                16,
                19
            },
            new int[4]
            {
                6,
                13,
                18,
                21
            },
            new int[4]
            {
                7,
                14,
                21,
                25
            },
            new int[4]
            {
                8,
                16,
                20,
                25
            },
            new int[4]
            {
                8,
                17,
                23,
                25
            },
            new int[4]
            {
                9,
                17,
                23,
                34
            },
            new int[4]
            {
                9,
                18,
                25,
                30
            },
            new int[4]
            {
                10,
                20,
                27,
                32
            },
            new int[4]
            {
                12,
                21,
                29,
                35
            },
            new int[4]
            {
                12,
                23,
                34,
                37
            },
            new int[4]
            {
                12,
                25,
                34,
                40
            },
            new int[4]
            {
                13,
                26,
                35,
                42
            },
            new int[4]
            {
                14,
                28,
                38,
                45
            },
            new int[4]
            {
                15,
                29,
                40,
                48
            },
            new int[4]
            {
                16,
                31,
                43,
                51
            },
            new int[4]
            {
                17,
                33,
                45,
                54
            },
            new int[4]
            {
                18,
                35,
                48,
                57
            },
            new int[4]
            {
                19,
                37,
                51,
                60
            },
            new int[4]
            {
                19,
                38,
                53,
                63
            },
            new int[4]
            {
                20,
                40,
                56,
                66
            },
            new int[4]
            {
                21,
                43,
                59,
                70
            },
            new int[4]
            {
                22,
                45,
                62,
                74
            },
            new int[4]
            {
                24,
                47,
                65,
                77
            },
            new int[4]
            {
                25,
                49,
                68,
                81
            }
    };

    public virtual int NumErrorCollectionCode => numErrorCollectionCode[version - 1][errorCollectionLevel];

    public virtual int NumRSBlocks => numRSBlocks[version - 1][errorCollectionLevel];

    public virtual int Version => version;

    public virtual string VersionReference
    {
        get
        {
            char[] array = new char[4]
            {
                    'L',
                    'M',
                    'Q',
                    'H'
            };
            return Convert.ToString(version) + "-" + array[errorCollectionLevel];
        }
    }

    public virtual Point[][] AlignmentPattern => alignmentPattern;

    public virtual int DataCapacity => dataCapacity;

    public virtual int ErrorCollectionLevel => errorCollectionLevel;

    public virtual int MaskPatternReferer => maskPattern;

    public virtual string MaskPatternRefererAsString
    {
        get
        {
            string text = Convert.ToString(MaskPatternReferer, 2);
            int length = text.Length;
            for (int i = 0; i < 3 - length; i++)
            {
                text = "0" + text;
            }
            return text;
        }
    }

    public virtual int Width => width;

    public virtual int Height => height;

    public virtual int[] Blocks
    {
        get
        {
            int num = Width;
            int num2 = Height;
            int num3 = num - 1;
            int num4 = num2 - 1;
            ArrayList arrayList = ArrayList.Synchronized(new ArrayList(10));
            ArrayList arrayList2 = ArrayList.Synchronized(new ArrayList(10));
            int num5 = 0;
            int num6 = 7;
            int num7 = 0;
            bool flag = true;
            bool flag2 = false;
            bool flag3 = flag;
            do
            {
                arrayList.Add(getElement(num3, num4));
                if (getElement(num3, num4))
                {
                    num5 += 1 << num6;
                }
                num6--;
                if (num6 == -1)
                {
                    arrayList2.Add(num5);
                    num6 = 7;
                    num5 = 0;
                }
                do
                {
                    if (flag3 == flag)
                    {
                        if ((num3 + num7) % 2 == 0)
                        {
                            num3--;
                            continue;
                        }
                        if (num4 > 0)
                        {
                            num3++;
                            num4--;
                            continue;
                        }
                        num3--;
                        if (num3 == 6)
                        {
                            num3--;
                            num7 = 1;
                        }
                        flag3 = flag2;
                    }
                    else if ((num3 + num7) % 2 == 0)
                    {
                        num3--;
                    }
                    else if (num4 < num2 - 1)
                    {
                        num3++;
                        num4++;
                    }
                    else
                    {
                        num3--;
                        if (num3 == 6)
                        {
                            num3--;
                            num7 = 1;
                        }
                        flag3 = flag;
                    }
                }
                while (isInFunctionPattern(num3, num4));
            }
            while (num3 != -1);
            int[] array = new int[arrayList2.Count];
            for (int i = 0; i < arrayList2.Count; i++)
            {
                int num8 = (array[i] = (int)arrayList2[i]);
            }
            return array;
        }
    }

    public virtual bool getElement(int x, int y)
    {
        return moduleMatrix[x][y];
    }

    public QRCodeSymbol(bool[][] moduleMatrix)
    {
        this.moduleMatrix = moduleMatrix;
        width = moduleMatrix.Length;
        height = moduleMatrix[0].Length;
        initialize();
    }

    internal virtual void initialize()
    {
        version = (width - 17) / 4;
        Point[][] array = new Point[1][];
        for (int i = 0; i < 1; i++)
        {
            array[i] = new Point[1];
        }
        int[] array2 = new int[1];
        if (version >= 2 && version <= 40)
        {
            array2 = LogicalSeed.getSeed(version);
            Point[][] array3 = new Point[array2.Length][];
            for (int j = 0; j < array2.Length; j++)
            {
                array3[j] = new Point[array2.Length];
            }
            array = array3;
        }
        for (int k = 0; k < array2.Length; k++)
        {
            for (int l = 0; l < array2.Length; l++)
            {
                array[l][k] = new Point(array2[l], array2[k]);
            }
        }
        alignmentPattern = array;
        dataCapacity = calcDataCapacity();
        bool[] formatInformation = readFormatInformation();
        decodeFormatInformation(formatInformation);
        unmask();
    }

    internal virtual bool[] readFormatInformation()
    {
        bool[] array = new bool[15];
        for (int i = 0; i <= 5; i++)
        {
            array[i] = getElement(8, i);
        }
        array[6] = getElement(8, 7);
        array[7] = getElement(8, 8);
        array[8] = getElement(7, 8);
        for (int i = 9; i <= 14; i++)
        {
            array[i] = getElement(14 - i, 8);
        }
        int number = 21522;
        for (int i = 0; i <= 14; i++)
        {
            bool flag = false;
            flag = (((SystemUtils.URShift(number, i) & 1) == 1) ? true : false);
            if (array[i] == flag)
            {
                array[i] = false;
            }
            else
            {
                array[i] = true;
            }
        }
        BCH15_5 bCH15_ = new BCH15_5(array);
        bool[] array2 = bCH15_.correct();
        bool[] array3 = new bool[5];
        for (int i = 0; i < 5; i++)
        {
            array3[i] = array2[10 + i];
        }
        return array3;
    }

    internal virtual void unmask()
    {
        bool[][] array = generateMaskPattern();
        int num = Width;
        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < num; j++)
            {
                if (array[j][i])
                {
                    reverseElement(j, i);
                }
            }
        }
    }

    internal virtual bool[][] generateMaskPattern()
    {
        int maskPatternReferer = MaskPatternReferer;
        int num = Width;
        int num2 = Height;
        bool[][] array = new bool[num][];
        for (int i = 0; i < num; i++)
        {
            array[i] = new bool[num2];
        }
        for (int j = 0; j < num2; j++)
        {
            for (int k = 0; k < num; k++)
            {
                if (isInFunctionPattern(k, j))
                {
                    continue;
                }
                switch (maskPatternReferer)
                {
                    case 0:
                        if ((j + k) % 2 == 0)
                        {
                            array[k][j] = true;
                        }
                        break;
                    case 1:
                        if (j % 2 == 0)
                        {
                            array[k][j] = true;
                        }
                        break;
                    case 2:
                        if (k % 3 == 0)
                        {
                            array[k][j] = true;
                        }
                        break;
                    case 3:
                        if ((j + k) % 3 == 0)
                        {
                            array[k][j] = true;
                        }
                        break;
                    case 4:
                        if ((j / 2 + k / 3) % 2 == 0)
                        {
                            array[k][j] = true;
                        }
                        break;
                    case 5:
                        if (j * k % 2 + j * k % 3 == 0)
                        {
                            array[k][j] = true;
                        }
                        break;
                    case 6:
                        if ((j * k % 2 + j * k % 3) % 2 == 0)
                        {
                            array[k][j] = true;
                        }
                        break;
                    case 7:
                        if ((j * k % 3 + (j + k) % 2) % 2 == 0)
                        {
                            array[k][j] = true;
                        }
                        break;
                }
            }
        }
        return array;
    }

    private int calcDataCapacity()
    {
        int num = 0;
        int num2 = 0;
        int num3 = Version;
        num2 = ((num3 > 6) ? 67 : 31);
        int num4 = num3 / 7 + 2;
        int num5 = ((num3 == 1) ? 192 : (192 + (num4 * num4 - 3) * 25));
        num = num5 + 8 * num3 + 2 - (num4 - 2) * 10;
        return (width * width - num - num2) / 8;
    }

    internal virtual void decodeFormatInformation(bool[] formatInformation)
    {
        if (!formatInformation[4])
        {
            if (formatInformation[3])
            {
                errorCollectionLevel = 0;
            }
            else
            {
                errorCollectionLevel = 1;
            }
        }
        else if (formatInformation[3])
        {
            errorCollectionLevel = 2;
        }
        else
        {
            errorCollectionLevel = 3;
        }
        for (int num = 2; num >= 0; num--)
        {
            if (formatInformation[num])
            {
                maskPattern += 1 << num;
            }
        }
    }

    public virtual void reverseElement(int x, int y)
    {
        moduleMatrix[x][y] = !moduleMatrix[x][y];
    }

    public virtual bool isInFunctionPattern(int targetX, int targetY)
    {
        if (targetX < 9 && targetY < 9)
        {
            return true;
        }
        if (targetX > Width - 9 && targetY < 9)
        {
            return true;
        }
        if (targetX < 9 && targetY > Height - 9)
        {
            return true;
        }
        if (version >= 7)
        {
            if (targetX > Width - 12 && targetY < 6)
            {
                return true;
            }
            if (targetX < 6 && targetY > Height - 12)
            {
                return true;
            }
        }
        if (targetX == 6 || targetY == 6)
        {
            return true;
        }
        Point[][] array = AlignmentPattern;
        int num = array.Length;
        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < num; j++)
            {
                if ((j != 0 || i != 0) && (j != num - 1 || i != 0) && (j != 0 || i != num - 1) && Math.Abs(array[j][i].X - targetX) < 3 && Math.Abs(array[j][i].Y - targetY) < 3)
                {
                    return true;
                }
            }
        }
        return false;
    }
}