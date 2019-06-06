using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMini.Extensions.Hosting
{
    public static class HostingAbstractionsHostExtensions
    {
        /// <summary>
        /// Runs an application and block the calling thread until host shutdown.
        /// </summary>
        /// <param name="host">The <see cref="IHost"/> to run.</param>
        public static void Run(this IHost host)
        {
            host.RunAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Runs an application and returns a Task that only completes when the token is triggered or shutdown is triggered.
        /// </summary>
        /// <param name="host">The <see cref="IHost"/> to run.</param>
        /// <param name="token">The token to trigger shutdown.</param>
        public static async Task RunAsync(this IHost host, CancellationToken token = default)
        {
            try
            {
                await host.StartAsync(token);
                await host.WaitForShutdownAsync(token);
            }
            finally
            {
                {
                    host.Dispose();
                }
            }
        }

        /// <summary>
        /// Returns a Task that completes when shutdown is triggered via the given token.
        /// </summary>
        /// <param name="host">The running <see cref="IHost"/>.</param>
        /// <param name="token">The token to trigger shutdown.</param>
        public static async Task WaitForShutdownAsync(this IHost host, CancellationToken token = default)
        {
            var applicationLifetime = host.Services.GetService<IHostApplicationLifetime>();

            //token.Register(state =>
            //{
            //    ((IHostApplicationLifetime)state).StopApplication();
            //}, applicationLifetime);

            //var waitForStop = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

            //applicationLifetime.ApplicationStopping.Register(obj =>
            //{
            //    var tcs = (TaskCompletionSource<object>)obj;
            //    tcs.TrySetResult(null);
            //}, waitForStop);

            //await waitForStop.Task;

            // Host will use its default ShutdownTimeout if none is specified.
            await host.StopAsync();
        }
    }
}
