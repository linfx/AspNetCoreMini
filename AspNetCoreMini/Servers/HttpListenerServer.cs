﻿using AspNetCoreMini.Hosting.Server.Abstractions;
using AspNetCoreMini.Http.Features;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMini.Servers
{
    public class HttpListenerServer : IServer
    {
        private readonly HttpListener _httpListener;
        private readonly string[] _urls;

        public HttpListenerServer()
        {
            _httpListener = new HttpListener();
            _urls = new string[] { "http://localhost:5000/" };
        }

        public IFeatureCollection Features { get; }

        public async Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken)
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

                var httpContext = application.CreateContext(features);
                await application.ProcessRequestAsync(httpContext);

                listenerContext.Response.Close();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _httpListener.Stop();
            return Task.CompletedTask;
        }
    }
}
