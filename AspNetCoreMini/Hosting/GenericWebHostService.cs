using AspNetCoreMini.Hosting.Extensions;
using AspNetCoreMini.Hosting.Server.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMini.Hosting
{
    public class GenericWebHostService : IHostedService
    {
        public GenericWebHostService(IServer server)
        {
            Server = server;
        }

        public IServer Server { get; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //await Server.StartAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
