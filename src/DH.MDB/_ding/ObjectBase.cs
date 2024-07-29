namespace DH;

public class ExtData : Dictionary<String, String>
{
    public String show
    {
        get { return this["show"]; }
        set { this["show"] = value; }
    }
    public String edit
    {
        get { return this["edit"]; }
        set { this["edit"] = value; }
    }
    public String delete
    {
        get { return this["delete"]; }
        set { this["delete"] = value; }
    }
}
