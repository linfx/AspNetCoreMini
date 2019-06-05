using AspNetCoreMini.Hosting.Extensions;
using AspNetCoreMini.Hosting.Server.Abstractions;
using AspNetCoreMini.Http;
using AspNetCoreMini.Http.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMini.Hosting
{
    public class GenericWebHostService : IHostedService
    {
        public GenericWebHostService(IServer server, IHttpContextFactory httpContextFactory)
        {
            Server = server;
            HttpContextFactory = httpContextFactory;
        }

        public IServer Server { get; }

        public IHttpContextFactory HttpContextFactory { get; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            RequestDelegate application;

            try
            {
                var builder = new ApplicationBuilder(null);

                application = builder.Build();
            }
            catch (Exception)
            {
                throw;
            }

            var httpApplication = new HostingApplication(application, HttpContextFactory);

            await Server.StartAsync(httpApplication, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
