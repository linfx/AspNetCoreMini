using AspNetCoreMini.Http;
using System;

namespace AspNetCoreMini.Hosting
{
    internal class GenericWebHostServiceOptions
    {
        public Action<IApplicationBuilder> ConfigureApplication { get; set; }

        public WebHostOptions WebHostOptions { get; set; }
    }
}