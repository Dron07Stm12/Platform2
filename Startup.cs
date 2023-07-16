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

            //используем делегат Func
            Func<HttpContext, Func<Task>,Task> func = delegate (HttpContext context,Func<Task> task) {

                if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true" )
                {
                    Task tsk0 = context.Response.WriteAsync("Custom Middleware\n");
                    return tsk0;
                   // return context.Response.WriteAsync("Custom Middleware\n");
                }
                Task tsk = task();
                return tsk;

            };
            //ложим эту ссылку в метод Use дл€ регистрации компонента промежуточного сло€
            app.Use(func);
            //јргументы  Ч это объект HttpContext и функци€, котора€ вызываетс€, чтобы указать ASP.NET Core передать запрос
            //к следующему компоненту промежуточного программного обеспечени€ в конвейере.
            app.Use(delegate (HttpContext context, Func<Task> task) {

                if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom2"] == "true")
                {
                    return context.Response.WriteAsync("Custom2 Middleware\n");
                }
                return task.Invoke();           
            });

            app.Use(async(context,task) => {

                if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom3"] == "true")
                {
                    await context.Response.WriteAsync("Custom3 Middleware\n");
                }
                await task();
            });


            app.UseRouting();

            app.UseEndpoints(delegate (IEndpointRouteBuilder endpoint) {  endpoint.MapGet("/", delegate (HttpContext context) {
                return context.Response.WriteAsync("Hello Dron"); }); 
            });
           
        }
    }
}
