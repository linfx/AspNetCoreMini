using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreMini.Hosting.Extensions
{
    internal class HostBuilder : IHostBuilder
    {
        //private IServiceFactoryAdapter _serviceProviderFactory = new ServiceFactoryAdapter<IServiceCollection>(new DefaultServiceProviderFactory());
        private bool _hostBuilt;
        private IServiceProvider _appServices;

        public IHost Build()
        {
            if (_hostBuilt)
            {
                throw new InvalidOperationException("Build can only be called once.");
            }
            _hostBuilt = true;

            //BuildHostConfiguration();
            //CreateHostingEnvironment();
            //CreateHostBuilderContext();
            //BuildAppConfiguration();
            CreateServiceProvider();

            return _appServices.GetRequiredService<IHost>();
        }

        internal void UseDefaultServiceProvider(Func<object, object, object> p)
        {
            throw new NotImplementedException();
        }

        private void CreateServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IHost, Internal.Host>();
            services.AddOptions();
            services.AddLogging();

            //foreach (var configureServicesAction in _configureServicesActions)
            //{
            //    configureServicesAction(_hostBuilderContext, services);
            //}

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