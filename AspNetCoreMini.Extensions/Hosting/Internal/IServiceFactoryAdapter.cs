using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreMini.Extensions.Hosting.Internal
{
    internal interface IServiceFactoryAdapter
    {
        object CreateBuilder(IServiceCollection services);

        IServiceProvider CreateServiceProvider(object containerBuilder);
    }
}
