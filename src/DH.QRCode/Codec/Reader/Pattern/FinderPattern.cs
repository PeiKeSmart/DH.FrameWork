using DH.QRCode.Codec.Util;
using DH.QRCode.ExceptionHandler;
using DH.QRCode.Geom;

using System.Collections;

namespace DH.QRCode.Codec.Reader.Pattern;

public class FinderPattern {
    public const int UL = 0;

    public const int UR = 1;

    public const int DL = 2;

    internal static readonly int[] VersionInfoBit;

    internal static DebugCanvas canvas;

    internal Point[] center;

    internal int version;

    internal int[] sincos;

    internal int[] width;

    internal int[] moduleSize;

    public virtual int Version => version;

    public virtual int SqrtNumModules => 17 + 4 * version;

    public static FinderPattern findFinderPattern(bool[][] image)
    {
        Line[] lineAcross = findLineAcross(image);
        Line[] crossLines = findLineCross(lineAcross);
        Point[] array = null;
        try
        {
            array = getCenter(crossLines);
        }
        catch (FinderPatternNotFoundException ex)
        {
            throw ex;
        }
        int[] angle = getAngle(array);
        array = sort(array, angle);
        int[] array2 = getWidth(image, array, angle);
        int[] array3 = new int[3]
        {
                (array2[0] << QRCodeImageReader.DECIMAL_POINT) / 7,
                (array2[1] << QRCodeImageReader.DECIMAL_POINT) / 7,
                (array2[2] << QRCodeImageReader.DECIMAL_POINT) / 7
        };
        int num = calcRoughVersion(array, array2);
        if (num > 6)
        {
            try
            {
                num = calcExactVersion(array, angle, array3, image);
            }
            catch (VersionInformationException)
            {
            }
        }
        return new FinderPattern(array, num, angle, array2, array3);
    }

    internal FinderPattern(Point[] center, int version, int[] sincos, int[] width, int[] moduleSize)
    {
        this.center = center;
        this.version = version;
        this.sincos = sincos;
        this.width = width;
        this.moduleSize = moduleSize;
    }

    public virtual Point[] getCenter()
    {
        return center;
    }

    public virtual Point getCenter(int position)
    {
        if (position >= 0 && position <= 2)
        {
            return center[position];
        }
        return null;
    }

    public virtual int getWidth(int position)
    {
        return width[position];
    }

    public virtual int[] getAngle()
    {
        return sincos;
    }

    public virtual int getModuleSize()
    {
        return moduleSize[0];
    }

    public virtual int getModuleSize(int place)
    {
        return moduleSize[place];
    }

    internal static Line[] findLineAcross(bool[][] image)
    {
        int num = 0;
        int num2 = 1;
        int num3 = image.Length;
        int num4 = image[0].Length;
        Point point = new Point();
        ArrayList arrayList = ArrayList.Synchronized(new ArrayList(10));
        int[] array = new int[5];
        int num5 = 0;
        int num6 = num;
        bool flag = false;
        while (true)
        {
            bool flag2 = true;
            bool flag3 = image[point.X][point.Y];
            if (flag3 == flag)
            {
                array[num5]++;
            }
            else
            {
                if (!flag3 && checkPattern(array, num5))
                {
                    int num7;
                    int x;
                    int num8;
                    int y;
                    if (num6 == num)
                    {
                        num7 = point.X;
                        for (int i = 0; i < 5; i++)
                        {
                            num7 -= array[i];
                        }
                        x = point.X - 1;
                        num8 = (y = point.Y);
                    }
                    else
                    {
                        num7 = (x = point.X);
                        num8 = point.Y;
                        for (int i = 0; i < 5; i++)
                        {
                            num8 -= array[i];
                        }
                        y = point.Y - 1;
                    }
                    arrayList.Add(new Line(num7, num8, x, y));
                }
                num5 = (num5 + 1) % 5;
                array[num5] = 1;
                flag = !flag;
            }
            if (num6 == num)
            {
                if (point.X < num3 - 1)
                {
                    point.translate(1, 0);
                }
                else if (point.Y < num4 - 1)
                {
                    point.set_Renamed(0, point.Y + 1);
                    array = new int[5];
                }
                else
                {
                    point.set_Renamed(0, 0);
                    array = new int[5];
                    num6 = num2;
                }
            }
            else if (point.Y < num4 - 1)
            {
                point.translate(0, 1);
            }
            else
            {
                if (point.X >= num3 - 1)
                {
                    break;
                }
                point.set_Renamed(point.X + 1, 0);
                array = new int[5];
            }
        }
        Line[] array2 = new Line[arrayList.Count];
        for (int j = 0; j < array2.Length; j++)
        {
            array2[j] = (Line)arrayList[j];
        }
        canvas.drawLines(array2, Color_Fields.LIGHTGREEN);
        return array2;
    }

