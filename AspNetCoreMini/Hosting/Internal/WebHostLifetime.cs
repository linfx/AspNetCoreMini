using System;
using System.Threading;

namespace AspNetCoreMini.Hosting
{
    internal class WebHostLifetime : IDisposable
    {
        private CancellationTokenSource cts;
        private ManualResetEventSlim done;
        private string shutdownMessage;

        public WebHostLifetime(CancellationTokenSource cts, ManualResetEventSlim done, string shutdownMessage)
        {
            this.cts = cts;
            this.done = done;
            this.shutdownMessage = shutdownMessage;
        }

        public void Dispose()
        {
        }
    }
}