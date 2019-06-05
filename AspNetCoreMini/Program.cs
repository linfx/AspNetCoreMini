using AspNetCoreMini.Hosting;
using AspNetCoreMini.Hosting.Extensions;
using AspNetCoreMini.Http;
using AspNetCoreMini.Servers;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHost(builder =>
                {
                    builder
                    .UseHttpListenerServer()
                    .Configure(app => app
                        .Use(FooMiddleware)
                        .Use(BarMiddleware));
                })
                .Build();

            await host.RunAsync();
        }

        //public static void Main(string[] args)
        //{
        //    CreateHostBuilder(args).Build().Run();
        //}

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            //webBuilder.UseStartup<Startup>();
        //        });

        public static RequestDelegate FooMiddleware(RequestDelegate next) => async context =>
        {
            //await context.Response.WriteAsync("Foo=>");
            await next(context);
        };

        public static RequestDelegate BarMiddleware(RequestDelegate next) => async context =>
        {
            //await context.Response.WriteAsync("Bar=>");
            await next(context);
        };
    }
}
