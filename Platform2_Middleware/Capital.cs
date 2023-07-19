using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;



namespace Platform2.Platform2_Middleware
{
    public class Capital
    {
        private RequestDelegate request;

        public Capital(RequestDelegate request)
        {
            this.request = request;
        }

        public Capital()
        {
                
        }

        public async Task Invoke(HttpContext context) 
        { 
        
            string[] str = context.Request.Path.ToString().Split("/",StringSplitOptions.RemoveEmptyEntries);  
            if (str.Length == 2 && str[0].ToLower() == "capital")
            {
                string capital = null;
                string country = str[1];
                switch (country)
                {
                    case "uk":
                        capital = "london";
                        break;

                    case "france":
                        capital = "paris";
                        break;

                    case "monaco":
                        context.Response.Redirect($"/population/{country}");
                        return;
                  
                }

                if (capital != null)
                {
                    await context.Response.WriteAsync($"{capital} is the capital of: {country}");
                    return;
                }


            }

            if (request != null)
            {
                await request(context);
            }
        
        }

    }
}
