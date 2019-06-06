using AspNetCoreMini.Http;
using System;

namespace AspNetCoreMini.Hosting
{
    internal interface ISupportsStartup
    {
        IWebHostBuilder Configure(Action<WebHostBuilderContext, IApplicationBuilder> configure);
        IWebHostBuilder UseStartup(Type startupType);
    }
}