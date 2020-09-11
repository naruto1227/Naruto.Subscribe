using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace test.api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(opeion =>
                    {
                        opeion.ListenAnyIP(new Random().Next(5000, 6000));
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
