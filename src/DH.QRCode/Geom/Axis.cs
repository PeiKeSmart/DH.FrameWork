using DH.QRCode.Codec.Reader;

namespace DH.QRCode.Geom;

public class Axis {
    internal int sin;

    internal int cos;

    internal int modulePitch;

    internal Point origin;

    public virtual Point Origin
    {
        set
        {
            origin = value;
        }
    }

    public virtual int ModulePitch
    {
        set
        {
            modulePitch = value;
        }
    }

    public Axis(int[] angle, int modulePitch)
    {
        sin = angle[0];
        cos = angle[1];
        this.modulePitch = modulePitch;
        origin = new Point();
    }

    public virtual Point translate(Point offset)
    {
        int x = offset.X;
        int y = offset.Y;
        return translate(x, y);
    }

    public virtual Point translate(Point origin, Point offset)
    {
        Origin = origin;
        int x = offset.X;
        int y = offset.Y;
        return translate(x, y);
    }

    public virtual Point translate(Point origin, int moveX, int moveY)
    {
        Origin = origin;
        return translate(moveX, moveY);
    }

    public virtual Point translate(Point origin, int modulePitch, int moveX, int moveY)
    {
        Origin = origin;
        this.modulePitch = modulePitch;
        return translate(moveX, moveY);
    }

    public virtual Point translate(int moveX, int moveY)
    {
        long num = QRCodeImageReader.DECIMAL_POINT;
        Point point = new Point();
        int num2 = ((moveX != 0) ? (modulePitch * moveX >> (int)num) : 0);
        int num3 = ((moveY != 0) ? (modulePitch * moveY >> (int)num) : 0);
        point.translate(num2 * cos - num3 * sin >> (int)num, num2 * sin + num3 * cos >> (int)num);
        point.translate(origin.X, origin.Y);
        return point;
    }
}