using XCode.Membership;

namespace DH.Entity;

public class LogE : Log {
    public static String GetRoutName(String Remark)
    {
        if (!Remark.Contains("GET") && !Remark.Contains("POST")) return Remark;

        Remark = Remark.Trim(' ');

        var remarkSplit = Remark.Split(' ');
        if (remarkSplit.Length != 3) return Remark;

        var Url = remarkSplit[1].Trim(' ');
        var model = SystemRout.FindByUrl(Url);
        if (model == null) return Remark;
        return LocaleStringResource.GetResource(model.Name) + " " + Remark;
    }
}