    internal static bool checkPattern(int[] buffer, int pointer)
    {
        int[] array = new int[5]
        {
                1,
                1,
                3,
                1,
                1
        };
        int num = 0;
        for (int i = 0; i < 5; i++)
        {
            num += buffer[i];
        }
        num <<= QRCodeImageReader.DECIMAL_POINT;
        num /= 7;
        for (int j = 0; j < 5; j++)
        {
            int num2 = num * array[j] - num / 2;
            int num3 = num * array[j] + num / 2;
            int num4 = buffer[(pointer + j + 1) % 5] << QRCodeImageReader.DECIMAL_POINT;
            if (num4 < num2 || num4 > num3)
            {
                return false;
            }
        }
        return true;
    }

    internal static Line[] findLineCross(Line[] lineAcross)
    {
        ArrayList arrayList = ArrayList.Synchronized(new ArrayList(10));
        ArrayList arrayList2 = ArrayList.Synchronized(new ArrayList(10));
        ArrayList arrayList3 = ArrayList.Synchronized(new ArrayList(10));
        for (int i = 0; i < lineAcross.Length; i++)
        {
            arrayList3.Add(lineAcross[i]);
        }
        for (int i = 0; i < arrayList3.Count - 1; i++)
        {
            arrayList2.Clear();
            arrayList2.Add(arrayList3[i]);
            for (int j = i + 1; j < arrayList3.Count; j++)
            {
                if (Line.isNeighbor((Line)arrayList2[arrayList2.Count - 1], (Line)arrayList3[j]))
                {
                    arrayList2.Add(arrayList3[j]);
                    Line line = (Line)arrayList2[arrayList2.Count - 1];
                    if (arrayList2.Count * 5 > line.Length && j == arrayList3.Count - 1)
                    {
                        arrayList.Add(arrayList2[arrayList2.Count / 2]);
                        for (int k = 0; k < arrayList2.Count; k++)
                        {
                            arrayList3.Remove(arrayList2[k]);
                        }
                    }
                }
                else
                {
                    if (!cantNeighbor((Line)arrayList2[arrayList2.Count - 1], (Line)arrayList3[j]) && j != arrayList3.Count - 1)
                    {
                        continue;
                    }
                    Line line = (Line)arrayList2[arrayList2.Count - 1];
                    if (arrayList2.Count * 6 > line.Length)
                    {
                        arrayList.Add(arrayList2[arrayList2.Count / 2]);
                        for (int k = 0; k < arrayList2.Count; k++)
                        {
                            arrayList3.Remove(arrayList2[k]);
                        }
                    }
                    break;
                }
            }
        }
        Line[] array = new Line[arrayList.Count];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = (Line)arrayList[i];
        }
        return array;
    }

    internal static bool cantNeighbor(Line line1, Line line2)
    {
        if (Line.isCross(line1, line2))
        {
            return true;
        }
        if (line1.Horizontal)
        {
            if (Math.Abs(line1.getP1().Y - line2.getP1().Y) > 1)
            {
                return true;
            }
            return false;
        }
        if (Math.Abs(line1.getP1().X - line2.getP1().X) > 1)
        {
            return true;
        }
        return false;
    }

    internal static int[] getAngle(Point[] centers)
    {
        Line[] array = new Line[3];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = new Line(centers[i], centers[(i + 1) % array.Length]);
        }
        Line longest = Line.getLongest(array);
        Point point = new Point();
        for (int i = 0; i < centers.Length; i++)
        {
            if (!longest.getP1().equals(centers[i]) && !longest.getP2().equals(centers[i]))
            {
                point = centers[i];
                break;
            }
        }
        canvas.println("originPoint is: " + point);
        Point point2 = new Point();
        point2 = (((point.Y <= longest.getP1().Y) & (point.Y <= longest.getP2().Y)) ? ((longest.getP1().X >= longest.getP2().X) ? longest.getP1() : longest.getP2()) : (((point.X >= longest.getP1().X) & (point.X >= longest.getP2().X)) ? ((longest.getP1().Y >= longest.getP2().Y) ? longest.getP1() : longest.getP2()) : (((point.Y >= longest.getP1().Y) & (point.Y >= longest.getP2().Y)) ? ((longest.getP1().X >= longest.getP2().X) ? longest.getP2() : longest.getP1()) : ((longest.getP1().Y >= longest.getP2().Y) ? longest.getP2() : longest.getP1()))));
        int length = new Line(point, point2).Length;
        return new int[2]
        {
                (point2.Y - point.Y << QRCodeImageReader.DECIMAL_POINT) / length,
                (point2.X - point.X << QRCodeImageReader.DECIMAL_POINT) / length
        };
    }

    internal static Point[] getCenter(Line[] crossLines)
    {
        ArrayList arrayList = ArrayList.Synchronized(new ArrayList(10));
        for (int i = 0; i < crossLines.Length - 1; i++)
        {
            Line line = crossLines[i];
            for (int j = i + 1; j < crossLines.Length; j++)
            {
                Line line2 = crossLines[j];
                if (Line.isCross(line, line2))
                {
                    int num = 0;
                    int num2 = 0;
                    if (line.Horizontal)
                    {
                        num = line.Center.X;
                        num2 = line2.Center.Y;
                    }
                    else
                    {
                        num = line2.Center.X;
                        num2 = line.Center.Y;
                    }
                    arrayList.Add(new Point(num, num2));
                }
            }
        }
        Point[] array = new Point[arrayList.Count];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = (Point)arrayList[i];
        }
        if (array.Length == 3)
        {
            canvas.drawPolygon(array, Color_Fields.RED);
            return array;
        }
        throw new FinderPatternNotFoundException("Invalid number of Finder Pattern detected");
    }

    internal static Point[] sort(Point[] centers, int[] angle)
    {
        Point[] array = new Point[3];
        switch (getURQuadant(angle))
        {
            case 1:
                array[1] = getPointAtSide(centers, 1, 2);
                array[2] = getPointAtSide(centers, 2, 4);
                break;
            case 2:
                array[1] = getPointAtSide(centers, 2, 4);
                array[2] = getPointAtSide(centers, 8, 4);
                break;
            case 3:
                array[1] = getPointAtSide(centers, 4, 8);
                array[2] = getPointAtSide(centers, 1, 8);
                break;
            case 4:
                array[1] = getPointAtSide(centers, 8, 1);
                array[2] = getPointAtSide(centers, 2, 1);
                break;
        }
        for (int i = 0; i < centers.Length; i++)
        {
            if (!centers[i].equals(array[1]) && !centers[i].equals(array[2]))
            {
                array[0] = centers[i];
            }
        }
        return array;
    }

    internal static int getURQuadant(int[] angle)
    {
        int num = angle[0];
        int num2 = angle[1];
        if (num >= 0 && num2 > 0)
        {
            return 1;
        }
        if (num > 0 && num2 <= 0)
        {
            return 2;
        }
        if (num <= 0 && num2 < 0)
        {
            return 3;
        }
        if (num < 0 && num2 >= 0)
        {
            return 4;
        }
        return 0;
    }

    internal static Point getPointAtSide(Point[] points, int side1, int side2)
    {
        Point point = new Point();
        int x = ((side1 != 1 && side2 != 1) ? int.MaxValue : 0);
        int y = ((side1 != 2 && side2 != 2) ? int.MaxValue : 0);
        point = new Point(x, y);
        for (int i = 0; i < points.Length; i++)
        {
            switch (side1)
            {
                case 1:
                    if (point.X < points[i].X)
                    {
                        point = points[i];
                    }
                    else
                    {
                        if (point.X != points[i].X)
                        {
                            break;
                        }
                        if (side2 == 2)
                        {
                            if (point.Y < points[i].Y)
                            {
                                point = points[i];
                            }
                        }
                        else if (point.Y > points[i].Y)
                        {
                            point = points[i];
                        }
                    }
                    break;
                case 2:
                    if (point.Y < points[i].Y)
                    {
                        point = points[i];
                    }
                    else
                    {
                        if (point.Y != points[i].Y)
                        {
                            break;
                        }
                        if (side2 == 1)
                        {
                            if (point.X < points[i].X)
                            {
                                point = points[i];
                            }
                        }
                        else if (point.X > points[i].X)
                        {
                            point = points[i];
                        }
                    }
                    break;
                case 4:
                    if (point.X > points[i].X)
                    {
                        point = points[i];
                    }
                    else
                    {
                        if (point.X != points[i].X)
                        {
                            break;
                        }
                        if (side2 == 2)
                        {
                            if (point.Y < points[i].Y)
                            {
                                point = points[i];
                            }
                        }
                        else if (point.Y > points[i].Y)
                        {
                            point = points[i];
                        }
                    }
                    break;
                case 8:
                    if (point.Y > points[i].Y)
                    {
                        point = points[i];
                    }
                    else
                    {
                        if (point.Y != points[i].Y)
                        {
                            break;
                        }
                        if (side2 == 1)
                        {
                            if (point.X < points[i].X)
                            {
                                point = points[i];
                            }
                        }
                        else if (point.X > points[i].X)
                        {
                            point = points[i];
                        }
                    }
                    break;
            }
        }
        return point;
    }

    internal static int[] getWidth(bool[][] image, Point[] centers, int[] sincos)
    {
        int[] array = new int[3];
        for (int i = 0; i < 3; i++)
        {
            bool flag = false;
            int y = centers[i].Y;
            int num;
            for (num = centers[i].X; num > 0; num--)
            {
                if (image[num][y] && !image[num - 1][y])
                {
                    if (flag)
                    {
                        break;
                    }
                    flag = true;
                }
            }
            flag = false;
            int j;
            for (j = centers[i].X; j < image.Length; j++)
            {
                if (image[j][y] && !image[j + 1][y])
                {
                    if (flag)
                    {
                        break;
                    }
                    flag = true;
                }
            }
            array[i] = j - num + 1;
        }
        return array;
    }

    internal static int calcRoughVersion(Point[] center, int[] width)
    {
        int dECIMAL_POINT = QRCodeImageReader.DECIMAL_POINT;
        int num = new Line(center[0], center[1]).Length << dECIMAL_POINT;
        int num2 = (width[0] + width[1] << dECIMAL_POINT) / 14;
        int num3 = (num / num2 - 10) / 4;
        if ((num / num2 - 10) % 4 >= 2)
        {
            num3++;
        }
        return num3;
    }

    internal static int calcExactVersion(Point[] centers, int[] angle, int[] moduleSize, bool[][] image)
    {
        bool[] array = new bool[18];
        Point[] array2 = new Point[18];
        Axis axis = new Axis(angle, moduleSize[1]);
        axis.Origin = centers[1];
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Point point = axis.translate(j - 7, i - 3);
                array[j + i * 3] = image[point.X][point.Y];
                array2[j + i * 3] = point;
            }
        }
        canvas.drawPoints(array2, Color_Fields.RED);
        int num = 0;
        try
        {
            return checkVersionInfo(array);
        }
        catch (InvalidVersionInfoException)
        {
            canvas.println("Version info error. now retry with other place one.");
            axis.Origin = centers[2];
            axis.ModulePitch = moduleSize[2];
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    Point point = axis.translate(j - 3, i - 7);
                    array[i + j * 3] = image[point.X][point.Y];
                    array2[j + i * 3] = point;
                }
            }
            canvas.drawPoints(array2, Color_Fields.RED);
            try
            {
                return checkVersionInfo(array);
            }
            catch (VersionInformationException ex)
            {
                throw ex;
            }
        }
    }

    internal static int checkVersionInfo(bool[] target)
    {
        int num = 0;
        int i;
        for (i = 0; i < VersionInfoBit.Length; i++)
        {
            num = 0;
            for (int j = 0; j < 18; j++)
            {
                if (target[j] ^ ((VersionInfoBit[i] >> j) % 2 == 1))
                {
                    num++;
                }
            }
            if (num <= 3)
            {
                break;
            }
        }
        if (num <= 3)
        {
            return 7 + i;
        }
        throw new InvalidVersionInfoException("Too many errors in version information");
    }

    static FinderPattern()
    {
        VersionInfoBit = new int[34]
        {
                31892,
                34236,
                39577,
                42195,
                48118,
                51042,
                55367,
                58893,
                63784,
                68472,
                70749,
                76311,
                79154,
                84390,
                87683,
                92361,
                96236,
                102084,
                102881,
                110507,
                110734,
                117786,
                119615,
                126325,
                127568,
                133589,
                136944,
                141498,
                145311,
                150283,
                152622,
                158308,
                161089,
                167017
        };
        canvas = QRCodeDecoder.Canvas;
    }
}