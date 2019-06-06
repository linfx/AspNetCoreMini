using AspNetCoreMini.Extensions.Hosting;
using System;

namespace AspNetCoreMini.Hosting
{
    internal static class GenericHostWebHostBuilderExtensions
    {
        public static IHostBuilder ConfigureWebHost(this IHostBuilder builder, Action<IWebHostBuilder> configure)
        {
            var webhostBuilder = new GenericWebHostBuilder(builder);
            configure(webhostBuilder);
            return builder;
        }
    }
}
