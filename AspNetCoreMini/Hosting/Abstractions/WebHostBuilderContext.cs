using Microsoft.Extensions.Configuration;

namespace AspNetCoreMini.Hosting
{
    public class WebHostBuilderContext
    {
        /// <summary>
        /// The <see cref="IConfiguration" /> containing the merged configuration of the application and the <see cref="IWebHost" />.
        /// </summary>
        public IConfiguration Configuration { get; set; }
    }
}