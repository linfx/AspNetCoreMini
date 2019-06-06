using AspNetCoreMini.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreMini.Hosting
{
    public class DelegateStartup : StartupBase<IServiceCollection>
    {
        private Action<IApplicationBuilder> _configureApp;

        public DelegateStartup(IServiceProviderFactory<IServiceCollection> factory, Action<IApplicationBuilder> configureApp) : base(factory)
        {
            _configureApp = configureApp;
        }

        public override void Configure(IApplicationBuilder app) => _configureApp(app);
    }

    public abstract class StartupBase<TBuilder> : StartupBase
    {
        private readonly IServiceProviderFactory<TBuilder> _factory;

        public StartupBase(IServiceProviderFactory<TBuilder> factory)
        {
            _factory = factory;
        }

        public override IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            var builder = _factory.CreateBuilder(services);
            ConfigureContainer(builder);
            return _factory.CreateServiceProvider(builder);
        }

        public virtual void ConfigureContainer(TBuilder builder)
        {
        }
    }
}
