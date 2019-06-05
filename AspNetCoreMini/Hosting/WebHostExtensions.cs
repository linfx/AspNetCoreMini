using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMini.Hosting
{
    public static class WebHostExtensions
    {
        /// <summary>
        /// Runs a web application and returns a Task that only completes when the token is triggered or shutdown is triggered.
        /// </summary>
        /// <param name="host">The <see cref="IWebHost"/> to run.</param>
        /// <param name="token">The token to trigger shutdown.</param>
        public static async Task RunAsync(this IWebHost host, CancellationToken token = default)
        {
            // Wait for token shutdown if it can be canceled
            //if (token.CanBeCanceled)
            //{
            //    await host.RunAsync(token, startupMessage: null);
            //    return;
            //}

            // If token cannot be canceled, attach Ctrl+C and SIGTERM shutdown
            //var done = new ManualResetEventSlim(false);
            //using (var cts = new CancellationTokenSource())
            //{
            //    var shutdownMessage = host.Services.GetRequiredService<WebHostOptions>().SuppressStatusMessages ? string.Empty : "Application is shutting down...";
            //    using (var lifetime = new WebHostLifetime(cts, done, shutdownMessage: shutdownMessage))
            //    {
            //        try
            //        {
            //            await host.RunAsync(cts.Token, "Application started. Press Ctrl+C to shut down.");
            //            lifetime.SetExitedGracefully();
            //        }
            //        finally
            //        {
            //            done.Set();
            //        }
            //    }
            //}

            try
            {
                await host.StartAsync(token);
            }
            finally
            {
                host.Dispose();
            }
        }
    }
}
