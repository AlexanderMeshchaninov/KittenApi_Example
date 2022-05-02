using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureAppConfiguration((ctx, config) =>
                        {
                            config.AddEnvironmentVariables();
                        })
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .ConfigureLogging(builder => builder.AddJsonConsole())
                        .UseStartup<Startup>();
                });
        }
    }
}
