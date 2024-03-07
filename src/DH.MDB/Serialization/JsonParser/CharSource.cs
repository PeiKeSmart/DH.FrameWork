using System.Text;

namespace DH.Serialization;

internal class CharSource
{

    public char[] charList;
    private int index = -1;

    public String strSrc { get; set; }

    public CharSource(String src)
    {
        this.strSrc = clearComment(src);
        this.charList = this.strSrc.ToCharArray();
    }

    private String clearComment(String src)
    {
        if (src == null) return null;
        String[] arr = src.Split('\n');
        StringBuilder sb = new StringBuilder();
        foreach (String line in arr)
        {
            if (line.Trim().StartsWith("//")) continue;
            sb.Append(line);
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public char getCurrent()
    {

        return charList[index];
    }

    public void move()
    {

        if (index >= charList.Length - 1) return;

        index++;
    }

    public void back()
    {
        index--;
    }

    public void moveToText()
    {

        index++;

        char c = this.getCurrent();
        if (c == 0 || c > ' ') return;

        moveToText();
    }

    public Boolean isEnd()
    {
        return (index >= charList.Length - 1);
    }

    public int getIndex()
    {
        return this.index;
    }

}
