﻿using AspNetCoreMini.Http;
using System;
using System.Threading.Tasks;

namespace AspNetCoreMini.Http
{
    /// <summary>
    /// Extension methods for adding middleware.
    /// </summary>
    public static class UseExtensions
    {
        /// <summary>
        /// Adds a middleware delegate defined in-line to the application's request pipeline.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
        /// <param name="middleware">A function that handles the request or calls the given next function.</param>
        /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
        public static IApplicationBuilder Use(this IApplicationBuilder app, Func<HttpContext, Func<Task>, Task> middleware)
        {
            return app.Use(next =>
            {
                return context =>
                {
                    Func<Task> simpleNext = () => next(context);
                    return middleware(context, simpleNext);
                };
            });
        }
    }
}
