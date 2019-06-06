using AspNetCoreMini.Hosting.Builder;
using AspNetCoreMini.Extensions.Hosting;
using AspNetCoreMini.Hosting.Server.Abstractions;
using AspNetCoreMini.Http;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMini.Hosting
{
    internal class GenericWebHostService : IHostedService
    {
        public GenericWebHostService(IOptions<GenericWebHostServiceOptions> options, IServer server, IHttpContextFactory httpContextFactory, IApplicationBuilderFactory applicationBuilderFactory)
        {
            Options = options.Value;
            Server = server;
            HttpContextFactory = httpContextFactory;
            ApplicationBuilderFactory = applicationBuilderFactory;
        }

        public GenericWebHostServiceOptions Options { get; }
        public IServer Server { get; }
        public IHttpContextFactory HttpContextFactory { get; }
        public IApplicationBuilderFactory ApplicationBuilderFactory { get; }
        //public IConfiguration Configuration { get; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            RequestDelegate application;

            try
            {
                Action<IApplicationBuilder> configure = Options.ConfigureApplication;

                if (configure == null)
                {
                    throw new InvalidOperationException($"No application configured. Please specify an application via IWebHostBuilder.UseStartup, IWebHostBuilder.Configure, or specifying the startup assembly via {nameof(Microsoft.AspNetCore.Hosting.WebHostDefaults.StartupAssemblyKey)} in the web host configuration.");
                }

                var builder = ApplicationBuilderFactory.CreateBuilder(Server.Features);

                configure(builder);

                // Build the request pipeline
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
