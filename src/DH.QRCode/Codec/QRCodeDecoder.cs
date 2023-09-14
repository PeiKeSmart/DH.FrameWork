using DH.QRCode.Codec.Data;
using DH.QRCode.Codec.Ecc;
using DH.QRCode.Codec.Reader;
using DH.QRCode.Codec.Util;
using DH.QRCode.ExceptionHandler;
using DH.QRCode.Geom;

using System.Collections;
using System.Text;

namespace DH.QRCode.Codec;

public class QRCodeDecoder {
    internal class DecodeResult {
        internal int numCorrections;

        internal bool correctionSucceeded;

        internal sbyte[] decodedBytes;

        private QRCodeDecoder enclosingInstance;

        public virtual sbyte[] DecodedBytes => decodedBytes;

        public virtual int NumErrors => numCorrections;

        public virtual bool CorrectionSucceeded => correctionSucceeded;

        public QRCodeDecoder Enclosing_Instance => enclosingInstance;

        public DecodeResult(QRCodeDecoder enclosingInstance, sbyte[] decodedBytes, int numErrors, bool correctionSucceeded)
        {
            InitBlock(enclosingInstance);
            this.decodedBytes = decodedBytes;
            numCorrections = numErrors;
            this.correctionSucceeded = correctionSucceeded;
        }

        private void InitBlock(QRCodeDecoder enclosingInstance)
        {
            this.enclosingInstance = enclosingInstance;
        }
    }

    internal QRCodeSymbol qrCodeSymbol;

    internal int numTryDecode;

    internal ArrayList results;

    internal ArrayList lastResults = ArrayList.Synchronized(new ArrayList(10));

    internal static DebugCanvas canvas;

    internal QRCodeImageReader imageReader;

    internal int numLastCorrections;

    internal bool correctionSucceeded;

    public static DebugCanvas Canvas
    {
        get
        {
            return canvas;
        }
        set
        {
            canvas = value;
        }
    }

    internal virtual Point[] AdjustPoints
    {
        get
        {
            ArrayList arrayList = ArrayList.Synchronized(new ArrayList(10));
            for (int i = 0; i < 4; i++)
            {
                arrayList.Add(new Point(1, 1));
            }
            int num = 0;
            int num2 = 0;
            for (int num3 = 0; num3 > -4; num3--)
            {
                for (int num4 = 0; num4 > -4; num4--)
                {
                    if (num4 != num3 && (num4 + num3) % 2 == 0)
                    {
                        arrayList.Add(new Point(num4 - num, num3 - num2));
                        num = num4;
                        num2 = num3;
                    }
                }
            }
            Point[] array = new Point[arrayList.Count];
            for (int j = 0; j < array.Length; j++)
            {
                array[j] = (Point)arrayList[j];
            }
            return array;
        }
    }

    public QRCodeDecoder()
    {
        numTryDecode = 0;
        results = ArrayList.Synchronized(new ArrayList(10));
        canvas = new DebugCanvasAdapter();
    }

    public virtual sbyte[] decodeBytes(QRCodeImage qrCodeImage)
    {
        Point[] adjustPoints = AdjustPoints;
        ArrayList arrayList = ArrayList.Synchronized(new ArrayList(10));
        while (numTryDecode < adjustPoints.Length)
        {
            try
            {
                DecodeResult decodeResult = decode(qrCodeImage, adjustPoints[numTryDecode]);
                if (decodeResult.CorrectionSucceeded)
                {
                    return decodeResult.DecodedBytes;
                }
                arrayList.Add(decodeResult);
                canvas.println("Decoding succeeded but could not correct");
                canvas.println("all errors. Retrying..");
            }
            catch (DecodingFailedException ex)
            {
                if (ex.Message.IndexOf("Finder Pattern") >= 0)
                {
                    throw ex;
                }
            }
            finally
            {
                numTryDecode++;
            }
        }
        if (arrayList.Count == 0)
        {
            throw new DecodingFailedException("Give up decoding");
        }
        int num = -1;
        int num2 = int.MaxValue;
        for (int i = 0; i < arrayList.Count; i++)
        {
            DecodeResult decodeResult = (DecodeResult)arrayList[i];
            if (decodeResult.NumErrors < num2)
            {
                num2 = decodeResult.NumErrors;
                num = i;
            }
        }
        canvas.println("All trials need for correct error");
        canvas.println("Reporting #" + num + " that,");
        canvas.println("corrected minimum errors (" + num2 + ")");
        canvas.println("Decoding finished.");
        return ((DecodeResult)arrayList[num]).DecodedBytes;
    }

