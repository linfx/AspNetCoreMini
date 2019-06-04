using System;
using AspNetCoreMini.Http.Features;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreMini.Http
{
    public class DefaultHttpContext : HttpContext
    {
        private readonly static Func<DefaultHttpContext, IServiceProvidersFeature> _newServiceProvidersFeature = context => new RequestServicesFeature(context, context.ServiceScopeFactory);

        private readonly DefaultHttpRequest _request;
        private readonly DefaultHttpResponse _response;
        private FeatureReferences<FeatureInterfaces> _features;

        public DefaultHttpContext(IFeatureCollection features)
        {
            _features.Initalize(features);
            _request = new DefaultHttpRequest(this);
            _response = new DefaultHttpResponse(this);
        }

        public IServiceScopeFactory ServiceScopeFactory { get; set; }

        public override IFeatureCollection Features => _features.Collection;

        private IServiceProvidersFeature ServiceProvidersFeature => _features.Fetch(ref _features.Cache.ServiceProviders, this, _newServiceProvidersFeature);

        public override HttpRequest Request => _request;

        public override HttpResponse Response => _response;

        public override IServiceProvider RequestServices
        {
            get { return ServiceProvidersFeature.RequestServices; }
            set { ServiceProvidersFeature.RequestServices = value; }
        }

        struct FeatureInterfaces
        {
            public IServiceProvidersFeature ServiceProviders;
        }
    }
}
