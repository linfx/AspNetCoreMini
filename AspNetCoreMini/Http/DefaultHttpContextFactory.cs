using AspNetCoreMini.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreMini.Http
{
    public class DefaultHttpContextFactory : IHttpContextFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DefaultHttpContextFactory(IServiceProvider serviceProvider)
        {
            // May be null
            _httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            //_formOptions = serviceProvider.GetRequiredService<IOptions<FormOptions>>().Value;
            _serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        }

        public HttpContext Create(IFeatureCollection featureCollection)
        {
            if (featureCollection == null)
            {
                throw new ArgumentNullException(nameof(featureCollection));
            }

            var httpContext = CreateHttpContext(featureCollection);
            if (_httpContextAccessor != null)
            {
                _httpContextAccessor.HttpContext = httpContext;
            }

            //httpContext.FormOptions = _formOptions;
            httpContext.ServiceScopeFactory = _serviceScopeFactory;

            return httpContext;
        }

        private static DefaultHttpContext CreateHttpContext(IFeatureCollection featureCollection)
        {
            //if (featureCollection is IDefaultHttpContextContainer container)
            //{
            //    return container.HttpContext;
            //}

            return new DefaultHttpContext(featureCollection);
        }

        public void Dispose(HttpContext httpContext)
        {
            if (_httpContextAccessor != null)
            {
                _httpContextAccessor.HttpContext = null;
            }
        }
    }
}