    public virtual string decode(QRCodeImage qrCodeImage, Encoding encoding)
    {
        sbyte[] array = decodeBytes(qrCodeImage);
        byte[] array2 = new byte[array.Length];
        Buffer.BlockCopy(array, 0, array2, 0, array2.Length);
        return encoding.GetString(array2);
    }

    public virtual string decode(QRCodeImage qrCodeImage)
    {
        sbyte[] array = decodeBytes(qrCodeImage);
        byte[] array2 = new byte[array.Length];
        Buffer.BlockCopy(array, 0, array2, 0, array2.Length);
        Encoding encoding = Encoding.GetEncoding("gb2312");
        return encoding.GetString(array2);
    }

    internal virtual DecodeResult decode(QRCodeImage qrCodeImage, Point adjust)
    {
        try
        {
            if (numTryDecode == 0)
            {
                canvas.println("Decoding started");
                int[][] image = imageToIntArray(qrCodeImage);
                imageReader = new QRCodeImageReader();
                qrCodeSymbol = imageReader.getQRCodeSymbol(image);
            }
            else
            {
                canvas.println("--");
                canvas.println("Decoding restarted #" + numTryDecode);
                qrCodeSymbol = imageReader.getQRCodeSymbolWithAdjustedGrid(adjust);
            }
        }
        catch (SymbolNotFoundException ex)
        {
            throw new DecodingFailedException(ex.Message);
        }
        canvas.println("Created QRCode symbol.");
        canvas.println("Reading symbol.");
        canvas.println("Version: " + qrCodeSymbol.VersionReference);
        canvas.println("Mask pattern: " + qrCodeSymbol.MaskPatternRefererAsString);
        int[] blocks = qrCodeSymbol.Blocks;
        canvas.println("Correcting data errors.");
        blocks = correctDataBlocks(blocks);
        try
        {
            sbyte[] decodedByteArray = getDecodedByteArray(blocks, qrCodeSymbol.Version, qrCodeSymbol.NumErrorCollectionCode);
            return new DecodeResult(this, decodedByteArray, numLastCorrections, correctionSucceeded);
        }
        catch (InvalidDataBlockException ex2)
        {
            canvas.println(ex2.Message);
            throw new DecodingFailedException(ex2.Message);
        }
    }

    internal virtual int[][] imageToIntArray(QRCodeImage image)
    {
        int width = image.Width;
        int height = image.Height;
        int[][] array = new int[width][];
        for (int i = 0; i < width; i++)
        {
            array[i] = new int[height];
        }
        for (int j = 0; j < height; j++)
        {
            for (int k = 0; k < width; k++)
            {
                array[k][j] = image.getPixel(k, j);
            }
        }
        return array;
    }

