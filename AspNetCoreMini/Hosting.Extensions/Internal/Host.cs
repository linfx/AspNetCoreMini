using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMini.Hosting.Extensions.Internal
{
    public class Host : IHost
    {
        private readonly ILogger<Host> _logger;
        private readonly HostOptions _options;
        private IEnumerable<IHostedService> _hostedServices;

        public Host(
            ILogger<Host> logger,
            IOptions<HostOptions> options, 
            IServiceProvider services)
        {
            _logger = logger;
            _options = options.Value;
            Services = services;
        }

        public IServiceProvider Services { get; }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            _hostedServices = Services.GetService<IEnumerable<IHostedService>>();

            foreach (var hostedService in _hostedServices)
            {
                // Fire IHostedService.Start
                await hostedService.StartAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            using (var cts = new CancellationTokenSource(_options.ShutdownTimeout))
            using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, cancellationToken))
            {
                var token = linkedCts.Token;
                // Trigger IHostApplicationLifetime.ApplicationStopping
                //_applicationLifetime?.StopApplication();

                IList<Exception> exceptions = new List<Exception>();
                if (_hostedServices != null) // Started?
                {
                    foreach (var hostedService in _hostedServices.Reverse())
                    {
                        token.ThrowIfCancellationRequested();
                        try
                        {
                            await hostedService.StopAsync(token).ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            exceptions.Add(ex);
                        }
                    }
                }

                token.ThrowIfCancellationRequested();
                //await _hostLifetime.StopAsync(token);

                // Fire IHostApplicationLifetime.Stopped
                //_applicationLifetime?.NotifyStopped();

                if (exceptions.Count > 0)
                {
                    var ex = new AggregateException("One or more hosted services failed to stop.", exceptions);
                    //_logger.StoppedWithException(ex);
                    throw ex;
                }
            }
        }

        public void Dispose()
        {
            (Services as IDisposable)?.Dispose();
        }
    }
}
