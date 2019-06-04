using System.IO;

namespace AspNetCoreMini.Http
{
    /// <summary>
    /// Represents the incoming side of an individual HTTP request.
    /// </summary>
    public abstract class HttpRequest
    {
        /// <summary>
        /// Gets the <see cref="HttpContext"/> for this request.
        /// </summary>
        public abstract HttpContext HttpContext { get; }

        /// <summary>
        /// Gets or sets the RequestBody Stream.
        /// </summary>
        /// <returns>The RequestBody Stream.</returns>
        public abstract Stream Body { get; }
    }
}
