using AspNetCoreMini.Http;

namespace AspNetCoreMini.Routing.Builder
{
    internal class DefaultEndpointRouteBuilder
    {
        private IApplicationBuilder builder;

        public DefaultEndpointRouteBuilder(IApplicationBuilder builder)
        {
            this.builder = builder;
        }
    }
}