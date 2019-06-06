using AspNetCoreMini.Http;
using AspNetCoreMini.Http.Features;

namespace AspNetCoreMini.Hosting.Builder
{
    public interface IApplicationBuilderFactory
    {
        IApplicationBuilder CreateBuilder(IFeatureCollection serverFeatures);
    }
}
