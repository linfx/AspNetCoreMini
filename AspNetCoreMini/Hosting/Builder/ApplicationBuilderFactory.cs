using AspNetCoreMini.Http;
using AspNetCoreMini.Http.Features;
using System;

namespace AspNetCoreMini.Hosting.Builder
{
    public class ApplicationBuilderFactory : IApplicationBuilderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ApplicationBuilderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IApplicationBuilder CreateBuilder(IFeatureCollection serverFeatures)
        {
            return new ApplicationBuilder(_serviceProvider, serverFeatures);
        }
    }
}
