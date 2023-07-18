using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;



namespace Platform2.Platform2_Middleware
{
    public class QueryStringMiddleware
    {
        private RequestDelegate request;
        
        public QueryStringMiddleware(RequestDelegate request)
        {
                 this.request = request;
                 
        }

        public QueryStringMiddleware()
        {
                
        }
        public  async Task Invoke(HttpContext context) {

            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
            {
                await context.Response.WriteAsync($" Class-based Middleware\t");
            }

            if (request != null)
            {
                await request(context);
            }
               
        }
    }


    public class LocationMiddleware 
    {
        private RequestDelegate next;
        private MessageOptions message;

        public LocationMiddleware(RequestDelegate request, IOptions<MessageOptions> options)
        {
            next = request;
            message = options.Value;
        }

        public async Task Invoke(HttpContext context) 
        {
            if (context.Request.Path == "/location2")
            {
                await context.Response.WriteAsync($"IOptions<MessageOptions> options:" +
                    $" {message.CountryName},\t{message.CityName},\t {message.Pipell}");
            }
            else
            {
                await next(context);    
            }
        
        }
     
    }



}
