namespace AspNetCoreMini.Hosting.Extensions
{
    internal interface IHostApplicationLifetime
    {
        object ApplicationStopping { get; }

        void StopApplication();
    }
}