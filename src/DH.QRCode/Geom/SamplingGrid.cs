namespace DH.QRCode.Geom;

public class SamplingGrid {
    private class AreaGrid {
        private SamplingGrid enclosingInstance;

        private Line[] xLine;

        private Line[] yLine;

        public virtual int Width => xLine.Length;

        public virtual int Height => yLine.Length;

        public virtual Line[] XLines => xLine;

        public virtual Line[] YLines => yLine;

        public SamplingGrid Enclosing_Instance => enclosingInstance;

        private void InitBlock(SamplingGrid enclosingInstance)
        {
            this.enclosingInstance = enclosingInstance;
        }

        public AreaGrid(SamplingGrid enclosingInstance, int width, int height)
        {
            InitBlock(enclosingInstance);
            xLine = new Line[width];
            yLine = new Line[height];
        }

        public virtual Line getXLine(int x)
        {
            return xLine[x];
        }

        public virtual Line getYLine(int y)
        {
            return yLine[y];
        }

        public virtual void setXLine(int x, Line line)
        {
            xLine[x] = line;
        }

        public virtual void setYLine(int y, Line line)
        {
            yLine[y] = line;
        }
    }

    private AreaGrid[][] grid;

    public virtual int TotalWidth
    {
        get
        {
            int num = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                num += grid[i][0].Width;
                if (i > 0)
                {
                    num--;
                }
            }
            return num;
        }
    }

    public virtual int TotalHeight
    {
        get
        {
            int num = 0;
            for (int i = 0; i < grid[0].Length; i++)
            {
                num += grid[0][i].Height;
                if (i > 0)
                {
                    num--;
                }
            }
            return num;
        }
    }

    public SamplingGrid(int sqrtNumArea)
    {
        grid = new AreaGrid[sqrtNumArea][];
        for (int i = 0; i < sqrtNumArea; i++)
        {
            grid[i] = new AreaGrid[sqrtNumArea];
        }
    }

    public virtual void initGrid(int ax, int ay, int width, int height)
    {
        grid[ax][ay] = new AreaGrid(this, width, height);
    }

    public virtual void setXLine(int ax, int ay, int x, Line line)
    {
        grid[ax][ay].setXLine(x, line);
    }

    public virtual void setYLine(int ax, int ay, int y, Line line)
    {
        grid[ax][ay].setYLine(y, line);
    }

    public virtual Line getXLine(int ax, int ay, int x)
    {
        return grid[ax][ay].getXLine(x);
    }

    public virtual Line getYLine(int ax, int ay, int y)
    {
        return grid[ax][ay].getYLine(y);
    }

    public virtual Line[] getXLines(int ax, int ay)
    {
        return grid[ax][ay].XLines;
    }

    public virtual Line[] getYLines(int ax, int ay)
    {
        return grid[ax][ay].YLines;
    }

    public virtual int getWidth()
    {
        return grid[0].Length;
    }

    public virtual int getHeight()
    {
        return grid.Length;
    }

    public virtual int getWidth(int ax, int ay)
    {
        return grid[ax][ay].Width;
    }

    public virtual int getHeight(int ax, int ay)
    {
        return grid[ax][ay].Height;
    }

    public virtual int getX(int ax, int x)
    {
        int num = x;
        for (int i = 0; i < ax; i++)
        {
            num += grid[i][0].Width - 1;
        }
        return num;
    }

    public virtual int getY(int ay, int y)
    {
        int num = y;
        for (int i = 0; i < ay; i++)
        {
            num += grid[0][i].Height - 1;
        }
        return num;
    }

    public virtual void adjust(Point adjust)
    {
        int x = adjust.X;
        int y = adjust.Y;
        for (int i = 0; i < grid[0].Length; i++)
        {
            for (int j = 0; j < grid.Length; j++)
            {
                for (int k = 0; k < grid[j][i].XLines.Length; k++)
                {
                    grid[j][i].XLines[k].translate(x, y);
                }
                for (int l = 0; l < grid[j][i].YLines.Length; l++)
                {
                    grid[j][i].YLines[l].translate(x, y);
                }
            }
        }
    }
}