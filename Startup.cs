using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Platform2.Platform2_Middleware;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Platform2
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
           

            services.Configure(delegate (MessageOptions message) {
                message.CityName = "Stachanov";
                message.CountryName = "Donbass";
                message.Pipell = 10;
            });

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env/*, IOptions<MessageOptions> options*/)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<Population>();
            app.UseMiddleware<Capital>();
            app.UseRouting();

            app.UseEndpoints(delegate (IEndpointRouteBuilder endpoint)
            {
                endpoint.MapGet("/", async delegate (HttpContext context)
                {
                    await context.Response.WriteAsync("Hello Dron");
                });
            });
        }
    }
}


//public class Startup
//{
//    public void ConfigureServices(IServiceCollection services)
//    {
//    }
//    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//    {
//        if (env.IsDevelopment())
//        {
//            app.UseDeveloperExceptionPage();
//        }

//        //���������� ������� Func
//        Func<HttpContext, Func<Task>, Task> func = async delegate (HttpContext context, Func<Task> task)
//        {

//            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
//            {
//                // Task tsk0 = context.Response.WriteAsync("Custom Middleware\n");
//                //  return tsk0;
//                await context.Response.WriteAsync("Custom Middleware\n");
//            }
//            // Task tsk = task();
//            //  return tsk;
//            await task();

//        };
//        //����� ��� ������ � ����� Use ��� ����������� ���������� �������������� ����
//        app.Use(func);

//        //���������  � ��� ������ HttpContext � �������, ������� ����������, ����� ������� ASP.NET Core �������� ������
//        // � ���������� ���������� �������������� ������������ ����������� � ���������.
//        app.Use(async delegate (HttpContext context, Func<Task> task)
//        {
//            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
//            {
//                await context.Response.WriteAsync("Custom2 Middleware\n");

//            }

//            await task();

//        });

//        app.Use(async (context, task) =>
//        {

//            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
//            {
//                await context.Response.WriteAsync("Custom3 Middleware\n");
//            }
//            await task();
//        });

//        app.UseMiddleware<QueryStringMiddleware>();

//        app.UseRouting();


//        app.UseEndpoints(delegate (IEndpointRouteBuilder endpoint)
//        {
//            endpoint.MapGet("/", async delegate (HttpContext context)
//            {
//                await context.Response.WriteAsync("Hello Dron");
//            });
//        });

//        //app.UseEndpoints(delegate (IEndpointRouteBuilder endpoint) { endpoint.MapGet("/connection", delegate (HttpContext context) {

//        //    return context.Response.WriteAsync($"{context.Request.ContentLength ?? 3}," +
//        //        $"{context.Request.IsHttps}, {context.Request.Path}," +
//        //        $"{context.Request.Query["con"].Count}," +
//        //        $"{context.Response.StatusCode}," +
//        //        $"{context.Response.ContentType = "string"}," +
//        //        $"{context.Response.Headers["responce"].Count}");

//        //}); });

//    }
//}






//public class Startup
//{
//    public void ConfigureServices(IServiceCollection services)
//    {
//    }
//    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//    {
//        if (env.IsDevelopment())
//        {
//            app.UseDeveloperExceptionPage();
//        }

//        //���������  � ��� ������ HttpContext � �������, ������� ����������, ����� ������� ASP.NET Core �������� ������
//        // � ���������� ���������� �������������� ������������ ����������� � ���������.

//        app.Use(async delegate (HttpContext context, Func<Task> task)
//        {
//            //����������� ������� �� ��������
//            await task();
//            await context.Response.WriteAsync($" \n Status code: {context.Response.StatusCode}");
//        });
//        //�������� ���������
//        app.Use(async delegate (HttpContext context, Func<Task> tsk) {

//            if (context.Request.Path == "/short")
//            {
//                await context.Response.WriteAsync("short");
//            }
//            else
//            {
//                await tsk();
//            }

//        });



//        app.Use(async (context, task) =>
//        {

//            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
//            {
//                await context.Response.WriteAsync("Custom3 Middleware\n");
//            }
//            await task();
//        });

//        app.UseMiddleware<QueryStringMiddleware>();

//        app.UseRouting();


//        app.UseEndpoints(delegate (IEndpointRouteBuilder endpoint)
//        {
//            endpoint.MapGet("/", async delegate (HttpContext context)
//            {
//                await context.Response.WriteAsync("Hello Dron");
//            });
//        });
//    }
//}



//����� � ��������
//app.Map("/branch", delegate (IApplicationBuilder branch)
//{

//    branch.UseMiddleware<QueryStringMiddleware>();



//    branch.Use(async delegate (HttpContext context, Func<Task> func)
//    {
//        await context.Response.WriteAsync("\n branch Middlware");
//        await func();
//    });

//    branch.Run(new QueryStringMiddleware().Invoke);



//    branch.Run(delegate (HttpContext context) { return context.Response.WriteAsync("\n run"); });

//    branch.Use(async delegate (HttpContext context, Func<Task> func) {
//        await context.Response.WriteAsync("\n new Use");
//    });

//});






//    public class Startup
//    {
//        public void ConfigureServices(IServiceCollection services)
//        {


//            services.Configure(delegate (MessageOptions message) {
//                message.CityName = "Stachanov";
//                message.CountryName = "Donbass";
//                message.Pipell = 10;
//            });

//        }
//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<MessageOptions> options)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }

//            app.Use(async delegate (HttpContext context, Func<Task> func)
//            {

//                if (context.Request.Path == "/location")
//                {
//                    MessageOptions message = options.Value;
//                    await context.Response.WriteAsync($"{message.CountryName}, \n {message.CityName}, \n {message.Pipell}");
//                }

//                else
//                {
//                    await func();
//                }

//            });

//            app.UseMiddleware<LocationMiddleware>();
//            app.UseRouting();

//            app.UseEndpoints(delegate (IEndpointRouteBuilder endpoint)
//            {
//                endpoint.MapGet("/", async delegate (HttpContext context)
//                {
//                    await context.Response.WriteAsync("Hello Dron");
//                });
//            });
//        }
//    }
