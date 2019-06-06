using AspNetCoreMini.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreMini.Hosting
{
    public interface IStartup
    {
        IServiceProvider ConfigureServices(IServiceCollection services);

        void Configure(IApplicationBuilder app);
    }
}