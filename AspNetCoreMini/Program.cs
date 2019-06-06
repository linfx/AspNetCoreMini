using AspNetCoreMini.Extensions.Hosting;
using AspNetCoreMini.Hosting;
using AspNetCoreMini.Http;
using AspNetCoreMini.Servers;
using System.Threading.Tasks;

namespace AspNetCoreMini
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureWebHost(builder =>
                {
                    builder.UseHttpListenerServer()
                    .Configure(app =>
                    {
                        app.Run(async (context) =>
                        {
                            await context.Response.WriteAsync("Hello World!");
                        });
                    });
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
    }
}
