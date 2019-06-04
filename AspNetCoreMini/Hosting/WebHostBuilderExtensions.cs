using AspNetCoreMini.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseHttpListener(this IWebHostBuilder builder, params string[] urls)
         => builder.UseServer(new HttpListenerServer(urls));

        public static Task WriteAsync(this HttpResponse response, string contents)
        {
            var buffer = Encoding.UTF8.GetBytes(contents);
            return response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
