using Microsoft.AspNetCore.Hosting;

using NewLife.Log;

namespace CoolCatPluginDemo;

public class Program
{
    public static void Main(string[] args)
    {
        XTrace.UseConsole();
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
