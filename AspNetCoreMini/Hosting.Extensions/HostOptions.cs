using System;

namespace AspNetCoreMini.Hosting.Extensions
{
    /// <summary>
    /// Options for <see cref="IHost"/>
    /// </summary>
    public class HostOptions
    {
        /// <summary>
        /// The default timeout for <see cref="IHost.StopAsync(System.Threading.CancellationToken)"/>.
        /// </summary>
        public TimeSpan ShutdownTimeout { get; set; } = TimeSpan.FromSeconds(5);
    }
}
