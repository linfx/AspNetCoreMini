using Microsoft.Extensions.Configuration;

namespace AspNetCoreMini.Hosting
{
    public class WebHostOptions
    {
        public WebHostOptions(IConfiguration configuration, string applicationNameFallback)
        {
            ApplicationName = applicationNameFallback;
        }

        public string ApplicationName { get; set; }
    }
}