using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace AspNetCoreMini.Hosting.Extensions
{
    internal class HostBuilder : IHostBuilder
    {
        //private IServiceFactoryAdapter _serviceProviderFactory = new ServiceFactoryAdapter<IServiceCollection>(new DefaultServiceProviderFactory());
        private List<Action<HostBuilderContext, IServiceCollection>> _configureServicesActions = new List<Action<HostBuilderContext, IServiceCollection>>();
        private bool _hostBuilt;
        private IConfiguration _hostConfiguration;
        private HostBuilderContext _hostBuilderContext;
        private IServiceProvider _appServices;

        /// <summary>
        /// A central location for sharing state between components during the host building process.
        /// </summary>
        public IDictionary<object, object> Properties { get; } = new Dictionary<object, object>();

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureDelegate"></param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            _configureServicesActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }

        public IHost Build()
        {
            if (_hostBuilt)
            {
                throw new InvalidOperationException("Build can only be called once.");
            }
            _hostBuilt = true;

            BuildHostConfiguration();
            //CreateHostingEnvironment();
            CreateHostBuilderContext();
            //BuildAppConfiguration();
            CreateServiceProvider();

            return _appServices.GetRequiredService<IHost>();
        }

        private void BuildHostConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(); // Make sure there's some default storage since there are no default providers

            //foreach (var buildAction in _configureHostConfigActions)
            //{
            //    buildAction(configBuilder);
            //}
            _hostConfiguration = configBuilder.Build();
        }


        private void CreateHostBuilderContext()
        {
            _hostBuilderContext = new HostBuilderContext(Properties)
            {
                //HostingEnvironment = _hostingEnvironment,
                Configuration = _hostConfiguration
            };
        }

        private void CreateServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IHost, Internal.Host>();
            services.AddOptions();
            services.AddLogging();

            foreach (var configureServicesAction in _configureServicesActions)
            {
                configureServicesAction(_hostBuilderContext, services);
            }

            //var containerBuilder = _serviceProviderFactory.CreateBuilder(services);

            //foreach (var containerAction in _configureContainerActions)
            //{
            //    containerAction.ConfigureContainer(_hostBuilderContext, containerBuilder);
            //}

            //_appServices = _serviceProviderFactory.CreateServiceProvider(containerBuilder);
            _appServices = services.BuildServiceProvider();

            if (_appServices == null)
            {
                throw new InvalidOperationException($"The IServiceProviderFactory returned a null IServiceProvider.");
            }

            // resolve configuration explicitly once to mark it as resolved within the
            // service provider, ensuring it will be properly disposed with the provider
            //_ = _appServices.GetService<IConfiguration>();
        }
    }
}