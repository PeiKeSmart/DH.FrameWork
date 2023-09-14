﻿using DH.QRCode.Geom;

namespace DH.QRCode.Codec.Util;

public class ConsoleCanvas : DebugCanvas {
    public void println(string str)
    {
        Console.WriteLine(str);
    }

    public void drawPoint(Point point, int color)
    {
    }

    public void drawCross(Point point, int color)
    {
    }

    public void drawPoints(Point[] points, int color)
    {
    }

    public void drawLine(Line line, int color)
    {
    }

    public void drawLines(Line[] lines, int color)
    {
    }

    public void drawPolygon(Point[] points, int color)
    {
    }

    public void drawMatrix(bool[][] matrix)
    {
    }
}