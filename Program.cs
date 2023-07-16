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
        //через делегат
        public static void Main()
        {        
            CreateHostBuilder().Build().Run();
        }
        public static IHostBuilder CreateHostBuilder()
        {
            //—реда выполнени€ .NET Core вызывает метод Main, который вызывает метод CreateHostBuilder.
            //ѕервый шаг в настройке Ч это вызов метода Host.CreateDefaultBuilder. Ётот метод отвечает
            //за настройку основных функций платформы ASP.NET Core, включа€ создание сервисов отвечает
            //за данные конфигурации и ведение журнала. Ётот метод также настраивает HTTP-сервер,с именем Kestrel,
            //который используетс€ дл€ получени€ HTTP-запросов и добавл€ет поддержку работы с Internet Information Services (IIS).
            //–езультат метода CreateDefaultBuilder передаетс€ методу ConfigureWebHostDefaults, который выбирает класс Startap
            //в качестве следующего шага в процессе запуска.
            return Host.CreateDefaultBuilder().ConfigureWebHostDefaults(delegate (IWebHostBuilder webHost)
            {
               webHost.UseStartup<Startup>();
            });
        }

        //через л€мбду
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

    }
}
