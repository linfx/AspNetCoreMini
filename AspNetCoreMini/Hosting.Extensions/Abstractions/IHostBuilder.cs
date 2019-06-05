using System;

namespace AspNetCoreMini.Hosting.Extensions
{
    /// <summary>
    /// A program initialization abstraction.
    /// </summary>
    public interface IHostBuilder
    {
        /// <summary>
        /// Run the given actions to initialize the host. This can only be called once.
        /// </summary>
        /// <returns>An initialized <see cref="IHost"/></returns>
        IHost Build();
    }
}
