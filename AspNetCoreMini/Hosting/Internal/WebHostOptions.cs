using Microsoft.Extensions.Configuration;

namespace AspNetCoreMini.Hosting
{
    internal class WebHostOptions
    {
        private IConfiguration configuration;
        private string name;

        public WebHostOptions(IConfiguration configuration, string name)
        {
            this.configuration = configuration;
            this.name = name;
        }

        public bool CaptureStartupErrors { get; internal set; }
    }
}