namespace DH.VirtualFileSystem;

public class DHAspNetCoreContentOptions
{
    public List<string> AllowedExtraWebContentFolders { get; }
    public List<string> AllowedExtraWebContentFileExtensions { get; }

    public DHAspNetCoreContentOptions()
    {
        AllowedExtraWebContentFolders = new List<string>
            {
                "/Pages",
                "/Views",
                "/Themes"
            };

        AllowedExtraWebContentFileExtensions = new List<string>
            {
                ".js",
                ".css",
                ".png",
                ".jpg",
                ".jpeg",
                ".woff",
                ".woff2",
                ".tff",
                ".otf"
            };
    }
}