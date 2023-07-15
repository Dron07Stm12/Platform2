using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Platform2
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    CreateHostBuilder(args).Build().Run();
        //}

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });



        public static void Main()
        {
            //Creat().Build().Start();
            Creat().Build().Run();

        }

        public static IHostBuilder Creat() {
            return Host.CreateDefaultBuilder().ConfigureWebHostDefaults(delegate (IWebHostBuilder webHost) {
            webHost.UseStartup<Startup>();
        }); }



        
    }
}
