using System.IO;

namespace AspNetCoreMini.Http
{
    public abstract class HttpResponse
    {
        /// <summary>
        /// Gets the <see cref="HttpContext"/> for this response.
        /// </summary>
        public abstract HttpContext HttpContext { get; }

        /// <summary>
        /// Gets or sets the HTTP response code.
        /// </summary>
        public abstract int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the response body <see cref="Stream"/>.
        /// </summary>
        public abstract Stream Body { get; }
    }
}
