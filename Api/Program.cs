using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting.WindowsServices;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var isService = !(Debugger.IsAttached || args.Contains("--console"));
        var builder = CreateWebHostBuilder(args.Where(arg => arg != "--console").ToArray());

        var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (enviroment.Equals("Production"))
        {
            builder = builder.UseUrls($"http://*:{GetPort(args)}");
        }

        if (isService && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var pathToExe = $"{Process.GetCurrentProcess().MainModule.FileName} --port={GetPort(args)}";
            var pathToContentRoot = Path.GetDirectoryName(pathToExe);
            builder.UseContentRoot(pathToContentRoot);
        }

        try
        {
            var host = builder.Build();

            if (isService && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                host.RunAsService();
            }
            else
            {
                host.Run();

            }
        }
        catch (Exception ex)
        {
            return;
        }
    }
    public static string GetPort(string[] args)
    {
        var portsArgs = args.Where(arg => arg.IndexOf("--port") >= 0);

        if (portsArgs.Any())
        {
            var portArgs = portsArgs.First();

            return portArgs.Substring(portArgs.IndexOf("--port=") + 7);
        }

        return "8043";
    }
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>();
}