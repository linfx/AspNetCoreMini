﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMini.Hosting.Extensions
{
    /// <summary>
    /// A program abstraction.
    /// </summary>
    public interface IHost : IDisposable
    {
        /// <summary>
        /// The programs configured services.
        /// </summary>
        IServiceProvider Services { get; }

        /// <summary>
        /// Start the program.
        /// </summary>
        /// <param name="cancellationToken">Used to abort program start.</param>
        /// <returns></returns>
        Task StartAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Attempts to gracefully stop the program.
        /// </summary>
        /// <param name="cancellationToken">Used to indicate when stop should no longer be graceful.</param>
        /// <returns></returns>
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}
