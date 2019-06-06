using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreMini.Hosting
{
    /// <summary>
    /// 第七个对象：WebHostBuilder
    /// WebHostBuilder的使命非常明确：就是创建作为应用宿主的WebHost。
    /// </summary>
    public interface IWebHostBuilder
    {
        /// <summary>
        /// Adds a delegate for configuring additional services for the host or web application. This may be called
        /// multiple times.
        /// </summary>
        /// <param name="configureServices">A delegate for configuring the <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
        IWebHostBuilder ConfigureServices(Action<IServiceCollection> configureServices);

        /// <summary>
        /// Adds a delegate for configuring additional services for the host or web application. This may be called
        /// multiple times.
        /// </summary>
        /// <param name="configureServices">A delegate for configuring the <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
        IWebHostBuilder ConfigureServices(Action<WebHostBuilderContext, IServiceCollection> configureServices);
    }
}
