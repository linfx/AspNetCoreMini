using System.Collections.Generic;

namespace AspNetCoreMini.Hosting.Server.Features
{
    public interface IServerAddressesFeature
    {
        ICollection<string> Addresses { get; }

        bool PreferHostingUrls { get; set; }
    }
}
