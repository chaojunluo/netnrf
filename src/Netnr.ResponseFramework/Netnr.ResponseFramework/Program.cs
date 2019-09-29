using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Netnr.ResponseFramework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        //dotnet Netnr.ResponseFramework.dll "http://*:59"

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseUrls(args)
            .UseStartup<Startup>();
    }
}
