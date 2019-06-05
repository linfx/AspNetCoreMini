using AspNetCoreMini.Hosting.Extensions;
using AspNetCoreMini.Http;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            //if (hostBuilder is ISupportsStartup supportsStartup)
            //{
            //    return supportsStartup.Configure(configureApp);
            //}

            return hostBuilder.ConfigureServices((context, services) =>
            {
                //services.AddSingleton<IStartup>(sp =>
                //{
                //    return new DelegateStartup(sp.GetRequiredService<IServiceProviderFactory<IServiceCollection>>(), (app => configureApp(context, app)));
                //});
            });
        }

        ////public static IWebHostBuilder UseHttpListener(this IWebHostBuilder builder, params string[] urls)
        ////{
        ////    //=> builder.UseServer(new HttpListenerServer(urls));
        ////    return builder;
        ////}

        ////public static Task WriteAsync(this HttpResponse response, string contents)
        ////{
        ////    var buffer = Encoding.UTF8.GetBytes(contents);
        ////    return response.Body.WriteAsync(buffer, 0, buffer.Length);
        ////}

        ///// <summary>
        ///// Initializes a new instance of the <see cref="IWebHostBuilder"/> class with pre-configured defaults.
        ///// </summary>
        ///// <remarks>
        /////   The following defaults are applied to the <see cref="IWebHostBuilder"/>:
        /////     use Kestrel as the web server and configure it using the application's configuration providers,
        /////     adds the HostFiltering middleware,
        /////     adds the ForwardedHeaders middleware if ASPNETCORE_FORWARDEDHEADERS_ENABLED=true,
        /////     and enable IIS integration.
        ///// </remarks>
        ///// <param name="builder">The <see cref="IHostBuilder" /> instance to configure</param>
        ///// <param name="configure">The configure callback</param>
        ///// <returns></returns>
        //public static IHostBuilder ConfigureWebHostDefaults(this IHostBuilder builder, Action<IWebHostBuilder> configure)
        //{
        //    return builder.ConfigureWebHost(webHostBuilder =>
        //    {
        //        //WebHost.ConfigureWebDefaults(webHostBuilder);
        //        ConfigureWebDefaults(webHostBuilder);
        //        configure(webHostBuilder);
        //    });
        //}

        //public static IHostBuilder ConfigureWebHost(this IHostBuilder builder, Action<IWebHostBuilder> configure)
        //{
        //    //var webhostBuilder = new GenericWebHostBuilder(builder);
        //    //configure(webhostBuilder);

        //    return builder;
        //}

        //internal static void ConfigureWebDefaults(IWebHostBuilder builder)
        //{
        //    builder.UseHttpListener();
        //}
    }
}
