using DH.QRCode.Codec.Util;
using DH.QRCode.ExceptionHandler;

namespace DH.QRCode.Codec.Reader;

public class QRCodeDataBlockReader {
    private const int MODE_NUMBER = 1;

    private const int MODE_ROMAN_AND_NUMBER = 2;

    private const int MODE_8BIT_BYTE = 4;

    private const int MODE_KANJI = 8;

    internal int[] blocks;

    internal int dataLengthMode;

    internal int blockPointer;

    internal int bitPointer;

    internal int dataLength;

    internal int numErrorCorrectionCode;

    internal DebugCanvas canvas;

    private int[][] sizeOfDataLengthInfo = new int[3][]
    {
            new int[4]
            {
                10,
                9,
                8,
                8
            },
            new int[4]
            {
                12,
                11,
                16,
                10
            },
            new int[4]
            {
                14,
                13,
                16,
                12
            }
    };

    internal virtual int NextMode
    {
        get
        {
            if (blockPointer > blocks.Length - numErrorCorrectionCode - 2)
            {
                return 0;
            }
            return getNextBits(4);
        }
    }

    public virtual sbyte[] DataByte
    {
        get
        {
            canvas.println("Reading data blocks.");
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                while (true)
                {
                    int nextMode = NextMode;
                    int num;
                    switch (nextMode)
                    {
                        case 0:
                            if (memoryStream.Length <= 0)
                            {
                                throw new InvalidDataBlockException("Empty data block");
                            }
                            goto end_IL_0018;
                        default:
                            num = ((nextMode == 8) ? 1 : 0);
                            break;
                        case 1:
                        case 2:
                        case 4:
                            num = 1;
                            break;
                    }
                    if (num == 0)
                    {
                        throw new InvalidDataBlockException("Invalid mode: " + nextMode + " in (block:" + blockPointer + " bit:" + bitPointer + ")");
                    }
                    dataLength = getDataLength(nextMode);
                    if (dataLength < 1)
                    {
                        throw new InvalidDataBlockException("Invalid data length: " + dataLength);
                    }
                    switch (nextMode)
                    {
                        case 1:
                            {
                                sbyte[] array4 = SystemUtils.ToSByteArray(SystemUtils.ToByteArray(getFigureString(dataLength)));
                                memoryStream.Write(SystemUtils.ToByteArray(array4), 0, array4.Length);
                                break;
                            }
                        case 2:
                            {
                                sbyte[] array3 = SystemUtils.ToSByteArray(SystemUtils.ToByteArray(getRomanAndFigureString(dataLength)));
                                memoryStream.Write(SystemUtils.ToByteArray(array3), 0, array3.Length);
                                break;
                            }
                        case 4:
                            {
                                sbyte[] array2 = get8bitByteArray(dataLength);
                                memoryStream.Write(SystemUtils.ToByteArray(array2), 0, array2.Length);
                                break;
                            }
                        case 8:
                            {
                                sbyte[] array = SystemUtils.ToSByteArray(SystemUtils.ToByteArray(getKanjiString(dataLength)));
                                memoryStream.Write(SystemUtils.ToByteArray(array), 0, array.Length);
                                break;
                            }
                    }
                    bool flag = true;
                }
            end_IL_0018:;
            }
            catch (IndexOutOfRangeException throwable)
            {
                SystemUtils.WriteStackTrace(throwable, Console.Error);
                throw new InvalidDataBlockException("Data Block Error in (block:" + blockPointer + " bit:" + bitPointer + ")");
            }
            catch (IOException ex)
            {
                throw new InvalidDataBlockException(ex.Message);
            }
            return SystemUtils.ToSByteArray(memoryStream.ToArray());
        }
    }

    public virtual string DataString
    {
        get
        {
            canvas.println("Reading data blocks...");
            string text = "";
            while (true)
            {
                int nextMode = NextMode;
                canvas.println("mode: " + nextMode);
                if (nextMode == 0)
                {
                    break;
                }
                if (nextMode != 1 && nextMode != 2 && nextMode != 4 && nextMode != 8)
                {
                }
                dataLength = getDataLength(nextMode);
                canvas.println(Convert.ToString(blocks[blockPointer]));
                Console.Out.WriteLine("length: " + dataLength);
                switch (nextMode)
                {
                    case 1:
                        text += getFigureString(dataLength);
                        break;
                    case 2:
                        text += getRomanAndFigureString(dataLength);
                        break;
                    case 4:
                        text += get8bitByteString(dataLength);
                        break;
                    case 8:
                        text += getKanjiString(dataLength);
                        break;
                }
                bool flag = true;
            }
            Console.Out.WriteLine("");
            return text;
        }
    }

    public QRCodeDataBlockReader(int[] blocks, int version, int numErrorCorrectionCode)
    {
        blockPointer = 0;
        bitPointer = 7;
        dataLength = 0;
        this.blocks = blocks;
        this.numErrorCorrectionCode = numErrorCorrectionCode;
        if (version <= 9)
        {
            dataLengthMode = 0;
        }
        else if (version >= 10 && version <= 26)
        {
            dataLengthMode = 1;
        }
        else if (version >= 27 && version <= 40)
        {
            dataLengthMode = 2;
        }
        canvas = QRCodeDecoder.Canvas;
    }

    internal virtual int getNextBits(int numBits)
    {
        int num = 0;
        if (numBits < bitPointer + 1)
        {
            int num2 = 0;
            for (int i = 0; i < numBits; i++)
            {
                num2 += 1 << i;
            }
            num2 <<= bitPointer - numBits + 1;
            num = (blocks[blockPointer] & num2) >> bitPointer - numBits + 1;
            bitPointer -= numBits;
            return num;
        }
        if (numBits < bitPointer + 1 + 8)
        {
            int num3 = 0;
            for (int i = 0; i < bitPointer + 1; i++)
            {
                num3 += 1 << i;
            }
            num = (blocks[blockPointer] & num3) << numBits - (bitPointer + 1);
            blockPointer++;
            num += blocks[blockPointer] >> 8 - (numBits - (bitPointer + 1));
            bitPointer -= numBits % 8;
            if (bitPointer < 0)
            {
                bitPointer = 8 + bitPointer;
            }
            return num;
        }
        if (numBits < bitPointer + 1 + 16)
        {
            int num3 = 0;
            int num4 = 0;
            for (int i = 0; i < bitPointer + 1; i++)
            {
                num3 += 1 << i;
            }
            int num5 = (blocks[blockPointer] & num3) << numBits - (bitPointer + 1);
            blockPointer++;
            int num6 = blocks[blockPointer] << numBits - (bitPointer + 1 + 8);
            blockPointer++;
            for (int i = 0; i < numBits - (bitPointer + 1 + 8); i++)
            {
                num4 += 1 << i;
            }
            num4 <<= 8 - (numBits - (bitPointer + 1 + 8));
            int num7 = (blocks[blockPointer] & num4) >> 8 - (numBits - (bitPointer + 1 + 8));
            num = num5 + num6 + num7;
            bitPointer -= (numBits - 8) % 8;
            if (bitPointer < 0)
            {
                bitPointer = 8 + bitPointer;
            }
            return num;
        }
        Console.Out.WriteLine("ERROR!");
        return 0;
    }

    internal virtual int guessMode(int mode)
    {
        return mode switch
        {
            3 => 1,
            5 => 4,
            6 => 4,
            7 => 4,
            9 => 8,
            10 => 8,
            11 => 8,
            12 => 4,
            13 => 4,
            14 => 4,
            15 => 4,
            _ => 8,
        };
    }

    internal virtual int getDataLength(int modeIndicator)
    {
        int num = 0;
        while (true)
        {
            bool flag = true;
            if (modeIndicator >> num == 1)
            {
                break;
            }
            num++;
        }
        return getNextBits(sizeOfDataLengthInfo[dataLengthMode][num]);
    }

    internal virtual string getFigureString(int dataLength)
    {
        int num = dataLength;
        int num2 = 0;
        string text = "";
        do
        {
            if (num >= 3)
            {
                num2 = getNextBits(10);
                if (num2 < 100)
                {
                    text += "0";
                }
                if (num2 < 10)
                {
                    text += "0";
                }
                num -= 3;
            }
            else
            {
                switch (num)
                {
                    case 2:
                        num2 = getNextBits(7);
                        if (num2 < 10)
                        {
                            text += "0";
                        }
                        num -= 2;
                        break;
                    case 1:
                        num2 = getNextBits(4);
                        num--;
                        break;
                }
            }
            text += Convert.ToString(num2);
        }
        while (num > 0);
        return text;
    }

    internal virtual string getRomanAndFigureString(int dataLength)
    {
        int num = dataLength;
        int num2 = 0;
        string text = "";
        char[] array = new char[45]
        {
                '0',
                '1',
                '2',
                '3',
                '4',
                '5',
                '6',
                '7',
                '8',
                '9',
                'A',
                'B',
                'C',
                'D',
                'E',
                'F',
                'G',
                'H',
                'I',
                'J',
                'K',
                'L',
                'M',
                'N',
                'O',
                'P',
                'Q',
                'R',
                'S',
                'T',
                'U',
                'V',
                'W',
                'X',
                'Y',
                'Z',
                ' ',
                '$',
                '%',
                '*',
                '+',
                '-',
                '.',
                '/',
                ':'
        };
        do
        {
            if (num > 1)
            {
                num2 = getNextBits(11);
                int num3 = num2 / 45;
                int num4 = num2 % 45;
                text += Convert.ToString(array[num3]);
                text += Convert.ToString(array[num4]);
                num -= 2;
            }
            else if (num == 1)
            {
                num2 = getNextBits(6);
                text += Convert.ToString(array[num2]);
                num--;
            }
        }
        while (num > 0);
        return text;
    }

    public virtual sbyte[] get8bitByteArray(int dataLength)
    {
        int num = dataLength;
        int num2 = 0;
        MemoryStream memoryStream = new MemoryStream();
        do
        {
            canvas.println("Length: " + num);
            num2 = getNextBits(8);
            memoryStream.WriteByte((byte)num2);
            num--;
        }
        while (num > 0);
        return SystemUtils.ToSByteArray(memoryStream.ToArray());
    }

    internal virtual string get8bitByteString(int dataLength)
    {
        int num = dataLength;
        int num2 = 0;
        string text = "";
        do
        {
            num2 = getNextBits(8);
            text += (char)num2;
            num--;
        }
        while (num > 0);
        return text;
    }

    internal virtual string getKanjiString(int dataLength)
    {
        int num = dataLength;
        int num2 = 0;
        string text = "";
        do
        {
            num2 = getNextBits(13);
            int num3 = num2 % 192;
            int num4 = num2 / 192;
            int num5 = (num4 << 8) + num3;
            int num6 = 0;
            num6 = ((num5 + 33088 > 40956) ? (num5 + 49472) : (num5 + 33088));
            text += new string(SystemUtils.ToCharArray(SystemUtils.ToByteArray(new sbyte[2]
            {
                    (sbyte)(num6 >> 8),
                    (sbyte)(num6 & 0xFF)
            })));
            num--;
        }
        while (num > 0);
        return text;
    }
}