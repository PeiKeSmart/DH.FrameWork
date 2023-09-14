using DH.QRCode.Codec.Util;

namespace DH.QRCode.Geom;

public class Point {
    public const int RIGHT = 1;

    public const int BOTTOM = 2;

    public const int LEFT = 4;

    public const int TOP = 8;

    internal int x;

    internal int y;

    public virtual int X
    {
        get
        {
            return x;
        }
        set
        {
            x = value;
        }
    }

    public virtual int Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
        }
    }

    public Point()
    {
        x = 0;
        y = 0;
    }

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public virtual void translate(int dx, int dy)
    {
        x += dx;
        y += dy;
    }

    public virtual void set_Renamed(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return "(" + Convert.ToString(x) + "," + Convert.ToString(y) + ")";
    }

    public static Point getCenter(Point p1, Point p2)
    {
        return new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
    }

    public bool equals(Point compare)
    {
        if (x == compare.x && y == compare.y)
        {
            return true;
        }
        return false;
    }

    public virtual int distanceOf(Point other)
    {
        int num = other.X;
        int num2 = other.Y;
        return QRCodeUtility.sqrt((x - num) * (x - num) + (y - num2) * (y - num2));
    }
}