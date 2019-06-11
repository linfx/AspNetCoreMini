using AspNetCoreMini.Http;
using System;
using System.Collections.Generic;

namespace AspNetCoreMini.Routing.Builder
{
    public interface IRouteBuilder
    {
        /// <summary>
        /// Gets the <see cref="IApplicationBuilder"/>.
        /// </summary>
        IApplicationBuilder ApplicationBuilder { get; }

        /// <summary>
        /// Gets or sets the default <see cref="IRouter"/> that is used as a handler if an <see cref="IRouter"/>
        /// is added to the list of routes but does not specify its own.
        /// </summary>
        IRouter DefaultHandler { get; set; }

        /// <summary>
        /// Gets the sets the <see cref="IServiceProvider"/> used to resolve services for routes.
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the routes configured in the builder.
        /// </summary>
        IList<IRouter> Routes { get; }

        /// <summary>
        /// Builds an <see cref="IRouter"/> that routes the routes specified in the <see cref="Routes"/> property.
        /// </summary>
        IRouter Build();
    }
}