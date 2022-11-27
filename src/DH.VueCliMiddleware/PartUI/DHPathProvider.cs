namespace VueCliMiddleware.PartUI;

public class DHPathProvider
{

    public static string DHVueDir()
    {
        var dhVueDir = Path.Combine(Directory.GetCurrentDirectory(), "DHVueUI");

        return dhVueDir;
    }

}
