using AspNetCoreMini.Http.Features;
using System;
using System.IO;

namespace AspNetCoreMini.Http
{
    public sealed class DefaultHttpRequest : HttpRequest
    {
        private readonly static Func<IFeatureCollection, IHttpRequestFeature> _nullRequestFeature = f => null;

        private readonly DefaultHttpContext _context;
        private FeatureReferences<FeatureInterfaces> _features;

        public DefaultHttpRequest(DefaultHttpContext context)
        {
            _context = context;
            _features.Initalize(context.Features);
        }

        public override HttpContext HttpContext => _context;

        private IHttpRequestFeature HttpRequestFeature => _features.Fetch(ref _features.Cache.Request, _nullRequestFeature);

        public override Stream Body
        {
            get { return HttpRequestFeature.Body; }
        }

        struct FeatureInterfaces
        {
            public IHttpRequestFeature Request;
        }
    }
}
