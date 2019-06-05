using AspNetCoreMini.Hosting;
using AspNetCoreMini.Hosting.Server.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreMini.Servers
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseHttpListenerServer(this IWebHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IServer, HttpListenerServer>();
            });
        }
    }
}
