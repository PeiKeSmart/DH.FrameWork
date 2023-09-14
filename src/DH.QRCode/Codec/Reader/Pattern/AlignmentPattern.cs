using DH.QRCode.Codec.Util;
using DH.QRCode.ExceptionHandler;
using DH.QRCode.Geom;

namespace DH.QRCode.Codec.Reader.Pattern;

public class AlignmentPattern {
    internal const int RIGHT = 1;

    internal const int BOTTOM = 2;

    internal const int LEFT = 3;

    internal const int TOP = 4;

    internal static DebugCanvas canvas;

    internal Point[][] center;

    internal int patternDistance;

    public virtual int LogicalDistance => patternDistance;

    internal AlignmentPattern(Point[][] center, int patternDistance)
    {
        this.center = center;
        this.patternDistance = patternDistance;
    }

    public static AlignmentPattern findAlignmentPattern(bool[][] image, FinderPattern finderPattern)
    {
        Point[][] logicalCenter = getLogicalCenter(finderPattern);
        int num = logicalCenter[1][0].X - logicalCenter[0][0].X;
        Point[][] array = null;
        array = getCenter(image, finderPattern, logicalCenter);
        return new AlignmentPattern(array, num);
    }

    public virtual Point[][] getCenter()
    {
        return center;
    }

    public virtual void setCenter(Point[][] center)
    {
        this.center = center;
    }

    internal static Point[][] getCenter(bool[][] image, FinderPattern finderPattern, Point[][] logicalCenters)
    {
        int moduleSize = finderPattern.getModuleSize();
        Axis axis = new Axis(finderPattern.getAngle(), moduleSize);
        int num = logicalCenters.Length;
        Point[][] array = new Point[num][];
        for (int i = 0; i < num; i++)
        {
            array[i] = new Point[num];
        }
        axis.Origin = finderPattern.getCenter(0);
        array[0][0] = axis.translate(3, 3);
        canvas.drawCross(array[0][0], Color_Fields.BLUE);
        axis.Origin = finderPattern.getCenter(1);
        array[num - 1][0] = axis.translate(-3, 3);
        canvas.drawCross(array[num - 1][0], Color_Fields.BLUE);
        axis.Origin = finderPattern.getCenter(2);
        array[0][num - 1] = axis.translate(3, -3);
        canvas.drawCross(array[0][num - 1], Color_Fields.BLUE);
        Point p = array[0][0];
        for (int j = 0; j < num; j++)
        {
            for (int k = 0; k < num; k++)
            {
                if ((k == 0 && j == 0) || (k == 0 && j == num - 1) || (k == num - 1 && j == 0))
                {
                    continue;
                }
                Point point = null;
                if (j == 0)
                {
                    if (k > 0 && k < num - 1)
                    {
                        point = axis.translate(array[k - 1][j], logicalCenters[k][j].X - logicalCenters[k - 1][j].X, 0);
                    }
                    array[k][j] = new Point(point.X, point.Y);
                    canvas.drawCross(array[k][j], Color_Fields.RED);
                }
                else if (k == 0)
                {
                    if (j > 0 && j < num - 1)
                    {
                        point = axis.translate(array[k][j - 1], 0, logicalCenters[k][j].Y - logicalCenters[k][j - 1].Y);
                    }
                    array[k][j] = new Point(point.X, point.Y);
                    canvas.drawCross(array[k][j], Color_Fields.RED);
                }
                else
                {
                    Point point2 = axis.translate(array[k - 1][j], logicalCenters[k][j].X - logicalCenters[k - 1][j].X, 0);
                    Point point3 = axis.translate(array[k][j - 1], 0, logicalCenters[k][j].Y - logicalCenters[k][j - 1].Y);
                    array[k][j] = new Point((point2.X + point3.X) / 2, (point2.Y + point3.Y) / 2 + 1);
                }
                if (finderPattern.Version > 1)
                {
                    Point precisionCenter = getPrecisionCenter(image, array[k][j]);
                    if (array[k][j].distanceOf(precisionCenter) < 6)
                    {
                        canvas.drawCross(array[k][j], Color_Fields.RED);
                        int num2 = precisionCenter.X - array[k][j].X;
                        int num3 = precisionCenter.Y - array[k][j].Y;
                        canvas.println("Adjust AP(" + k + "," + j + ") to d(" + num2 + "," + num3 + ")");
                        array[k][j] = precisionCenter;
                    }
                }
                canvas.drawCross(array[k][j], Color_Fields.BLUE);
                canvas.drawLine(new Line(p, array[k][j]), Color_Fields.LIGHTBLUE);
                p = array[k][j];
            }
        }
        return array;
    }

