using AspNetCoreMini.Http.Features;
using System;
using System.IO;

namespace AspNetCoreMini.Http
{
    public class DefaultHttpResponse : HttpResponse
    {
        private readonly static Func<IFeatureCollection, IHttpResponseFeature> _nullResponseFeature = f => null;

        private readonly DefaultHttpContext _context;
        private FeatureReferences<FeatureInterfaces> _features;

        public DefaultHttpResponse(DefaultHttpContext context)
        {
            _context = context;
            _features.Initalize(context.Features);
        }

        private IHttpResponseFeature HttpResponseFeature => _features.Fetch(ref _features.Cache.Response, _nullResponseFeature);

        public override HttpContext HttpContext { get { return _context; } }

        public override int StatusCode
        {
            get { return HttpResponseFeature.StatusCode; }
            set { HttpResponseFeature.StatusCode = value; }
        }
    
        public override Stream Body
        {
            get { return HttpResponseFeature.Body; }
        }

        struct FeatureInterfaces
        {
            public IHttpResponseFeature Response;
        }
    }
}