    internal virtual int[] correctDataBlocks(int[] blocks)
    {
        int num = 0;
        int dataCapacity = qrCodeSymbol.DataCapacity;
        int[] array = new int[dataCapacity];
        int numErrorCollectionCode = qrCodeSymbol.NumErrorCollectionCode;
        int numRSBlocks = qrCodeSymbol.NumRSBlocks;
        int num2 = numErrorCollectionCode / numRSBlocks;
        if (numRSBlocks == 1)
        {
            ReedSolomon reedSolomon = new ReedSolomon(blocks, num2);
            reedSolomon.correct();
            num += reedSolomon.NumCorrectedErrors;
            if (num > 0)
            {
                canvas.println(Convert.ToString(num) + " data errors corrected.");
            }
            else
            {
                canvas.println("No errors found.");
            }
            numLastCorrections = num;
            correctionSucceeded = reedSolomon.CorrectionSucceeded;
            return blocks;
        }
        int num3 = dataCapacity % numRSBlocks;
        if (num3 == 0)
        {
            int num4 = dataCapacity / numRSBlocks;
            int[][] array2 = new int[numRSBlocks][];
            for (int i = 0; i < numRSBlocks; i++)
            {
                array2[i] = new int[num4];
            }
            int[][] array3 = array2;
            for (int i = 0; i < numRSBlocks; i++)
            {
                for (int j = 0; j < num4; j++)
                {
                    array3[i][j] = blocks[j * numRSBlocks + i];
                }
                ReedSolomon reedSolomon = new ReedSolomon(array3[i], num2);
                reedSolomon.correct();
                num += reedSolomon.NumCorrectedErrors;
                correctionSucceeded = reedSolomon.CorrectionSucceeded;
            }
            int num5 = 0;
            for (int i = 0; i < numRSBlocks; i++)
            {
                for (int j = 0; j < num4 - num2; j++)
                {
                    array[num5++] = array3[i][j];
                }
            }
        }
        else
        {
            int num6 = dataCapacity / numRSBlocks;
            int num7 = dataCapacity / numRSBlocks + 1;
            int num8 = numRSBlocks - num3;
            int[][] array4 = new int[num8][];
            for (int k = 0; k < num8; k++)
            {
                array4[k] = new int[num6];
            }
            int[][] array5 = array4;
            int[][] array6 = new int[num3][];
            for (int l = 0; l < num3; l++)
            {
                array6[l] = new int[num7];
            }
            int[][] array7 = array6;
            for (int i = 0; i < numRSBlocks; i++)
            {
                int num9;
                ReedSolomon reedSolomon;
                if (i < num8)
                {
                    num9 = 0;
                    for (int j = 0; j < num6; j++)
                    {
                        if (j == num6 - num2)
                        {
                            num9 = num3;
                        }
                        array5[i][j] = blocks[j * numRSBlocks + i + num9];
                    }
                    reedSolomon = new ReedSolomon(array5[i], num2);
                    reedSolomon.correct();
                    num += reedSolomon.NumCorrectedErrors;
                    correctionSucceeded = reedSolomon.CorrectionSucceeded;
                    continue;
                }
                num9 = 0;
                for (int j = 0; j < num7; j++)
                {
                    if (j == num6 - num2)
                    {
                        num9 = num8;
                    }
                    array7[i - num8][j] = blocks[j * numRSBlocks + i - num9];
                }
                reedSolomon = new ReedSolomon(array7[i - num8], num2);
                reedSolomon.correct();
                num += reedSolomon.NumCorrectedErrors;
                correctionSucceeded = reedSolomon.CorrectionSucceeded;
            }
            int num5 = 0;
            for (int i = 0; i < numRSBlocks; i++)
            {
                if (i < num8)
                {
                    for (int j = 0; j < num6 - num2; j++)
                    {
                        array[num5++] = array5[i][j];
                    }
                }
                else
                {
                    for (int j = 0; j < num7 - num2; j++)
                    {
                        array[num5++] = array7[i - num8][j];
                    }
                }
            }
        }
        if (num > 0)
        {
            canvas.println(Convert.ToString(num) + " data errors corrected.");
        }
        else
        {
            canvas.println("No errors found.");
        }
        numLastCorrections = num;
        return array;
    }

    internal virtual sbyte[] getDecodedByteArray(int[] blocks, int version, int numErrorCorrectionCode)
    {
        QRCodeDataBlockReader qRCodeDataBlockReader = new QRCodeDataBlockReader(blocks, version, numErrorCorrectionCode);
        try
        {
            return qRCodeDataBlockReader.DataByte;
        }
        catch (InvalidDataBlockException ex)
        {
            throw ex;
        }
    }

    internal virtual string getDecodedString(int[] blocks, int version, int numErrorCorrectionCode)
    {
        string text = null;
        QRCodeDataBlockReader qRCodeDataBlockReader = new QRCodeDataBlockReader(blocks, version, numErrorCorrectionCode);
        try
        {
            return qRCodeDataBlockReader.DataString;
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new InvalidDataBlockException(ex.Message);
        }
    }
}