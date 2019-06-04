using AspNetCoreMini.Http.Features;

namespace AspNetCoreMini.Http
{
    public class DefaultHttpContext : HttpContext
    {
        private readonly DefaultHttpRequest _request;
        private readonly DefaultHttpResponse _response;

        private FeatureReferences<FeatureInterfaces> _features;

        //public DefaultHttpContext()
        //    : this(new FeatureCollection())
        //{
        //    Features.Set<IHttpRequestFeature>(new HttpRequestFeature());
        //    Features.Set<IHttpResponseFeature>(new HttpResponseFeature());
        //}

        public DefaultHttpContext(IFeatureCollection features)
        {
            _features.Initalize(features);
            _request = new DefaultHttpRequest(this);
            _response = new DefaultHttpResponse(this);
        }

        public override IFeatureCollection Features => _features.Collection;

        public override HttpRequest Request => _request;

        public override HttpResponse Response => _response;

        struct FeatureInterfaces
        {
        }
    }
}
