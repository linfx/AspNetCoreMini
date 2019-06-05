using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMini.Hosting.Extensions
{
    public static class HostingHostBuilderExtensions
    {
        //public static IHostBuilder UseDefaultServiceProvider(this IHostBuilder hostBuilder, Action<ServiceProviderOptions> configure)
        //{
        //    return hostBuilder.UseServiceProviderFactory(context =>
        //    {
        //        var options = new ServiceProviderOptions();
        //        configure(options);
        //        return new DefaultServiceProviderFactory(options);
        //    });
        //}

        ///// <summary>
        ///// Adds services to the container. This can be called multiple times and the results will be additive.
        ///// </summary>
        ///// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        ///// <param name="configureDelegate"></param>
        ///// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        //public static IHostBuilder ConfigureServices(this IHostBuilder hostBuilder, Action<IServiceCollection> configureDelegate)
        //{
        //    return hostBuilder.ConfigureServices((context, collection) => configureDelegate(collection));
        //}
    }
}
