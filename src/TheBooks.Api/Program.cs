using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TheBooks.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddCommandLine(args).Build();

            var host = new WebHostBuilder()
            .UseKestrel(x => { x.AddServerHeader = false; })
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseConfiguration(config)
            .UseStartup<Startup>()
            .Build();
 
            host.Run();
        }
    }
}