using System;
using System.Diagnostics;
using System.Reflection;
using AspNetCoreMini.Hosting.Extensions;
using AspNetCoreMini.Http;
using AspNetCoreMini.Http.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AspNetCoreMini.Hosting
{
    internal class GenericWebHostBuilder : IWebHostBuilder
    {
        private IHostBuilder _builder;
        private readonly IConfiguration _config;

        public GenericWebHostBuilder(IHostBuilder builder)
        {
            _builder = builder;

            _config = new ConfigurationBuilder()
                 .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                 .Build();

            _builder.ConfigureServices((context, services) =>
            {
                //services.AddHostedService<GenericWebHostService>();
                services.TryAddEnumerable(ServiceDescriptor.Singleton<IHostedService, GenericWebHostService>());

                services.TryAddSingleton<IHttpContextFactory, DefaultHttpContextFactory>();
                //services.TryAddScoped<IMiddlewareFactory, MiddlewareFactory>();
                //services.TryAddSingleton<IApplicationBuilderFactory, ApplicationBuilderFactory>();
            });
        }

        public IWebHost Build()
        {
            throw new NotSupportedException($"Building this implementation of {nameof(IWebHostBuilder)} is not supported.");
        }

        public IWebHostBuilder ConfigureServices(Action<IServiceCollection> configureServices)
        {
            return ConfigureServices((context, services) => configureServices(services));
        }

        public IWebHostBuilder ConfigureServices(Action<WebHostBuilderContext, IServiceCollection> configureServices)
        {
            _builder.ConfigureServices((context, builder) =>
            {
                var webhostBuilderContext = GetWebHostBuilderContext(context);
                configureServices(webhostBuilderContext, builder);
            });

            return this;
        }

        private WebHostBuilderContext GetWebHostBuilderContext(HostBuilderContext context)
        {
            if (!context.Properties.TryGetValue(typeof(WebHostBuilderContext), out var contextVal))
            {
                var options = new WebHostOptions(context.Configuration, Assembly.GetEntryAssembly()?.GetName().Name);
                var webHostBuilderContext = new WebHostBuilderContext
                {
                    //Configuration = context.Configuration,
                    //HostingEnvironment = new HostingEnvironment(),
                };
                //webHostBuilderContext.HostingEnvironment.Initialize(context.HostingEnvironment.ContentRootPath, options);
                context.Properties[typeof(WebHostBuilderContext)] = webHostBuilderContext;
                context.Properties[typeof(WebHostOptions)] = options;
                return webHostBuilderContext;
            }

            // Refresh config, it's periodically updated/replaced
            var webHostContext = (WebHostBuilderContext)contextVal;
            webHostContext.Configuration = context.Configuration;
            return webHostContext;
        }
    }
}