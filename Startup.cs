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

namespace Platform2
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //јргументы  Ч это объект HttpContext и функци€, котора€ вызываетс€, чтобы указать ASP.NET Core передать запрос
            // к следующему компоненту промежуточного программного обеспечени€ в конвейере.

            app.Use( async delegate (HttpContext context, Func<Task> task)
            {
                //очередность прохода по конвееру
                await task();
                await  context.Response.WriteAsync($" \n Status code: {context.Response.StatusCode}");             
            });
            //короткое замыкание
            app.Use(async delegate (HttpContext context, Func<Task> tsk) {

                if (context.Request.Path == "/short")
                {
                    await context.Response.WriteAsync("short");
                }
                else
                {
                      await tsk();  
                }
            
            });



            app.Use(async (context, task) =>
            {

                if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
                {
                    await context.Response.WriteAsync("Custom3 Middleware\n");
                }
                await task();
            });

            app.UseMiddleware<QueryStringMiddleware>();

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

//        //используем делегат Func
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
//        //ложим эту ссылку в метод Use дл€ регистрации компонента промежуточного сло€
//        app.Use(func);

//        //јргументы  Ч это объект HttpContext и функци€, котора€ вызываетс€, чтобы указать ASP.NET Core передать запрос
//        // к следующему компоненту промежуточного программного обеспечени€ в конвейере.
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