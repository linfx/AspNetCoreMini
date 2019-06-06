﻿using AspNetCoreMini.Hosting.Server.Abstractions;
using AspNetCoreMini.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace AspNetCoreMini.Hosting
{
    public class WebHostBuilder : IWebHostBuilder
    {
        private Action<WebHostBuilderContext, IServiceCollection> _configureServices;

        private IServer _server;
        private readonly List<Action<IApplicationBuilder>> _configures = new List<Action<IApplicationBuilder>>();

        public IWebHostBuilder Configure(Action<IApplicationBuilder> configure)
        {
            _configures.Add(configure);
            return this;
        }

        /// <summary>
        /// Adds a delegate for configuring additional services for the host or web application. This may be called
        /// multiple times.
        /// </summary>
        /// <param name="configureServices">A delegate for configuring the <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
        public IWebHostBuilder ConfigureServices(Action<IServiceCollection> configureServices)
        {
            if (configureServices == null)
            {
                throw new ArgumentNullException(nameof(configureServices));
            }

            return ConfigureServices((_, services) => configureServices(services));
        }

        /// <summary>
        /// Adds a delegate for configuring additional services for the host or web application. This may be called
        /// multiple times.
        /// </summary>
        /// <param name="configureServices">A delegate for configuring the <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
        public IWebHostBuilder ConfigureServices(Action<WebHostBuilderContext, IServiceCollection> configureServices)
        {
            _configureServices += configureServices;
            return this;
        }

        public IWebHost Build()
        {
            var builder = new ApplicationBuilder(null);
            foreach (var configure in _configures)
            {
                configure(builder);
            }
            return new Internal.WebHost(_server, builder.Build());
        }
    }
}
