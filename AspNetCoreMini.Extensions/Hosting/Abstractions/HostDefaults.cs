namespace AspNetCoreMini.Extensions.Hosting
{
    /// <summary>
    /// Constants for HostBuilder configuration keys.
    /// </summary>
    public static class HostDefaults
    {
        /// <summary>
        /// The configuration key used to set <see cref="IHostEnvironment.ApplicationName"/>.
        /// </summary>
        public static readonly string ApplicationKey = "applicationName";

        /// <summary>
        /// The configuration key used to set <see cref="IHostEnvironment.EnvironmentName"/>.
        /// </summary>
        public static readonly string EnvironmentKey = "environment";

        /// <summary>
        /// The configuration key used to set <see cref="IHostEnvironment.ContentRootPath"/>
        /// and <see cref="IHostEnvironment.ContentRootFileProvider"/>.
        /// </summary>
        public static readonly string ContentRootKey = "contentRoot";
    }
}
