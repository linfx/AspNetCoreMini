using AspNetCoreMini.Http;
using AspNetCoreMini.Http.Abstractions.Routing;
using AspNetCoreMini.Http.Features;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApplication1
{
    /// <summary>
    ///  第五个对象：Server
    /// </summary>
    public interface IServer
    {
        Task StartAsync(RequestDelegate handler);
    }

    public class HttpListenerServer : IServer
    {
        private readonly HttpListener _httpListener;
        private readonly string[] _urls;

        public HttpListenerServer(params string[] urls)
        {
            _httpListener = new HttpListener();
            _urls = urls.Any() ? urls : new string[] { "http://localhost:5000/" };
        }

        public async Task StartAsync(RequestDelegate handler)
        {
            Array.ForEach(_urls, url => _httpListener.Prefixes.Add(url));
            _httpListener.Start();
            while (true)
            {
                var listenerContext = await _httpListener.GetContextAsync();
                var feature = new HttpListenerFeature(listenerContext);
                var features = new FeatureCollection();
                features.Set<IHttpRequestFeature>(feature);
                features.Set<IHttpResponseFeature>(feature);

                var endpoint = new Endpoint(context =>
                {
                    //endpointCalled = true;
                    return Task.CompletedTask;
                }, EndpointMetadataCollection.Empty, "Test endpoint");

                var httpContext = new DefaultHttpContext(features);
                httpContext.SetEndpoint(endpoint);

                await handler(httpContext);
                listenerContext.Response.Close();
            }
        }
    }

    public class HttpListenerFeature : IHttpRequestFeature, IHttpResponseFeature
    {
        private readonly HttpListenerContext _context;

        public HttpListenerFeature(HttpListenerContext context) => _context = context;

        Uri IHttpRequestFeature.Url => _context.Request.Url;

        NameValueCollection IHttpRequestFeature.Headers => _context.Request.Headers;

        NameValueCollection IHttpResponseFeature.Headers => _context.Response.Headers;

        Stream IHttpRequestFeature.Body => _context.Request.InputStream;

        Stream IHttpResponseFeature.Body => _context.Response.OutputStream;

        int IHttpResponseFeature.StatusCode { get => _context.Response.StatusCode; set => _context.Response.StatusCode = value; }
    }
}
