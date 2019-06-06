using AspNetCoreMini.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace AspNetCoreMini.Hosting
{
    public static class WebHostBuilderExtensions
    {
        /// <summary>
        /// Specify the startup method to be used to configure the web application.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IWebHostBuilder"/> to configure.</param>
        /// <param name="configureApp">The delegate that configures the <see cref="IApplicationBuilder"/>.</param>
        /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
        public static IWebHostBuilder Configure(this IWebHostBuilder hostBuilder, Action<IApplicationBuilder> configureApp)
        {
            return hostBuilder.Configure((_, app) => configureApp(app), configureApp.GetMethodInfo().DeclaringType.GetTypeInfo().Assembly.GetName().Name);
        }

        /// <summary>
        /// Specify the startup method to be used to configure the web application.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IWebHostBuilder"/> to configure.</param>
        /// <param name="configureApp">The delegate that configures the <see cref="IApplicationBuilder"/>.</param>
        /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
        public static IWebHostBuilder Configure(this IWebHostBuilder hostBuilder, Action<WebHostBuilderContext, IApplicationBuilder> configureApp)
        {
            return hostBuilder.Configure(configureApp, configureApp.GetMethodInfo().DeclaringType.GetTypeInfo().Assembly.GetName().Name);
        }

        private static IWebHostBuilder Configure(this IWebHostBuilder hostBuilder, Action<WebHostBuilderContext, IApplicationBuilder> configureApp, string startupAssemblyName)
        {
            if (configureApp == null)
            {
                throw new ArgumentNullException(nameof(configureApp));
            }

            //hostBuilder.UseSetting(WebHostDefaults.ApplicationKey, startupAssemblyName);

            // Light up the ISupportsStartup implementation
            if (hostBuilder is ISupportsStartup supportsStartup)
            {
                return supportsStartup.Configure(configureApp);
            }

            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IStartup>(sp =>
                {
                    return new DelegateStartup(sp.GetRequiredService<IServiceProviderFactory<IServiceCollection>>(), app => configureApp(context, app));
                });
            });
        }
    }
}
