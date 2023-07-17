using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;



namespace Platform2.Platform2_Middleware
{
    public class QueryStringMiddleware
    {
        private RequestDelegate request;

        public QueryStringMiddleware(RequestDelegate next)
        {
                 request = next;
        }

        public  async Task Invoke(HttpContext context) {

            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
            {
                await context.Response.WriteAsync("Class-based Middleware\n");
            }
        
           await request(context);  
        }
    }
}
