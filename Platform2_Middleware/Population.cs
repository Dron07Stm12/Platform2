using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Platform2.Platform2_Middleware
{
    public class Population
    {
        private RequestDelegate request;

        public Population(RequestDelegate request)
        {
            this.request = request;   
        }

        public Population()
        {
                
        }

        public async Task Invoke(HttpContext context)
        {

            //while (context.Request.Path == "/") { }

            string[] parts = context.Request.Path.ToString().Split("/",StringSplitOptions.RemoveEmptyEntries);
            //string[] parts2 = new string[parts.Length - 1];
            //int j = 0;

            //for (int i = 0; i < parts.Length; i++)
            //{
            //    if (parts[i] != null && parts[i] != "" && parts[i] != " ")
            //    {
            //        parts2[j] = parts[i];
            //        j++;
            //    }
            //}

            if (parts.Length == 2 && parts[0].ToLower() == "population") 
            {
                    string city = parts[1].ToLower();                        
                    int? pop = null;
                    switch (city.ToLower())
                    {
                        case "london":
                            pop = 80000000;
                            break;

                        case "paris":
                            pop = 40000000;
                            break;

                        case "monaco":
                            pop = 100000;
                            break;
                    }

                    if (pop.HasValue)
                    {
                        await context.Response.WriteAsync($"City: {city}, Population: {pop}");
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
