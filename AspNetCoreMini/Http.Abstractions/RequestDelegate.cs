using System.Threading.Tasks;

namespace AspNetCoreMini.Http
{
    /// <summary>
    /// 第二个对象：RequestDelegate
    /// A function that can process an HTTP request.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> for the request.</param>
    /// <returns></returns>
    public delegate Task RequestDelegate(HttpContext context);
}
