using AspNetCoreMini.Hosting.Server.Abstractions;
using AspNetCoreMini.Http;
using AspNetCoreMini.Http.Features;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AspNetCoreMini.Hosting
{
    public class HostingApplication : IHttpApplication<HostingApplication.Context>
    {
        private readonly RequestDelegate _application;
        private readonly IHttpContextFactory _httpContextFactory;
        //private HostingApplicationDiagnostics _diagnostics;

        public HostingApplication(
            RequestDelegate application,
            IHttpContextFactory httpContextFactory)
        {
            _application = application;
            //_diagnostics = new HostingApplicationDiagnostics(logger, diagnosticSource);
            _httpContextFactory = httpContextFactory;
        }

        // Set up the request
        public Context CreateContext(IFeatureCollection contextFeatures)
        {
            var context = new Context();
            var httpContext = _httpContextFactory.Create(contextFeatures);

            //_diagnostics.BeginRequest(httpContext, ref context);

            context.HttpContext = httpContext;
            return context;
        }

        // Execute the request
        public Task ProcessRequestAsync(Context context)
        {
            return _application(context.HttpContext);
        }

        // Clean up the request
        public void DisposeContext(Context context, Exception exception)
        {
            var httpContext = context.HttpContext;
            //_diagnostics.RequestEnd(httpContext, exception, context);
            _httpContextFactory.Dispose(httpContext);
            //_diagnostics.ContextDisposed(context);
        }

        public struct Context
        {
            public HttpContext HttpContext { get; set; }
            public IDisposable Scope { get; set; }
            public long StartTimestamp { get; set; }
            public bool EventLogEnabled { get; set; }
            public Activity Activity { get; set; }
            internal bool HasDiagnosticListener { get; set; }
        }
    }
}
