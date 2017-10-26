using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using Microsoft.Extensions.Primitives;


namespace calcAspServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            double num1 = 0, 
                   num2 = 0, 
                   res = 0;
            string opr = "", response = "";

            app.UseCors(builder => builder.AllowAnyOrigin());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                response = string.Empty;
                IEnumerable<KeyValuePair<string, StringValues>> args;

                switch (context.Request.Method)
                {
                    case "GET":
                        args = context.Request.Query;
                        break;
                    case "POST":
                        args = context.Request.Form;
                        break;
                    default:
                        args = new NameValueCollection() as IEnumerable<KeyValuePair<string, StringValues>>;
                        response = "Not supported request-method";
                        break;
                }

                bool isQueryParams = args.Count() == 3 &&
                                     args.SingleOrDefault(p => p.Key == "num1").Value[0] != null &&
                                     args.SingleOrDefault(p => p.Key == "num2").Value[0] != null &&
                                     args.SingleOrDefault(p => p.Key == "opr").Value[0] != null;

                if (isQueryParams)
                {
                    opr = args.Single(p => p.Key == "opr").Value;
                    
                    string[] operations = new string[] { "+", "plus", "-", "/", "*" };
                    if (double.TryParse(args.Single(p => p.Key == "num1").Value, out num1) &&
                        double.TryParse(args.Single(p => p.Key == "num2").Value, out num2) &&
                        operations.Contains(opr))
                    {
                        switch (opr)
                        {
                            case "+":
                            case "plus":
                                res = num1 + num2;
                                break;
                            case "-":
                                res = num1 - num2;
                                break;
                            case "/":
                                res = num1 / num2;
                                break;
                            case "*":
                                res = num1 * num2;
                                break;
                        }
                        response = res.ToString();
                    }
                    else
                    {
                        if (!double.TryParse(args.Single(p => p.Key == "num1").Value, out num1))
                        { response += "Set valid number1; "; }
                        if (!double.TryParse(args.Single(p => p.Key == "num2").Value, out num2))
                        { response += "Set valid number2; "; }
                        if (!operations.Contains(opr)) { response += "Set valid operation ( +, -, *, / )"; }
                    }
                }

                await next.Invoke();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(response);
            });
        }
    }
}