    internal static Point getPrecisionCenter(bool[][] image, Point targetPoint)
    {
        int x = targetPoint.X;
        int y = targetPoint.Y;
        if (x < 0 || y < 0 || x > image.Length - 1 || y > image[0].Length - 1)
        {
            throw new AlignmentPatternNotFoundException("Alignment Pattern finder exceeded out of image");
        }
        if (!image[targetPoint.X][targetPoint.Y])
        {
            int num = 0;
            bool flag = false;
            while (!flag)
            {
                num++;
                for (int num2 = num; num2 > -num; num2--)
                {
                    for (int num3 = num; num3 > -num; num3--)
                    {
                        int num4 = targetPoint.X + num3;
                        int num5 = targetPoint.Y + num2;
                        if (num4 < 0 || num5 < 0 || num4 > image.Length - 1 || num5 > image[0].Length - 1)
                        {
                            throw new AlignmentPatternNotFoundException("Alignment Pattern finder exceeded out of image");
                        }
                        if (image[num4][num5])
                        {
                            targetPoint = new Point(targetPoint.X + num3, targetPoint.Y + num2);
                            flag = true;
                        }
                    }
                }
            }
        }
        int num6;
        int i;
        int num7 = (num6 = (i = targetPoint.X));
        int num8;
        int j;
        int num9 = (num8 = (j = targetPoint.Y));
        while (num6 >= 1 && !targetPointOnTheCorner(image, num6, num9, num6 - 1, num9))
        {
            num6--;
        }
        for (; i < image.Length - 1 && !targetPointOnTheCorner(image, i, num9, i + 1, num9); i++)
        {
        }
        while (num8 >= 1 && !targetPointOnTheCorner(image, num7, num8, num7, num8 - 1))
        {
            num8--;
        }
        for (; j < image[0].Length - 1 && !targetPointOnTheCorner(image, num7, j, num7, j + 1); j++)
        {
        }
        return new Point((num6 + i + 1) / 2, (num8 + j + 1) / 2);
    }

    internal static bool targetPointOnTheCorner(bool[][] image, int x, int y, int nx, int ny)
    {
        if (x < 0 || y < 0 || nx < 0 || ny < 0 || x > image.Length || y > image[0].Length || nx > image.Length || ny > image[0].Length)
        {
            throw new AlignmentPatternNotFoundException("Alignment Pattern Finder exceeded image edge");
        }
        return !image[x][y] && image[nx][ny];
    }

    public static Point[][] getLogicalCenter(FinderPattern finderPattern)
    {
        int version = finderPattern.Version;
        Point[][] array = new Point[1][];
        for (int i = 0; i < 1; i++)
        {
            array[i] = new Point[1];
        }
        int[] array2 = new int[1];
        array2 = LogicalSeed.getSeed(version);
        array = new Point[array2.Length][];
        for (int j = 0; j < array2.Length; j++)
        {
            array[j] = new Point[array2.Length];
        }
        for (int k = 0; k < array.Length; k++)
        {
            for (int l = 0; l < array.Length; l++)
            {
                array[l][k] = new Point(array2[l], array2[k]);
            }
        }
        return array;
    }

    static AlignmentPattern()
    {
        canvas = QRCodeDecoder.Canvas;
    }
}