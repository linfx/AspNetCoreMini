using AspNetCoreMini.Http.Features;

namespace AspNetCoreMini.Http
{
    public interface IHttpContextFactory
    {
        HttpContext Create(IFeatureCollection featureCollection);

        void Dispose(HttpContext httpContext);
    }
}
