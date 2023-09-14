using DH.QRCode.Codec.Util;

namespace DH.QRCode.Geom;

public class Line {
    internal int x1;

    internal int y1;

    internal int x2;

    internal int y2;

    public virtual bool Horizontal
    {
        get
        {
            if (y1 == y2)
            {
                return true;
            }
            return false;
        }
    }

    public virtual bool Vertical
    {
        get
        {
            if (x1 == x2)
            {
                return true;
            }
            return false;
        }
    }

    public virtual Point Center
    {
        get
        {
            int x = (x1 + x2) / 2;
            int y = (y1 + y2) / 2;
            return new Point(x, y);
        }
    }

    public virtual int Length
    {
        get
        {
            int num = Math.Abs(x2 - x1);
            int num2 = Math.Abs(y2 - y1);
            return QRCodeUtility.sqrt(num * num + num2 * num2);
        }
    }

    public Line()
    {
        x1 = (y1 = (x2 = (y2 = 0)));
    }

    public Line(int x1, int y1, int x2, int y2)
    {
        this.x1 = x1;
        this.y1 = y1;
        this.x2 = x2;
        this.y2 = y2;
    }

    public Line(Point p1, Point p2)
    {
        x1 = p1.X;
        y1 = p1.Y;
        x2 = p2.X;
        y2 = p2.Y;
    }

    public virtual Point getP1()
    {
        return new Point(x1, y1);
    }

    public virtual Point getP2()
    {
        return new Point(x2, y2);
    }

    public virtual void setLine(int x1, int y1, int x2, int y2)
    {
        this.x1 = x1;
        this.y1 = y1;
        this.x2 = x2;
        this.y2 = y2;
    }

    public virtual void setP1(Point p1)
    {
        x1 = p1.X;
        y1 = p1.Y;
    }

    public virtual void setP1(int x1, int y1)
    {
        this.x1 = x1;
        this.y1 = y1;
    }

    public virtual void setP2(Point p2)
    {
        x2 = p2.X;
        y2 = p2.Y;
    }

    public virtual void setP2(int x2, int y2)
    {
        this.x2 = x2;
        this.y2 = y2;
    }

    public virtual void translate(int dx, int dy)
    {
        x1 += dx;
        y1 += dy;
        x2 += dx;
        y2 += dy;
    }

    public static bool isNeighbor(Line line1, Line line2)
    {
        if (Math.Abs(line1.getP1().X - line2.getP1().X) < 2 && Math.Abs(line1.getP1().Y - line2.getP1().Y) < 2 && Math.Abs(line1.getP2().X - line2.getP2().X) < 2 && Math.Abs(line1.getP2().Y - line2.getP2().Y) < 2)
        {
            return true;
        }
        return false;
    }

    public static bool isCross(Line line1, Line line2)
    {
        if (line1.Horizontal && line2.Vertical)
        {
            if (line1.getP1().Y > line2.getP1().Y && line1.getP1().Y < line2.getP2().Y && line2.getP1().X > line1.getP1().X && line2.getP1().X < line1.getP2().X)
            {
                return true;
            }
        }
        else if (line1.Vertical && line2.Horizontal && line1.getP1().X > line2.getP1().X && line1.getP1().X < line2.getP2().X && line2.getP1().Y > line1.getP1().Y && line2.getP1().Y < line1.getP2().Y)
        {
            return true;
        }
        return false;
    }

    public static Line getLongest(Line[] lines)
    {
        Line line = new Line();
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Length > line.Length)
            {
                line = lines[i];
            }
        }
        return line;
    }

    public override string ToString()
    {
        return "(" + Convert.ToString(x1) + "," + Convert.ToString(y1) + ")-(" + Convert.ToString(x2) + "," + Convert.ToString(y2) + ")";
    }
}