using AspNetCoreMini.Hosting.Server.Abstractions;
using AspNetCoreMini.Http;
using AspNetCoreMini.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMini.Hosting
{

    public class WebHost : IWebHost
    {
        private readonly IServiceProvider _hostingServiceProvider;

        private readonly IServer _server;
        private readonly RequestDelegate _handler;
        private IServiceProvider _applicationServices;

        public WebHost(IServer server, RequestDelegate handler)
        {
            _server = server;
            _handler = handler;
        }

        private IServer Server { get; set; }

        public IServiceProvider Services
        {
            get { return _applicationServices; }
        }

        public IFeatureCollection ServerFeatures
        {
            get
            {
                EnsureServer();
                return Server?.Features;
            }
        }

        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        private void EnsureServer()
        {
            if (Server == null)
            {
                Server = _applicationServices.GetRequiredService<IServer>();
            }
        }

        public void Dispose()
        {
        }
    }
}