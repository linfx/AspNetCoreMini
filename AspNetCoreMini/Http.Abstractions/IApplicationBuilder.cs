using System;

namespace AspNetCoreMini.Http
{
    /// <summary>
    /// 第四个对象：ApplicationBuilder
    /// </summary>
    public interface IApplicationBuilder
    {
        /// <summary>
        /// Adds a middleware delegate to the application's request pipeline.
        /// </summary>
        /// <param name="middleware">The middleware delegate.</param>
        /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
        IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware);

        /// <summary>
        /// Builds the delegate used by this application to process HTTP request.
        /// </summary>
        /// <returns></returns>
        RequestDelegate Build();
    }
}
