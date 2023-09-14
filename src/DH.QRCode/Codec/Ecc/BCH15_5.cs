namespace DH.QRCode.Codec.Ecc;

public class BCH15_5 {
    internal int[][] gf16;

    internal bool[] recieveData;

    internal int numCorrectedError;

    internal static string[] bitName = new string[15]
    {
            "c0",
            "c1",
            "c2",
            "c3",
            "c4",
            "c5",
            "c6",
            "c7",
            "c8",
            "c9",
            "d0",
            "d1",
            "d2",
            "d3",
            "d4"
    };

    public virtual int NumCorrectedError => numCorrectedError;

    public BCH15_5(bool[] source)
    {
        gf16 = createGF16();
        recieveData = source;
    }

    public virtual bool[] correct()
    {
        int[] s = calcSyndrome(recieveData);
        int[] errorPos = detectErrorBitPosition(s);
        return correctErrorBit(recieveData, errorPos);
    }

    internal virtual int[][] createGF16()
    {
        gf16 = new int[16][];
        for (int i = 0; i < 16; i++)
        {
            gf16[i] = new int[4];
        }
        int[] array = new int[4]
        {
                1,
                1,
                0,
                0
        };
        for (int i = 0; i < 4; i++)
        {
            gf16[i][i] = 1;
        }
        for (int i = 0; i < 4; i++)
        {
            gf16[4][i] = array[i];
        }
        for (int i = 5; i < 16; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                gf16[i][j] = gf16[i - 1][j - 1];
            }
            if (gf16[i - 1][3] == 1)
            {
                for (int j = 0; j < 4; j++)
                {
                    gf16[i][j] = (gf16[i][j] + array[j]) % 2;
                }
            }
        }
        return gf16;
    }

    internal virtual int searchElement(int[] x)
    {
        int i;
        for (i = 0; i < 15 && (x[0] != gf16[i][0] || x[1] != gf16[i][1] || x[2] != gf16[i][2] || x[3] != gf16[i][3]); i++)
        {
        }
        return i;
    }

    internal virtual int[] getCode(int input)
    {
        int[] array = new int[15];
        int[] array2 = new int[8];
        for (int i = 0; i < 15; i++)
        {
            int num = array2[7];
            int num2;
            int num3;
            if (i < 7)
            {
                num2 = (input >> 6 - i) % 2;
                num3 = (num2 + num) % 2;
            }
            else
            {
                num2 = num;
                num3 = 0;
            }
            array2[7] = (array2[6] + num3) % 2;
            array2[6] = (array2[5] + num3) % 2;
            array2[5] = array2[4];
            array2[4] = (array2[3] + num3) % 2;
            array2[3] = array2[2];
            array2[2] = array2[1];
            array2[1] = array2[0];
            array2[0] = num3;
            array[14 - i] = num2;
        }
        return array;
    }

    internal virtual int addGF(int arg1, int arg2)
    {
        int[] array = new int[4];
        for (int i = 0; i < 4; i++)
        {
            int num = ((arg1 >= 0 && arg1 < 15) ? gf16[arg1][i] : 0);
            int num2 = ((arg2 >= 0 && arg2 < 15) ? gf16[arg2][i] : 0);
            array[i] = (num + num2) % 2;
        }
        return searchElement(array);
    }

    internal virtual int[] calcSyndrome(bool[] y)
    {
        int[] array = new int[5];
        int[] array2 = new int[4];
        int i;
        for (i = 0; i < 15; i++)
        {
            if (y[i])
            {
                for (int j = 0; j < 4; j++)
                {
                    array2[j] = (array2[j] + gf16[i][j]) % 2;
                }
            }
        }
        i = searchElement(array2);
        array[0] = ((i >= 15) ? (-1) : i);
        array2 = new int[4];
        for (i = 0; i < 15; i++)
        {
            if (y[i])
            {
                for (int j = 0; j < 4; j++)
                {
                    array2[j] = (array2[j] + gf16[i * 3 % 15][j]) % 2;
                }
            }
        }
        i = searchElement(array2);
        array[2] = ((i >= 15) ? (-1) : i);
        array2 = new int[4];
        for (i = 0; i < 15; i++)
        {
            if (y[i])
            {
                for (int j = 0; j < 4; j++)
                {
                    array2[j] = (array2[j] + gf16[i * 5 % 15][j]) % 2;
                }
            }
        }
        i = searchElement(array2);
        array[4] = ((i >= 15) ? (-1) : i);
        return array;
    }

    internal virtual int[] calcErrorPositionVariable(int[] s)
    {
        int[] array = new int[4]
        {
                s[0],
                0,
                0,
                0
        };
        int arg = (s[0] + s[1]) % 15;
        int num = addGF(s[2], arg);
        num = ((num >= 15) ? (-1) : num);
        arg = (s[2] + s[1]) % 15;
        int num2 = addGF(s[4], arg);
        num2 = ((num2 >= 15) ? (-1) : num2);
        array[1] = ((num2 < 0 && num < 0) ? (-1) : ((num2 - num + 15) % 15));
        arg = (s[1] + array[0]) % 15;
        int arg2 = addGF(s[2], arg);
        arg = (s[0] + array[1]) % 15;
        array[2] = addGF(arg2, arg);
        return array;
    }

    internal virtual int[] detectErrorBitPosition(int[] s)
    {
        int[] array = calcErrorPositionVariable(s);
        int[] array2 = new int[4];
        if (array[0] == -1)
        {
            return array2;
        }
        if (array[1] == -1)
        {
            array2[0] = 1;
            array2[1] = array[0];
            return array2;
        }
        for (int i = 0; i < 15; i++)
        {
            int arg = i * 3 % 15;
            int num = i * 2 % 15;
            int num2 = i;
            int arg2 = (array[0] + num) % 15;
            int arg3 = addGF(arg, arg2);
            arg2 = (array[1] + num2) % 15;
            int arg4 = addGF(arg2, array[2]);
            int num3 = addGF(arg3, arg4);
            if (num3 >= 15)
            {
                array2[0]++;
                array2[array2[0]] = i;
            }
        }
        return array2;
    }

    internal virtual bool[] correctErrorBit(bool[] y, int[] errorPos)
    {
        for (int i = 1; i <= errorPos[0]; i++)
        {
            y[errorPos[i]] = !y[errorPos[i]];
        }
        numCorrectedError = errorPos[0];
        return y;
    }
}