using System;

namespace AspNetCoreMini.Http
{
    internal interface IServiceProvidersFeature
    {
        IServiceProvider RequestServices { get; set; }
    }
}